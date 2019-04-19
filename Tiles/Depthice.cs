using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthice : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileMerge[TileID.SnowBlock][Type] = true;
            soundType = 21;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthice"); //put your CustomBlock name
            AddMapEntry(new Color(0, 60, 127));
        }
    }
}