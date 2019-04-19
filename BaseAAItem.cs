using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items
{
    public abstract class BaseAAItem : ModItem
    {
        public const int GLOWMASKTYPE_NONE = -1; //for shit like Daystorm which is a 'projectile' gun
        public const int GLOWMASKTYPE_SWORD = 0; //for swords and swordlike items
        public const int GLOWMASKTYPE_GUN = 1; //for guns and gunlike items (bows too)

        //glowmask shenanigans
        public string glowmaskTexture = null;
        public int glowmaskDrawType = 0;
        public Color glowmaskDrawColor = Color.White; //default white	

        //custom name color
        public Color? customNameColor = null;

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            if (glowmaskTexture != null)
            {
                Texture2D texture = mod.GetTexture(glowmaskTexture);
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        item.position.X - Main.screenPosition.X + item.width * 0.5f,
                        item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
            }
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (customNameColor != null)
            {
                foreach (TooltipLine line2 in list)
                {
                    if (line2.mod == "Terraria" && line2.Name == "ItemName")
                    {
                        line2.overrideColor = (Color) customNameColor;
                    }
                }
            }
        }

        //DO NOT FUCK WITH THIS!! EDITING THIS COULD BREAK ITEMS BADLY!!!
        public override ModItem NewInstance(Item itemClone)
        {
            BaseAAItem newItem = (BaseAAItem) base.NewInstance(itemClone);
            newItem.glowmaskTexture = glowmaskTexture;
            newItem.glowmaskDrawType = glowmaskDrawType;
            newItem.glowmaskDrawColor = glowmaskDrawColor;
            newItem.customNameColor = customNameColor;
            return newItem;
        }
    }
}