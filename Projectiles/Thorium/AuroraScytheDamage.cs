using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod;
using BaseMod;

namespace AAMod.Projectiles.Thorium
{
    public class AuroraScytheDamage : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 130;
            projectile.height = 128;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ownerHitCheck = true;
            projectile.ignoreWater = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 8;
            aiType = ProjectileID.Bullet;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(44, 200, false);
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit,
            ref int hitDirection)
        {
            Player player = Main.player[projectile.owner];
            if (Main.rand.Next(100) <=
                ((ModSupportPlayer) player.GetModPlayer(mod, "ModSupportPlayer")).thorium_radiantCrit)
            {
                crit = true;
            }
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];

            projectile.position.X = player.Center.X - ((float) projectile.width / 2f);
            projectile.position.Y = player.Center.Y - ((float) projectile.height / 2f);
        }
    }
}