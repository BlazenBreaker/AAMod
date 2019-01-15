using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.Yamata.Awakened;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class Hydra : YamataBoss
	{
        public NPC Head1;
        public NPC Head2;
        public NPC Head3;
        public bool HeadsSpawned = false;

        public override void SetStaticDefaults()
        {
            displayName = "Hydra";
            Main.npcFrameCount[npc.type] = 15;
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 96;
            animationType = NPCID.HellArmoredBonesSword;
            npc.height = 78;
            npc.aiStyle = -1;
            npc.damage = 40;
            npc.defense = 10;
            npc.lifeMax = 4000;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/HydraTheme");
            npc.noGravity = false;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            npc.frame = BaseDrawing.GetFrame(frameCount, frameWidth, frameHeight, 0, 2);
            frameBottom = BaseDrawing.GetFrame(frameCount, frameWidth, 44, 0, 2);
            bossBag = mod.ItemType("HydraBag");
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }

        public override void NPCLoot()
        {
            AAWorld.downedHydra = true;
            //npc.DropLoot(Items.Boss.Hydra.HydraTrophy.type, 1f / 10);

            if (!Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("HydraHide"), 30, 50);
                npc.DropLoot(mod.ItemType("Abyssium"), 40, 90);
                //npc.DropLoot(Items.Vanity.Mask.HydraMask.type, 1f / 7);
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            npc.value = 0f;
            npc.boss = false;

        }

        public Rectangle frameBottom = new Rectangle(0, 0, 1, 1);
        public Player playerTarget = null;
		public bool chasePlayer = false;

        //clientside stuff
        public Vector2 bottomVisualOffset = default(Vector2);

        public override void AI()
        {
            if (!HeadsSpawned)
            {
                if (Main.netMode != 1)
                {
                    int latestNPC = npc.whoAmI;
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 60, mod.NPCType("HydraHead1"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Head1 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 60, mod.NPCType("HydraHead2"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Head2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y - 60, mod.NPCType("HydraHead3"), 0, npc.whoAmI);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[0] = npc.whoAmI;
                    Head3 = Main.npc[latestNPC];
                }
                HeadsSpawned = true;
            }
			if(playerTarget != null)
			{
				float dist = npc.Distance(playerTarget.Center);
				if (dist > 1000)
				{
					if(!chasePlayer) npc.netUpdate = true;		
					chasePlayer = true;
					npc.noTileCollide = true;
				}
				else
				{
					if(chasePlayer) npc.netUpdate = true;		
					chasePlayer = false;
					npc.noTileCollide = false;
				}
			}
            for (int m = npc.oldPos.Length - 1; m > 0; m--)
            {
                npc.oldPos[m] = npc.oldPos[m - 1];
            }
            npc.oldPos[0] = npc.position;

            bool foundTarget = TargetClosest();
            if (foundTarget)
            {
                int tileY = BaseWorldGen.GetFirstTileFloor((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f));
                npc.timeLeft = 300;
                float playerDistance = Vector2.Distance(playerTarget.Center, npc.Center);
                if (Math.Abs(npc.velocity.X) > 12f) npc.velocity.X *= 0.8f;
                if (Math.Abs(npc.velocity.Y) > 12f) npc.velocity.Y *= 0.8f;
                if (npc.velocity.Y > 7f) npc.velocity.Y *= 0.75f;
                AIMovementNormal();
            }else
            {
                AIMovementRunAway();
            }
            bottomVisualOffset = new Vector2(Math.Min(3f, Math.Abs(npc.velocity.X)), 30f) * (npc.velocity.X < 0 ? 1 : -1);
        }

        public void AIMovementRunAway()
        {
            npc.alpha += 10;

			npc.noTileCollide = false;
            npc.rotation = 0f;
            if ((npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) || npc.alpha >= 255) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; } //if out of map, kill boss
        }

        public void AIMovementNormal(float movementScalar = 1f, float playerDistance = -1f)
        {
            BaseAI.AIZombie(npc, ref npc.ai, false, false, -1, 0.07f, 1f, 14, 20, 1, true, 1, 1, true, null, false);
            npc.rotation = 0f;
        }

        public bool TargetClosest()
        {
            int[] players = BaseAI.GetPlayers(npc.Center, 4200f);
            float dist = 200;
            int foundPlayer = -1;
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            else
            {
                for (int m = 0; m < players.Length; m++)
                {
                    Player p = Main.player[players[m]];
                    if (Vector2.Distance(p.Center, npc.Center) < dist)
                    {
                        dist = Vector2.Distance(p.Center, npc.Center);
                        foundPlayer = p.whoAmI;
                    }
                }
            }
            if (foundPlayer != -1)
            {
                BaseAI.SetTarget(npc, foundPlayer);
                playerTarget = Main.player[foundPlayer];
                return true;
            }
            return false;
        }

        
        public void DrawHead(SpriteBatch spriteBatch, string headTexture, string glowMaskTexture, NPC head, Color drawColor)
        {
            if (head != null && head.active)
            {
                string neckTex = ("NPCs/Bosses/Hydra/HydraNeck");
                Texture2D neckTex2D = mod.GetTexture(neckTex);
                Vector2 neckOrigin = new Vector2(npc.Center.X, npc.Center.Y - 30);
                Vector2 connector = head.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { null, neckTex2D, null }, 0, neckOrigin, connector, neckTex2D.Height - 10f, null, 1f, false, null);
                spriteBatch.Draw(mod.GetTexture(headTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, drawColor, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(mod.GetTexture(glowMaskTexture), new Vector2(head.Center.X - Main.screenPosition.X, head.Center.Y - Main.screenPosition.Y), head.frame, Color.White, head.rotation, new Vector2(36 * 0.5f, 32 * 0.5f), 1f, SpriteEffects.None, 0f);
                
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color dColor)
        {
            DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead2", "NPCs/Bosses/Hydra/HydraHead2_Glow", Head2, dColor);
            DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead3", "NPCs/Bosses/Hydra/HydraHead3_Glow", Head3, dColor);
            string tailTex = ("NPCs/Bosses/Hydra/HydraTail");
            BaseDrawing.DrawTexture(sb, mod.GetTexture(tailTex), 0, npc.position + new Vector2(0f, npc.gfxOffY) + bottomVisualOffset, npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], frameBottom, dColor, false);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], 0, npc.position + new Vector2(0f, npc.gfxOffY), npc.width, npc.height, npc.scale, npc.rotation, npc.spriteDirection, Main.npcFrameCount[npc.type], npc.frame, dColor, false);
            DrawHead(sb, "NPCs/Bosses/Hydra/HydraHead1", "NPCs/Bosses/Hydra/HydraHead1_Glow", Head1, dColor);
            return false;
        }		
    }
}