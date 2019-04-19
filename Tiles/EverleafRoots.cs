using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class EverleafRoots : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            Main.tileMerge[Type][TileID.Mud] = true;
            Main.tileSpelunker[Type] = true;
            Main.tileBlockLight[Type] = true;
            Main.tileLighted[Type] = true;
            soundType = 21;
            dustType = mod.DustType("EverleafDust");
            drop = mod.ItemType("Everleaf");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Everleaf Root");
            AddMapEntry(new Color(10, 80, 15), name);
            minPick = 225;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) //light colors
        {
            r = 0;
            g = .30f;
            b = 0f;
        }
    }
}