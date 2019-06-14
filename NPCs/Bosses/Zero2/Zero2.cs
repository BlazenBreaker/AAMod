using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Zero;

namespace AAMod.NPCs.Bosses.Zero2
{
    [AutoloadBossHead]	
    public class Zero2 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.npcFrameCount[npc.type] = 2; 
        }

        public override void SetDefaults()
        {
            npc.damage = 100;
            npc.defense = 90;
            npc.lifeMax = 175000;
            if (Main.expertMode)
            {
                npc.value = 0;
            }
            else
            {
                npc.value = 120000f;
            }
            npc.width = 206;
            npc.height = 208;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Zero");
            npc.noTileCollide = true;
            
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.friendly = false;
            npc.npcSlots = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.netAlways = true;
            musicPriority = MusicPriority.BossHigh;

            if (AAWorld.downedAllAncients)
            {
                npc.lifeMax = 200000;
                npc.damage = 140;
                npc.defense = 120;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.type == mod.NPCType<Zero2>() && (NPC.AnyNPCs(mod.NPCType<VoidStar>()) || NPC.AnyNPCs(mod.NPCType<Taser>()) || NPC.AnyNPCs(mod.NPCType<RealityCannon>()) || NPC.AnyNPCs(mod.NPCType<RiftShredder>())))
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
            }
            if (npc.life <= 0 && npc.type == mod.NPCType<Zero2>())
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 100;
                npc.height = 100;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                if (Main.expertMode)
                {
                    Main.NewText("PHYSICAL ZER0 UNIT IN CRITICAL C0NDITI0N. DISCARDING AND ENGAGING D00MSDAY PR0T0C0L.", Color.Red.R, Color.Red.G, Color.Red.B);
                    NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("ZeroAwakened"));
                }
                if (!Main.expertMode)
                {
                    Main.NewText("D00MSDAY PR0T0CALL MALFUNCTI0N. MAIN.EXPERT M0DE = FALSE.", Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
            }
            else
            {
                if (!AAWorld.downedZero)
                {
                    Main.NewText("Doomstone stops glowing. You can now mine it.", Color.Silver);
                }
                AAWorld.downedZero = true;
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
                npc.DropLoot(mod.ItemType("UnstableSingularity"), 25, 35);
                string[] lootTable =
                {
                    "Battery",
                    "ZeroArrow",
                    "Vortex",
                    "EventHorizon",
                    "RealityCannon",
                    "RiftShredder",
                    "VoidStar",
                    "TeslaHand",
                    "ZeroStar",
                    "Neutralizer",
                    "ZeroTerratool",
                    "DoomPortal"
                };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                npc.DropLoot(Items.Vanity.Mask.ZeroMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Zero.ZeroTrophy.type, 1f / 10);
                npc.DropLoot(Items.Boss.EXSoul.type, 1f / 10);
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
                }
            }
        }
        
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
            }
            else
            {
                potionType = 0;
            }
        }

        public Color GetGlowAlpha()
        {
            return AAColor.ZeroShield * (Main.mouseTextColor / 255f);
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Zero_Glow");
            Texture2D Shield = mod.GetTexture("NPCs/Bosses/Zero/ZeroShield");
            Texture2D Ring = mod.GetTexture("NPCs/Bosses/Zero/ZeroShieldRing");
            Texture2D RingGlow = mod.GetTexture("Glowmasks/ZeroShieldRing_Glow");

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, GetGlowAlpha());

            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Shield, 0, npc.position, npc.width, npc.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), GetGlowAlpha(), true);
                BaseDrawing.DrawTexture(spritebatch, Ring, 0, npc.position, npc.width, npc.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingGlow, 0, npc.position, npc.width, npc.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), GetGlowAlpha(), true);
            }
            return false;
        }

        public int MinionTimer = 0;
        public float[] internalAI = new float[4];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public bool saythelinezero = false;
        public bool ArmsGone = false;
        public float ShieldScale = 0.5f;
        public float RingRoatation = 0;
        public int WeaponCount = Main.expertMode ? 6 : 4;

        public override void AI()
        {
            RingRoatation += 0.03f;

            

            if (internalAI[0] == 0 && Main.netMode != 1)
            {
                for (int m = 0; m < WeaponCount; m++)
                {
                    int npcID = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType(ArmChoice()), 0);
                    Main.npc[npcID].Center = npc.Center;
                    Main.npc[npcID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                    Main.npc[npcID].velocity *= 8f;
                    Main.npc[npcID].ai[0] = m;
                    Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                }
                internalAI[0] = 1;
            }

            if (!NPC.AnyNPCs(mod.NPCType<VoidStar>()) &&
                !NPC.AnyNPCs(mod.NPCType<Taser>()) &&
                !NPC.AnyNPCs(mod.NPCType<RealityCannon>()) &&
                !NPC.AnyNPCs(mod.NPCType<RiftShredder>()) &&
                !NPC.AnyNPCs(mod.NPCType<Neutralizer>()) &&
                !NPC.AnyNPCs(mod.NPCType<OmegaVolley>()) &&
                !NPC.AnyNPCs(mod.NPCType<NovaFocus>()) &&
                !NPC.AnyNPCs(mod.NPCType<GenocideCannon>()))
            {
                npc.ai[1] = 1;
            }
            else
            {
                npc.ai[1] = 0;
            }

            if (npc.ai[1] == 0)
            {
                ArmsGone = false;
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.damage = 0;
                saythelinezero = false;
                if (ShieldScale < .5f)
                {
                    ShieldScale += .05f;
                }
                if (ShieldScale > .5f)
                {
                    ShieldScale = .5f;
                }
                if (internalAI[1] == 0)
                {
                    npc.velocity.Y += 0.003f;
                    if (npc.velocity.Y > .3f)
                    {
                        internalAI[1] = 1f;
                        npc.netUpdate = true;
                    }
                }
                else if (internalAI[1] == 1)
                {
                    npc.velocity.Y -= 0.003f;
                    if (npc.velocity.Y < -.3f)
                    {
                        internalAI[1] = 0f;
                        npc.netUpdate = true;
                    }
                }
            }
            else
            {
                ArmsGone = true;
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.damage = 160;
                if (ShieldScale > 0)
                {
                    ShieldScale -= .07f;
                }
                if (ShieldScale < 0)
                {
                    ShieldScale = 0;
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (ArmsGone)
            {
                npc.frame.Y = 0;
            }
            else
            {
                npc.frame.Y = frameHeight;
            }
        }

        public string ArmChoice()
        {
            string Choice = null;
            while (Choice == null)
            {
                int Arms = Main.rand.Next(8);
                switch (Arms)
                {
                    case 0:
                        Choice = "GenocideCannon2";
                        break;
                    case 1:
                        Choice = "Neutralizer2";
                        break;
                    case 2:
                        Choice = "NovaFocus2";
                        break;
                    case 3:
                        Choice = "OmegaVolley2";
                        break;
                    case 4:
                        Choice = "RealityCannon2";
                        break;
                    case 5:
                        Choice = "RiftShredder2";
                        break;
                    case 6:
                        Choice = "Taser2";
                        break;
                    case 7:
                        Choice = "VoidStar2";
                        break;
                }

                if(NPC.AnyNPCs(mod.NPCType(Choice)))
                {
                    Choice = null;
                }
            }
            return Choice;
        }
    }
}







