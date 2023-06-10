using Coralite.Core;
using Coralite.Core.Prefabs.Items;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Weapons_Shoot
{
    public class AncientCore : BaseMaterial
    {
        public AncientCore() : base(9999, Item.sellPrice(1, 0, 0, 0), ItemRarityID.Yellow, AssetDirectory.Weapons_Shoot)
        { }
    }
}