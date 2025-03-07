using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Serpent
{
    public class IceCrystal : ModNPC
    {
        public override void SetDefaults()
        {
            npc.life = 200;
            npc.defense = 10;
            npc.width = 30;
            npc.height = 30;
            npc.aiStyle = -1;
            npc.alpha = 255;
            npc.value = 0;
            npc.noGravity = true;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Ice Crystal");
		}

        public override void AI()
        {
            if (Main.netMode != 1)
            {
                npc.ai[2]++;
            }
            if (npc.alpha > 40)
            {
                npc.alpha -= 3;
            }
            else
            {
                BaseMod.BaseAI.ShootPeriodic(npc, npc.position, npc.width, npc.height, mod.ProjectileType<IceSpike>(), ref npc.ai[0], 180, npc.damage / 2, 7, true);
                npc.alpha = 40;
            }
        }

        public override void NPCLoot()
        {
            Main.PlaySound(2, npc.position, 50);
            int pieCut = 20;
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(6f, 0f), m / pieCut * 6.28f);
            }
            for (int m = 0; m < pieCut; m++)
            {
                int dustID = Dust.NewDust(new Vector2(npc.Center.X - 1, npc.Center.Y - 1), 2, 2, mod.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = BaseMod.BaseUtility.RotateVector(default, new Vector2(9f, 0f), m /pieCut * 6.28f);
                Main.dust[dustID].noLight = false;
            }
        }
    }
}
