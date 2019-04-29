using AAMod.Items.Materials;
using AAMod.Items.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class HydraPod : ModTile
    {
        public int drop1;
        public int drop2;
        public int drop3;
        public int drop4;
        public int drop5;
        private Player player;

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            Main.tileHammer[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16 };
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorWall = true;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Hydra Pod");
            drop1 = mod.ItemType<HydrasSpear>(); //change me
            drop2 = mod.ItemType<Items.Ranged.Mossket>(); //change me
            drop3 = mod.ItemType<Items.Magic.GunkWand>(); //change me
            AddMapEntry(new Color(200, 200, 200), name);
            disableSmartCursor = true;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.3f;
            g = 0.0f;
            b = 0.9f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int thinger = Main.rand.Next(3);
            if (thinger == 0)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop1);
            }
            else if (thinger == 1)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop2);
            }
            else if (thinger == 2)
            {
                Item.NewItem(i * 16, j * 16, 32, 32, drop3);
            }
            

            if (AAWorld.SmashHydraPod == 2)
            {
                AAWorld.SmashHydraPod--;
                Main.NewText("The contents spill from the broken pod. Nasty.", Color.Blue);
            }
            else if (AAWorld.SmashHydraPod == 1)
            {
                AAWorld.SmashHydraPod--;
                Main.NewText("You hear hissing somewhere nearby", Color.Blue);
            }
            else
            {
                AAWorld.SmashHydraPod = 2;
                SpawnBoss(player, "Hydra", "The Hydra");
            }
        }
        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-100f, 100f, (float)Main.rand.NextDouble()), 800f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
                string npcName = (!string.IsNullOrEmpty(Main.npc[npcID].GivenName) ? Main.npc[npcID].GivenName : displayName);
                if (Main.netMode == 0) { Main.NewText(Language.GetTextValue("Announcement.HasAwoken", npcName), 175, 75, 255, false); }
                else
                if (Main.netMode == 2)
                {
                    NetMessage.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[]
                    {
                        NetworkText.FromLiteral(npcName)
                    }), new Color(175, 75, 255), -1);
                }
            }
        }
    }
}