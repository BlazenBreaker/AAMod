using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.Shen
{
    public class WrathHarukaVanish : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Haruka Yamata");     
            Main.npcFrameCount[npc.type] = 17;     
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 90;
            npc.height = 78;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;

            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            npc.velocity.Y += .1f;

            npc.frame.Y = 78 * (int)npc.ai[1];

            if (npc.ai[2] == 0)
            {
                if (++npc.ai[0] >= 6)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] += 1;
                    if (npc.frame.Y > (92 * 12))
                    {
                        npc.ai[2] = 1;
                        Main.PlaySound(SoundID.Item14, npc.position);
                        Vector2 position = npc.Center + (Vector2.One * -20f);
                        int num84 = 40;
                        int height3 = num84;
                        for (int num85 = 0; num85 < 3; num85++)
                        {
                            int num86 = Dust.NewDust(position, num84, height3, 240, 0f, 0f, 100, default, 1.5f);
                            Main.dust[num86].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                        }
                        for (int num87 = 0; num87 < 15; num87++)
                        {
                            int num88 = Dust.NewDust(position, num84, height3, mod.DustType<Dusts.AbyssDust>(), 0f, 0f, 200, default, 3.7f);
                            Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity *= 3f;
                            Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                            num88 = Dust.NewDust(position, num84, height3, mod.DustType<Dusts.YamataDust>(), 0f, 0f, 100, default, 1.5f);
                            Main.dust[num88].position = npc.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                            Main.dust[num88].velocity *= 2f;
                            Main.dust[num88].noGravity = true;
                            Main.dust[num88].fadeIn = 1f;
                            Main.dust[num88].color = Color.Crimson * 0.5f;
                            Main.dust[num88].noLight = true;
                            Main.dust[num88].velocity += npc.DirectionTo(Main.dust[num88].position) * 8f;
                        }
                        for (int num89 = 0; num89 < 10; num89++)
                        {
                            npc.ai[2] = 1;
                            for (int Loop = 0; Loop < 20; Loop++)
                            {
                                int Smoke2 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 31), npc.width, npc.height, 186, 1 * Main.rand.NextFloat(-1, 1), -1, 0);
                                Main.dust[Smoke2].noGravity = true;
                                Main.dust[Smoke2].noLight = true;
                                int Smoke = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y + 31), npc.width, npc.height, 186, 1 * Main.rand.NextFloat(-1, 1), -1, 0, default, 2f);
                                Main.dust[Smoke].noGravity = true;
                                Main.dust[Smoke].noLight = true;
                            }
                        }
                    }
                }
            }
            else
            {
                npc.alpha += 15;
                if (npc.alpha > 255)
                {
                    npc.active = false;
                    npc.netUpdate = true;
                }
                if (++npc.ai[0] >= 6)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] += 1;
                    if (npc.ai[1] < 13 || npc.ai[1] > 16)
                    {
                        npc.ai[1] = 13;
                    }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/WrathHarukVanish_Glow");

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 17, npc.frame, npc.GetAlpha(dColor), false);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, 17, npc.frame, Color.White, false);
            BaseDrawing.DrawAfterimage(spritebatch, glowTex, 0, npc, 0.8f, 1f, 4, true, 0f, 0f, Color.White, npc.frame, 17);
            return false;
        }
    }
}
