using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Depthsandstone : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[this.Type] = true;
            Main.tileLighted[Type] = false;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("Depthsandstone"); //put your CustomBlock name
            AddMapEntry(new Color(0, 20, 127));
            minPick = 65;
        }
    }
}