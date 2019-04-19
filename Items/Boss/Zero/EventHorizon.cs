using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System;

namespace AAMod.Items.Boss.Zero
{
    public class EventHorizon : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Event Horizon");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 54;
            item.damage = 270;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.channel = true;
            item.autoReuse = true;
            item.melee = true;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 5;
            item.knockBack = 2f;
            item.UseSound = SoundID.Item116;
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.shoot = mod.ProjectileType("EventHorizon");
            item.shootSpeed = 22f;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.SolarEruption);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX,
            ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float ai3 = (Main.rand.NextFloat() - 0.75f) * 0.7853982f; //0.5
            float ai3X = (Main.rand.NextFloat() - 0.50f) * 0.7853982f; //0.5
            float ai3Y = (Main.rand.NextFloat() - 0.25f) * 0.7853982f; //0.5
            float ai3Z = (Main.rand.NextFloat() - 0.12f) * 0.7853982f;
            float spread = 45f * 0.0174f;
            float baseSpeed = (float) Math.Sqrt((speedX * speedX) + (speedY * speedY));
            double startAngle = Math.Atan2(speedX, speedY) - .1d;
            double deltaAngle = spread / 6f;
            double offsetAngle;
            for (int i = 0; i < 4; i++)
            {
                float ai1;
                if (i == 0)
                {
                    ai1 = ai3;
                }

                if (i == 1)
                {
                    ai1 = ai3X;
                }

                if (i == 2)
                {
                    ai1 = ai3Y;
                }

                if (i == 3)
                {
                    ai1 = ai3Z;
                }

                if (i == 4)
                {
                    ai1 = ai3;
                }

                offsetAngle = startAngle + (deltaAngle * i);
                Projectile.NewProjectile(position.X, position.Y, baseSpeed * (float) Math.Sin(offsetAngle),
                    baseSpeed * (float) Math.Cos(offsetAngle), mod.ProjectileType("EventHorizon"), damage, knockBack,
                    player.whoAmI, 0.0f, ai3);
            }

            return false;
        }
    }
}