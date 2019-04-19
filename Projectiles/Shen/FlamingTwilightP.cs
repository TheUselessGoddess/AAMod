﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace AAMod.Projectiles.Shen
{
    public class FlamingTwilightP : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.ranged = true;
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.timeLeft = 120;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flaming Twilight Projectile");
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.25f) / 255f,
                ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.05f) / 255f);
            if (projectile.timeLeft > 60)
            {
                projectile.timeLeft = 60;
            }

            if (projectile.ai[0] > 1f)
            {
                float num296 = 1f;
                if (projectile.ai[0] == 8f)
                {
                    num296 = 0.25f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    num296 = 0.5f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    num296 = 0.75f;
                }

                projectile.ai[0] += 1f;
                int num297 = mod.DustType<Dusts.AkumaADust>();
                if (Main.rand.NextBool(2))
                {
                    for (int num298 = 0; num298 < 1; num298++)
                    {
                        int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y),
                            projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f,
                            projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
                        Main.dust[num299].scale *= 1.5f;
                        Dust expr_DC74_cp_0 = Main.dust[num299];
                        expr_DC74_cp_0.velocity.X = expr_DC74_cp_0.velocity.X * 1.2f;
                        Dust expr_DC94_cp_0 = Main.dust[num299];
                        expr_DC94_cp_0.velocity.Y = expr_DC94_cp_0.velocity.Y * 1.2f;
                        Main.dust[num299].scale *= num296;
                    }
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }

            projectile.rotation += 0.3f * projectile.direction;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.penetrate--;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                projectile.ai[0] += 0.1f;
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }

                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }

                projectile.velocity *= 0.75f;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 2;
        }
    }
}