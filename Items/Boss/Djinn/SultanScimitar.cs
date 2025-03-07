using Terraria.ID;

namespace AAMod.Items.Boss.Djinn

{
    public class SultanScimitar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sultan's Scimitar");
        }

		public override void SetDefaults()
		{
            
			item.damage = 24;
			item.melee = true;
			item.width = 58;
			item.height = 66;
			item.useTime = 26;
            item.useAnimation = 26;
            item.shoot = mod.ProjectileType("DesertGust");
            item.shootSpeed = 2f;
	        item.UseSound = SoundID.Item1;
			item.useStyle = 1;
			item.knockBack = 3;
            item.value = 50000;
            item.autoReuse = true;
            item.rare = 3;
		}
	}
}
