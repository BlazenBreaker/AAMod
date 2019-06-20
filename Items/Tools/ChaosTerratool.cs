using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class ChaosTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.melee = true;
            item.width = 54;
            item.height = 60;
			item.useStyle = 1;
            item.useTime = 5;
            item.useAnimation = 20;
            item.tileBoost += 3;
            item.knockBack = 3;
            item.value = 1000000;
            item.rare = 8;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 60;
            item.pick = 215;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            if (player.altFunctionUse != 2)
            {
                byte pre = item.prefix;
                item.TurnToAir();
                int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("ChaosTerratool_Axe"), 1, false, pre, false, false);
                if (Main.netMode == 1)
                {
                    NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
                }
            }
        }

        public override void AddRecipes()  //How to craft this item
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(mod, "PerfectStonebreaker");
                recipe.AddIngredient(mod, "PerfectShadowDrill");
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
