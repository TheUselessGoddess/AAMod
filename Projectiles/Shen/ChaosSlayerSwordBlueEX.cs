﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Projectiles.Shen
{
    public class ChaosSlayerSwordBlueEX : ChaosSlayerSwordEX
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blade of Blazing Fury");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            swordType = 2;
            offsetLeft = true;
        }
    }
}