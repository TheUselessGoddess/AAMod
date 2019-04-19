using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.TrueBlazing
{
    [AutoloadEquip(EquipType.Body)]
    public class TrueBlazingDou : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("True Blazing Dao");
            Tooltip.SetDefault(@"5% increased damage resistance");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.value = 60000;
            item.rare = 4;
            item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.endurance += 0.05f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("KindledDou"));
            recipe.AddIngredient(mod.ItemType("OceanShirt"));
            recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(mod.ItemType("DoomiteUPlate"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}