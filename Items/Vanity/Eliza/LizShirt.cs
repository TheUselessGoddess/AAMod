using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Vanity.Eliza

{
    [AutoloadEquip(EquipType.Body)]
    public class LizShirt : ModItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Midnight Cat Blouse");
            Tooltip.SetDefault(@"'Great for impersonating Ancients Awakened Devs!'");
        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(121, 21, 214);
                }
            }
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            drawHands = true;
        }

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 20;
            item.rare = 11;
            item.vanity = true;
        }
    }
}