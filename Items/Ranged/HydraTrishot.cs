using Terraria;
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class HydraTrishot : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 10;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("HydraShot");
            item.useAmmo = AmmoID.Bullet;
            item.knockBack = 0;
            item.value = 2000;
            item.rare = 2;
            item.UseSound = SoundID.Item11;
            item.shootSpeed = 12f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra Trishot");
            Tooltip.SetDefault("");
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX,
            ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float spread = 45f * 0.0174f;
            float baseSpeed = (float) Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 3; i++)
            {
                offsetAngle = startAngle + (deltaAngle * i);
                Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float) Math.Sin(offsetAngle),
                    baseSpeed * (float) Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
            }

            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AbyssiumBar", 8);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}