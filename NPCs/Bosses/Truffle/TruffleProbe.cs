using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ReLogic.Utilities;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Truffle
{
    public class TruffleProbe : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Probe");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 14;
            npc.height = 14;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 0;
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.defense = 0;
            npc.damage = 20;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = null;
            npc.knockBackResist = 0f;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target]; // makes it so you can reference the player the npc is targetting

            BaseAI.AIFloater(npc, ref npc.ai, false, 0.2f, 2f, 1.5f, 0.04f, 1.5f, 3);

            npc.frameCounter++;
            if (npc.frameCounter > 8)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 20;
                if (npc.frame.Y > 60)
                {
                    npc.frame.Y = 0;
                }
            }
        }
    }
}


