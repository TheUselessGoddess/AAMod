using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Djinn
{
    public class DjinnBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
            bossBagNPC = mod.NPCType("Djinn");
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("DjinnMask"));
            }

            if (Main.rand.NextFloat() < 0.01f)
            {
                int choice = Main.rand.Next(17);
                {
                    AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                    modPlayer.PHMDevArmor();
                }
            }

            player.QuickSpawnItem(mod.ItemType("DesertMana"), Main.rand.Next(15, 20));
            string[] lootTable = {"Djinnerang", "SandLamp", "SandScepter", "SandstormCrossbow", "SultanScimitar"};
            int loot = Main.rand.Next(lootTable.Length);
            if (Main.rand.Next(9) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("Sandagger"), Main.rand.Next(100, 130));
            }
            else
            {
                player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
            }

            player.QuickSpawnItem(mod.ItemType("SandstormMedallion"));
        }
    }
}