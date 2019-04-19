using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Items.Banners
{
    public class Banners : ModItem
    {
        int pStyle = -1;
        string dName = null;

        public override bool CloneNewInstances
        {
            get { return true; }
        }

        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void AutoStaticDefaults()
        {
            DisplayName.SetDefault(Regex.Replace(base.GetType().Name, "([A-Z])", " $1").Trim());
        }

        public Banners SetupBanner(string dname, int pstyle)
        {
            pStyle = pstyle;
            dName = dname;
            return this;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Banner");
            if (dName != null)
            {
                DisplayName.SetDefault(dName + " Banner");
                BaseMod.BaseUtility.AddTooltips(item, new string[] {"Nearby players get a bonus against: " + dName});
            }
        }

        public override void SetDefaults()
        {
            if (dName != null)
            {
                item.createTile = mod.TileType("Banners");
                item.placeStyle = pStyle;
            }

            item.scale = 0.7f;
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = 1;
            item.value = BaseMod.BaseUtility.CalcValue(0, 0, 10, 0);

            item.useStyle = 1;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.useTurn = true;
            item.consumable = true;
        }
    }
}