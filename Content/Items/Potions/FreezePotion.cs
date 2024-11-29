using Coralite.Content.Items.Icicle;
using Coralite.Content.ModPlayers;
using Coralite.Core;
using Coralite.Core.Prefabs.Items;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Potions
{
    public class FreezePotion() : BasePotion(10 * 60 * 60, Item.sellPrice(silver: 10), ItemRarityID.Blue, AssetDirectory.PotionItems)
    {
        public override int BuffType => ModContent.BuffType<FreezePotionBuff>();

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.Shiverthorn, 2)
                .AddIngredient<IcicleBreath>()
                .AddTile(TileID.Bottles)
                .DisableDecraft()
                .Register();

            CreateRecipe(3)
                .AddIngredient(ItemID.BottledWater, 3)
                .AddIngredient(ItemID.Shiverthorn, 4)
                .AddIngredient<IcicleCrystal>()
                .AddTile(TileID.Bottles)
                .DisableDecraft()
                .Register();
        }
    }

    public class FreezePotionBuff : ModBuff
    {
        public override string Texture => AssetDirectory.PotionBuffs + Name;

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
            {
                cp.coldDamageBonus += 0.1f;
                player.statDefense -= 8;
            }
        }
    }
}
