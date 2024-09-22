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
    public class HellLens() : MagikeApparatusItem(TileType<HellLensTile>(), Item.sellPrice(silver: 5)
        , RarityType<MagicCrystalRarity>(), AssetDirectory.MagikeLens)
    {
        public override bool CanUseItem(Player player)
        {
            return player.ZoneUnderworldHeight;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Basalt>(10)
                .AddIngredient(ItemID.AshBlock, 10)
                .AddCondition(CoraliteConditions.LearnedMagikeBase)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class HellLensTile() : BaseLensTile
        (Color.DarkRed, DustID.Torch)
    {
        public override int DropItemType => ItemType<HellLens>();

        public override int[] GetAnchorValidTiles()
        {
            return
            [
                TileID.Ash,TileID.AshGrass,TileID.Hellstone,TileID.HellstoneBrick,TileID.AncientHellstoneBrick,TileID.ObsidianBrick
            ];
        }

        public override MagikeTileEntity GetEntityInstance() => GetInstance<HellLensTileEntity>();

        public override MagikeApparatusLevel[] GetAllLevels()
        {
            return
            [
                MagikeApparatusLevel.None,
                MagikeApparatusLevel.Hellstone,
                MagikeApparatusLevel.EternalFlame,
            ];
        }
    }

    public class HellLensTileEntity : BaseActiveProducerTileEntity<HellLensTile>
    {
        public override MagikeContainer GetStartContainer()
            => new HellLensContainer();

        public override MagikeLinerSender GetStartSender()
            => new HellLensSender();

        public override MagikeActiveProducer GetStartProducer()
            => new HellProducer();
    }

    public class HellLensContainer : UpgradeableContainer
    {
        public override void Upgrade(MagikeApparatusLevel incomeLevel)
        {
            switch (incomeLevel)
            {
                default:
                    MagikeMaxBase = 0;
                    AntiMagikeMaxBase = 0;
                    break;
                case MagikeApparatusLevel.Hellstone:
                    MagikeMaxBase = 9;
                    AntiMagikeMaxBase = MagikeMaxBase * 3;
                    break;
                case MagikeApparatusLevel.EternalFlame:
                    MagikeMaxBase = 562;
                    AntiMagikeMaxBase = MagikeMaxBase * 2;
                    break;
            }

            LimitMagikeAmount();
            LimitAntiMagikeAmount();
        }
    }

    public class HellLensSender : UpgradeableLinerSender
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
                case MagikeApparatusLevel.Hellstone:
                    UnitDeliveryBase = 3;
                    SendDelayBase = 10;
                    break;
                case MagikeApparatusLevel.EternalFlame:
                    UnitDeliveryBase = 150;
                    SendDelayBase = 8;
                    break;
            }

            SendDelayBase *= 60;
            RecheckConnect();
        }
    }

    public class HellProducer : UpgradeableProducerByBiome
    {
        public override MagikeSystem.UITextID ApparatusName()
            => MagikeSystem.UITextID.HellLensName;

        public override MagikeSystem.UITextID ProduceCondition()
            => MagikeSystem.UITextID.HellCondition;

        public override bool CheckTile(Tile tile)
            => tile.TileType is TileID.Ash or TileID.AshGrass or TileID.Hellstone or TileID.HellstoneBrick or TileID.AncientHellstoneBrick or TileID.ObsidianBrick;

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
                case MagikeApparatusLevel.Hellstone:
                    ProductionDelayBase = 10;
                    ThroughputBase = 1;
                    break;
                case MagikeApparatusLevel.EternalFlame:
                    ProductionDelayBase = 8;
                    ThroughputBase = 50;
                    break;
            }

            ProductionDelayBase *= 60;
            Timer = ProductionDelayBase;
        }
    }
}
