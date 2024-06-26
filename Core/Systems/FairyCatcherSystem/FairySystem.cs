﻿using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader.Core;

namespace Coralite.Core.Systems.FairyCatcherSystem
{
    public class FairySystem : ModSystem, ILocalizedModType
    {
        //-----------------------------------
        //         个体值评级的本地化
        //-----------------------------------
        public static LocalizedText WeakLevel;
        public static LocalizedText VeryCommonLevel;
        public static LocalizedText CommonLevel;
        public static LocalizedText UncommonLevel;
        public static LocalizedText RareLevel;
        public static LocalizedText SpecialLevel;
        public static LocalizedText UniqueLevel;
        public static LocalizedText TimelessLevel;

        //-----------------------------------
        //         个体描述的本地化
        //-----------------------------------
        public static LocalizedText fairyLifeMax;
        public static LocalizedText fairyDamage;
        public static LocalizedText fairyDefence;
        public static LocalizedText fairyScale;


        /// <summary>
        /// 键值是墙壁的type，-1表示没有墙壁
        /// </summary>
        public static Dictionary<int, List<FairySpawnCondition>> fairySpawnConditions;
        /// <summary>
        /// 键值是仙灵的种类，这个是用来在仙灵百科全书中使用的
        /// </summary>
        public static Dictionary<int, FairySpawnCondition> fairySpawnConditions_InEncyclopedia;

        public string LocalizationCategory => "Systems";

        public override void Load()
        {
            Mod Mod = Coralite.Instance;

            fairySpawnConditions = new Dictionary<int, List<FairySpawnCondition>>();
            fairySpawnConditions_InEncyclopedia = new Dictionary<int, FairySpawnCondition>();

            foreach (Type t in AssemblyManager.GetLoadableTypes(Mod.Code))  //添加生成条件
            {
                if (!t.IsAbstract && t.IsSubclassOf(typeof(Fairy)))
                {
                    Fairy fairy = Activator.CreateInstance(t) as Fairy;
                    fairy.RegisterSpawn();
                }
            }

            foreach (var mod in ModLoader.Mods)
                if (mod is ICoralite)
                    foreach (Type t in AssemblyManager.GetLoadableTypes(mod.Code))  //添加生成条件
                    {
                        if (!t.IsAbstract && t.IsSubclassOf(typeof(Fairy)))
                        {
                            Fairy fairy = Activator.CreateInstance(t) as Fairy;
                            fairy.RegisterSpawn();
                        }
                    }
        }

        public override void Unload()
        {
            fairySpawnConditions = null;
        }

        public static bool SpawnFairy(FairyAttempt attempt, out Fairy fairy)
        {
            fairy = null;

            if (fairySpawnConditions.ContainsKey(attempt.wallType) && fairySpawnConditions[attempt.wallType] != null)
            {
                List<FairySpawnCondition> currentCondition = new List<FairySpawnCondition>();
                List<FairySpawnCondition> totalCondition = fairySpawnConditions[attempt.wallType];
                foreach (var condition in totalCondition)
                    if (condition.CheckCondition(attempt))
                        currentCondition.Add(condition);

                if (currentCondition.Count == 0)
                    return false;

                fairy = Main.rand.NextFromList(currentCondition.ToArray()).SpawnFairy();
            }

            return false;
        }
    }
}
