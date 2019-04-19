using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class DragonScale : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Scale");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 1;
        }
    }
}