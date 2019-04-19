using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Legs)]
    public class TrueBlazingSuneate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Blazing Suneate");
            Tooltip.SetDefault(@"5% increased Damage Resistance
5% increased movement speed");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 18;
            item.value = 50000;
            item.rare = 4;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.5f;
            player.moveSpeed += 0.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledSuneate"));
            recipe.AddIngredient(mod.ItemType("OceanBoots"));
            recipe.AddIngredient(ItemID.FossilPants);
            recipe.AddIngredient(mod.ItemType("DoomiteUGreaves"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}