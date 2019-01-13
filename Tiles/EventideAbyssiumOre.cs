using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class EventideAbyssiumOre : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileMerge[TileID.Mud][Type] = true;
            //true for block to emit light
            soundType = 21;
            Main.tileLighted[Type] = true;
            drop = mod.ItemType("EventideAbyssiumOre");   //put your CustomBlock name
            dustType = mod.DustType("YamataADust");
            AddMapEntry(new Color(0, 0, 30));
			minPick = 225;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/EventideAbyssiumOre_Glow");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetYamataColorDim);
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)   //light colors
        {
            r = .51f;
            g = 0f;
            b = 0f;
        }
    }
}