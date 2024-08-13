﻿using Coralite.Helpers;
using Terraria;

namespace Coralite.Core.Systems.MagikeSystem.Components
{
    public abstract class MagikeCostItemProducer : MagikeActiveProducer
    {
        private int _index;

        /// <summary>
        /// 是否能消耗物品
        /// </summary>
        /// <returns></returns>
        public abstract bool CanConsumeItem(Item item);

        /// <summary>
        /// 获取具体生产的魔能量
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract int GetMagikeAmount(Item item);

        public override bool CanProduce()
        {
            if (!Entity.HasComponent(MagikeComponentID.ItemContainer))
                return false;

            Item[] items = ((ItemContainer)Entity.GetSingleComponent(MagikeComponentID.ItemContainer)).Items;

            for (int i = 0; i < items.Length; i++)
            {
                Item item = items[i];
                if (item == null || item.IsAir)
                    continue;

                if (CanConsumeItem(item))
                {
                    _index = i;
                    return true;
                }
            }

            _index = -1;
            return false;
        }

        public override void Produce()
        {
            Item item = ((ItemContainer)Entity.GetSingleComponent(MagikeComponentID.ItemContainer)).Items[_index];

            Entity.GetMagikeContainer().AddMagike(GetMagikeAmount(item));

            item.stack--;
            if (item.stack <= 0)
                item.TurnToAir();
        }
    }
}
