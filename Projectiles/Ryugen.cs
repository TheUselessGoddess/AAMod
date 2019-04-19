using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    class Ryugen : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 28;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Arkhalis);
            aiType = ProjectileID.Arkhalis;
            projectile.width = 132;
            projectile.height = 64;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 120;
            projectile.penetrate = -1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            //return Color.White;
            return new Color(0, 200, 0, 0) * (1f - (projectile.alpha / 255f));
        }
    }
}