﻿using Coralite.Core.Systems.ParticleSystem;
using Terraria.ModLoader;

namespace Coralite.Core
{
    public static class CoraliteContent
    {
        /// <summary>
        /// 根据类型获取这个粒子的ID（type）。假设一个类一个实例。
        /// </summary>
        public static int ParticleType<T>() where T : ModParticle => ModContent.GetInstance<T>()?.Type ?? 0;

    }
}