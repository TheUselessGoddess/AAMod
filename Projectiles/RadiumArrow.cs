using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class RadiumArrow : ModProjectile
    {
        public short customGlowMask = 0;

        public override void SetStaticDefaults()
        {
            if (Main.netMode != 2)
            {
                Texture2D[] glowMasks = new Microsoft.Xna.Framework.Graphics.Texture2D[Main.glowMaskTexture.Length + 1];
                for (int i = 0; i < Main.glowMaskTexture.Length; i++)
                {
                    glowMasks[i] = Main.glowMaskTexture[i];
                }

                glowMasks[glowMasks.Length - 1] = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short) (glowMasks.Length - 1);
                Main.glowMaskTexture = glowMasks;
            }

            projectile.glowMask = customGlowMask;
            DisplayName.SetDefault("Radium Arrow"); //The English name of the projectile
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8; //The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[projectile.type] = 0; //The recording mode
        }

        public override void SetDefaults()
        {
            projectile.width = 14; //The width of projectile hitbox
            projectile.height = 32; //The height of projectile hitbox
            projectile.aiStyle = 1; //The ai style of the projectile, please reference the source code of Terraria
            projectile.friendly = true; //Can the projectile deal damage to enemies?
            projectile.hostile = false; //Can the projectile deal damage to the player?
            projectile.ranged = true; //Is the projectile shoot by a ranged weapon?
            projectile.penetrate =
                2; //How many monsters the projectile can penetrate. (OnTileCollide below also decrements penetrate for bounces as well)
            projectile.timeLeft =
                600; //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in)
            projectile.light = 2f; //How much light emit around the projectile
            projectile.ignoreWater = true;
            projectile.tileCollide = false; //Can the projectile collide with tiles?
            projectile.extraUpdates = 1; //Set to above 0 if you want the projectile to update multiple time in a frame
            aiType = ProjectileID.JestersArrow;
        }
    }
}