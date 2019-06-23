using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Terraria.Audio;

namespace AAMod.Items.Boss.Zero
{
    public class Neutralizer : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Neutralizer");
            Tooltip.SetDefault("Fires bouncing lasers that get more powerful as they bounce off walls");
            
        }

        public override void SetDefaults()
		{
			item.damage = 420;
			item.ranged = true;
			item.width = 34;
			item.height = 58;
			item.useTime = 13;
			item.useAnimation = 13;
			item.useStyle = 5;
			item.noMelee = true;
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.UseSound = new LegacySoundStyle(2, 75, Terraria.Audio.SoundType.Sound);
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Neutralizer");
			item.shootSpeed = 8f;
            item.rare = 9; AARarity = 13;

        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "ApollosWrath", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
