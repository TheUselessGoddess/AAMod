using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    public class MudkipBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
            DisplayName.SetDefault("Mud Fish Ball");

            Tooltip.SetDefault("It seems to have something in it already");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.UnluckyYarn);
            item.shoot = mod.ProjectileType("Mudkip");

            item.buffType = mod.BuffType("Mudkip");
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}