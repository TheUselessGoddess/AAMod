using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Shen;
using System.Collections.Generic;
using BaseMod;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace AAMod.Items.BossSummons
{
    public class ChaosRune : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Rune");
            Tooltip.SetDefault(@"A cursed tablet bursting with chaotic energy
Summons Shen Doragon's true awakened form");
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

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor,
            float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                AAColor.Shen2,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame,
            Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = Main.itemTexture[item.type];
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, AAColor.Shen2, 0, origin, scale, SpriteEffects.None, 0f);
            }

            return false;
        }


        // We use the CanUseItem hook to prevent a player from using this item while the boss is present in the world.
        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<ShenSpawn>()))
            {
                return false;
            }

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
            Main.NewText("Time to face true, uniyielding chaos, child...", Color.DarkMagenta.R, Color.DarkMagenta.G,
                Color.DarkMagenta.B);
            NPC.SpawnOnPlayer(item.owner, mod.NPCType<ShenA>());
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
                                             800f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DraconianRune", 1);
            recipe.AddIngredient(null, "DreadRune", 1);
            recipe.AddIngredient(null, "ChaosSigil", 10);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}