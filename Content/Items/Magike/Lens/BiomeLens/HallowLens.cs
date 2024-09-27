﻿using Coralite.Content.Items.MagikeSeries2;
using Coralite.Content.Raritys;
using Coralite.Core;
using Coralite.Core.Systems.MagikeSystem;
using Coralite.Core.Systems.MagikeSystem.BaseItems;
using Coralite.Core.Systems.MagikeSystem.Components;
using Coralite.Core.Systems.MagikeSystem.Components.Producers;
using Coralite.Core.Systems.MagikeSystem.TileEntities;
using Coralite.Core.Systems.MagikeSystem.Tiles;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Magike.Lens.BiomeLens
{
    public class HallowLens() : MagikeApparatusItem(TileType<HallowLensTile>(), Item.sellPrice(silver: 20)
        , RarityType<CrystallineMagikeRarity>(), AssetDirectory.MagikeLens)
    {
        public override bool CanUseItem(Player player)
        {
            return player.ZoneHallow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Skarn>(10)
                .AddIngredient(ItemID.UnicornHorn)
                .AddCondition(CoraliteConditions.LearnedMagikeBase)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class HallowLensTile() : BaseLensTile
        (Coralite.HallowYellow, DustID.HallowedTorch)
    {
        public override int DropItemType => ItemType<HallowLens>();

        public override int[] GetAnchorValidTiles()
        {
            return
            [
                TileID.HallowedGrass,TileID.HallowedIce,TileID.HallowHardenedSand
                ,TileID.HallowSandstone,TileID.GolfGrassHallowed,TileID.Pearlstone
                ,TileID.Pearlstone,TileID.PearlstoneBrick,TileID.Pearlwood
            ];
        }

        public override MagikeTileEntity GetEntityInstance() => GetInstance<HallowLensTileEntity>();

        public override MagikeApparatusLevel[] GetAllLevels()
        {
            return
            [
                MagikeApparatusLevel.None,
                MagikeApparatusLevel.Hallow,
                MagikeApparatusLevel.HolyLight,
            ];
        }

        public override void DrawTopTex(SpriteBatch spriteBatch, Texture2D tex, Vector2 drawPos, Color lightColor, MagikeApparatusLevel level, bool canProduce)
        {
            switch (level)
            {
                default:
                    base.DrawTopTex(spriteBatch, tex, drawPos, lightColor, level, canProduce);
                    return;
                case MagikeApparatusLevel.Hallow:
                    {
                        Rectangle frame;
                        if (canProduce)
                        {
                            int yframe = (int)(6 * Main.GlobalTimeWrappedHourly % 10);
                            frame = tex.Frame(2, 10, 0, yframe);
                        }
                        else
                            frame = tex.Frame(2, 10, 1, 0);

                        spriteBatch.Draw(tex, drawPos, frame, lightColor, 0, frame.Size() / 2, 1f, 0, 0f);
                    }
                    return;
                case MagikeApparatusLevel.HolyLight:
                    {
                        Rectangle frame;
                        if (canProduce)
                        {
                            int yframe = (int)(6 * Main.GlobalTimeWrappedHourly % 18);
                            frame = tex.Frame(2, 18, 0, yframe);
                        }
                        else
                            frame = tex.Frame(2, 18, 1, 0);

                        spriteBatch.Draw(tex, drawPos, frame, lightColor, 0, frame.Size() / 2, 1f, 0, 0f);
                    }
                    return;
            }

        }
    }

    public class HallowLensTileEntity : BaseActiveProducerTileEntity<HallowLensTile>
    {
        public override MagikeContainer GetStartContainer()
            => new HallowLensContainer();

        public override MagikeLinerSender GetStartSender()
            => new HallowLensSender();

        public override MagikeActiveProducer GetStartProducer()
            => new HallowProducer();
    }

    public class HallowLensContainer : UpgradeableContainer
    {
        public override void Upgrade(MagikeApparatusLevel incomeLevel)
        {
            switch (incomeLevel)
            {
                default:
                    MagikeMaxBase = 0;
                    AntiMagikeMaxBase = 0;
                    break;
                case MagikeApparatusLevel.Hallow:
                    MagikeMaxBase = 9;
                    AntiMagikeMaxBase = MagikeMaxBase * 3;
                    break;
                case MagikeApparatusLevel.HolyLight:
                    MagikeMaxBase = 562;
                    AntiMagikeMaxBase = MagikeMaxBase * 2;
                    break;
            }

            LimitMagikeAmount();
            LimitAntiMagikeAmount();
        }
    }

    public class HallowLensSender : UpgradeableLinerSender
    {
        public override void Upgrade(MagikeApparatusLevel incomeLevel)
        {
            MaxConnectBase = 1;
            ConnectLengthBase = 4 * 16;

            switch (incomeLevel)
            {
                default:
                    MaxConnectBase = 0;
                    UnitDeliveryBase = 0;
                    SendDelayBase = 1_0000_0000 / 60;//随便填个大数
                    ConnectLengthBase = 0;
                    break;
                case MagikeApparatusLevel.Hallow:
                    UnitDeliveryBase = 3;
                    SendDelayBase = 10;
                    break;
                case MagikeApparatusLevel.HolyLight:
                    UnitDeliveryBase = 150;
                    SendDelayBase = 8;
                    break;
            }

            SendDelayBase *= 60;
            RecheckConnect();
        }
    }

    public class HallowProducer : UpgradeableProducerByBiome
    {
        public override MagikeSystem.UITextID ApparatusName()
            => MagikeSystem.UITextID.HallowLensName;

        public override MagikeSystem.UITextID ProduceCondition()
            => MagikeSystem.UITextID.HallowCondition;

        public override bool CheckTile(Tile tile)
            => TileID.Sets.Hallow[tile.TileType] || TileID.Sets.HallowBiome[tile.TileType] > 0 || TileID.Sets.HallowBiomeSight[tile.TileType];

        public override bool CheckWall(Tile tile)
            => true;

        public override void Upgrade(MagikeApparatusLevel incomeLevel)
        {
            switch (incomeLevel)
            {
                default:
                    ProductionDelayBase = 1_0000_0000 / 60;//随便填个大数
                    ThroughputBase = 1;
                    break;
                case MagikeApparatusLevel.Hallow:
                    ProductionDelayBase = 10;
                    ThroughputBase = 1;
                    break;
                case MagikeApparatusLevel.HolyLight:
                    ProductionDelayBase = 8;
                    ThroughputBase = 50;
                    break;
            }

            ProductionDelayBase *= 60;
            Timer = ProductionDelayBase;
        }
    }
}
