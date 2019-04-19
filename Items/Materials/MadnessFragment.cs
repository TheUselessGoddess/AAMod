using Terraria.ModLoader;

namespace AAMod.Items.Materials
{
    public class MadnessFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Madness Fragment");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 24;
            item.maxStack = 99;
            item.rare = 2;
        }
    }
}