﻿using Coralite.Content.ModPlayers;
using Coralite.Core.Prefabs.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Items.Accessories.FlyingShields
{
    public class ShieldbearersBand : BaseAccessory
    {
        public ShieldbearersBand() : base(ItemRarityID.Green, Item.sellPrice(0, 0, 20))
        { }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !((equippedItem.type == ModContent.ItemType<PowerliftExoskeleton>()//上位
                || equippedItem.type == ModContent.ItemType<BeetleLimbStrap>())//上位

                && incomingItem.type == ModContent.ItemType<ShieldbearersBand>());
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
            {
                cp.MaxFlyingShield++;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Leather)
                .AddIngredient(ItemID.ShadowScale, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.Leather)
                .AddIngredient(ItemID.TissueSample, 5)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
