using System;
using System.IO;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Achievements;
using BaseMod;
using System.Collections.Generic;
using Terraria.ID;

namespace AAMod
{
    internal class AANet
    {
        private static Dictionary<MPMessageType, Action<BinaryReader, int>> _packetActions;

        private static ModPacket GetPacket(int capacity = 256)
        {
            return AAMod.instance.GetPacket(capacity);
        }

        public static void Load()
        {
            _packetActions = new Dictionary<MPMessageType, Action<BinaryReader, int>>
            {
                { MPMessageType.RequestUpdateSquidLady, RequestUpdateSquidLadyAction }
            };
        }

        public static void Unload()
        {
            _packetActions = null;
        }

        public static void HandlePacket(BinaryReader reader, int fromWho)
        {
            MPMessageType messageType = (MPMessageType)reader.ReadByte();
            if (_packetActions != null && _packetActions.ContainsKey(messageType))
            {
                _packetActions[messageType].Invoke(reader, fromWho);
            }
        }

        private static void RequestUpdateSquidLadyAction(BinaryReader reader, int fromWho)
        {
            int whichCookX = reader.ReadInt32();
            if (whichCookX == 1)
            {
                AAWorld.squid1 = AAWorld.squid1 + 1;
            }
            else if (whichCookX == 2)
            {
                AAWorld.squid2 = AAWorld.squid2 + 1;
            }
            else if (whichCookX == 3)
            {
                AAWorld.squid3 = AAWorld.squid3 + 1;
            }
            else if (whichCookX == 4)
            {
                AAWorld.squid4 = AAWorld.squid4 + 1;
            }
            else if (whichCookX == 5)
            {
                AAWorld.squid5 = AAWorld.squid5 + 1;
            }
            else if (whichCookX == 6)
            {
                AAWorld.squid6 = AAWorld.squid6 + 1;
            }
            else if (whichCookX == 7)
            {
                AAWorld.squid7 = AAWorld.squid7 + 1;
            }
            else if (whichCookX == 8)
            {
                AAWorld.squid8 = AAWorld.squid8 + 1;
            }
            else if (whichCookX == 9)
            {
                AAWorld.squid9 = AAWorld.squid9 + 1;
            }
            else if (whichCookX == 10)
            {
                AAWorld.squid10 = AAWorld.squid10 + 1;
            }
            else if (whichCookX == 11)
            {
                AAWorld.squid11 = AAWorld.squid11 + 1;
            }
            else if (whichCookX == 12)
            {
                AAWorld.squid12 = AAWorld.squid12 + 1;
            }
            else if (whichCookX == 13)
            {
                AAWorld.squid13 = AAWorld.squid13 + 1;
            }
            else if (whichCookX == 14)
            {
                AAWorld.squid14 = AAWorld.squid14 + 1;
            }
            else if (whichCookX == 16)
            {
                AAWorld.squid15 = AAWorld.squid15 + 1;
            }
            else if (whichCookX == 17)
            {
                AAWorld.squid16 = AAWorld.squid16 + 1;
            }

            NetMessage.SendData(MessageID.WorldData);
        }
    }
}