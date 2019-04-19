using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Serpent
{
    public class Sickle : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 23;
            item.magic = true;
            item.width = 24;
            item.height = 28;
            item.useTime = 23;
            item.useAnimation = 23;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.rare = 3;
            item.mana = 9;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = ProjectileID.CrystalBullet;
            item.shootSpeed = 5f;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sickle");
            Tooltip.SetDefault("Casts crystals that shatter in pieces.");
        }
    }
}