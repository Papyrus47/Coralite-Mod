﻿using Coralite.Content.ModPlayers;
using Coralite.Core.Prefabs.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace Coralite.Content.Items.Accessories.FlyingShields
{
    public class PowerliftExoskeleton : BaseAccessory
    {
        public PowerliftExoskeleton() : base(ItemRarityID.Pink, Item.sellPrice(0, 1, 30))
        { }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            return !((equippedItem.type == ModContent.ItemType<ShieldbearersBand>()//下位
                || equippedItem.type == ModContent.ItemType<BeetleLimbStrap>())//上位

                && incomingItem.type == ModContent.ItemType<PowerliftExoskeleton>());
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
            {
                cp.MaxFlyingShield += 2;
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ShieldbearersBand>()
                .AddIngredient(ItemID.HallowedBar, 8)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
