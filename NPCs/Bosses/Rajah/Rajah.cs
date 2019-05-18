using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Rajah
{
    public class Rajah : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 200;
            npc.height = 214;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 80;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.NPCKilled, "Sounds/Sounds/Rajah");
            npc.value = 10000f;
            npc.boss = true;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RajahTheme");
            bossBag = mod.ItemType("RajahBag");
        }

        public float[] internalAI = new float[5];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat(); //SpaceOctopus AI stuff
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat(); //Minion/Rocket Timer
                internalAI[3] = reader.ReadFloat(); //Ground Minion Alternation
                internalAI[4] = reader.ReadFloat(); //Is Flying
            }
        }

        private Texture2D RajahTex;
        private Texture2D CapeTex;
        private Texture2D ArmTex;
        public Vector2 WeaponPos;

        /*
         * npc.ai[0] = Jump Timer
         * npc.ai[1] = Jumping
         * npc.ai[2] = Weapon Change timer
         * npc.ai[3] = Weapon type
         */

        public override void AI()
        {
            AAModGlobalNPC.Rajah = npc.whoAmI;
            WeaponPos = new Vector2(npc.Center.X + (npc.direction == 1 ? -78 : 78), npc.Center.Y - 9);

            Player player = Main.player[npc.target];
            if (npc.target >= 0 && Main.player[npc.target].dead || Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 3000)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead || Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > 3000)
                {
                    Main.NewText("Justice has been served...", 107, 137, 179);
                    Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
                    npc.active = false;
                    npc.noTileCollide = true;
                }
            }

            if (player.Center.X < npc.Center.X)
            {
                npc.direction = -1;
            }
            else
            {
                npc.direction = 1;
            }

            if (player.Center.Y < npc.position.Y || TileBelowEmpty())
            {
                npc.noGravity = true;
                FlyAI();
            }
            else
            {
                npc.noGravity = false;
                JumpAI();
            }

            if (npc.target <= 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }

            npc.ai[2]++;
            internalAI[2]++;
            if (npc.ai[3] != 0 && npc.ai[2] >= 500)
            {
                internalAI[2] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
            }
            else if (npc.ai[3] == 0 && npc.ai[2] > 240)
            {
                internalAI[2] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = Main.rand.Next(3);
            }

            if (npc.ai[3] == 0) //Minion Phase
            {
                if (internalAI[2] >= 60)
                {
                    internalAI[2] = 0;
                    if (internalAI[4] == 0)
                    {
                        if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) < 5)
                        {
                            NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<RabbitcopterSoldier>(), 0);
                        }
                    }
                    else
                    {
                        if (internalAI[3] > 2)
                        {
                            internalAI[3] = 0;
                        }
                        if (internalAI[3] == 0)
                        {
                            if (NPC.CountNPCS(mod.NPCType<RabbitcopterSoldier>()) < 5)
                            {
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<RabbitcopterSoldier>(), 0);
                            }
                        }
                        else if (internalAI[3] == 1)
                        {
                            if (NPC.CountNPCS(mod.NPCType<BunnyBrawler>()) < 3)
                            {
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<BunnyBrawler>(), 0);
                            }
                        }
                        else if (internalAI[3] == 2)
                        {
                            if (NPC.CountNPCS(mod.NPCType<BunnyBrawler>()) < 8)
                            {
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<BunnyBattler>(), 0);
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<BunnyBattler>(), 0);
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<BunnyBattler>(), 0);
                                NPC.NewNPC(Main.rand.Next((int)npc.Center.X - 200, (int)npc.Center.X + 200), Main.rand.Next((int)npc.Center.Y - 200, (int)npc.Center.Y - 50), mod.NPCType<BunnyBattler>(), 0);
                            }
                        }
                        internalAI[3] += 1;
                    }
                }
            }

            if (npc.ai[3] == 1) //Bunzooka
            {
                internalAI[2]++;
                if (internalAI[2] > 40)
                {
                    internalAI[2] = 0;
                    Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                    dir *= 9f;
                    float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                    Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, dir.X, dir.Y, mod.ProjectileType<RajahRocket>(), (int)(npc.damage * .75f), 5, Main.myPlayer);
                }
            }

            if (npc.ai[3] == 2) //Royal Scepter
            {
                float spread = 45f * 0.0174f;
                Vector2 dir = Vector2.Normalize(player.Center - npc.Center);
                dir *= 9f;
                float baseSpeed = (float)Math.Sqrt((dir.X * dir.X) + (dir.Y * dir.Y));
                double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                double deltaAngle = spread / 6f;
                internalAI[2]++;
                if (internalAI[2] > 40)
                {
                    internalAI[2] = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        double offsetAngle = startAngle + (deltaAngle * i);
                        Projectile.NewProjectile(WeaponPos.X, WeaponPos.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType("CarrotHostile"), (int)(npc.damage / 1.5f), 5, Main.myPlayer);
                    }
                }
            }

            if (Main.expertMode)
            {
                if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
                {
                    npc.damage = (int)(npc.defDamage * 1.1f);
                }
                if (npc.life < (npc.lifeMax * .7f))
                {
                    npc.damage = (int)(npc.defDamage * 1.3f);
                }
                if (npc.life < (npc.lifeMax * .65f))
                {
                    npc.damage = (int)(npc.defDamage * 1.5f);
                }
                if (npc.life < (npc.lifeMax * .4f))
                {
                    npc.damage = (int)(npc.defDamage * 1.7f);
                }
                if (npc.life < (npc.lifeMax * .25f))
                {
                    npc.damage = (int)(npc.defDamage * 1.9f);
                }
                if (npc.life < (npc.lifeMax * .1f))
                {
                    npc.damage = npc.defDamage * 2;
                }
            }

            npc.rotation = 0;
        }

        public bool TileBelowEmpty()
        {
            int tileX = (int)(npc.Center.X / 16f) + npc.direction * 2;
            int tileY = (int)((npc.position.Y + (float)npc.height) / 16f);

            for (int tY = tileY; tY < tileY + 17; tY++)
            {
                if (Main.tile[tileX, tY] == null)
                {
                    Main.tile[tileX, tY] = new Tile();
                }
                if ((Main.tile[tileX, tY].nactive() && Main.tileSolid[(int)Main.tile[tileX, tY].type]) || Main.tile[tileX, tY].liquid > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public string WeaponTexture()
        {
            if (npc.ai[3] == 0) //No Weapon/Punisher
            {
                if (internalAI[4] == 0)
                {
                    return "NPCs/Bosses/Rajah/RajahArms_Fly";
                }
                else
                {
                    return "NPCs/Bosses/Rajah/RajahArms";
                }
            }
            else if (npc.ai[3] == 1) //Bunzooka
            {
                if (internalAI[4] == 0)
                {
                    return "NPCs/Bosses/Rajah/RajahArmsB_Fly";
                }
                else
                {
                    return "NPCs/Bosses/Rajah/RajahArmsB";
                }
            }
            else //Royal Scepter
            {
                if (internalAI[4] == 0)
                {
                    return "NPCs/Bosses/Rajah/RajahArmsR_Fly";
                }
                else
                {
                    return "NPCs/Bosses/Rajah/RajahArmsR";
                }
            }
        }

        public void JumpAI()
        {
            internalAI[4] = 1;
            if (npc.ai[0] == 0f)
            {
                npc.noTileCollide = false;
                if (npc.velocity.Y == 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.8f;
                    npc.ai[1] += 1f;
                    if (npc.ai[1] > 0f)
                    {
                        if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more frequent the jumps
                        {
                            npc.ai[1] += 3;
                        }
                        if (npc.life < (npc.lifeMax * .7f))
                        {
                            npc.ai[1] += 3;
                        }
                        if (npc.life < (npc.lifeMax * .65f))
                        {
                            npc.ai[1] += 3;
                        }
                        if (npc.life < (npc.lifeMax * .4f))
                        {
                            npc.ai[1] += 3;
                        }
                        if (npc.life < (npc.lifeMax * .25f))
                        {
                            npc.ai[1] += 3;
                        }
                        if (npc.life < (npc.lifeMax * .1f))
                        {
                            npc.ai[1] += 3;
                        }
                    }
                    if (npc.ai[1] >= 200f)
                    {
                        npc.ai[1] = -20f;
                    }
                    else if (npc.ai[1] == -1f)
                    {
                        npc.TargetClosest(true);
                        npc.velocity.X = 6 * npc.direction;
                        npc.velocity.Y = -12.1f;
                        npc.ai[0] = 1f;
                        npc.ai[1] = 0f;
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                if (npc.velocity.Y == 0f)
                {
                    Main.PlaySound(SoundID.Item14, npc.position);
                    npc.ai[0] = 0f;
                    for (int num622 = (int)npc.position.X - 20; num622 < (int)npc.position.X + npc.width + 40; num622 += 20)
                    {
                        for (int num623 = 0; num623 < 4; num623++)
                        {
                            int num624 = Dust.NewDust(new Vector2(npc.position.X - 20f, npc.position.Y + (float)npc.height), npc.width + 20, 4, 31, 0f, 0f, 100, default(Color), 1.5f);
                            Main.dust[num624].velocity *= 0.2f;
                        }
                        int num625 = Gore.NewGore(new Vector2((float)(num622 - 20), npc.position.Y + (float)npc.height - 8f), default(Vector2), Main.rand.Next(61, 64), 1f);
                        Main.gore[num625].velocity *= 0.4f;
                    }
                }
                else
                {
                    npc.TargetClosest(true);
                    if (npc.position.X < Main.player[npc.target].position.X && npc.position.X + (float)npc.width > Main.player[npc.target].position.X + (float)Main.player[npc.target].width)
                    {
                        npc.velocity.X = npc.velocity.X * 0.9f;
                        npc.velocity.Y = npc.velocity.Y + 0.4f;
                    }
                    else
                    {
                        if (npc.direction < 0)
                        {
                            npc.velocity.X = npc.velocity.X - 0.2f;
                        }
                        else if (npc.direction > 0)
                        {
                            npc.velocity.X = npc.velocity.X + 0.2f;
                        }
                        float num626 = 3f;
                        if (npc.life < npc.lifeMax)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 2)
                        {
                            num626 += 1f;
                        }
                        if (npc.life < npc.lifeMax / 4)
                        {
                            num626 += 1f;
                        }
                        if (npc.velocity.X < -num626)
                        {
                            npc.velocity.X = -num626;
                        }
                        if (npc.velocity.X > num626)
                        {
                            npc.velocity.X = num626;
                        }
                    }
                }
            }
        }

        public void FlyAI()
        {
            float speed = 7f;
            if (npc.life < (npc.lifeMax * .85f)) //The lower the health, the more damage is done
            {
                speed = 7.5f;
            }
            if (npc.life < (npc.lifeMax * .7f))
            {
                speed = 8f;
            }
            if (npc.life < (npc.lifeMax * .65f))
            {
                speed = 8.5f;
            }
            if (npc.life < (npc.lifeMax * .4f))
            {
                speed = 9f;
            }
            if (npc.life < (npc.lifeMax * .25f))
            {
                speed = 9.5f;
            }
            if (npc.life < (npc.lifeMax * .1f))
            {
                speed = 10f;
            }
            BaseAI.AISpaceOctopus(npc, ref internalAI, .25f, speed, 300, 0, null);
            internalAI[4] = 0;
        }

        public override void FindFrame(int frameHeight)
        {
            if (internalAI[4] == 0)
            {
                if (npc.frameCounter++ > 3)
                {
                    npc.frame.Y += frameHeight;
                    npc.frameCounter = 0;
                    if (npc.frame.Y > frameHeight * 7)
                    {
                        npc.frame.Y = 0;
                    }
                }
            }
            else
            {
                if (npc.ai[0] == 0f)
                {
                    if (npc.ai[1] < -17f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = 0;
                    }
                    else if (npc.ai[1] < -14f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight;
                    }
                    else if (npc.ai[1] < -11f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 2;
                    }
                    else if (npc.ai[1] < -8f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 3;
                    }
                    else if (npc.ai[1] < -5f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 4;
                    }
                    else if (npc.ai[1] < -2f)
                    {
                        npc.frameCounter = 0;
                        npc.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        if (npc.frameCounter++ > 7.5f)
                        {
                            npc.frameCounter = 0;
                            npc.frame.Y += frameHeight;
                            if (npc.frame.Y > frameHeight * 2)
                            {
                                npc.frame.Y = 0;
                            }
                        }
                    }
                }
                else if (npc.ai[0] == 1f)
                {
                    if (npc.velocity.Y != 0f)
                    {
                        npc.frame.Y = frameHeight * 5;
                    }
                    else
                    {
                        npc.frameCounter++;
                        if  (npc.frame.Y > 3)
                        {
                            if (npc.frameCounter > 0)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = frameHeight * 6;
                            }
                            else if (npc.frameCounter > 4)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = frameHeight * 7;
                            }
                            else if (npc.frameCounter > 8)
                            {
                                npc.frameCounter = 0;
                                npc.frame.Y = 0;
                            }
                        }
                        else
                        {
                            if (npc.frameCounter > 7.5f)
                            {
                        npc.frameCounter = 0;
                                npc.frame.Y += frameHeight;
                                if (npc.frame.Y > frameHeight * 2)
                                {
                                    npc.frame.Y = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            if (!Main.expertMode)
            {
                string[] lootTableA = { "BaneOfTheBunny", "Bunnyzooka", "RoyalScepter", "Punisher", "RabbitcopterEars"};
                int lootA = Main.rand.Next(lootTableA.Length);
                npc.DropLoot(mod.ItemType(lootTableA[lootA]));
            }
            Main.NewText("You win this time, murderer...but I will avenge those you've mercilicely slain...", 107, 137, 179);
            Projectile.NewProjectile(npc.position, npc.velocity, mod.ProjectileType<RajahBookIt>(), 100, 0, Main.myPlayer);
            npc.value = 0f;
            npc.boss = false;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);  //boss life scale in expertmode
            npc.damage = (int)(npc.damage * 1.3f);  //boss damage increase in expermode
        }

        public void RajahTexture()
        {
            if (CapeTex == null)
            {
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape");
            }
            if (ArmTex == null)
            {
                ArmTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahArms");
            }
            if (internalAI[4] == 0)
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/Rajah_Fly");
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape_Fly");
            }
            else
            {
                RajahTex = mod.GetTexture("NPCs/Bosses/Rajah/Rajah");
                CapeTex = mod.GetTexture("NPCs/Bosses/Rajah/RajahCape");
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            ArmTex = mod.GetTexture(WeaponTexture());
            RajahTexture();
            BaseDrawing.DrawTexture(spriteBatch, RajahTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, ArmTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, CapeTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 8, npc.frame, drawColor, true);
            return false;
        }
    }
}