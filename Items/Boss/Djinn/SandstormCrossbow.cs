using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class SandstormCrossbow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstorm Crossbow");
            Tooltip.SetDefault("Replaces arrows with desert bolts");
        }

        public override void SetDefaults()
        {
            item.damage = 28;
            item.ranged = true;
            item.width = 40;
            item.height = 26;
            item.useTime = 20;
            item.reuseDelay = 24;
            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2.5f;
            item.value = 50000;
            item.rare = 3;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 8f;
            item.useAmmo = 40;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            int Shoot = Main.rand.Next(2);
            switch (Shoot)
            {
                case 0:
                    Shoot = mod.ProjectileType<Projectiles.Djinn.DesertBolt1>();
                    break;
                default:
                    Shoot = mod.ProjectileType<Projectiles.Djinn.DesertBolt2>();
                    break;
            }

            float baseSpeed = (float) Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            Projectile.NewProjectile(position.X, position.Y, speedX + 5, speedY, Shoot, damage, knockBack,
                player.whoAmI, 0f, 0f);

            return false;
        }
    }
}