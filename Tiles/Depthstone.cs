using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMerge[Type][mod.TileType("AbyssiumOre")] = true;
            Main.tileMergeDirt[Type] = true;
            Main.tileBlendAll[this.Type] = false;
            Main.tileMerge[TileID.Mud][Type] = true;
            Main.tileLighted[Type] = false;
            soundType = 21;
            TileID.Sets.JungleSpecial[Type] = true;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthstone"); //put your CustomBlock name
            AddMapEntry(new Color(27, 19, 50));
            minPick = 65;
        }
    }
}