using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterHelm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darkmatter Helm");
            Tooltip.SetDefault(@"18% increased throwing damage
Dark, yet still barely visible");
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
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 24;
            item.value = 300000;
            item.rare = 11;
            item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.thrownDamage += 0.18f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("DarkmatterBreastplate") && legs.type == mod.ItemType("DarkmatterGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"30% throwing crit chance and velocity
Your thrown weapons electrocute enemies
8% increased damage resistance at night";
            if (!Main.dayTime)
            {
                player.endurance += .08f;
            }

            player.meleeSpeed += 0.30f;
            player.thrownCrit += 30;
            player.GetModPlayer<AAPlayer>(mod).darkmatterSetTh = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}