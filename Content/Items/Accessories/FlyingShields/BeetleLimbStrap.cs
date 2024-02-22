﻿using Coralite.Content.ModPlayers;
using Coralite.Core.Prefabs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace Coralite.Content.Items.Accessories.FlyingShields
{
    public class BeetleLimbStrap : BaseAccessory
    {
        public BeetleLimbStrap() : base(ItemRarityID.Yellow, Item.sellPrice(0, 5))
        { }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !((equippedItem.type == ModContent.ItemType<ShieldbearersBand>()//下位
                || equippedItem.type == ModContent.ItemType<PowerliftExoskeleton>())//下位

                && incomingItem.type == ModContent.ItemType<BeetleLimbStrap>());
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
            {
                cp.MaxFlyingShield += 3;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<PowerliftExoskeleton>()
                .AddIngredient(ItemID.BeetleHusk, 4)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}