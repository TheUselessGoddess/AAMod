using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.MushroomMonarch
{
    public class HeartyTruffle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hearty Truffle");
            Tooltip.SetDefault(
                @"+50 Health
Don't eat it");
        }


        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 50;
        }
    }
}