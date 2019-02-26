﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Snow.Snakes
{
	public class SnakeHeadM : ModNPC
    {
        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/Snakes/SnakeHeadM"; } }
        bool TailSpawned = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Snow Serpent");
		}

		public override void SetDefaults()
		{
			npc.damage = 20;
			npc.npcSlots = 5f;
            npc.damage = 35;
            npc.width = 28;
            npc.height = 28;
            npc.defense = 13;
            npc.lifeMax = 250;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 10, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneSnow && spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneMire && NPC.downedBoss3 && !Main.dayTime ? .1f : 0f;
        }

        public override void AI()
        {
			BaseMod.BaseAI.AIWorm(npc, new int[]{ mod.NPCType("SnakeHeadM"), mod.NPCType("SnakeBodyM"), mod.NPCType("SnakeTailM") }, 5, 8f, 12f, 0.1f, false, false);

            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
            }
        }
        
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			if (Main.expertMode)
			{
				player.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
				player.AddBuff(BuffID.Chilled, 100, true);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * 0.75f * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.85f);
		}

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0, default(Color), 1f);
            }
            if (npc.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDustLight>(), hitDirection, -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.rand.Next(4) == 0)
            {
                npc.DropLoot(mod.ItemType("SubzeroCrystal"));
            }
        }
    }

    public class SnakeBodyM : SnakeHeadM
    {
        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/Snakes/SnakeBodyM"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Serpent");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }

    public class SnakeTailM : SnakeHeadM
    {
        public override string Texture { get { return "AAMod/NPCs/Enemies/Snow/Snakes/SnakeHeadM"; } }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Serpent");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }
    }
}