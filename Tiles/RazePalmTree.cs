﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    class RazePalmTree : ModPalmTree
    {
        private Mod mod
        {
            get { return ModLoader.GetMod("AAMod"); }
        }

        public override int DropWood()
        {
            return mod.ItemType("Razewood");
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/RazePalmTree");
        }

        public override Texture2D GetTopTextures()
        {
            return mod.GetTexture("Tiles/RazePalmTreetops");
        }
    }
}