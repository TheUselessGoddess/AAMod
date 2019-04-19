using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Boss.AH
{
    public class AsheSatchel : ModItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.consumable = true;
            item.width = 16;
            item.height = 16;
            item.rare = 11;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ashe's Satchel");
            Tooltip.SetDefault("Contains a set of Fury Witch's robes");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            byte pre = item.prefix;
            item.TurnToAir();
            int itemID = Item.NewItem((int) player.position.X, (int) player.position.Y, player.width, player.height,
                mod.ItemType("WitchHood"), 1, false, 0, false, false);
            int itemID1 = Item.NewItem((int) player.position.X, (int) player.position.Y, player.width, player.height,
                mod.ItemType("WitchRobe"), 1, false, 0, false, false);
            int itemID2 = Item.NewItem((int) player.position.X, (int) player.position.Y, player.width, player.height,
                mod.ItemType("WitchBoots"), 1, false, 0, false, false);
            if (Main.netMode == 1)
            {
                NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(21, -1, -1, null, itemID1, 1f, 0f, 0f, 0, 0, 0);
                NetMessage.SendData(21, -1, -1, null, itemID2, 1f, 0f, 0f, 0, 0, 0);
            }
        }
    }
}