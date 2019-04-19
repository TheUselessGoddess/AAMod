using System.Collections.Generic;
using Microsoft.Xna.Framework;

//using AAMod.NPCs.Bosses.Infinity;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using BaseMod;

namespace AAMod.Items.BossSummons
{
    public class DreadRune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Moon Rune");
            Tooltip.SetDefault(@"An enchanted tablet eminating with dark chaotic energy
Summons Yamata Awakened
Only Usable at night in the mire
Only craftable in expert mode");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = 2;
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
            item.consumable = false;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(146, 30, 68);
                }
            }
        }

        public override bool UseItem(Player player)
        {
            Main.NewText("Yamata has been Awakened!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            Main.NewText("Yeah, yeah I get it, my first phase is obnoxious. Let�s just get this over with..!",
                new Color(146, 30, 68));
            SpawnBoss(player, "YamataA", "Yamata Awakened");
            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/YamataRoar"), player.position);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer)
                    BaseUtility.Chat("NO! I DON'T WANNA FIGHT NOW! I NEED MY BEAUTY SLEEP! COME BACK AT NIGHT!",
                        new Color(45, 46, 70), false);
                return false;
            }

            if (player.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                if (!player.GetModPlayer<AAPlayer>(mod).ZoneRisingMoonLake && !AAWorld.downedYamata)
                {
                    if (player.whoAmI == Main.myPlayer)
                        BaseUtility.Chat(
                            "An image of the strange tree at the heart of the mire flashes through your mind",
                            Color.Indigo, false);
                    return false;
                }

                if (NPC.AnyNPCs(mod.NPCType("Yamata")))
                {
                    if (player.whoAmI == Main.myPlayer)
                        BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(45, 46, 70),
                            false);
                    return false;
                }

                if (NPC.AnyNPCs(mod.NPCType("YamataA")))
                {
                    if (player.whoAmI == Main.myPlayer)
                        BaseUtility.Chat("WHAT THE HELL ARE YOU DOING?! I'M ALREADY HERE!!!", new Color(146, 30, 68),
                            false);
                    return false;
                }

                if (NPC.AnyNPCs(mod.NPCType("YamataTransition")))
                {
                    return false;
                }

                return true;
            }

            if (player.whoAmI == Main.myPlayer)
                BaseUtility.Chat("Hey Dumbo! Mire is that way!", new Color(45, 46, 70), false);
            return false;
        }

        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                } //don't spawn if there's already a boss!

                int npcID = NPC.NewNPC((int) player.Center.X, (int) player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(0f, 100f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void UseStyle(Player p)
        {
            BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true);
        }

        public override bool UseItemFrame(Player p)
        {
            BaseMod.BaseUseStyle.SetFrameBoss(p, item);
            return true;
        }
    }
}