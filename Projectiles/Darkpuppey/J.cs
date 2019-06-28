﻿using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Projectiles.Darkpuppey
{
    public class J : A
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            Length = 15;
            GlowColor = Color.LimeGreen;
            AlphaInterval = 70;
            Debuff = BuffID.CursedInferno;
        }
    }
}