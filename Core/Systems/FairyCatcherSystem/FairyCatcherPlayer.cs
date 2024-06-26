﻿using Coralite.Core.Systems.FairyCatcherSystem.Bases;
using Terraria;

namespace Coralite.Core.Systems.FairyCatcherSystem
{
    public class FairyCatcherPlayer:ModPlayer
    {
        public struct FairyIVRandomModifyer
        {
            public float base_Min;
            public float base_Max;
            public float Flat_Min;
            public float Flat_Max;
            public float additive_Min;
            public float additive_Max;
            public float multiplicative_Min;
            public float multiplicative_Max;
        }

        /// <summary>
        /// 仙灵捕捉器指针的大小，用于对于其进行缩放<br></br>
        /// 捕捉器默认大小由贴图决定，查看<see cref="BaseFairyCatcherProj.GetCursorBox"/>以了解更多
        /// </summary>
        public StatModifier cursorSizeBonus;
        /// <summary>
        /// 仙灵捕捉器的捕捉力增幅
        /// </summary>
        public StatModifier fairyCatchPowerBonus;
        public float fairyCatcherRadius;

        /// <summary>
        /// 每次生成仙灵时会生成多少个
        /// </summary>
        public int spawnFairyCount;

        public FairyIVRandomModifyer damageRamdom;
        public FairyIVRandomModifyer defenceRamdom;
        public FairyIVRandomModifyer lifeMaxRamdom;
        public (float, float) ScaleRange;

        public Color CatcherCircleColor;
        public Color CatcherBackColor;

        /// <summary>
        /// 仙灵伤害加成，使用仙灵捕获力增强幅度来增幅伤害
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public float FairyDamageBonus(float damage)
        {
            return fairyCatchPowerBonus.ApplyTo(damage);
        }

        public override void ResetEffects()
        {
            cursorSizeBonus = new StatModifier();
            fairyCatchPowerBonus = new StatModifier();

            //默认伤害区间为0.8-1.2
            damageRamdom = new FairyIVRandomModifyer()
            {
                additive_Min = 0.8f,
                additive_Max = 1.2f
            };

            //默认防御区间为0.8-1.15
            defenceRamdom = new FairyIVRandomModifyer()
            {
                additive_Min = 0.9f,
                additive_Max = 1.15f
            };

            //默认血量区间为0.8-1.3
            lifeMaxRamdom = new FairyIVRandomModifyer()
            {
                additive_Min = 0.8f,
                additive_Max = 1.3f
            };

            //默认大小区间0.9-1.1
            ScaleRange = (0.9f, 1.1f);

            spawnFairyCount = 1;
            fairyCatcherRadius = 7 * 16;

            CatcherCircleColor = Color.White;
            CatcherBackColor = Color.DarkBlue * 0.5f;
        }

        /// <summary>
        /// 遍历玩家背包获取仙灵饵料
        /// </summary>
        /// <param name="bait"></param>
        public void FairyCatch_GetBait(out Item bait)
        {
            bait = null;
            for (int i = 54; i < 58; i++)
            {
                if (Player.inventory[i].stack > 0 && Player.inventory[i].bait > 0)
                {
                    bait = Player.inventory[i];
                    break;
                }
            }

            if (bait != null)
                return;

            for (int j = 0; j < 50; j++)
            {
                if (Player.inventory[j].stack > 0 && Player.inventory[j].bait > 0)
                {
                    bait = Player.inventory[j];
                    break;
                }
            }
        }

        public bool FairyShoot_GetFairyBottle(out IFairyBottle bait)
        {
            bait = null;

            for (int j = 0; j < 50; j++)
            {
                if (Player.inventory[j].stack > 0 && Player.inventory[j].ModItem is IFairyBottle fairyBottle)
                {
                    bait = fairyBottle;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取仙灵捕捉力，默认使用玩家手持物品进行计算，如果手持物品不是仙灵捕手则返回一个增幅倍率
        /// </summary>
        /// <returns></returns>
        public int FairyCatch_GetCatchPower()
        {
            int basePower = 1;
            if (Player.HeldItem.ModItem is BaseFairyCatcher catcher)
                basePower = catcher.catchPower;

            return (int)fairyCatchPowerBonus.ApplyTo(basePower);
        }

        public override bool OnPickup(Item item)
        {


            return base.OnPickup(item);
        }

        /// <summary>
        /// 随机仙灵的个体值
        /// </summary>
        /// <param name="faityItem"></param>
        /// <returns></returns>
        public FairyData RollFairyIndividualValues(BaseFairyItem faityItem)
        {
            FairyData data = new FairyData();

            data.damageBonus = new StatModifier(
                Main.rand.NextFloat(damageRamdom.additive_Min, damageRamdom.additive_Max),
                Main.rand.NextFloat(damageRamdom.multiplicative_Min, damageRamdom.multiplicative_Max),
                Main.rand.NextFloat(damageRamdom.additive_Min, damageRamdom.additive_Max),
                Main.rand.NextFloat(damageRamdom.additive_Min, damageRamdom.additive_Max)
                );
            data.defenceBonus = new StatModifier(
                Main.rand.NextFloat(defenceRamdom.additive_Min, defenceRamdom.additive_Max),
                Main.rand.NextFloat(defenceRamdom.multiplicative_Min, defenceRamdom.multiplicative_Max),
                Main.rand.NextFloat(defenceRamdom.additive_Min, defenceRamdom.additive_Max),
                Main.rand.NextFloat(defenceRamdom.additive_Min, defenceRamdom.additive_Max)
                );
            data.lifeMaxBonus = new StatModifier(
                Main.rand.NextFloat(lifeMaxRamdom.additive_Min, lifeMaxRamdom.additive_Max),
                Main.rand.NextFloat(lifeMaxRamdom.multiplicative_Min, lifeMaxRamdom.multiplicative_Max),
                Main.rand.NextFloat(lifeMaxRamdom.additive_Min, lifeMaxRamdom.additive_Max),
                Main.rand.NextFloat(lifeMaxRamdom.additive_Min, lifeMaxRamdom.additive_Max)
                );

            data.scaleBonus=Main.rand.NextFloat(ScaleRange.Item1,ScaleRange.Item2);

            return default;
        }
    }
}
