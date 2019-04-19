using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.MadTitan
{
    [AutoloadEquip(EquipType.Legs)]
    public class MadTitanBoots : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mad Titan's Greaves");
            Tooltip.SetDefault(@"40% increased movement speed
20% Increased Melee Speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 3000000;
            item.rare = 11;
            item.defense = 30;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.4f;
            player.meleeSpeed += 0.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkmatterGreaves", 1);
            recipe.AddIngredient(null, "RadiumCuisses", 1);
            recipe.AddIngredient(ItemID.SolarFlareLeggings, 1);
            recipe.AddIngredient(ItemID.VortexLeggings, 1);
            recipe.AddIngredient(ItemID.NebulaLeggings, 1);
            recipe.AddIngredient(ItemID.StardustLeggings, 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}