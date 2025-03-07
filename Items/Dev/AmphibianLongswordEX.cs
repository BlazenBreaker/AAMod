using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class AmphibianLongswordEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Amphibious Greatblade");
            Tooltip.SetDefault(@"Amphibious Longsword EX");
        }
		public override void SetDefaults()
		{
			item.damage = 350;
			item.melee = true;
			item.width = 64;
			item.height = 64;
            item.useTime = 27;
			item.useAnimation = 27;
            item.useStyle = 1;
			item.knockBack = 7;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("AmphibiousProjectileEX");
            item.shootSpeed = 18f;
            item.expert = true; item.expertOnly = true;
		}
        
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AmphibianLongsword");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
