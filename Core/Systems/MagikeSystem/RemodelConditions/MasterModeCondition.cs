﻿using System;
using Terraria;

namespace Coralite.Core.Systems.MagikeSystem.RemodelConditions
{
    public class MasterModeCondition : IMagikeRemodelCondition
    {
        private static readonly Lazy<MasterModeCondition> singleton = new Lazy<MasterModeCondition>(() => new MasterModeCondition());
        public static MasterModeCondition Instance { get => singleton.Value; }

        public string Description => "大师模式以上可重塑";

        public bool CanRemodel() => Main.masterMode;
    }
}
