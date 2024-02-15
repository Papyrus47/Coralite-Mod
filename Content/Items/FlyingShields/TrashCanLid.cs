﻿using Coralite.Core;
using Coralite.Core.Prefabs.Items;
using Coralite.Core.Prefabs.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Items.FlyingShields
{
    public class TrashCanLid : BaseFlyingShieldItem<TrashCanLidGuard>
    {
        public TrashCanLid() : base(Item.sellPrice(0, 0, 0, 10), ItemRarityID.White, AssetDirectory.FlyingShieldItems)
        {
        }

        public override void SetDefaults2()
        {
            Item.useTime = Item.useAnimation = 15;
            Item.shoot = ModContent.ProjectileType<TrashCanLidProj>();
            Item.shootSpeed = 12;
            Item.damage = 15;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    public class TrashCanLidProj : BaseFlyingShield
    {
        public override string Texture => AssetDirectory.FlyingShieldItems + "TrashCanLid";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = Projectile.height = 30;
        }

        public override void SetOtherValues()
        {
            flyingTime = 20;
            backTime = 20;
            backSpeed = 12;
            trailCachesLength = 5;
            trailWidth = 24/2;
        }

        public override void OnShootDusts()
        {
            extraRotation += 0.4f;
        }

        public override void OnBackDusts()
        {
            extraRotation += 0.4f;
        }

        public override Color GetColor(float factor)
        {
            return Color.Gray * factor;
        }
    }

    public class TrashCanLidGuard : BaseFlyingShieldGuard
    {
        public override string Texture => AssetDirectory.FlyingShieldItems + "TrashCanLid";

        public override void SetDefaults()
        {
            base.SetDefaults();
            Projectile.width = Projectile.height = 30;
        }

        public override void SetOtherValues()
        {
            DamageReduce = 0.05f;
        }
    }
}
