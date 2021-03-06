using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class DoomsdayChest : ModTile
	{
        public Texture2D glowTex = null;

        public override void SetDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileContainer[Type] = true;
			Main.tileShine2[Type] = true;
			Main.tileShine[Type] = 1200;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileValue[Type] = 500;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
			TileObjectData.newTile.HookCheck = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
			TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
			TileObjectData.newTile.AnchorInvalidTiles = new int[] { 127 };
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Doomsday Chest");
			AddMapEntry(new Color(0, 0, 255), name);
			dustType = mod.DustType<Dusts.VoidDust>();
			disableSmartCursor = true;
			adjTiles = new int[] { TileID.Containers };
			chest = "Doomsday Chest";
		}

		public string MapChestName(string name, int i, int j)
		{
			int left = i;
			int top = j;
			Tile tile = Main.tile[i, j];
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			if (Main.chest[chest].name == "")
			{
				return name;
			}
			else
			{
				return name + ": " + Main.chest[chest].name;
			}
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

		public override bool CanKillTile(int i, int j, ref bool blockDamaged)
		{
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			return Chest.CanDestroyChest(left, top);
		}

        private int LockFrame = 0;

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (++frameCounter >= 6)
            {
                frameCounter = 0;
                if (++LockFrame >= 8)
                {
                    LockFrame = 0;
                }
            }
        }
        
        public Color GetColor(Color color)
        {
            Color glowColor = AAColor.ZeroShield;
            return glowColor;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D LockTex = mod.GetTexture("Tiles/DoomsdayChestLockedFrame");
            Texture2D glowTex = mod.GetTexture("Glowmasks/DoomsdayChest_Glow");

            int frameX = (tile != null && tile.active() ? tile.frameX + (Main.tileFrame[Type] * 36) : 0);
            int frameY = (tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 38) : 0);

            BaseDrawing.DrawTileTexture(sb, LockTex, x, y, 16, 16, frameX, frameY, false, false, false, null, GetColor);

            int LockframeY = (tile != null && tile.active() ? tile.frameY + (LockFrame * 38) : 0);

            if (Chest.isLocked(x, y))
            {
                BaseDrawing.DrawTileTexture(sb, LockTex, x, y, 16, 16, 36, LockframeY, false, false, false, null, GetColor);
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 32, 32, mod.ItemType("DoomsdayChest"));
			Chest.DestroyChest(i, j);
		}

		public override void RightClick(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
            Tile tile2 = Main.tile[i, j];
            if (tile2.frameX == 72 || tile2.frameX == 90)
            {
                for (int num66 = 0; num66 < 58; num66++)
                {
                    if (player.inventory[num66].type == mod.ItemType("DoomstopperKey") && player.inventory[num66].stack > 0)
                    {
                        player.inventory[num66].stack--;
                        int left = i;
                        int top = j;
                        if (tile2.frameX % 36 != 0)
                        {
                            left--;
                        }
                        if (tile2.frameY != 0)
                        {
                            top--;
                        }
                        Main.tile[left, top].frameX = 0;
                        Main.tile[left, top + 1].frameX = 0;
                        Main.tile[left + 1, top].frameX = 18;
                        Main.tile[left + 1, top + 1].frameX = 18;
                        NetMessage.SendTileSquare(-1, left, top, 2, TileChangeType.None);
                        Main.PlaySound(22, left * 16, top * 16);
                    }
                }
            }

			Tile tile = Main.tile[i, j];
			if (tile.frameX != 72 && tile.frameX != 90)
			{
				Main.mouseRightRelease = false;
				int left = i;
				int top = j;
				if (tile.frameX % 36 != 0)
				{
					left--;
				}
				if (tile.frameY != 0)
				{
					top--;
				}
				if (player.sign >= 0)
				{
					Main.PlaySound(11, -1, -1, 1);
					player.sign = -1;
					Main.editSign = false;
					Main.npcChatText = "";
				}
				if (Main.editChest)
				{
					Main.PlaySound(12, -1, -1, 1);
					Main.editChest = false;
					Main.npcChatText = "";
				}
				if (player.editedChestName)
				{
					NetMessage.SendData(33, -1, -1, NetworkText.FromLiteral(Main.chest[player.chest].name), player.chest, 1f, 0f, 0f, 0, 0, 0);
					player.editedChestName = false;
				}
				if (Main.netMode == 1)
				{
					if (left == player.chestX && top == player.chestY && player.chest >= 0)
					{
						player.chest = -1;
						Recipe.FindRecipes();
						Main.PlaySound(11, -1, -1, 1);
					}
					else
					{
						NetMessage.SendData(31, -1, -1, null, left, (float)top, 0f, 0f, 0, 0, 0);
						Main.stackSplit = 600;
					}
				}
				else
				{
					int chest = Chest.FindChest(left, top);
					if (chest >= 0)
					{
						Main.stackSplit = 600;
						if (chest == player.chest)
						{
							player.chest = -1;
							Main.PlaySound(11, -1, -1, 1);
						}
						else
						{
							player.chest = chest;
							Main.playerInventory = true;
							Main.recBigList = false;
							player.chestX = left;
							player.chestY = top;
							Main.PlaySound(player.chest < 0 ? 10 : 12, -1, -1, 1);
						}
						Recipe.FindRecipes();
					}
				}
			}
		}

		public override void MouseOver(int i, int j)
		{
			Player player = Main.player[Main.myPlayer];
			Tile tile = Main.tile[i, j];
			int left = i;
			int top = j;
			if (tile.frameX % 36 != 0)
			{
				left--;
			}
			if (tile.frameY != 0)
			{
				top--;
			}
			int chest = Chest.FindChest(left, top);
			player.showItemIcon2 = -1;
            player.showItemIconText = Main.chest[chest].name.Length > 0 ? Main.chest[chest].name : "Doomsday Chest";
            if (player.showItemIconText == "Doomsday Chest")
            {
                if (tile.frameX == 72 || tile.frameX == 90)
                {
                    player.showItemIcon2 = mod.ItemType("DoomstopperKey");
                    player.showItemIconText = "";
                }
            }
            player.noThrow = 2;
			player.showItemIcon = true;
		}

		public override void MouseOverFar(int i, int j)
		{
			MouseOver(i, j);
			Player player = Main.player[Main.myPlayer];
			if (player.showItemIconText == "")
			{
				player.showItemIcon = false;
				player.showItemIcon2 = 0;
			}
		}
	}
}