using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class CometBar : ModTile
    {
        public override void SetDefaults()
        {
            soundType = 21;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileSolid[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new[] { 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            drop = mod.ItemType("CometBar");   //put your CustomBlock name
            dustType = mod.DustType("AbyssDust");
            AddMapEntry(new Color(100, 0, 255));
			minPick = 0;
        }
    }
}