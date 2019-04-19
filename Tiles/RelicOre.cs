using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RelicOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = false;
            drop = mod.ItemType("VikingRelic"); //put your CustomBlock name
            AddMapEntry(new Color(58, 68, 102));
            minPick = 65;
        }
    }
}