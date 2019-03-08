﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BaseMod;

namespace AAMod.Tiles
{
    public class BinaryReassembler : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = true;
            dustType = mod.DustType("DoomDust");
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Binary Reassembler");
            AddMapEntry(new Color(40, 0, 0), name);
            disableSmartCursor = true;
            adjTiles = new int[]
            {
                TileID.LunarCraftingStation,
                TileID.WorkBenches,
                TileID.Hellforge,
                TileID.Furnaces,
                TileID.TinkerersWorkbench,
                TileID.AlchemyTable,
                TileID.Bottles,
                TileID.MythrilAnvil,
                TileID.Tables,
                TileID.DemonAltar,
                TileID.Chairs,
                TileID.Anvils,
                mod.TileType("HellstoneAnvil"),
                mod.TileType("HallowedAnvil"),
                mod.TileType("HallowedForge"),
                mod.TileType("QuantumFusionAccelerator"),
                mod.TileType("ACS"),
                TileID.MythrilAnvil,
                TileID.Anvils,
                TileID.CrystalBall,
                TileID.HeavyWorkBench,
                TileID.Hellforge,
                TileID.Furnaces,
                TileID.AdamantiteForge,
                TileID.Autohammer,
                TileID.ImbuingStation
            };
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.AlchemyTable];
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 0.50f;
            g = 0;
            b = 0f;
        }

        public Texture2D glowTex = null;

        public Color GetColor(Color color)
        {
            Color glowColor = AAColor.ZeroShield;
            return glowColor;
        }


        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/BinaryReassemblerTile_Glow");
            if (glowTex != null && tile != null && tile.active() && tile.type == this.Type)
            {
                int width = 16, height = 16;
                int frameX = (tile != null && tile.active() ? tile.frameX : 0);
                int frameY = (tile != null && tile.active() ? tile.frameY + (Main.tileFrame[this.Type] * 50) : 0);
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor);
                for (int m = 0; m < 3; m++)
                {
                    BaseDrawing.DrawTileTexture(sb, glowTex, x, y, width, height, frameX, frameY, false, false, false, null, GetColor, new Vector2(Main.rand.Next(-3, 4) * 0.5f, Main.rand.Next(-3, 4) * 0.5f));
                }
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 32, 16, mod.ItemType("BinaryReassembler"));
        }
    }
}