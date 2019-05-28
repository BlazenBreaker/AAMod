using Terraria.ModLoader;

namespace AAMod.Items.Mushrooms
{
    public class MadnessShroom : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Mushroom");
            Tooltip.SetDefault(@"An exceedingly rare mushroom
Maybe the Mushman knows what to do with it?");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 11;
        }
    }
}