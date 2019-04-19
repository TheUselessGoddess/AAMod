using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class VikingRelic : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Viking Relic");
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 34;
            item.maxStack = 99;
            item.value = Item.sellPrice(0, 0, 8, 0);
            item.rare = 1;
        }
    }
}