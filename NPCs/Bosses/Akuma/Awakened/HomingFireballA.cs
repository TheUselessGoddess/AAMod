﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Akuma.Awakened
{
    internal class HomingFireballA : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Firebomb");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.scale = 1.1f;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 60;
            projectile.timeLeft = 300;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.AkumaA;
        }

        bool DidntHitPlayer = false;

        public override void AI()
        {
            if (projectile.timeLeft > 0)
            {
                projectile.timeLeft--;
            }

            if (projectile.timeLeft == 0)
            {
                DidntHitPlayer = true;
                projectile.Kill();
            }

            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }

            projectile.rotation = (float) Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            const int aislotHomingCooldown = 0;
            const int homingDelay = 0;
            const float desiredFlySpeedInPixelsPerFrame = 10;
            const float
                amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    Player target = Main.player[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(target.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity =
                        Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("DragonFire"), 300);
            Kill(0);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int) projectile.position.X, (int) projectile.position.Y, 20);
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X,
                projectile.velocity.Y, mod.ProjectileType("HomingBoom"), projectile.damage, projectile.knockBack,
                projectile.owner, 0f, 0f);
            for (int dust = 0; dust <= 10; dust++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height,
                    mod.DustType<Dusts.AkumaADust>(), projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 500;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                Player target = Main.player[i];
                if (target.active && (!target.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(target.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance
                        ) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }


        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch,
            Microsoft.Xna.Framework.Color lightColor)
        {
            int shader =
                Terraria.Graphics.Shaders.GameShaders.Armor.GetShaderIdFromItemId(Terraria.ID.ItemID.LivingOceanDye);
            Microsoft.Xna.Framework.Vector2 Drawpos = projectile.Center - Main.screenPosition +
                                                      new Microsoft.Xna.Framework.Vector2(0, projectile.gfxOffY);

            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame,
                Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height / 4, 0,
                2);

            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], shader,
                projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 4,
                frame, Color.White, true);
            return false;
        }
    }
}