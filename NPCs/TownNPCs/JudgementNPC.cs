﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.TownNPCs
{
    public class JudgementNPC : ModProjectile
    {
    	
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Judgement");
            Main.projFrames[projectile.type] = 4;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.extraUpdates = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            if (projectile.position.Y > Main.player[projectile.owner].position.Y - 300f)
            {
                projectile.tileCollide = true;
            }
            if ((double)projectile.position.Y < Main.worldSurface * 16.0)
            {
                projectile.tileCollide = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() - 1.57079637f;
            Vector2 position = projectile.Center + (Vector2.Normalize(projectile.velocity) * 10f);
            Dust dust20 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default(Color), 1f)];
            dust20.position = position;
            dust20.velocity = (projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2)) * 0.33f) + (projectile.velocity / 4f);
            dust20.position += projectile.velocity.RotatedBy(1.5707963705062866, default(Vector2));
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
            dust20 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.JudgementDust>(), 0f, 0f, 0, default(Color), 1f)];
            dust20.position = position;
            dust20.velocity = (projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2)) * 0.33f) + (projectile.velocity / 4f);
            dust20.position += projectile.velocity.RotatedBy(-1.5707963705062866, default(Vector2));
            dust20.fadeIn = 0.5f;
            dust20.noGravity = true;
        }

        public override void Kill(int timeLeft)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - (spread / 2);
            double deltaAngle = spread / 8f;
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.JudgementDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.JudgementDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                    projectile.frame = 0;
            }
            return true;
        }
    }
}