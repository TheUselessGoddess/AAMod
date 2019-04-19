﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class BogPalmTree : ModPalmTree
    {
        private Mod mod
        {
            get { return ModLoader.GetMod("AAMod"); }
        }

        public override int DropWood()
        {
            return mod.ItemType("Bogwood");
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/BogPalmTree");
        }

        public override Texture2D GetTopTextures()
        {
            return mod.GetTexture("Tiles/BogPalmTreetops");
        }
    }
}