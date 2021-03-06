using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Boss.Zero
{
    public class Vortex : ModItem
    {

        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Vortex");
            Tooltip.SetDefault(@"Spins fast enough to drag all enemies into its gravitational pull");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.Terrarian);
            item.damage = 250;                            
            item.value = Item.buyPrice(1, 0, 0, 0);
            item.rare = 2;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 15;
            item.useTime = 15;
            item.shoot = mod.ProjectileType("Vortex");  
		}

        //public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        //{
        //    Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
        //    spriteBatch.Draw
        //    (
        //        texture,
        //        new Vector2
        //        (
        //            item.position.X - Main.screenPosition.X + item.width * 0.5f,
        //            item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
        //        ),
        //        new Rectangle(0, 0, texture.Width, texture.Height),
        //        Color.White,
        //        rotation,
        //        texture.Size() * 0.5f,
        //        scale,
        //        SpriteEffects.None,
        //        0f
        //    );
        //}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Zero;
                }
            }
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.Terrarian);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
