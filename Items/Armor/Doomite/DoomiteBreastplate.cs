using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Doomite
{
    [AutoloadEquip(EquipType.Body)]
    public class DoomiteBreastplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Plate");
            Tooltip.SetDefault(@"+1 Minion slot");
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 4;
            item.defense = 7;
            item.value = 9000;
        }

        public override void UpdateEquip(Player player)
        {
            player.maxMinions += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}