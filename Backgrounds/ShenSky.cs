﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;

namespace AAMod.Backgrounds
{
    public class ShenSky : CustomSky
    {
        
        public static Texture2D Sun;
        public static Texture2D Moon;
        public static Texture2D BGTexture;
        public static Texture2D SkyTex;
        public bool Active;
        public float Intensity;

        public override void OnLoad()
        {
            SkyTex = TextureManager.Load("Backgrounds/Sky");
            Moon = TextureManager.Load("Backgrounds/MireMoon");
            Sun = TextureManager.Load("Backgrounds/InfernoSun");
        }


        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (!Main.dayTime)
                {

                    Vector2 SkyPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
                    spriteBatch.Draw(SkyTex, SkyPos, null, Color.Magenta, 0f, new Vector2(SkyTex.Width >> 1, SkyTex.Height >> 1), 1f, SpriteEffects.None, 1f);
                    var planetPos = new Vector2((Main.screenWidth / 4f) * 2, Main.screenHeight / 4);
                    spriteBatch.Draw(Moon, planetPos, null, Color.White * 0.9f * this.Intensity, 0f, new Vector2(Moon.Width >> 1, Moon.Height >> 1), 1f, SpriteEffects.None, 1f);
                    planetPos = new Vector2(Main.screenWidth * .75f, Main.screenHeight / 4);
                    spriteBatch.Draw(Sun, planetPos, null, Color.White * 0.9f * Intensity, 0f, new Vector2(Sun.Width >> 1, Sun.Height >> 1), 1f, SpriteEffects.None, 1f);
                }
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }
        

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }
}