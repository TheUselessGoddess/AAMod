﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using BaseMod;

namespace AAMod.Tiles
{
    public class DataBank : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;

        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Height = 4;
            TileObjectData.newTile.Width = 3;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                16,
                16,
                16,
                16,
                16
            };
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Data Bank");
            AddMapEntry(new Color(60, 0, 120), name);
            disableSmartCursor = true;
            adjTiles = new int[] {TileID.Bookcases};
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(AAPlayer.ZeroColor, 0.7f);
            r = (color.R / 255f);
            g = (color.G / 255f);
            b = (color.B / 255f);
        }

        public Color color;

        public override void PostDraw(int i, int j, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            color = BaseUtility.MultiLerpColor((float) (Main.player[Main.myPlayer].miscCounter % 100) / 100f,
                Color.White, Color.White, Color.Violet, Color.White, Color.Violet, Color.White, Color.White,
                Color.White, Color.White, Color.Violet, Color.White, Color.Violet);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }

            int height = tile.frameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(mod.GetTexture("Glowmasks/DataBank_Glow"),
                new Vector2((i * 16) - (int) Main.screenPosition.X, (j * 16) - (int) Main.screenPosition.Y) + zero,
                new Rectangle(tile.frameX, tile.frameY, 16, height), color, 0f, Vector2.Zero, 1f, SpriteEffects.None,
                0f);
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}