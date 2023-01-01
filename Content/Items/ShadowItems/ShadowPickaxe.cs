﻿using Coralite.Core;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Items.ShadowItems
{
    public class ShadowPickaxe : ModItem
    {
        public override string Texture => AssetDirectory.ShadowItems + Name;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("影子镐");

            Tooltip.SetDefault("影子凝聚在这把稿子中");
        }

        public override void SetDefaults()
        {
            Item.height = Item.width = 40;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.damage = 6;
            Item.useTime = 16;
            Item.useAnimation = 20;
            Item.knockBack = 2f;
<<<<<<< HEAD
            Item.value = Item.sellPrice(0, 1, 50, 0);
=======
            Item.value = Item.sellPrice(0,1,50,0);
>>>>>>> 3e3a356d43305b33add95547cc02507bbb4a8d4b
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
<<<<<<< HEAD
            
=======

>>>>>>> 3e3a356d43305b33add95547cc02507bbb4a8d4b
            Item.pick = 100;
        }

        public override void AddRecipes()
        {
<<<<<<< HEAD
             CreateRecipe()
            .AddIngredient(ModContent.ItemType<ShadowCrystal>(), 8)
            .AddTile(TileID.Anvils)
            .Register();
=======
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ShadowCrystal>(), 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
>>>>>>> 3e3a356d43305b33add95547cc02507bbb4a8d4b
        }
    }
}
