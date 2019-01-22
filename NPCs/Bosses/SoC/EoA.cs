using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    public class EoA : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye of Azathoth");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.defense = 40;
            npc.damage = 90;
            npc.lifeMax = 3000;
            npc.aiStyle = 81;
            npc.width = 60;
            npc.height = 60;
            npc.value = 0f;
            npc.knockBackResist = 0f;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.npcSlots = 0f;
            npc.noGravity = true;
            npc.dontTakeDamage = false;
            npc.noTileCollide = true;
            npc.netAlways = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {

            if (Main.rand.Next(420) == 0)
            {
                Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, Main.rand.Next(100, 101), 1f, 0f);
            }
            Vector2 value31 = new Vector2(30f);
            float num1194 = 0f;
            float num1195 = npc.ai[0];
            npc.ai[1] += 1f;
            int num1196 = 0;
            int num1197 = 0;
            while (num1196 < 10)
            {
                num1194 = (float)NPC.MoonLordAttacksArray2[1, num1196];
                if (num1194 + (float)num1197 > npc.ai[1])
                {
                    break;
                }
                num1197 += (int)num1194;
                num1196++;
            }
            if (num1196 == 10)
            {
                num1196 = 0;
                npc.ai[1] = 0f;
                num1194 = (float)NPC.MoonLordAttacksArray2[1, num1196];
                num1197 = 0;
            }
            npc.ai[0] = (float)NPC.MoonLordAttacksArray2[0, num1196];
            float num1198 = (float)((int)npc.ai[1] - num1197);
            if (npc.ai[0] != num1195)
            {
                npc.netUpdate = true;
            }
            if (npc.ai[0] == -1f)
            {
                npc.ai[1] += 1f;
                if (npc.ai[1] > 180f)
                {
                    npc.ai[1] = 0f;
                }
                float value32;
                if (npc.ai[1] < 60f)
                {
                    value32 = 0.75f;
                    npc.localAI[0] = 0f;
                    npc.localAI[1] = (float)Math.Sin((double)(npc.ai[1] * 6.28318548f / 15f)) * 0.35f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[0] = 3.14159274f;
                    }
                }
                else if (npc.ai[1] < 120f)
                {
                    value32 = 1f;
                    if (npc.localAI[1] < 0.5f)
                    {
                        npc.localAI[1] += 0.025f;
                    }
                    npc.localAI[0] += 0.209439516f;
                }
                else
                {
                    value32 = 1.15f;
                    npc.localAI[1] -= 0.05f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[1] = 0f;
                    }
                }
                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], value32, 0.3f);
            }
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(false);
                Vector2 v8 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
                npc.localAI[0] = npc.localAI[0].AngleLerp(v8.ToRotation(), 0.5f);
                npc.localAI[1] += 0.05f;
                if (npc.localAI[1] > 0.7f)
                {
                    npc.localAI[1] = 0.7f;
                }
                npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                float scaleFactor9 = 24f;
                Vector2 center23 = npc.Center;
                Vector2 center24 = Main.player[npc.target].Center;
                Vector2 value33 = center24 - center23;
                Vector2 vector187 = value33 - Vector2.UnitY * 200f;
                vector187 = Vector2.Normalize(vector187) * scaleFactor9;
                int num1199 = 30;
                npc.velocity.X = (npc.velocity.X * (float)(num1199 - 1) + vector187.X) / (float)num1199;
                npc.velocity.Y = (npc.velocity.Y * (float)(num1199 - 1) + vector187.Y) / (float)num1199;
                float num1200 = 0.25f;
                for (int num1201 = 0; num1201 < 200; num1201++)
                {
                    if (num1201 != npc.whoAmI && Main.npc[num1201].active && Main.npc[num1201].type == mod.NPCType<EoA>() && Vector2.Distance(npc.Center, Main.npc[num1201].Center) < 150f)
                    {
                        if (npc.position.X < Main.npc[num1201].position.X)
                        {
                            npc.velocity.X = npc.velocity.X - num1200;
                        }
                        else
                        {
                            npc.velocity.X = npc.velocity.X + num1200;
                        }
                        if (npc.position.Y < Main.npc[num1201].position.Y)
                        {
                            npc.velocity.Y = npc.velocity.Y - num1200;
                        }
                        else
                        {
                            npc.velocity.Y = npc.velocity.Y + num1200;
                        }
                    }
                }
                return;
            }
            if (npc.ai[0] == 1f)
            {
                if (num1198 == 0f)
                {
                    npc.TargetClosest(false);
                    npc.netUpdate = true;
                }
                npc.velocity *= 0.95f;
                if (npc.velocity.Length() < 1f)
                {
                    npc.velocity = Vector2.Zero;
                }
                Vector2 v9 = Main.player[npc.target].Center + Main.player[npc.target].velocity * 20f - npc.Center;
                npc.localAI[0] = npc.localAI[0].AngleLerp(v9.ToRotation(), 0.5f);
                npc.localAI[1] += 0.05f;
                if (npc.localAI[1] > 1f)
                {
                    npc.localAI[1] = 1f;
                }
                if (num1198 < 20f)
                {
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1.1f, 0.2f);
                }
                else
                {
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                }
                if (num1198 == num1194 - 35f)
                {
                    Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 6, 1f, 0f);
                }
                if ((num1198 == num1194 - 14f || num1198 == num1194 - 7f || num1198 == num1194) && Main.netMode != 1)
                {
                    Vector2 vector188 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value31 * npc.localAI[1]);
                    Vector2 vector189 = Vector2.Normalize(v9) * 8f;
                    Projectile.NewProjectile(npc.Center.X + vector188.X, npc.Center.Y + vector188.Y, vector189.X, vector189.Y, 462, 35, 0f, Main.myPlayer, 0f, 0f);
                    return;
                }
            }
            else if (npc.ai[0] == 2f)
            {
                if (num1198 < 15f)
                {
                    npc.localAI[1] -= 0.07f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[1] = 0f;
                    }
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                    npc.velocity *= 0.8f;
                    if (npc.velocity.Length() < 1f)
                    {
                        npc.velocity = Vector2.Zero;
                        return;
                    }
                }
                else if (num1198 < 75f)
                {
                    float num1202 = (num1198 - 15f) / 10f;
                    int num1203 = 0;
                    int num1204 = 0;
                    switch ((int)num1202)
                    {
                        case 0:
                            num1203 = 0;
                            num1204 = 2;
                            break;
                        case 1:
                            num1203 = 2;
                            num1204 = 5;
                            break;
                        case 2:
                            num1203 = 5;
                            num1204 = 3;
                            break;
                        case 3:
                            num1203 = 3;
                            num1204 = 1;
                            break;
                        case 4:
                            num1203 = 1;
                            num1204 = 4;
                            break;
                        case 5:
                            num1203 = 4;
                            num1204 = 0;
                            break;
                    }
                    Vector2 spinningpoint8 = Vector2.UnitY * -30f;
                    Vector2 value34 = spinningpoint8.RotatedBy((double)((float)num1203 * 6.28318548f / 6f), default(Vector2));
                    Vector2 value35 = spinningpoint8.RotatedBy((double)((float)num1204 * 6.28318548f / 6f), default(Vector2));
                    Vector2 vector190 = Vector2.Lerp(value34, value35, num1202 - (float)((int)num1202));
                    float value36 = vector190.Length() / 30f;
                    npc.localAI[0] = vector190.ToRotation();
                    npc.localAI[1] = MathHelper.Lerp(npc.localAI[1], value36, 0.5f);
                    for (int num1205 = 0; num1205 < 2; num1205++)
                    {
                        int num1206 = Dust.NewDust(npc.Center + vector190 - Vector2.One * 4f, 0, 0, 229, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num1206].velocity += vector190 / 15f;
                        Main.dust[num1206].noGravity = true;
                    }
                    if ((num1198 - 15f) % 10f == 0f && Main.netMode != 1)
                    {
                        Vector2 vec4 = Vector2.Normalize(vector190);
                        if (vec4.HasNaNs())
                        {
                            vec4 = Vector2.UnitY * -1f;
                        }
                        vec4 *= 4f;
                        Projectile.NewProjectile(npc.Center.X + vector190.X, npc.Center.Y + vector190.Y, vec4.X, vec4.Y, 454, 55, 0f, Main.myPlayer, 30f, (float)npc.whoAmI);
                        return;
                    }
                }
                else
                {
                    if (num1198 < 105f)
                    {
                        npc.localAI[0] = npc.localAI[0].AngleLerp(npc.ai[2] - 1.57079637f, 0.2f);
                        npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.75f, 0.2f);
                        if (num1198 == 75f)
                        {
                            npc.TargetClosest(false);
                            npc.netUpdate = true;
                            npc.velocity = Vector2.UnitY * -7f;
                            for (int num1207 = 0; num1207 < 1000; num1207++)
                            {
                                Projectile projectile7 = Main.projectile[num1207];
                                if (projectile7.active && projectile7.type == 454 && projectile7.ai[1] == (float)npc.whoAmI && projectile7.ai[0] != -1f)
                                {
                                    projectile7.velocity += npc.velocity;
                                    projectile7.netUpdate = true;
                                }
                            }
                        }
                        npc.velocity.Y = npc.velocity.Y * 0.96f;
                        npc.ai[2] = (Main.player[npc.target].Center - npc.Center).ToRotation() + 1.57079637f;
                        npc.rotation = npc.rotation.AngleTowards(npc.ai[2], 0.104719758f);
                        return;
                    }
                    if (num1198 < 120f)
                    {
                        Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 102, 1f, 0f);
                        if (num1198 == 105f)
                        {
                            npc.netUpdate = true;
                        }
                        Vector2 velocity8 = (npc.ai[2] - 1.57079637f).ToRotationVector2() * 12f;
                        npc.velocity = velocity8 * 2f;
                        for (int num1208 = 0; num1208 < 1000; num1208++)
                        {
                            Projectile projectile8 = Main.projectile[num1208];
                            if (projectile8.active && projectile8.type == 454 && projectile8.ai[1] == (float)npc.whoAmI && projectile8.ai[0] != -1f)
                            {
                                projectile8.ai[0] = -1f;
                                projectile8.velocity = velocity8;
                                projectile8.netUpdate = true;
                            }
                        }
                        return;
                    }
                    npc.velocity *= 0.92f;
                    npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
                    return;
                }
            }
            else if (npc.ai[0] == 3f)
            {
                if (num1198 < 15f)
                {
                    npc.localAI[1] -= 0.07f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[1] = 0f;
                    }
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 0.4f, 0.2f);
                    npc.velocity *= 0.9f;
                    if (npc.velocity.Length() < 1f)
                    {
                        npc.velocity = Vector2.Zero;
                        return;
                    }
                }
                else if (num1198 < 45f)
                {
                    npc.localAI[0] = 0f;
                    npc.localAI[1] = (float)Math.Sin((double)((num1198 - 15f) * 6.28318548f / 15f)) * 0.5f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[0] = 3.14159274f;
                        return;
                    }
                }
                else
                {
                    if (num1198 >= 185f)
                    {
                        npc.velocity *= 0.88f;
                        npc.rotation = npc.rotation.AngleLerp(0f, 0.2f);
                        npc.localAI[1] -= 0.07f;
                        if (npc.localAI[1] < 0f)
                        {
                            npc.localAI[1] = 0f;
                        }
                        npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                        return;
                    }
                    if (num1198 == 45f)
                    {
                        npc.ai[2] = (float)(Main.rand.Next(2) == 0).ToDirectionInt() * 6.28318548f / 40f;
                        npc.netUpdate = true;
                    }
                    if ((num1198 - 15f - 30f) % 40f == 0f)
                    {
                        npc.ai[2] *= 0.95f;
                    }
                    npc.localAI[0] += npc.ai[2];
                    npc.localAI[1] += 0.05f;
                    if (npc.localAI[1] > 1f)
                    {
                        npc.localAI[1] = 1f;
                    }
                    Vector2 vector191 = npc.localAI[0].ToRotationVector2() * value31 * npc.localAI[1];
                    float scaleFactor10 = MathHelper.Lerp(8f, 20f, (num1198 - 15f - 30f) / 140f);
                    npc.velocity = Vector2.Normalize(vector191) * scaleFactor10;
                    npc.rotation = npc.rotation.AngleLerp(npc.velocity.ToRotation() + 1.57079637f, 0.2f);
                    if ((num1198 - 15f - 30f) % 10f == 0f && Main.netMode != 1)
                    {
                        Vector2 vector192 = npc.Center + Vector2.Normalize(vector191) * value31.Length() * 0.4f;
                        Vector2 vector193 = Vector2.Normalize(vector191) * 8f;
                        float ai3 = (6.28318548f * (float)Main.rand.NextDouble() - 3.14159274f) / 30f + 0.0174532924f * npc.ai[2];
                        Projectile.NewProjectile(vector192.X, vector192.Y, vector193.X, vector193.Y, 452, 35, 0f, Main.myPlayer, 0f, ai3);
                        return;
                    }
                }
            }
            else if (npc.ai[0] == 4f)
            {
                if (num1198 == 0f)
                {
                    npc.TargetClosest(false);
                    npc.netUpdate = true;
                }
                if (num1198 < 180f)
                {
                    npc.localAI[2] = MathHelper.Lerp(npc.localAI[2], 1f, 0.2f);
                    npc.localAI[1] -= 0.05f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[1] = 0f;
                    }
                    npc.velocity *= 0.95f;
                    if (npc.velocity.Length() < 1f)
                    {
                        npc.velocity = Vector2.Zero;
                    }
                    if (num1198 >= 60f)
                    {
                        Vector2 center25 = npc.Center;
                        int num1209 = 0;
                        if (num1198 >= 120f)
                        {
                            num1209 = 1;
                        }
                        for (int num1210 = 0; num1210 < 1 + num1209; num1210++)
                        {
                            int num1211 = 229;
                            float num1212 = 0.8f;
                            if (num1210 % 2 == 1)
                            {
                                num1211 = 229;
                                num1212 = 1.65f;
                            }
                            Vector2 vector194 = center25 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * value31 / 2f;
                            int num1213 = Dust.NewDust(vector194 - Vector2.One * 8f, 16, 16, num1211, npc.velocity.X / 2f, npc.velocity.Y / 2f, 0, default(Color), 1f);
                            Main.dust[num1213].velocity = Vector2.Normalize(center25 - vector194) * 3.5f * (10f - (float)num1209 * 2f) / 10f;
                            Main.dust[num1213].noGravity = true;
                            Main.dust[num1213].scale = num1212;
                            Main.dust[num1213].customData = this;
                        }
                        return;
                    }
                }
                else
                {
                    if (num1198 < num1194 - 15f)
                    {
                        if (num1198 == 180f && Main.netMode != 1)
                        {
                            npc.TargetClosest(false);
                            Vector2 vector195 = Main.player[npc.target].Center - npc.Center;
                            vector195.Normalize();
                            float num1214 = -1f;
                            if (vector195.X < 0f)
                            {
                                num1214 = 1f;
                            }
                            vector195 = vector195.RotatedBy((double)(-(double)num1214 * 6.28318548f / 6f), default(Vector2));
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, vector195.X, vector195.Y, 455, 50, 0f, Main.myPlayer, num1214 * 6.28318548f / 540f, (float)npc.whoAmI);
                            npc.ai[2] = (vector195.ToRotation() + 9.424778f) * num1214;
                            npc.netUpdate = true;
                        }
                        npc.localAI[1] += 0.05f;
                        if (npc.localAI[1] > 1f)
                        {
                            npc.localAI[1] = 1f;
                        }
                        float num1215 = (float)(npc.ai[2] >= 0f).ToDirectionInt();
                        float num1216 = npc.ai[2];
                        if (num1216 < 0f)
                        {
                            num1216 *= -1f;
                        }
                        num1216 += -9.424778f;
                        num1216 += num1215 * 6.28318548f / 540f;
                        npc.localAI[0] = num1216;
                        npc.ai[2] = (num1216 + 9.424778f) * num1215;
                        return;
                    }
                    npc.localAI[1] -= 0.07f;
                    if (npc.localAI[1] < 0f)
                    {
                        npc.localAI[1] = 0f;
                        return;
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            for (int num468 = 0; num468 < 3; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, mod.DustType<Dusts.CthulhuDust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), -npc.velocity.X * 0.2f,
                    -npc.velocity.Y * 0.2f, 100, default(Color));
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture8 = Main.npcTexture[npc.type];
            Texture2D texture2D30 = Main.extraTexture[19];
            Vector2 origin15 = new Vector2(40f, 40f);
            Vector2 value33 = new Vector2(30f, 30f);
            Vector2 arg_A019_0 = npc.Center;
            Point point4 = npc.Center.ToTileCoordinates();
            Color alpha11 = npc.GetAlpha(Color.Lerp(Lighting.GetColor(point4.X, point4.Y), Color.White, 0.3f));
            Main.spriteBatch.Draw(texture8, npc.Center - Main.screenPosition, new Rectangle?(npc.frame), alpha11, npc.rotation, origin15, 1f, SpriteEffects.None, 0f);
            Vector2 value34 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value33 * npc.localAI[1]);
            Main.spriteBatch.Draw(texture2D30, npc.Center - Main.screenPosition + value34, null, alpha11, npc.rotation, texture2D30.Size() / 2f, npc.localAI[2], SpriteEffects.None, 0f);
            return false;
        }
    }
}