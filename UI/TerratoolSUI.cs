﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.UI;
using AAMod.Items.Boss.Shen;

namespace AAMod.UI
{
    internal sealed class TerratoolSUI : TerratoolUI
    {
        public static int Pick = 0;

        public static int Hammer = 0;

        public static int Axe = 0;

        public override Texture2D ButtonImages() { return AAMod.instance.GetTexture("UI/Tools/ToolUIS"); }

        public override Texture2D ButtonOnImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonS"); }

        public override Texture2D ButtonOffImage() { return AAMod.instance.GetTexture("UI/Tools/ToolButtonSOff"); }

        public override int HeldItemType() { return AAMod.instance.ItemType<ShenTerratool>(); }

        public override UserInterface Interface() { return AAMod.instance.TerratoolSInterface; }

        public override UIState State() { return AAMod.instance.TerratoolSState; }

        public override void ButtonClicked(int index)
        {
            base.ButtonClicked(index);
            Pick = selectedButtons.Contains(0) ? 320 : 0;
            Hammer = selectedButtons.Contains(1) ? 200 : 0;
            Axe = selectedButtons.Contains(2) ? 70 : 0;
        }
    }
}
