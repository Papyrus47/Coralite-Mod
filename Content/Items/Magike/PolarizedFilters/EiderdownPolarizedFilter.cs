﻿using Coralite.Core.Systems.MagikeSystem;
using Coralite.Core.Systems.MagikeSystem.BaseItems;
using Coralite.Core.Systems.MagikeSystem.Components;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Magike.PolarizedFilters
{
    public class EiderdownPolarizedFilter : PolarizedFilterItem
    {
        public override Color FilterColor => Color.CornflowerBlue;

        public EiderdownPolarizedFilter() : base(Item.sellPrice(0, 0, 50), ItemRarityID.Blue)
        {
        }

        public override MagikeFilter GetFilterComponent() => new EiderdownPolarizedFilterComponent();

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<BasicFilter>()
                .AddIngredient(ItemID.Feather, 5)
                .DisableDecraft()
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }

    public class EiderdownPolarizedFilterComponent : PolarizedFilter
    {
        public override MagikeApparatusLevel Level => MagikeApparatusLevel.Eiderdown;

        public override int ItemType => ModContent.ItemType<EiderdownPolarizedFilter>();
    }
}
