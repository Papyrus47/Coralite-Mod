using Coralite.Content.Items.Shadow;
using Coralite.Content.Projectiles.Projectiles_Shoot;
using Coralite.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Weapons_Shoot
{
    /// <summary>
    /// Arethusa：兰花的意思，但是也是希腊神话中阿瑞塞莎的名字，具体神话传说建议百度，另外这个我是在机翻时偶然发现的。
    /// 另外这把武器叫：幽兰
    /// </summary>
    public class Arethusa : ModItem
    {
        public override string Texture => AssetDirectory.Weapons_Shoot + Name;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ToolTipDamageMultiplier[Type] = 0.7f;
        }

        public int shootCount;

        public override void SetDefaults()
        {
            Item.damage = 39;
            Item.useTime = 26;
            Item.useAnimation = 26;
            Item.knockBack = 6;
            Item.shootSpeed = 10f;

            Item.useStyle = ItemUseStyleID.Rapier;
            Item.DamageType = DamageClass.Ranged;
            Item.value = Item.sellPrice(0, 0, 40, 0);
            Item.rare = ItemRarityID.Expert;
            Item.shoot = ProjectileType<ArethusaHeldProj>();
            Item.useAmmo = AmmoID.Bullet;

            Item.useTurn = false;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
        }

        public override bool CanUseItem(Player player)
        {
            shootCount++;
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.myPlayer == player.whoAmI)
            {
                Projectile.NewProjectile(source, player.Center, Vector2.Zero, ProjectileType<ArethusaHeldProj>(), damage, knockback, player.whoAmI);
                if (shootCount > 3)
                {
                    Vector2 targetDir = (Main.MouseWorld - player.Center).SafeNormalize(Vector2.Zero);
                    Projectile.NewProjectile(source, player.Center, targetDir * 14, ProjectileType<ArethusaBullet>(), (int)(damage * 1.35f), knockback, player.whoAmI);
                    SoundEngine.PlaySound(CoraliteSoundID.NoUse_SuperMagicShoot_Item68, player.Center);
                    shootCount = 0;
                    return false;
                }

                SoundEngine.PlaySound(CoraliteSoundID.Gun_Item11, player.Center);
                return true;
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<InvertedShadow>()
            .AddIngredient<WoodWax>()
            .AddIngredient(ItemID.Musket)
            .AddIngredient(ItemID.PhoenixBlaster)
            .AddIngredient(ItemID.Moonglow,5)
            .AddTile(TileID.Anvils)
            .Register();

            CreateRecipe()
            .AddIngredient<InvertedShadow>()
            .AddIngredient<WoodWax>()
            .AddIngredient(ItemID.TheUndertaker)
            .AddIngredient(ItemID.PhoenixBlaster)
            .AddIngredient(ItemID.Moonglow, 5)
            .AddTile(TileID.Anvils)
            .Register();
        }
    }
}