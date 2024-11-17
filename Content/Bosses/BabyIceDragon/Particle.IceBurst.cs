﻿using Coralite.Core;
using Coralite.Core.Systems.ParticleSystem;

namespace Coralite.Content.Bosses.BabyIceDragon
{
    public class IceBurstParticle : Particle
    {
        public override string Texture => AssetDirectory.BabyIceDragon + "IceBurst";

        public override void SetProperty()
        {
            Color = Color.White;
            Rotation = 0f;
            Frame = new Rectangle(0, 0, 128, 128);
            ShouldKillWhenOffScreen = false;
        }

        public override void AI()
        {
            if (fadeIn % 2 == 0)
                Frame.Y = (int)(fadeIn / 2) * 128;

            fadeIn++;

            if (fadeIn > 16)
                active = false;
        }
    }
}
