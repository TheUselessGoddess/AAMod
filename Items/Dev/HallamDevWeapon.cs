using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class HallamDevWeapon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prismeow Spectrum");
            Tooltip.SetDefault(@"Summons a Legendary Rainbow Cat at cursor point
Shoots Rainbow Bolts that move in the direction of your cursor
Prismeow EX");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 200;
            item.magic = true;
            item.mana = 200;
            item.width = 52;
            item.height = 52;
            item.useTime = 60;
            item.useAnimation = 60;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 3;
            item.value = Item.sellPrice(1, 0, 0, 0);
            item.rare = 11;
            item.UseSound = SoundID.Item44;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("RainbowCatPro");
            item.shootSpeed = 0f;
            item.expert = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType =
                BaseAAItem
                    .GLOWMASKTYPE_NONE; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White; //glowmask draw color
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            position = Main.MouseWorld;
            return true;
        }


        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Prismeow");
                recipe.AddIngredient(null, "EXSoul");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}