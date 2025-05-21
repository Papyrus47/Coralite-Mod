using Terraria;
using Terraria.DataStructures;

namespace Coralite.Core.Systems.YujianSystem
{
    public class YujianSource : EntitySource_ItemUse
    {
        public BaseYujian Yujian => Item.ModItem as BaseYujian;
        public BaseYujianProj yujianProj;
        public YujianSource(Player player, Item item,BaseYujianProj baseYujian = null, string context = null) : base(player, item, context)
        {
            yujianProj = baseYujian;
        }
    }
}
