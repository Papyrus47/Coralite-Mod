﻿using Coralite.Content.Tiles.Plants;
using Coralite.Core;
using Coralite.Helpers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Items
{
    internal class BotnicalStaff2 : ModItem
    {
        public override string Texture => AssetDirectory.DefaultItem;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("植物魔杖宽2");

            Tooltip.SetDefault("使植物生长状态+1");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
        }

        public override bool? UseItem(Player player)
        {
            Point point= Main.MouseWorld.ToTileCoordinates();
            int i = point.X;
            int j = point.Y;
            Tile tile = Framing.GetTileSafely(i, j);
            if (BotanicalHelper.TryGetTileEntityAs(i, j, out NormalPlantTileEntity plantEntity))
            {
                Main.NewText(plantEntity.growTime);
                Main.NewText(plantEntity.DominantGrowTime);
                Main.NewText(plantEntity.RecessiveGrowTime);
                plantEntity.growTime = plantEntity.DominantGrowTime;
                if (plantEntity.growTime >= plantEntity.DominantGrowTime)
                {
                    plantEntity.growTime = 0;
                    tile.TileFrameX += 34;
                    if (Main.netMode != NetmodeID.SinglePlayer)
                        NetMessage.SendTileSquare(-1, i, j, 1);
                }
            }
            return true;
        }
    }
}
