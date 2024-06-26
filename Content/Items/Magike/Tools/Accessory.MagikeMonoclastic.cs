﻿using Coralite.Content.ModPlayers;
using Coralite.Content.Raritys;
using Coralite.Core;
using Terraria;

namespace Coralite.Content.Items.Magike.Tools
{
    [AutoloadEquip(EquipType.Face)]
    public class MagikeMonoclastic : ModItem
    {
        public override string Texture => AssetDirectory.MagikeTools + Name;

        public override void SetDefaults()
        {
            Item.accessory = true;
            Item.maxStack = 1;
            Item.rare = ModContent.RarityType<MagicCrystalRarity>();

            Item.value = Item.sellPrice(0, 0, 10, 0);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (hideVisual)
            {
                return;
            }
            if (player.TryGetModPlayer(out MagikePlayer mp))
            {
                mp.equippedMagikeMonoclastic = true;
            }
        }
    }
}
