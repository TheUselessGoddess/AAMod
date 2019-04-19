using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;

namespace AAMod.Items.BossSummons
{
    public class ChaosSigil : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Sigil");
            Tooltip.SetDefault(@"A cursed tablet filled with unstable magic
Summons the chaos emperor");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 28;
            item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
            item.useTime = 45;
            item.useStyle = 4;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(176, 39, 157);
                }
            }
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<ShenDoragon>()))
            {
                if (player.whoAmI == Main.myPlayer)
                    BaseUtility.Chat("HAH! I WISH there were two of me to smash you into the ground!",
                        Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }

            if (NPC.AnyNPCs(mod.NPCType<ShenA>()))
            {
                if (player.whoAmI == Main.myPlayer)
                    BaseUtility.Chat("HAH! I WISH there were two of me to smash you into the ground!",
                        Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B, false);
                return false;
            }

            /*if (!AAWorld.downedShen)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Chaos Sigil glows, and imagery of the chaos pedestals flash through your mind", Color.DarkMagenta, false);
                return false;
            }*/
            for (int m = 0; m < Main.maxProjectiles; m++)
            {
                Projectile p = Main.projectile[m];
                if (p != null && p.active && p.type == mod.ProjectileType("ShenTransition"))
                {
                    return false;
                }

                if (p != null && p.active && p.type == mod.ProjectileType("ShenSpawn"))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool UseItem(Player player)
        {
            if (AAWorld.ShenSummoned)
            {
                Main.NewText(
                    AAWorld.downedShen
                        ? "Big mistake, child..."
                        : "Hmpf...Again..? Alright, let's just get this done and overwith.", Color.DarkMagenta.R,
                    Color.DarkMagenta.G, Color.DarkMagenta.B);

                SpawnBoss(player, "ShenDoragon", "Shen Doragon; Draconian Doomsayer");
            }

            if (!AAWorld.ShenSummoned)
            {
                SpawnBoss(player, "ShenSpawn", "Shen Doragon; Draconian Doomsayer");
                AAWorld.ShenSummoned = true;
            }

            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/ShenRoar"), player.position);
            return true;
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
                Main.npc[npcID].Center = player.Center -
                                         new Vector2(MathHelper.Lerp(-100f, 100f, (float) Main.rand.NextDouble()),
                                             100f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public void SpawnBoss2(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                } //don't spawn if there's already a boss!

                int npcID = NPC.NewNPC((int) player.Center.X, (int) player.Center.Y, bossType, 0);
                Main.npc[npcID].ai[0] = -1;
                Main.npc[npcID].Center = player.Center -
                                         new Vector2(MathHelper.Lerp(-100f, 100f, (float) Main.rand.NextDouble()), 600);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DraconianSigil", 1);
            recipe.AddIngredient(null, "DreadSigil", 1);
            recipe.AddIngredient(null, "Discordium", 10);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}