﻿using Coralite.Core.Prefabs.Items;
using Coralite.Core.Prefabs.Projectiles;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Accessories
{
    public class StretchGlue: BaseAccessory,IFlyingShieldAccessory
    {
        public StretchGlue() : base(ItemRarityID.Blue, Item.sellPrice(0,0,10))
        { }

       public void OnInitialize(BaseFlyingShield projectile)
        {
            projectile.maxJump++;
        }
    }
}
