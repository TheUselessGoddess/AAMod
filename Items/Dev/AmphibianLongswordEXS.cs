using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class AmphibianLongswordEXS : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amphibious Greatblade");
            Tooltip.SetDefault(@"Amphibious Longsword EX");
        }

        public override void SetDefaults()
        {
            item.damage = 350;
            item.melee = true;
            item.width = 64;
            item.height = 64;
            item.useTime = 18;
            item.useAnimation = 18;
            item.useStyle = 1;
            item.knockBack = 7;
            item.value = 300000;
            item.rare = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AmphibiousProjectileEXS");
            item.shootSpeed = 18f;
            item.expert = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }

        public override void AddRecipes()
        {
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "AmphibianLongswordEX");
                recipe.AddIngredient(null, "ShinyCharm");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "AmphibianLongswordS");
                recipe.AddIngredient(null, "EXSoul");
                recipe.AddTile(null, "QuantumFusionAccelerator");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}