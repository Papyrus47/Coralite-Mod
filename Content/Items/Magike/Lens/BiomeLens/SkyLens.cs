﻿using Coralite.Content.Items.MagikeSeries1;
using Coralite.Content.Raritys;
using Coralite.Core;
using Coralite.Core.Systems.MagikeSystem;
using Coralite.Core.Systems.MagikeSystem.BaseItems;
using Coralite.Core.Systems.MagikeSystem.Components;
using Coralite.Core.Systems.MagikeSystem.Components.Producers;
using Coralite.Core.Systems.MagikeSystem.TileEntities;
using Coralite.Core.Systems.MagikeSystem.Tiles;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Magike.Lens.BiomeLens
{
    public class SkyLens() : MagikeApparatusItem(TileType<SkyLensTile>(), Item.sellPrice(silver: 5)
        , RarityType<MagicCrystalRarity>(), AssetDirectory.MagikeLens)
    {
        public override bool CanUseItem(Player player)
        {
            return player.ZoneSkyHeight;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Basalt>(10)
                .AddIngredient(ItemID.Cloud, 5)
                .AddCondition(CoraliteConditions.LearnedMagikeBase)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class SkyLensTile() : BaseLensTile
        ( Color.SandyBrown, DustID.Cloud)
    {
        public override int DropItemType => ItemType<SkyLens>();

        public override int[] GetAnchorValidTiles()
        {
            return
            [
                TileID.Cloud,TileID.RainCloud,TileID.SnowCloud
            ];
        }

        public override MagikeTileEntity GetEntityInstance() => GetInstance<SkyLensTileEntity>();

        public override MALevel[] GetAllLevels()
        {
            return
            [
                MALevel.None,
                MALevel.Eiderdown,
                MALevel.Flight,
                MALevel.Feather,
            ];
        }
    }

    public class SkyLensTileEntity : BaseActiveProducerTileEntity<SkyLensTile>
    {
        public override MagikeContainer GetStartContainer()
            => new SkyLensContainer();

        public override MagikeLinerSender GetStartSender()
            => new SkyLensSender();

        public override MagikeActiveProducer GetStartProducer()
            => new SkyProducer();
    }

    public class SkyLensContainer : UpgradeableContainer
    {
        public override void Upgrade(MALevel incomeLevel)
        {
            switch (incomeLevel)
            {
                default:
                    MagikeMaxBase = 0;
                    AntiMagikeMaxBase = 0;
                    break;
                case MALevel.Eiderdown:
                    MagikeMaxBase = 18;
                    AntiMagikeMaxBase = MagikeMaxBase * 3;
                    break;
                case MALevel.Flight:
                    MagikeMaxBase = 540;
                    AntiMagikeMaxBase = MagikeMaxBase * 2;
                    break;
                case MALevel.Feather:
                    MagikeMaxBase = 720;
                    AntiMagikeMaxBase = MagikeMaxBase * 2;
                    break;
            }

            LimitMagikeAmount();
            LimitAntiMagikeAmount();
        }
    }

    public class SkyLensSender : UpgradeableLinerSender
    {
        public override void Upgrade(MALevel incomeLevel)
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
                case MALevel.Eiderdown:
                    UnitDeliveryBase = 6;
                    SendDelayBase = 10;
                    break;
                case MALevel.Flight:
                    UnitDeliveryBase = 144;
                    SendDelayBase = 8;
                    break;
                case MALevel.Feather:
                    UnitDeliveryBase = 168;
                    SendDelayBase = 7;
                    break;
            }

            SendDelayBase *= 60;
            RecheckConnect();
        }
    }

    public class SkyProducer : UpgradeableProducerByBiome
    {
        public override MagikeSystem.UITextID ApparatusName()
            => MagikeSystem.UITextID.SkyLensName;

        public override MagikeSystem.UITextID ProduceCondition()
            => MagikeSystem.UITextID.SkyCondition;

        public override bool CheckTile(Tile tile)
            => TileID.Sets.Clouds[tile.TileType];

        public override bool CheckWall(Tile tile)
            => true;

        public override void Upgrade(MALevel incomeLevel)
        {
            switch (incomeLevel)
            {
                default:
                    ProductionDelayBase = 1_0000_0000 / 60;//随便填个大数
                    ThroughputBase = 1;
                    break;
                case MALevel.Eiderdown:
                    ProductionDelayBase = 10;
                    ThroughputBase = 2;
                    break;
                case MALevel.Flight:
                    ProductionDelayBase = 8;
                    ThroughputBase = 48;
                    break;
                case MALevel.Feather:
                    ProductionDelayBase = 7;
                    ThroughputBase = 56;
                    break;
            }

            ProductionDelayBase *= 60;
            Timer = ProductionDelayBase;
        }
    }
}
