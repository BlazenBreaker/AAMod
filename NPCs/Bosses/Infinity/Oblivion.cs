using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Infinity
{
    public class Oblivion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oblivion");
            Main.npcFrameCount[npc.type] = 14;
        }
        public override void SetDefaults()
        {
            npc.width = 1;
            npc.height = 1;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/IZDeath");
        }

        public int OblivionSpeech = 0;

        public override void AI()
        {
            Color color1 = Color.DarkRed;
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            Player player = Main.player[Main.myPlayer];
            OblivionSpeech++;
            if (AAPlayer.ZeroKills == 1)
            {
                if (OblivionSpeech == 180)
                {
                    Main.NewText("...impressive.", color1);
                }
                if (OblivionSpeech == 360)
                {
                    Main.NewText("Defeating my mechanical body...", color1);
                }
                if (OblivionSpeech == 540)
                {
                    Main.NewText("...Is not a small feat...", color1);
                }
                if (OblivionSpeech == 720)
                {
                    Main.NewText("I applaud you, Terrarian.", color1);
                }
                if (player.difficulty == 2)
                {
                    if (OblivionSpeech == 900)
                    {
                        Main.NewText("I'm also impressed you made it this far in hardcore mode.", color1);
                    }
                    if (OblivionSpeech == 1080)
                    {
                        Main.NewText("Good job. Here, have a sticker.", color1);
                        Item.NewItem(npc.Center, mod.ItemType("Sticker"));
                    }
                    if (OblivionSpeech == 1260)
                    {
                        Main.NewText("Now, I bid you adieu...", color1);
                    }
                }
                else
                {
                    if (OblivionSpeech == 900)
                    {
                        Main.NewText("Although...next time we meet...when you're stronger...", color1);
                    }
                    if (OblivionSpeech == 1080)
                    {
                        Main.NewText("..." + player.name + "...", color1);
                    }
                    if (OblivionSpeech == 1260)
                    {
                        Main.NewText("I won't be so forgiving.", color1);
                    }
                }
                if (OblivionSpeech >= 1420)
                {
                    npc.alpha += 5;
                }
            }

            if (AAPlayer.ZeroKills == 2)
            {
                if (OblivionSpeech == 180)
                {
                    Main.NewText("Hmpf...", color1);
                }
                if (OblivionSpeech == 360)
                {
                    Main.NewText("...breaking it once wasn�t enough for you?", color1);
                }
                if (OblivionSpeech == 540)
                {
                    Main.NewText("I assume you�re doing it for the drops...", color1);
                }
                if (OblivionSpeech == 720)
                {
                    Main.NewText("...bragging rights with your friends...perhaps?", color1);
                }
                if (OblivionSpeech == 900)
                {
                    Main.NewText("Or maybe you get a sick kick out of breaking other people�s property.", color1);
                }
                if (OblivionSpeech == 1080)
                {
                    Main.NewText("Who am I to judge, though?", color1);
                }
                if (OblivionSpeech >= 1080)
                {
                    npc.alpha += 5;
                }

            }

            if (AAPlayer.ZeroKills == 3)
            {
                if (OblivionSpeech == 180)
                {
                    Main.NewText("Bravo. You scrapped my body again.", color1);
                }
                if (OblivionSpeech == 360)
                {
                    Main.NewText("What do you want? A sticker?", color1);
                }
                if (OblivionSpeech == 540)
                {
                    Main.NewText("A gold star?", color1);
                }
                if (OblivionSpeech == 720)
                {
                    Main.NewText("I can just remake it again instantly, so I don�t get why you keep going after me.", color1);
                }
                if (OblivionSpeech >= 720)
                {
                    npc.alpha += 5;
                }
            }

            if (AAPlayer.ZeroKills == 4)
            {
                if (OblivionSpeech == 180)
                {
                    Main.NewText("Sigh...you are a persistent one, aren�t you?", color1);
                }
                if (OblivionSpeech == 360)
                {
                    Main.NewText("4 times? You must be some kind of masochist or something.", color1);
                }
                if (OblivionSpeech == 540)
                {
                    Main.NewText("You know, I�m not the aggressor here", color1);
                }
                if (OblivionSpeech == 720)
                {
                    Main.NewText("You keep summoning me.", color1);
                }
                if (OblivionSpeech == 900)
                {
                    Main.NewText("I only showed up because you trashed my Zero unit.", color1);
                }
                if (OblivionSpeech == 1080)
                {
                    Main.NewText("The void wasn�t hurting you. It�s just there.", color1);
                }
                if (OblivionSpeech == 1260)
                {
                    Main.NewText("You�re the one who went in to kill my Zero unit", color1);
                }
                if (OblivionSpeech == 1440)
                {
                    Main.NewText("Whatever, just take your boss drops and go.", color1);
                }
                if (OblivionSpeech >= 1440)
                {
                    npc.alpha += 5;
                }
            }
            if (AAPlayer.ZeroKills == 10)
            {
                if (player.difficulty != 2)
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was destroyed by Oblivion"), player.statLifeMax + 10, 0, false);
                    if (OblivionSpeech == 180)
                    {
                        Main.NewText("That's what you get for bothering me 10 TIMES.", color1);
                    }
                    if (OblivionSpeech == 360)
                    {
                        Main.NewText("...but I doubt you'll go away.", color1);
                    }
                }
                else
                {
                    if (OblivionSpeech == 180)
                    {
                        Main.NewText("I would kill you to teach you a lesson...", color1);
                    }
                    if (OblivionSpeech == 360)
                    {
                        Main.NewText("...but you're in hardcore mode, and I'm not that much of an asshole.", color1);
                    }
                }
                if (OblivionSpeech == 540)
                {
                    Main.NewText("Oh well.", color1);
                }

                if (OblivionSpeech >= 540)
                {
                    npc.alpha += 5;
                }
            }

            else if (AAPlayer.ZeroKills >= 5)
            {
                if (OblivionSpeech == 180)
                {
                    if (Main.netMode != 2 && Main.netMode != 1) Main.NewText(AAPlayer.ZeroKills + " kills...congratulations. You have no life", color1);
                }
                if (OblivionSpeech == 300)
                {
                    int rand = Main.rand.Next(7);
                    if (rand == 0)
                    {
                        Main.NewText("Go outside and kick a ball or something.", color1);
                    }
                    else if (rand == 1)
                    {
                        Main.NewText("Isn't there some schoolwork you have to do or an application you need to fill out or something?", color1);
                    }
                    else if (rand == 2)
                    {
                        Main.NewText("Don't you have other games on Steam you could be playing right now?", color1);
                    }
                    else if (rand == 3)
                    {
                        Main.NewText("Now leave me alone, I have better things to do.", color1);
                    }
                    else if (rand == 4)
                    {
                        Main.NewText("Fighting you gets really boring you know. You use the same tactics every time.", color1);
                    }
                    else if (rand == 5)
                    {
                        Main.NewText("Whatever, I'll be seeing you soon...again...assuming you're still a persistent wretch.", color1);
                    }
                    else if (rand == 6)
                    {
                        ModCheck:
                        if (AAMod.calamityLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Go fight Supreme Calamitas or something. I'm sure she'll occupy your time.", color1);
                        }
                        else if (AAMod.thoriumLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("You know Ragnarok is a thing right? World-ending trio? They should be fun to fight. Now go away.", color1);
                        }
                        else if (AAMod.spiritLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Why don't you go frolic in the spirit biome. I'm sure one of the creatures there would love a big ol' hug.", color1);
                        }
                        else if (AAMod.fargoLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Hey, why not go bug the mutant. If you like killing bosses so much, he should be able to fix you right up.", color1);
                        }
                        else if (AAMod.redemptionLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("If you have such a hardon for killing robots, the Vlitch are a thing, you know.", color1);
                        }
                        else if (AAMod.tremorLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Wait you're playing Tremor? HAHAHAHAHAHAH!", color1);
                        }
                        else if (AAMod.sacredToolsLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Go bug the Lunarians or something. I'm sure they'll be more fun to fight than I am.", color1);
                        }
                        else if (AAMod.grealmLoaded && Main.rand.Next(MajorModCount()) == 0)
                        {
                            Main.NewText("Why don't you go fight the Horde for the 50th goddamn time. Maybe they have new drops or something since you last checked.", color1);
                        }
                        else if (MajorModCount() == 0)
                        {
                            Main.NewText("Go install another mod or something. There are plenty on the mod browser to choose from.", color1);
                        }
                        else
                        {
                            goto ModCheck;
                        }
                    }
                }
                if (OblivionSpeech >= 300)
                {
                    npc.alpha += 5;
                }
            }
            if (npc.alpha >= 255)
            {
                npc.active = false;
            }
        }

        public int MajorModCount()
        {
            int ModCount = 0;

            if (AAMod.calamityLoaded)
            {
                ModCount++;
            }
            if (AAMod.thoriumLoaded)
            {
                ModCount++;
            }
            if (AAMod.spiritLoaded)
            {
                ModCount++;
            }
            if (AAMod.fargoLoaded)
            {
                ModCount++;
            }
            if (AAMod.redemptionLoaded)
            {
                ModCount++;
            }
            if (AAMod.tremorLoaded)
            {
                ModCount++;
            }
            if (AAMod.sacredToolsLoaded)
            {
                ModCount++;
            }
            if (AAMod.grealmLoaded)
            {
                ModCount++;
            }

            return ModCount;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            if (npc.frameCounter < 5)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 7 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 0 * frameHeight;
                }
            }
            else if (npc.frameCounter < 10)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 8 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 1 * frameHeight;
                }
            }
            else if (npc.frameCounter < 15)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 9 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 2 * frameHeight;
                }
            }
            else if (npc.frameCounter < 20)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 10 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
            else if (npc.frameCounter < 25)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 11 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 4 * frameHeight;
                }
            }
            else if (npc.frameCounter < 30)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 12 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 5 * frameHeight;
                }
            }
            else if (npc.frameCounter < 35)
            {
                if (Main.rand.Next(9) == 0)
                {
                    npc.frame.Y = 13 * frameHeight;
                }
                else
                {
                    npc.frame.Y = 6 * frameHeight;
                }
            }
            else
            {
                npc.frameCounter = 0;
            }
        }

        public static Texture2D glowTex = null;
        public static Texture2D glitchTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("NPCs/Bosses/Infinity/Oblivion_Glow");
            }
            if (glitchTex == null)
            {
                glitchTex = mod.GetTexture("NPCs/Bosses/Infinity/OblivionGlitch");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(npc, npc.Center + new Vector2(0, -30), true, 0f), dColor));
            BaseDrawing.DrawAura(sb, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, Color.White);
            BaseDrawing.DrawTexture(sb, glowTex, 0, npc, Color.White);
            BaseDrawing.DrawAura(sb, glitchTex, 0, npc, auraPercent, 1f, 0f, 0f, AAColor.Oblivion);
            BaseDrawing.DrawTexture(sb, glitchTex, 0, npc, AAColor.Oblivion);

            return false;
        }
    }
}