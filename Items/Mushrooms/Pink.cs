using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Mushrooms
{
    public class Pink : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pink Alchemical Mushroom");
            Tooltip.SetDefault(@"It smells weird");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 2;
        }
    }
}