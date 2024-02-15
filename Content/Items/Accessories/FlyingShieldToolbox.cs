﻿using Coralite.Core.Prefabs.Items;
using Coralite.Core.Prefabs.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Items.Accessories
{
    public class FlyingShieldToolbox : BaseAccessory, IFlyingShieldAccessory
    {
        public FlyingShieldToolbox() : base(ItemRarityID.Green, Item.sellPrice(0, 0, 20))
        { }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !(equippedItem.type==ModContent.ItemType<FlyingShieldToolboxProMax>()
                && incomingItem.type==ModContent.ItemType<FlyingShieldToolbox>());
        }

        public void OnGuardInitialize(BaseFlyingShieldGuard projectile)
        {
            projectile.DamageReduce *= 1.2f;
        }

        public void OnInitialize(BaseFlyingShield projectile)
        {
            projectile.maxJump++;
        }

        public void PostInitialize(BaseFlyingShield projectile)
        {
            projectile.shootSpeed += 2.5f / (projectile.Projectile.extraUpdates + 1);//加速
            projectile.backSpeed += 2.5f / (projectile.Projectile.extraUpdates + 1);
            if (projectile.shootSpeed > projectile.Projectile.width / (projectile.Projectile.extraUpdates + 1))
            {
                projectile.Projectile.extraUpdates++;
                projectile.shootSpeed /= 2;
                projectile.backSpeed /= 2;
                projectile.flyingTime *= 2;
                projectile.backTime *= 2;
                projectile.trailCachesLength *= 2;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<StretchGlue>()
                .AddIngredient<FlyingShieldVarnish>()
                .AddIngredient<FlyingShieldMaintenanceGuide>()
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
