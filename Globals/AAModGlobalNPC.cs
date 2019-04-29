using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using BaseMod;
using Terraria.Localization;

namespace AAMod
{
    public class AAModGlobalNPC : GlobalNPC
	{
        //debuffs
        public bool TimeFrozen = false;
        public bool infinityOverload = false;
        public bool terraBlaze = false;
        public bool Dragonfire = false;
        public bool Hydratoxin = false;
        public bool Moonraze = false;
        public bool Electrified = false;
        public bool InfinityScorch = false;
        public bool irradiated = false;
        public bool DiscordInferno = false;
        public bool riftBent = false;
        public bool BrokenArmor = false;
        public static int Toad = -1;
        public static int Rose = -1;
        public static int Brain = -1;

        public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}

		public override void ResetEffects(NPC npc)
		{
            infinityOverload = false;
            terraBlaze = false;
            TimeFrozen = false;
            Dragonfire = false;
            Hydratoxin = false;
            Moonraze = false;
            Electrified = false;
            InfinityScorch = false;
            DiscordInferno = false;
            irradiated = false;
            riftBent = false;
            BrokenArmor = false;
        }

        public override void SetDefaults(NPC npc)
        {
            if (AAWorld.downedAllAncients == true)
            {
                if (npc.type == NPCID.GoblinSummoner)   //this is where you choose the npc you want
                {
                    npc.damage = 130;
                    npc.defense = 70;
                    npc.lifeMax = 10000;
                    npc.knockBackResist = 0.05f;
                    npc.value = 50000f;
                }
            }
        }

        public int RiftTimer;
        public int RiftDamage = 10;

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            int before = npc.lifeRegen;
            bool drain = false;
            bool noDamage = damage <= 1;
            int damageBefore = damage;

            if (infinityOverload)
            {
                drain = true;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 60;
                if (damage < 40)
                {
                    damage = 40;
                }
            }
            
            if (InfinityScorch)
            {
                drain = true;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 80;
                if (damage < 40)
                {
                    damage = 40;
                }
            }
            if (npc.type == NPCID.KingSlime || npc.type == NPCID.Plantera)
            {
                if (npc.onFire)
                {
                    if (npc.lifeRegen > 0)
                    {
                        npc.lifeRegen = 0;
                    }
                    npc.lifeRegen -= 20;
                }
            }

            if (riftBent)
            {
                RiftTimer++;
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen = 0;
                if (RiftTimer >= 120)
                {
                    RiftDamage += 10;
                    RiftTimer = 0;
                }
                if (RiftDamage >= 80)
                {
                    RiftDamage = 80;
                }
                npc.lifeRegen -= RiftDamage;
            }
            else
            {
                RiftDamage = 10;
                RiftTimer = 0;
            }

            if (noDamage)
                damage -= damageBefore;
            if (drain && before > 0)
                npc.lifeRegen -= before;
            if (terraBlaze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                /*if (npc.type == mod.NPCType<ShenDoragon>() || npc.type == mod.NPCType<ShenA>())
                {
                    npc.lifeRegen -= 48;
                }*/
                else
                {
                    npc.lifeRegen -= 16;
                }
                if (damage < 2)
                {
                    damage = 2;
                }
            }

            if (Moonraze)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                int num7 = 0;
                if (num7 == 0)
                {
                    num7 = 1;
                }
                npc.lifeRegen -= num7 * 2 * 100;
            }

            if (Electrified)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 8;
                if (npc.velocity.X >= 0 || npc.velocity.X <= 0)
                {
                    npc.lifeRegen -= 32;
                }
            }

            if (DiscordInferno)
            {
                npc.lifeRegen -= 52;
                npc.damage -= 10;
                if (npc.velocity.X < -2f || npc.velocity.X > 2f)
                {
                    npc.velocity.X *= 0.8f;
                }
                if (npc.velocity.Y < -2f || npc.velocity.Y > 2f)
                {
                    npc.velocity.Y *= 0.8f;
                }
            }

            if (BrokenArmor)
            {
               npc.defense *= (int).8f;
            }

            if (Dragonfire)
            {
                npc.damage -= 10;
            }

            if (Hydratoxin)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= (int)(npc.velocity.X);
            }

        }

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
            {
                if (item.pick > 0)
                {
                    damage = item.damage + item.pick;
                }
                else
                {
                    npc.defense = 20;
                }
            }
        }

        public override void NPCLoot(NPC npc)
        {
            if (npc.type == NPCID.FireImp)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(2, 3));
            }
            if (npc.type == NPCID.Demon)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(4, 5));
            }
            if (npc.type == NPCID.VoodooDemon)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilSilk"), Main.rand.Next(5, 6));
            }
            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PlanteraPetal"), Main.rand.Next(30, 40));
            }
            if (npc.type == NPCID.GreekSkeleton)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GladiatorsGlory"));
                }
            }

            if (npc.type == NPCID.DukeFishron)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Seashroom"));
                }
            }

            if (npc.type == NPCID.EnchantedSword)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Excalibur);
                }
            }

            if (npc.type == NPCID.CrimsonAxe)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.BloodLustCluster);
                }
            }

            if (npc.type == NPCID.CursedHammer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Shadowban"));
                }
            }

            if (Main.rand.Next(4096) == 0)   //item rarity
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShinyCharm")); //Item spawn
            }

            if (AAWorld.downedAllAncients == true)
            {
                if (npc.type == NPCID.GoblinSummoner)   //this is where you choose the npc you want
                {
                    if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GoblinDoll"), 1);
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss == true)
            {
                if (npc.type == NPCID.RedDevil)   //this is where you choose the npc you want
                {
                    if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PureEvil"), 1);
                        }
                    }
                }
            }
            if (npc.type == NPCID.EyeofCthulhu)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CthulhusBlade"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if (npc.type == NPCID.GiantFlyingFox)   //this is where you choose the npc you want
            {
                if (Main.rand.Next(4) == 0) //this is the item rarity, so 4 is 1 in 5 chance that the npc will drop the item.
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheFox"), 1); //this is where you set what item to drop, mod.ItemType("CustomSword") is an example of how to add your custom item. and 1 is the amount
                    }
                }
            }
            if (npc.type == NPCID.Necromancer)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Exorcist"));
                }
            }
            if (npc.type == NPCID.AngryBones ||
                npc.type == NPCID.AngryBonesBig ||
                npc.type == NPCID.AngryBonesBigHelmet ||
                npc.type == NPCID.AngryBonesBigMuscle)
            {
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AncientPoker"));
                }
            }
            if (npc.type == NPCID.Paladin)
            {
                if (Main.rand.NextFloat() < .17f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Helmet"));
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Chestplate"));
                    Item.NewItem(npc.getRect(), mod.ItemType("Paladin_Boots"));
                }
            }
            //Probes (Lil destroyer laser shits)
            if (npc.type == NPCID.Probe)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(3, 12));
            }
            //The Destroyer
            if (npc.type == NPCID.TheDestroyer)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(8, 16));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Laser_Rifle"));
                }
            }
            //Skeletrono Primeus (Skeletron Prime)
            if (npc.type == NPCID.SkeletronPrime)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Energy_Cell"), Main.rand.Next(8, 16));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Laser_Rifle"));
                }
            }
            //Wall Of Flesh
            if (npc.type == NPCID.WallofFlesh)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Nightmare_Ore"), Main.rand.Next(50, 60));
                if (Main.rand.NextFloat() < .34f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("HK_MP5"));
                }
            }
            //Mothership
            if (npc.type == NPCID.MartianSaucerCore)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Alien_Rifle"));
                }
                if (Main.rand.NextFloat() < .03f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Energy_Conduit"));
                }
            }
            if (npc.type == NPCID.CursedSkull)
            {
                if (Main.rand.NextFloat() < .12f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("SkullStaff"));
                }
            }
            if (npc.type == NPCID.Vulture)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("vulture_feather"), Main.rand.Next(1, 3));
            }
            //Drippler
            if (npc.type == NPCID.Drippler)
            {
                if (Main.rand.NextFloat() < .005f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("Bloody_Mary"));
                }
            }
			if (npc.type == NPCID.AngryBones || npc.type == NPCID.DarkCaster)
            {
                if (Main.rand.Next(200) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("M79Parts"));
                }
            }
            if (npc.type == NPCID.QueenBee)
            {
                if (Main.rand.NextFloat() < .01f)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("BugSwatter"));
                }
                Item.NewItem(npc.getRect(), ItemID.Stinger, Main.rand.Next(14, 20));
            }

            if (npc.type == NPCID.Plantera)
            {
                Item.NewItem(npc.getRect(),  ItemID.ChlorophyteOre, Main.rand.Next(50, 80));
            }

            if (npc.type == NPCID.SkeletronHand)
            {
                Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(4, 8));
            }
            if (npc.type == NPCID.SkeletronHead)
            {
                Item.NewItem(npc.getRect(), ItemID.Bone, Main.rand.Next(30, 45));
            }

            if (Main.player[Main.myPlayer].ZoneJungle && Main.rand.Next(30) == 0)
            {
                Item.NewItem(npc.getRect(), mod.ItemType("Everleaf"), Main.rand.Next(1, 2));
            }
            
            if ((npc.type == NPCID.GoblinArcher
                || npc.type == NPCID.GoblinPeon
                || npc.type == NPCID.GoblinScout
                || npc.type == NPCID.GoblinSorcerer
                || npc.type == NPCID.GoblinSummoner
                || npc.type == NPCID.GoblinThief
                || npc.type == NPCID.GoblinWarrior
                || npc.type == NPCID.DD2GoblinBomberT1
                || npc.type == NPCID.DD2GoblinBomberT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.DD2GoblinT1
                || npc.type == NPCID.DD2GoblinT2
                || npc.type == NPCID.DD2GoblinBomberT3
                || npc.type == NPCID.BoundGoblin
                || npc.type == NPCID.GoblinTinkerer) 
                && NPC.downedGoblins)
            {
                if (Main.rand.Next(20) == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType("GoblinSoul"), Main.rand.Next(1, 2));
                }
                
            }

            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneMire) && Main.hardMode)
            {
                if (Main.rand.Next(0, 100) >= 80)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSpite"), 1);
                }
            }
            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneInferno) && Main.hardMode)
            {
                if (Main.rand.Next(0, 100) >= 80)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SoulOfSmite"), 1);
                }
            }

            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneMire) && Main.hardMode)
            {
                if (Main.rand.Next(0, 2499) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MireKey"), 1);
                }
            }
            if ((Main.player[(int)Player.FindClosest(npc.position, npc.width, npc.height)].GetModPlayer<AAPlayer>(mod).ZoneInferno) && Main.hardMode)
            {
                if (Main.rand.Next(0, 2499) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("InfernoKey"), 1);
                }
            }
        }

        public void Anticheat(NPC npc, string Text, Color TextColor, ref double damage)
        {
            if (damage > npc.lifeMax / 8)
            {
                Main.NewText(Text, TextColor);
                damage = 0;
            }
        }

		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
            Rectangle hitbox = npc.Hitbox;
            if (Electrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (infinityOverload)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadB"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.3f, 0.7f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadR"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.2f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadG"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadY"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.5f, 0.5f, 0.1f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadP"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.6f, 0.1f, 0.6f);
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("InfinityOverloadO"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.8f, 0.5f, 0.1f);
            }

            if (Moonraze)
            {
                int dustCount = Math.Max(1, Math.Min(5, (Math.Max(npc.width, npc.height) / 10)));
                for (int i = 0; i < dustCount; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.Moonraze>(), 0f, 1f, 0, default(Color), 1f);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (riftBent)
            {
                int Loops = RiftDamage / 10;
                for (int i = 0; i < Loops; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.CthulhuAuraDust>(), 0f, 1f, 0, default(Color), 1f);
                    if (Main.dust[num4].velocity.Y > 0) Main.dust[num4].velocity.Y *= -1;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
                Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0f, 0.45f, 0.45f);
            }

            if (DiscordInferno)
            {
                for (int i = 0; i < 8; i++)
                {
                    int num4 = Dust.NewDust(hitbox.TopLeft(), npc.width, npc.height, mod.DustType<Dusts.Discord>(), 0f, -2.5f, 0, default(Color), 1f);
                    Main.dust[num4].alpha = 100;
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].scale += Main.rand.NextFloat();
                }
            }

            if (terraBlaze)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 107, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.7f, 0.2f);
            }

            if (InfinityScorch)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 107, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, mod.DustType<Dusts.VoidDust>(), default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.7f, 0.2f, 0.1f);
            }

        }

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            for (int i = 0; i < 200; ++i)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                {
                    spawnRate = 0;
                    maxSpawns = 0;
                }
            }
        }

        public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
        {
            Player player = Main.player[Main.myPlayer];
            

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneStars)
            {
                if (Main.dayTime)
                {
                    pool.Add(mod.NPCType("Sunwatcher"), .2f);
                }
                else
                {
                    pool.Add(mod.NPCType("Nightguard"), .2f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno)
            {
                pool.Clear();
                if ((player.position.Y < (Main.worldSurface * 16.0)) && (Main.dayTime || AAWorld.downedAkuma))
                {
                    pool.Add(mod.NPCType("Wyrmling"), .3f);
                    pool.Add(mod.NPCType("InfernalSlime"), .7f);
                    pool.Add(mod.NPCType("Flamebrute"), .3f);
                    pool.Add(mod.NPCType("InfernoSalamander"), .7f);
                    pool.Add(mod.NPCType("DragonClaw"), .7f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("Wyvern"), .2f);
                        pool.Add(mod.NPCType("BlazePhoenix"), .1f);
                    }
                }
                else if (player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(mod.NPCType("Wyrmling"), .3f);
                    pool.Add(mod.NPCType("Flamebrute"), .3f);
                    pool.Add(mod.NPCType("InfernoSalamander"), .7f);
                    pool.Add(mod.NPCType("DragonClaw"), .7f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("Wyvern"), .2f);
                        pool.Add(mod.NPCType("Wyrm"), .1f);
                        pool.Add(mod.NPCType("ChaoticDawn"), .1f);
                        if (player.ZoneSnow)
                        {
                            pool.Add(mod.NPCType("Dragron"), .05f);
                        }
                    }
                }
                if (AAWorld.downedGripsS)
                {
                    pool.Add(mod.NPCType("BlazeClaw"), .6f);
                }
                if (AAWorld.downedAkuma)
                {
                    pool.Add(mod.NPCType("Lung"), .2f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                pool.Clear();
                if ((player.position.Y < (Main.worldSurface * 16.0)) && (!Main.dayTime || AAWorld.downedYamata))
                {
                    pool.Add(mod.NPCType("MireSlime"), 1f);
                    pool.Add(mod.NPCType("Mosster"), .5f);
                    pool.Add(mod.NPCType("Newt"), 1f);
                    pool.Add(mod.NPCType("HydraClaw"), 1f);
                    pool.Add(mod.NPCType("MireSkulker"), .7f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("Toxitoad"), .2f);
                        pool.Add(mod.NPCType("Kappa"), .4f);
                    }
                }
                else if (player.position.Y > (Main.worldSurface * 16.0))
                {
                    pool.Add(mod.NPCType("Mosster"), .5f);
                    pool.Add(mod.NPCType("Newt"), 1f);
                    pool.Add(mod.NPCType("HydraClaw"), 1f);
                    pool.Add(mod.NPCType("MireSkulker"), .5f);
                    if (Main.hardMode)
                    {
                        pool.Add(mod.NPCType("Kappa"), .4f);
                        pool.Add(mod.NPCType("ChaoticTwilight"), .1f);
                        if (player.ZoneSnow)
                        {
                            pool.Add(mod.NPCType("Miregron"), .05f);
                        }
                    }
                }
                if (AAWorld.downedGripsS)
                {
                    pool.Add(mod.NPCType("AbyssClaw"), .8f);
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneVoid)
            {
                pool.Clear();
                pool.Add(mod.NPCType("Searcher1"), .05f);
                if (AAWorld.downedSag)
                {
                    pool.Add(mod.NPCType("SagittariusMini"), .025f);
                }
                if (NPC.downedPlantBoss)
                {
                    pool.Add(mod.NPCType("Vortex"), 0.05f);
                    pool.Add(mod.NPCType("Scout"), .05f);
                }
                if (NPC.downedMoonlord)
                {
                    pool.Add(mod.NPCType("Searcher"), .05f);
                    if (AAWorld.downedZero)
                    {
                        pool.Add(mod.NPCType("Null"), .05f);
                    }
                }
            }

            if (spawnInfo.player.GetModPlayer<AAPlayer>(mod).Terrarium)
            {
                pool.Clear();
                if (NPC.downedPlantBoss)
                {
                    pool.Add(mod.NPCType("Bladon"), .1f);
                    pool.Add(mod.NPCType("TerraDeadshot"), .1f);
                    pool.Add(mod.NPCType("TerraWizard"), .1f);
                    pool.Add(mod.NPCType("TerraWarlock"), .1f);
                }
                else
                {
                    pool.Add(mod.NPCType("PurityWeaver"), .1f);
                    pool.Add(mod.NPCType("PuritySphere"), .1f);
                    pool.Add(mod.NPCType("PurityCrawler"), .1f);
                    pool.Add(mod.NPCType("PuritySquid"), .1f);
                }
            }
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			if (type == NPCID.Demolitionist && !Main.dayTime)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("M79Round"));
                nextSlot++;
            }

            if (type == NPCID.WitchDoctor && Main.hardMode)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Mortar"));
                nextSlot++;
            }
        }

        public const string HeadTex = "AAMod/Resprites/TEoCHead";

        public override void BossHeadSlot(NPC npc, ref int index)
        {
            if (npc.type == NPCID.MoonLordFreeEye)
            {
                index = NPCHeadLoader.GetBossHeadSlot(HeadTex);
            }
        }

		//mod, player - self explanitory
		//type - the internal name of the boss to summon
		//SpawnMessage - wether or not to show a message when summoning (ie 'Has/Have Awoken!")
		//overrideDirection - wether to force the boss to spawn left/right of the player
		//overrideDirectionY - wether to force the boss to spawn above/below the player
		//overrideDisplayName - the name to use instead of the npc's display name (ie 'The Grips of Chaos')
		//namePlural - if true, puts 'Have Awoken' instead of 'Has Awoken'.
        public static void SpawnBoss(Mod mod, Player player, string type, bool SpawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
			//if the direction is not overriden (ie is 0), pick left/right at random
			if(overrideDirection == 0)
				overrideDirection = (Main.rand.Next(2) == 0 ? -1 : 1);
			//if the direction is not overriden (ie is 0), default to above			
			if(overrideDirectionY == 0)
				overrideDirectionY = -1;
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(type);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
                Main.npc[npcID].netUpdate2 = true;
                if (SpawnMessage)
                {
					//check if the npc has a 'given name' (not usually for modded) and if not use the display name given
					string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName);	
					//if npcName is still blank ("") then default to the npc's display name if it's modded
					if((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
						npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();					
					if(namePlural)
					{
						if (Main.netMode == 0) { Main.NewText(npcName + " have awoken!", 175, 75, 255, false); }
						else
						if (Main.netMode == 2)
						{
							NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
						}						
					}else
					{
						if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
						else
						if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " has awoken!"), new Color(175, 75, 255), -1);
                        }
					}
                }
            }
        }

        public static void SpawnBoss(Mod mod, Player player, int type, bool SpawnMessage = true, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            //if the direction is not overriden (ie is 0), pick left/right at random
            if (overrideDirection == 0)
                overrideDirection = (Main.rand.Next(2) == 0 ? -1 : 1);
            //if the direction is not overriden (ie is 0), default to above			
            if (overrideDirectionY == 0)
                overrideDirectionY = -1;
            if (Main.netMode != 1)
            {
                int bossType = type;
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
                Main.npc[npcID].netUpdate2 = true;
                if (SpawnMessage)
                {
                    //check if the npc has a 'given name' (not usually for modded) and if not use the display name given
                    string npcName = (!String.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : overrideDisplayName);
                    //if npcName is still blank ("") then default to the npc's display name if it's modded
                    if ((npcName == null || npcName.Equals("")) && Main.npc[npcID].modNPC != null)
                        npcName = Main.npc[npcID].modNPC.DisplayName.GetDefault();
                    if (namePlural)
                    {
                        if (Main.netMode == 0) { Main.NewText(npcName + " have awoken!", 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(npcName + " have awoken!"), new Color(175, 75, 255), -1);
                        }
                    }
                    else
                    {
                        if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                        else
                        if (Main.netMode == 2)
                        {
                            NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                            {
                            NetworkText.FromLiteral(npcName)
                            }), new Color(175, 75, 255), -1);
                        }
                    }
                }
            }
        }

    }
    public abstract class AANPC : ParentNPC
    {
        public virtual bool CanSpawn(int x, int y, int type, Player player, NPCSpawnInfo info)
        {
            return CanSpawn(x, y, type, player);
        }
        public virtual bool CanSpawn(int x, int y, int type, Player player)
        {
            return false;
        }
    }
}
