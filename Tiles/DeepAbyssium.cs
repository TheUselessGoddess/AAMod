using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class DeepAbyssium : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = false;
            Main.tileSolidTop[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 1;
            TileObjectData.newTile.CoordinateHeights = new[] {16};
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("DeepAbyssium"); //put your CustomBlock name
            AddMapEntry(new Color(0, 0, 100));
            minPick = 0;
        }
    }
}