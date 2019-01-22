using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.SoC;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace AAMod.Items.BossSummons
{
    public class CursedCompass : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cursed Compass");
        }

        private bool CthulhuFightable = AAWorld.downedAllAncients && !AAWorld.downedSoC;

        public override void SetDefaults()
        {
            Tooltip.SetDefault(@"Unnerving energy radiates from this old ship's wheel");
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.UseSound = SoundID.Item44;
            item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = CthulhuFightable ? new Color(100, 100, 100) : AAColor.Cthulhu;
                }
            }
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {

            Tooltip.SetDefault(CthulhuFightable ? "An old, broken compass. Who knows what it's for." : "The compass' arrow spins rapidly, giving off an eerie vibe.");
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<SoC>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The wheel doesn't do anything", Color.DarkCyan, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "CthulhuSpawn", "The Soul of Cthulhu");
            Main.PlaySound(SoundID.Roar, player.position, 0);
            return true;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; }
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-300f, 300f, (float)Main.rand.NextDouble()), 300f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public float ArrowSpin = 0;

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            ArrowSpin += .008f;
            Texture2D Arrow = mod.GetTexture("Items/BossSummons/CursedCompass_Arrow");
            Vector2 offsetPositon = new Vector2(item.position.X, item.position.Y - 2);
            spriteBatch.Draw(Arrow, position, null, drawColor, CthulhuFightable? ArrowSpin : 0, origin, scale, SpriteEffects.None, 0f);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            ArrowSpin += .008f;
            Texture2D texture2D13 = Main.itemTexture[item.type];
            Texture2D Arrow = mod.GetTexture("Items/BossSummons/CursedCompass_Arrow");
            Vector2 position = item.position - Main.screenPosition + new Vector2(item.width / 2, item.height - texture2D13.Height * 0.5f + 2f);
            Vector2 offsetPositon = new Vector2(item.position.X, item.position.Y - 2);
            spriteBatch.Draw(Arrow, position, null, Main.DiscoColor, CthulhuFightable ? ArrowSpin : rotation, texture2D13.Size() * 0.5f, scale, SpriteEffects.None, 0f);

        }

        public override void UseStyle(Player p) { BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseUseStyle.SetFrameBoss(p, item); return true; }
    }
}