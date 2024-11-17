﻿using Coralite.Core;
using Coralite.Core.Systems.ParticleSystem;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Linq;
using Terraria;

namespace Coralite.Content.Bosses.Rediancie
{
    public class RedShield : Particle
    {
        public override string Texture => AssetDirectory.Rediancie + "RedShield";
        public static Asset<Texture2D> flowTex;

        private Entity rediancie;
        private bool toFadeOut = false;
        private bool Init = true;

        public override void Load()
        {
            if (Main.dedServ)
            {
                return;
            }
            flowTex = ModContent.Request<Texture2D>(AssetDirectory.Rediancie + "RedShield_Flow");
        }

        public override void Unload()
        {
            flowTex = null;
        }

        public override bool ShouldUpdateCenter() => false;

        public override void SetProperty()
        {
            Color = Coralite.RedJadeRed;
            Color.A = 0;
            Rotation = Main.rand.NextFloat(6.282f);
            Scale = 0f;
            ShouldKillWhenOffScreen = false;
        }

        public override void AI()
        {
            Rotation += 0.06f;

            if (Init)
            {
                Scale += 0.05f;
                Color.A += 255 / 16;
                if (Scale > 0.8f)
                {
                    Scale = 0.8f;
                    Color.A = 255;
                    Init = false;
                }
            }

            if (rediancie != null)
                Position = rediancie.Center;

            fadeIn--;

            if (fadeIn < 0)
                toFadeOut = true;

            if (toFadeOut)
            {
                Color.A -= 255 / 16;
                if (Color.A < 10)
                    active = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = this.Position - Main.screenPosition;
            Texture2D mainTex = TexValue;
            spriteBatch.Draw(mainTex, center, null, Color, Rotation, mainTex.Size() / 2, Scale, SpriteEffects.None, 0);

            float extraRot1 = Rotation + (fadeIn * 0.1f);
            float extraRot2 = Rotation + (fadeIn * 0.05f);
            Vector2 flowOrigin = flowTex.Size() / 2;

            spriteBatch.Draw(flowTex.Value, center, null, Color, extraRot1, flowOrigin, Scale - 0.1f, SpriteEffects.None, 0);
            spriteBatch.Draw(flowTex.Value, center, null, new Color(255, 255, 255, Color.A * 3 / 4), extraRot1 + extraRot2, flowOrigin, Scale - 0.2f, SpriteEffects.FlipHorizontally, 0);
            spriteBatch.Draw(flowTex.Value, center, null, Color, extraRot2 + 3.141f, flowOrigin, Scale - 0.2f, SpriteEffects.FlipHorizontally, 0);
        }

        public static void Spawn(Entity rediancie, int maxTime)
        {
            RedShield particle = NewParticle<RedShield>(rediancie.Center, Vector2.Zero);
            particle.rediancie = rediancie;
            particle.fadeIn = maxTime;
        }

        public static void Kill()
        {
            int type = CoraliteContent.ParticleType<RedShield>();
            foreach (var particle in ParticleSystem.Particles.Where(p => p.active && p.ID == type))
            {
                particle.fadeIn = -1;
            }
        }
    }
}
