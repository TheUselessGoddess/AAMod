using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class Abyssium : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 1;
            item.value = 4000;
            item.consumable = true;
            item.createTile = mod.TileType("AbyssiumOre"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Abyssium");
            Tooltip.SetDefault("It's all mushy. Nasty.");
        }
    }
}