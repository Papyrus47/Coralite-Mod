﻿using Coralite.Content.Items.Materials;
using Coralite.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.TheHyacinthSeries
{
    public class EternalBloom : ModItem
    {
        public override string Texture => AssetDirectory.TheHyacinthSeriesItems + Name;

        public override void SetDefaults()
        {
            Item.damage = 58;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.knockBack = 5.5f;
            Item.shootSpeed = 14f;

            Item.useStyle = ItemUseStyleID.Rapier;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.shoot = ProjectileType<WoodWaxHeldProj>();
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = CoraliteSoundID.Gun3_Item41;

            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Projectile.NewProjectile(new EntitySource_ItemUse(player, Item), player.Center, Vector2.Zero, ProjectileType<EternalBloomHeldProj>(), 0, knockback, player.whoAmI);
                if (type == ProjectileID.Bullet)
                {
                    type = Main.rand.NextBool(4) ? ProjectileType<ThornBall>(): ProjectileType<SeedPlantera>();

                    int index = Projectile.NewProjectile(source, player.Center, velocity
                         , type, damage, knockback, player.whoAmI);
                    return false;
                }

                return true;
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<Floette>()
            .AddIngredient<RegrowthTentacle>( 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}
