using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Dusts
{
    public class Moonraze : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
            dust.velocity.Y = Main.rand.Next(-10, 6) * 0.1f;
            dust.velocity.X *= 0.3f;
		}
		
        public override bool MidUpdate(Dust dust)
        {
            if (!dust.noGravity)
            {
                dust.velocity.Y += 0.05f;
            }
            if (!dust.noLight)
            {
                float strength = dust.scale * 1.4f;
                if (strength > 1f)
                {
                    strength = 1f;
                }
                Lighting.AddLight(dust.position, 0.3f * strength, 0.3f * strength, 0.7f * strength);
            }
            return false;
        }		

		public override Color? GetAlpha(Dust dust, Color lightColor)
		{
			return  new Color(255, 255, 255, 150);
		}
	}
}