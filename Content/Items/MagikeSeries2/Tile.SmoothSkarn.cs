﻿using Coralite.Content.Tiles.MagikeSeries2;
using Coralite.Core;
using Terraria.ID;

namespace Coralite.Content.Items.MagikeSeries2
{
    public class SmoothSkarn : ModItem
    {
        public override string Texture => AssetDirectory.MagikeSeries2Item + Name;

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<SmoothSkarnTile>());
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<Skarn>(2)
                .AddTile(TileID.HeavyWorkBench)
                .Register();
        }
    }
}
