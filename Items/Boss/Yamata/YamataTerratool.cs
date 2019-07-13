using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Yamata
{
    public class YamataTerratool : BaseAAItem
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
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9; AARarity = 13;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.damage = 100;
            item.pick = 300;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Terratool");
            Tooltip.SetDefault(@"Right Click to change tool types
You may only have a maximum of 2 tool types active
+20 tile reach");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.noUseGraphic = true;
                AAMod.instance.TerratoolYState.ToggleUI(AAMod.instance.TerratoolYInterface);
                item.pick = 0;
                item.axe = 0;
                item.hammer = 0;
                item.damage = 0;
            }
            else
            {
                item.noUseGraphic = false;
                item.pick = UI.TerratoolYUI.Pick;
                item.axe = UI.TerratoolYUI.Axe;
                item.hammer = UI.TerratoolYUI.Hammer;
                item.damage = 100;
            }
            return true;
        }
    }
}