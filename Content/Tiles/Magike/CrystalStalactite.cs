﻿using Coralite.Core;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Enums;
using Coralite.Content.Items.Magike;
using System.Collections.Generic;

namespace Coralite.Content.Tiles.Magike
{
    public class CrystalStalactiteTop : ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = default(AnchorData);
            TileObjectData.newTile.CoordinateHeights = new int[1] { 24 };
            TileObjectData.newTile.StyleWrapLimit = 4;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }

    }

    public class CrystalStalactiteBottom : ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = new int[1] { 24 };
            TileObjectData.newTile.DrawYOffset = -6;
            TileObjectData.newTile.StyleWrapLimit = 4;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }

    }

    public class CrystalStalactiteLeft : ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.AnchorLeft = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = default(AnchorData);
            TileObjectData.newTile.CoordinateWidth = 24;
            TileObjectData.newTile.StyleWrapLimit = 4;
            TileObjectData.newTile.StyleMultiplier = 1;
            //TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }

    }

    public class CrystalStalactiteRight : ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.AnchorRight = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = default(AnchorData);
            TileObjectData.newTile.StyleWrapLimit = 1;
            TileObjectData.newTile.StyleMultiplier = 1;
            //TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 4;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }

    }

    public class BigCrystalStalactiteTop:ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.AnchorBottom = default(AnchorData);
            TileObjectData.newTile.StyleWrapLimit = 6;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 6;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }

    }

    public class BigCrystalStalactiteBottom : ModTile
    {
        public override string Texture => AssetDirectory.MagikeTiles + Name;

        public override void SetStaticDefaults()
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.DrawYOffset = 4;
            TileObjectData.newTile.StyleWrapLimit = 6;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 6;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = DustID.CrystalSerpent_Pink;
            AddMapEntry(Coralite.Instance.MagicCrystalPink);
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            return new Item[1]
            {
                new Item(ModContent.ItemType<MagicCrystal>())
            };
        }
    }

}