using Terraria.ModLoader;

namespace AAMod.Items.Blocks
{
    public class MireChest : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mire Chest");
        }


        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 28;
            item.value = 500;
            item.maxStack = 99;
            item.useStyle = 1;
            item.useTime = 10;
            item.useAnimation = 15;
            item.useTurn = true;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("MireChest");
        }
    }
}