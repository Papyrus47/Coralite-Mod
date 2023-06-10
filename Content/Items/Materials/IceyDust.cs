﻿using Coralite.Core;
using Coralite.Core.Prefabs.Items;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Materials
{
    public class IceyDust : BaseMaterial
    {
        public IceyDust() : base( 9999, Item.sellPrice(0, 0, 1, 50), ItemRarityID.Green, AssetDirectory.Materials) { }
    }
}
