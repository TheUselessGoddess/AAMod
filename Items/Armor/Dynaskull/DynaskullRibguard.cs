using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Dynaskull
{
    [AutoloadEquip(EquipType.Body)]
    public class DynaskullRibguard : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Dynaskull Ribguard");
            Tooltip.SetDefault("15% increased Ranged damage");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 18;
            item.value = 90000;
            item.rare = 4;
            item.defense = 8;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage *= 1.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FossilShirt, 1);
            recipe.AddIngredient(null, "DynaskullOre", 30);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}