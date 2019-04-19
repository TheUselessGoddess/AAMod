using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DoomstoneB : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("Apocalyptite")] = true;
            Main.tileMergeDirt[Type] = true;
            SetModTree(new OroborosTree());
            soundType = 21;
            drop = mod.ItemType("DoomstoneB"); //put your CustomBlock name
            dustType = mod.DustType("DoomDust");
            AddMapEntry(new Color(40, 20, 20));
            minPick = 60;
        }

        public static bool PlaceObject(int x, int y, int type, bool mute = false, int style = 0, int alternate = 0,
            int random = -1, int direction = -1)
        {
            TileObject toBePlaced;
            if (!TileObject.CanPlace(x, y, type, style, direction, out toBePlaced, false))
            {
                return false;
            }

            toBePlaced.random = random;
            if (TileObject.Place(toBePlaced) && !mute)
            {
                WorldGen.SquareTileFrame(x, y, true);
                //   Main.PlaySound(0, x * 16, y * 16, 1, 1f, 0f);
            }

            return false;
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return mod.TileType("OroborosSapling");
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}