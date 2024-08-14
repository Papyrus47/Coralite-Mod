﻿using Coralite.Content.UI.MagikeApparatusPanel;
using Coralite.Core.Loaders;
using Coralite.Core.Systems.CoraliteActorComponent;
using Coralite.Core.Systems.MagikeSystem.TileEntities;
using Coralite.Helpers;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;
using Terraria.ModLoader.UI.Elements;
using Terraria.UI;

namespace Coralite.Core.Systems.MagikeSystem.Components
{
    public class ItemContainer : Component, IUIShowable
    {
        public override int ID => MagikeComponentID.ItemContainer;

        private int _capacityBase;

        /// <summary> 基础容量，不能小于1 </summary>
        public int CapacityBase
        {
            get => _capacityBase;
            set
            {
                if (value < 1)
                    _capacityBase = 1;
                else
                    _capacityBase = value;
            }
        }

        /// <summary> 额外容量 </summary>
        public int CapacityExtra { get; set; }

        /// <summary> 容量 </summary>
        public int Capacity => CapacityBase + CapacityExtra;

        private Item[] _items;
        public Item[] Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new Item[Capacity];
                    FillItemArray();
                }

                return _items;
            }
        }

        public Item this[int index]
        {
            get
            {
                if (Items.IndexInRange(index))
                    return Items[index];

                return null;
            }

            set
            {
                if (Items.IndexInRange(index))
                    Items[index] = value;
            }
        }

        public override void Update(IEntity entity) { }

        /// <summary>
        /// 修改容量后必须调用这个方法！
        /// </summary>
        public void ResetCapacity()
        {
            Vector2 worldPos = (Entity as MagikeTileEntity).Position.ToWorldCoordinates();
            var source = new EntitySource_TileEntity(Entity as MagikeTileEntity);

            //超出容量的部分生成掉落物
            for (int i = Capacity; i < Items.Length; i++)
            {
                Item item = Items[i];
                if (item != null && !item.IsAir)
                    Item.NewItem(source, worldPos, item);
            }

            Array.Resize(ref _items, Capacity);
        }

        /// <summary>
        /// 填充物品数组让它不是null
        /// </summary>
        public void FillItemArray()
        {
            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i] == null)
                    Items[i] = new Item();
            }
        }

        public override void OnRemove(IEntity entity)
        {
            Point16 coord = (entity as MagikeTileEntity).Position;
            Vector2 pos = Helper.GetMagikeTileCenter(coord);
            for (int i = 0; i < Items.Length; i++)
                Item.NewItem(new EntitySource_TileBreak(coord.X, coord.Y), pos, Items[i]);
        }

        #region UI部分

        public void ShowInUI(UIElement parent)
        {
            UIElement title = this.AddTitle(MagikeSystem.UITextID.ItemContainerName, parent);

            UIGrid grid = new()
            {
                OverflowHidden = false
            };

            for (int i = 0; i < Items.Length; i++)
            {
                ItemContainerSlot slot = new(this, i);
                grid.Add(slot);
            }

            grid.SetSize(0, -title.Height.Pixels, 1, 1);
            grid.SetTopLeft(title.Height.Pixels+8, 0);

            parent.Append(grid);
        }

        #endregion

        #region 存储与加载部分

        public override void SaveData(string preName, TagCompound tag)
        {
            tag.Add(preName + nameof(CapacityBase), CapacityBase);
            tag.Add(preName + nameof(CapacityExtra), CapacityExtra);

            for (int i = 0; i < Items.Length; i++)
            {
                if (Items[i].IsAir)
                    continue;

                tag.Add(preName + nameof(_items) + i, Items[i]);
            }
        }

        public override void LoadData(string preName, TagCompound tag)
        {
            CapacityBase = tag.GetInt(preName + nameof(CapacityBase));
            CapacityExtra = tag.GetInt(preName + nameof(CapacityExtra));

            _items = new Item[Capacity];
            for (int i = 0; i < Items.Length; i++)
            {
                if (tag.TryGet(preName + nameof(_items) + i, out Item item))
                    _items[i] = item;
                else
                    _items[i] = new Item();
            }
        }

        #endregion
    }

    public class ItemContainerSlot:UIElement 
    {
        private readonly ItemContainer _container;
        private readonly int _index;
        private float _scale=1f;

        public ItemContainerSlot(ItemContainer container, int index)
        {
            _container = container;
            _index = index;
            this.SetSize(54, 54);
        }

        public bool TryGetItem(out Item item)
        {
            item = _container[_index];
            if (item == null)
            {
                UILoader.GetUIState<MagikeApparatusPanel>().Recalculate();
                return false;
            }

            return true;
        }

        //public void GrabSound()
        //{
        //    Helper.PlayPitched("Fairy/FairyBottleClick", 0.4f, 0);
        //}

        private void HandleItemSlotLogic()
        {
            if (IsMouseHovering)
            {
                Item inv = _container[_index];
                Main.LocalPlayer.mouseInterface = true;
                ItemSlot.OverrideHover(ref inv, ItemSlot.Context.VoidItem);
                ItemSlot.LeftClick(ref inv, ItemSlot.Context.VoidItem);
                ItemSlot.RightClick(ref inv, ItemSlot.Context.VoidItem);
                ItemSlot.MouseHover(ref inv, ItemSlot.Context.VoidItem);
                _container[_index] = inv;
                _scale = Helper.Lerp(_scale, 1.1f, 0.2f);
            }
            else
                _scale = Helper.Lerp(_scale, 1f, 0.2f);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!TryGetItem(out _))
                return;

            HandleItemSlotLogic();

            float scale = Main.inventoryScale;
            Main.inventoryScale = _scale;

            Item inv = _container[_index];
            Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
            ItemSlot.Draw(spriteBatch, ref inv, ItemSlot.Context.VoidItem, position,Coralite.MagicCrystalPink);

            Main.inventoryScale = scale;
        }
    }
}
