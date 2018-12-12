using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class ZeroDeath1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.projFrames[projectile.type] = 8;
        }
        public override void SetDefaults()
        {
            projectile.width = 1;
            projectile.height = 1;
            projectile.penetrate = -1;
            projectile.hostile = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }
        public bool linesaid = false;
        public override void AI()
        {
            if (Main.expertMode && !AAWorld.downedZero && !linesaid)
            {
                Main.NewText("MISSI0N FAILED. SENDING DISTRESS SIGNAL T0 H0ME BASE", Color.Red.R, Color.Red.G, Color.Red.B);
                linesaid = true;
            }
            if (Main.expertMode && AAWorld.downedZero && !linesaid)
            {
                Main.NewText("MISSI0N FAILED. ATTEMPTING DISTRESS SIGNAL AGAIN", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            if (!AAWorld.downedIZ)
            {
                AAMod.Slayer = true;
            }
            if (++projectile.frameCounter >= 12)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 8)
                {
                    projectile.Kill();
                   
                }
            }
            projectile.velocity.X *= 0.00f;
            projectile.velocity.Y += 0.00f;
           
        }
        public override void Kill(int timeLeft)
        {
            if (!AAWorld.downedZero && Main.expertMode)
            {
                Main.NewText("SENDING...", Color.Red.R, Color.Red.G, Color.Red.B);
            }
            Projectile.NewProjectile((new Vector2(projectile.position.X, projectile.position.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("ZeroDeath2"), 0, 0);
        }
    }
}