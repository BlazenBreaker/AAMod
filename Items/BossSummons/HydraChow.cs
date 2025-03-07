using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class HydraChow : BaseAAItem
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Chow");
            Tooltip.SetDefault(@"Just holding this makes you gag
Summons the Hydra
Can only be used at night");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 500;
            item.consumable = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MirePod", 15);
            recipe.AddIngredient(null, "Moonpowder", 30);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("Hydra"), true, 0, 0, "The Hydra", false);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
		}

		public override bool CanUseItem(Player player)
		{
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Nothing is coming. The creatures of the Mire sleep.", Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);
                return false;
            }
            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
			{
				if (NPC.AnyNPCs(mod.NPCType("Hydra")))
				{
					if(player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("The Hydra wants that food.", Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);
					return false;
				}
                return true;
			}
			if(player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("Nothing is coming. Now you look dumb holding out this smelly ball of gunk.", Color.Indigo.R, Color.Indigo.G, Color.Indigo.B, false);			
			return false;
		}	
	}
}