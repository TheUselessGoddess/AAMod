using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
    public class RadiumHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Radium Helm");
            Tooltip.SetDefault(@"25% increased throwing damage
Shines with the light of a starry night sky");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.value = 300000;
            item.rare = 11;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage += 0.25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("RadiumPlatemail") && legs.type == mod.ItemType("RadiumCuisses");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"30% throwing crit chance and velocity
Being hit causes stars from the heavans to fall around you and increases your movement speed
30% increased movement speed during the day";
            if (Main.dayTime)
            {
                player.moveSpeed += .3f;
            }

            player.GetModPlayer<AAPlayer>(mod).Radium = true;
            player.meleeSpeed += 0.30f;
            player.thrownCrit += 30;
            player.panic = true;
            player.starCloak = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}