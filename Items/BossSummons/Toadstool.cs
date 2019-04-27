using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Toad;

namespace AAMod.Items.BossSummons
{
    public class Toadstool : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Toadstool");
            Tooltip.SetDefault(@"Summons the Truffle Toad
Can only be used in a surface glowing mushroom biome");
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 22;
            item.maxStack = 20;
            item.value = 1000;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }
        
        public override bool UseItem(Player player)
        {
            SpawnBoss(player, "TruffleToad");
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (!player.ZoneGlowshroom && player.Center.Y > Main.worldSurface)
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The toadstool croaks", Color.Blue, false);
                return false;
            }
            if (NPC.AnyNPCs(mod.NPCType<TruffleToad>()))
            {
                if (player.whoAmI == Main.myPlayer) BaseUtility.Chat("The Truffle Toad Croaks", Color.Blue, false);
                return false;
            }
            return true;
        }

        public void SpawnBoss(Player player, string name)
        {
            int SpawnX = (int)MathHelper.Lerp(-500, 500, (float)Main.rand.NextDouble());
            int num = NPC.NewNPC(SpawnX, (int)(player.position.Y - 50), mod.NPCType(name), 0, 0f, 0f, 0f, 0f, 255);
            if (Main.netMode == 2 && num < 200)
            {
                NetMessage.SendData(23, -1, -1, null, num, 0f, 0f, 0f, 0, 0, 0);
            }
        }

        public override void UseStyle(Player p) { BaseMod.BaseUseStyle.SetStyleBoss(p, item, true, true); }
        public override bool UseItemFrame(Player p) { BaseMod.BaseUseStyle.SetFrameBoss(p, item); return true; }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GlowingMushroom, 15);
            recipe.AddIngredient(null, "Mushium", 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}