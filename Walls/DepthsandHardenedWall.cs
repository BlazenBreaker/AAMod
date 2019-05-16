using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class DepthsandHardenedWall : ModWall
	{
		public override void SetDefaults()
		{
			dustType = mod.DustType("AbyssiumDust");
			AddMapEntry(new Color(0, 10, 150));
            Terraria.ID.WallID.Sets.Conversion.HardenedSand[Type] = true;

        }

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
        
    }
}