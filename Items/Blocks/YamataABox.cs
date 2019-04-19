using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace AAMod.Items.Blocks
{
    public class YamataABox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata Awakened Music Box");
            Tooltip.SetDefault(@"Plays 'The Ultimate Show' from Super Paper Mario
Remix by GAMetal");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.YamataA;
                }
            }
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("YamataABox");
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.value = 10000;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "YamataBox");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}