using System;
using System.Collections.Generic;
using System.Text;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Buffs
{
	public class Orbiters : ModBuff
	{
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Orbiters");
            Description.SetDefault("Flames orbit you, empowering you");
            Main.buffNoTimeDisplay[Type] = true;		
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("FireOrbiter")] > 0) modPlayer.Orbiters = true;
            if (!modPlayer.Orbiters)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}