using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class InfernoFoliage1 : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            TileObjectData.addTile(Type);
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = true;
            //Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16
            };
            TileObjectData.addTile(Type);
            dustType = mod.DustType<Dusts.InfernoDust>();
            soundType = 6;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = 10;
        }
    }
}