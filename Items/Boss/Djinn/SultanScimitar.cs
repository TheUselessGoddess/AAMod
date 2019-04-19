using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Items.Boss.Djinn

{
    public class SultanScimitar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sultan's Scimitar");
        }

        public override void SetDefaults()
        {
            item.damage = 24;
            item.melee = true;
            item.width = 58;
            item.height = 66;
            item.useTime = 26;
            item.useAnimation = 26;
            item.shoot = mod.ProjectileType("DesertGust");
            item.shootSpeed = 2f;
            item.UseSound = SoundID.Item1;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.autoReuse = true;
            item.rare = 3;
        }
    }
}