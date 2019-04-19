using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class DragonArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Arrow");
            Tooltip.SetDefault("Lights your opponents ablaze");
        }

        public override void SetDefaults()
        {
            item.damage = 14;
            item.ranged = true;
            item.width = 14;
            item.height = 32;
            item.maxStack = 999;
            item.consumable = true; //You need to set the item consumable so that the ammo would automatically consumed
            item.knockBack = 4f;
            item.value = 30;
            item.rare = 1;
            item.shoot = mod.ProjectileType("DragonArrow"); //The projectile shoot when your weapon using this ammo
            item.shootSpeed = 1f; //The speed of the projectile
            item.ammo = AmmoID.Arrow; //The ammo class this ammo belongs to.
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodenArrow, 50);
            recipe.AddIngredient(null, "IncineriteBar", 1);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 50);
            recipe.AddRecipe();
        }
    }
}