using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BotchedBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 24;
            item.value = Item.sellPrice(0, 8, 0, 0);
            item.rare = 6;
            item.accessory = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .1f;
            player.magicDamage += .1f;
            player.meleeDamage += .1f;
            player.rangedDamage += .1f;
            player.thrownDamage += .1f;
            player.minionDamage += .1f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Botched Band");
            Tooltip.SetDefault(
@"10% Increased movement speed and damage");
        }

    }
}