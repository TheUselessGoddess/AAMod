using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DummyExplosionTerra : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dummy Explosion");
        }

        public override void SetDefaults()
        {
            projectile.magic = true;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.width = 32;
            projectile.height = 32;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 1;
        }
    }
}