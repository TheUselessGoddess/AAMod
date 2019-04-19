using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RottedDynastyWood : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[Type] = false;
            Main.tileBlockLight[Type] = false;

            drop = mod.ItemType("RottedDynastyWood"); //put your CustomBlock name
            AddMapEntry(new Color(39, 34, 8));
            minPick = 0;
        }
    }
}