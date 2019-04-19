using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee.Gem //where is located
{
    public class RubyGreatsword : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 32; //Sword damage
            item.melee = true; //if it's melee
            item.width = 58; //Sword width
            item.height = 60; //Sword height
            item.useTime = 20; //how fast 
            item.useAnimation = 20;
            item.useStyle = 1; //Style is how this item is used, 1 is the style of the sword
            item.knockBack = 5; //Sword knockback
            item.value = 3000;
            item.rare = 4;
            item.UseSound = SoundID.Item1; //1 is the sound of the sword
            item.autoReuse = false; //if it's capable of autoswing.
            item.useTurn = true;
            item.shoot = mod.ProjectileType<Projectiles.GemShot.RubyShot>();
            item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ruby Greatsword");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes() //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RubySaber", 1);
            recipe.AddIngredient(ItemID.LargeRuby, 1); //you need 1 DirtBlock
            recipe.AddTile(TileID.Anvils); //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}