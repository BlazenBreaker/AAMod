using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class AkumaTerratool : BaseAAItem
    {
        public override void SetDefaults()
        {

            item.melee = true;
            item.width = 54;
            item.height = 60;
            item.useStyle = 1;
            item.useTime = 4;
            item.useAnimation = 20;
            item.tileBoost += 20;
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 20, 0, 0);
            item.rare = 9;
            AARarity = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 100;
            item.pick = 300;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Draconian Terratool");
            Tooltip.SetDefault("Right Click to change tool types");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                AAMod.instance.TerratoolAState.ToggleUI(AAMod.instance.TerratoolAInterface);
                item.pick = UI.TerratoolAUI.Pick;
                item.axe = UI.TerratoolAUI.Axe;
                item.hammer = UI.TerratoolAUI.Hammer;
                return true;
            }
            else
            {
                // do stuff
            }

            return false;
        }
    }
}
