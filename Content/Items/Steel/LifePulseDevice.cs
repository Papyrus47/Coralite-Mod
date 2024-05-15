﻿using Coralite.Content.ModPlayers;
using Coralite.Core;
using Coralite.Core.Prefabs.Items;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Steel
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class LifePulseDevice : BaseAccessory
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public LifePulseDevice() : base(ItemRarityID.LightRed, Item.sellPrice(0, 2, 0, 0))
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
                cp.equippedLifePulseDevice = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>(8)
                .AddIngredient(ItemID.DeathweedSeeds)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
