﻿using Coralite.Core.Systems.CoraliteActorComponent;
using Coralite.Core.Systems.MagikeSystem.TileEntities;
using Coralite.Helpers;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace Coralite.Core.Systems.MagikeSystem.Components.Filters
{
    /// <summary>
    /// 全反射滤镜，增加魔能容量
    /// </summary>
    public abstract class TotalReflectionFilter : MagikeFilter, IUIShowable
    {
        public abstract float MagikeBonus { get; }

        public override bool CanInsert_SpecialCheck(MagikeTileEntity entity, ref string text)
        {
            if (!entity.HasComponent(MagikeComponentID.MagikeContainer))
            {
                text = MagikeSystem.GetFilterText(MagikeSystem.FilterID.MagikeContainerNotFound);
                return false;
            }

            return true;
        }

        public override void ChangeComponentValues(Component component)
        {
            if (component is MagikeContainer container)
            {
                container.MagikeMaxBonus += MagikeBonus;
            }
        }

        public override void RestoreComponentValues(Component component)
        {
            if (component is MagikeContainer container)
            {
                container.MagikeMaxBonus = 1f;

                float bonus = 0;

                foreach (MagikeFilter filter in ((List<Component>)(Entity.Components[MagikeComponentID.MagikeFilter])).Cast<MagikeFilter>())
                {
                    if (filter is TotalReflectionFilter trf)
                    {
                        if (filter == this)
                            continue;

                        bonus += trf.MagikeBonus;
                    }
                }

                container.MagikeMaxBonus += bonus;

                container.LimitMagikeAmount();
            }
        }

        #region UI部分

        public void ShowInUI(UIElement parent)
        {
            UIElement title = this.AddTitle(MagikeSystem.UITextID.TotalReflectionFilterName, parent);

            UIList list =
            [
                this.NewTextBar(c => 
                MagikeSystem.GetUIText(MagikeSystem.UITextID.TotalReflectionBonus)+MagikeBonus*100+"%", parent),
                new FilterText(c =>
                    $"  ▶ [i:{c.ItemType}]",this,parent),
                //取出按钮
                new FilterRemoveButton(Entity, this)
            ];

            list.SetSize(0, 0, 1, 1);
            list.SetTopLeft(title.Height.Pixels + 8, 0);

            list.QuickInvisibleScrollbar();

            parent.Append(list);
        }

        #endregion
    }
}
