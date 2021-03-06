using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class PlanteraPetal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera Petal");
            Tooltip.SetDefault("It's very pink");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 7;
        }
    }
}