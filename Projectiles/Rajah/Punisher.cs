using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah
{
    public class Punisher : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Punisher");
		}
        public override void SetDefaults()
        {
            projectile.width = 26;
            projectile.height = 26;
            projectile.aiStyle = 69;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.melee = true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            projectile.ai[0] = 1f;
            projectile.netUpdate = true;
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
            return false;
        }

        public override void AI()
        {
            Vector2 vector54 = Main.player[projectile.owner].Center - projectile.Center;
            projectile.rotation = vector54.ToRotation() - 1.57f;
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            Main.player[projectile.owner].itemAnimation = 10;
            Main.player[projectile.owner].itemTime = 10;
            if (vector54.X < 0f)
            {
                Main.player[projectile.owner].ChangeDir(1);
                projectile.direction = 1;
            }
            else
            {
                Main.player[projectile.owner].ChangeDir(-1);
                projectile.direction = -1;
            }
            Main.player[projectile.owner].itemRotation = (vector54 * -1f * (float)projectile.direction).ToRotation();
            projectile.spriteDirection = ((vector54.X > 0f) ? -1 : 1);
            if (projectile.ai[0] == 0f && vector54.Length() > 400f)
            {
                projectile.ai[1] = 1f;
                projectile.ai[0] = 1f;
            }
            if (projectile.ai[0] == 1f || projectile.ai[0] == 2f)
            {
                float num687 = vector54.Length();
                if (num687 > 1500f)
                {
                    projectile.Kill();
                    return;
                }
                if (num687 > 600f)
                {
                    projectile.ai[0] = 2f;
                }
                projectile.tileCollide = false;
                float num688 = 20f;
                if (projectile.ai[0] == 2f)
                {
                    num688 = 40f;
                }
                projectile.velocity = Vector2.Normalize(vector54) * num688;
                if (vector54.Length() < num688)
                {
                    projectile.Kill();
                    return;
                }
            }
            if (projectile.ai[1] == 1f)
            {
                float spread = 45f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((10 * 10) + (10 * 10));
                double startAngle = Math.Atan2(10, 10) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < 3; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(projectile.position.X, projectile.position.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), mod.ProjectileType<Carrot>(), projectile.damage, projectile.knockBack, projectile.owner);
                }
            }
        }

        public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
            if (projectile.ai[0] != 1)
            {
                projectile.ai[1] = 1f;
            }
            projectile.ai[0] = 1;
        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture = ModLoader.GetTexture("AAMod/Projectiles/Rajah/Punisher_Chain");
 
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, Color.White, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}