using Coralite.Content.Items.FlyingShields;
using Coralite.Core.Prefabs.Projectiles;
using Coralite.Core.Systems.Trails;
using Coralite.Core.Systems.YujianSystem;
using Coralite.Core.Systems.YujianSystem.YujianAIs;
using Coralite.Helpers;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;

namespace Coralite.Content.Items.YujianHulu
{
    public class TaiJiYujian : BaseYujian
    {
        public TaiJiYujian() : base(ItemRarityID.LightRed, Item.sellPrice(0, 12, 40, 0), 32, 0.9f)
        {
        }

        public override int ProjType => ModContent.ProjectileType<TaiJiYujianProj>();
    }
    public class TaiJiYujianProj : BaseYujianProj
    {
        public TaiJiYujianProj() : base(
            yujianAIs: new YujianAI[]
            {
                new YujianAI_BetterSpurt(80,20,35,180,0.95f),
                new YujianAI_Slash(startTime: 180,
                        slashWidth: 150,
                        slashTime: 120,
                        startAngle: -3f,
                        totalAngle: 3f,
                        turnSpeed: 1.2f,
                        roughlyVelocity: 0.9f,
                        halfShortAxis: 1f,
                        halfLongAxis: 1.2f,
                        Coralite.Instance.HeavySmootherInstance),
            },
            yujianAIsRandom:new int[] { 2, 5 },
            //powerfulAI: new YujianAI_TaiJiSlash(startTime: 70,
            //        slashWidth: 90,
            //        slashTime: 40,
            //        startAngle: -4f,
            //        totalAngle: 3f,
            //        turnSpeed: 2.2f,
            //        roughlyVelocity: 0.9f,
            //        halfShortAxis: 1f,
            //        halfLongAxis: 1f,
            //        Coralite.Instance.HeavySmootherInstance),
            powerfulAI:null,
            PowerfulAttackCost: 200,
            attackLength: 400,
            width: 30,height: 58,
            color1: new Color(200,230,220), color2: new Color(10,5,9),
            trailCacheLength: 18)
        { }
    }
    public class YujianAI_TaiJiSlash : YujianAI
    {
        private Trail trail;
        /// <summary>
        /// 可以进行攻击
        /// </summary>
        private bool CanAtk;
        public YujianAI_TaiJiSlash()
        {
        }

        protected override void Attack(BaseYujianProj yujianProj)
        {
            if (CanAtk)
            {
                switch (yujianProj.State)
                {
                    case 0: // 横砍
                        break;
                    case 1:
                        break;
                }

                return;
            }

            Move(yujianProj);
        }
        /// <summary>
        /// 移动AI
        /// </summary>
        /// <param name="yujianProj"></param>
        public bool Move(BaseYujianProj yujianProj)
        {
            Projectile projectile = yujianProj.Projectile;
            Vector2 targetCenter = yujianProj.GetTargetCenter(IsAimingMouse);
            Vector2 targetDirection = targetCenter - projectile.Center;

            projectile.velocity = targetDirection.SafeNormalize(Vector2.Zero) * 10;
            if(targetDirection.LengthSquared() < 10000)
            {
                projectile.velocity *= targetDirection.LengthSquared() / 15000;

                if(targetDirection.LengthSquared() < 5000) 
                    return true;
            }
            projectile.rotation = projectile.rotation.AngleLerp(projectile.velocity.RotatedBy((projectile.velocity.X > 0).ToDirectionInt() * 0.3).ToRotation(), 0.01f);
            Type type = GetType();
            type.GetField("_innerTimer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(this,0); // 反射阻止技能调用失败
            return false;
        }
        protected override void OnStartAttack(BaseYujianProj yujianProj)    //这里用于重置各种计时器,保存位置什么的
        {
            Vector2 TargetPos = yujianProj.GetTargetCenter(IsAimingMouse);
            Projectile projectile = yujianProj.Projectile;

            CanAtk = false;
            yujianProj.State = 0;
            projectile.localAI[0] = TargetPos.X;
            projectile.localAI[1]= TargetPos.Y;
        }
        protected override bool UpdateTime(BaseYujianProj yujianProj)
        {
            if (CanAtk)
                return true;
            return false;
        }
        public override void DrawPrimitives(BaseYujianProj yujianProj)
        {
            Effect effect = Filters.Scene["SimpleTrail"].GetShader().Shader;

            Matrix world = Matrix.CreateTranslation(-Main.screenPosition.Vec3());
            Matrix view = Main.GameViewMatrix.TransformationMatrix;
            Matrix projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, -1, 1);

            effect.Parameters["transformMatrix"].SetValue(world * view * projection);
            effect.Parameters["sampleTexture"].SetValue(ModContent.Request<Texture2D>(yujianProj.SlashTexture).Value);

            trail?.Render(effect);
        }
    }
}
