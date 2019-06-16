using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class RaiderMask : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Raider Ultima Mask");
		}

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.rare = 2;
            item.vanity = true;
        }
    }
}