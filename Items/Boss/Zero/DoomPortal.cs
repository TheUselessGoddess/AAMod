﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class DoomPortal : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doom Portal");
            Tooltip.SetDefault(@"Summons a small Doom Protocol to fight for you");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Oblivion;
        }

        public override void SetDefaults()
        {
            item.damage = 90;
            item.summon = true;
            item.mana = 10;
            item.width = 24;
            item.height = 24;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.knockBack = 3;
            item.rare = 8;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("Protocol");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("Protocol");
            item.buffTime = 3600;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float num72 = item.shootSpeed;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(item, num74);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num78 = (float) Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = (float) Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f)
            {
                num79 = Main.screenPosition.Y + (float) Main.screenHeight - (float) Main.mouseY - vector2.Y;
            }

            float num80 = (float) Math.Sqrt((double) (num78 * num78 + num79 * num79));
            float num81 = num80;
            if ((float.IsNaN(num78) && float.IsNaN(num79)) || (num78 == 0f && num79 == 0f))
            {
                num78 = (float) player.direction;
                num79 = 0f;
                num80 = num72;
            }
            else
            {
                num80 = num72 / num80;
            }

            num78 = 0f;
            num79 = 0f;
            vector2.X = (float) Main.mouseX + Main.screenPosition.X;
            vector2.Y = (float) Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, num78, num79, mod.ProjectileType("Protocol"), num73, num74,
                i, 0f, 0f);
            return false;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, AAColor.Oblivion.ToVector3() * 0.55f * Main.essScale);
        }
    }
}