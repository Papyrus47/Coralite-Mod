﻿using Coralite.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace Coralite.Content.Items.IcicleItems
{
    public class IcicleStaffHeldProj : ModProjectile, IDrawNonPremultiplied
    {
        public override string Texture => AssetDirectory.IcicleItems + "IcicleStaff";

        public const int SWING_TIME = 30;

        public ref float Timer => ref Projectile.localAI[0];
        public ref float Lenth => ref Projectile.localAI[1];
        public ref float Shoot => ref Projectile.ai[0];
        public ref float Rotate => ref Projectile.ai[1];
        public float visualEffectScale = 0f;
        public float visualEffectRotate = 0f;
        public Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.timeLeft = 40;
            Projectile.aiStyle = -1;
            Projectile.scale = 1.2f;

            Projectile.friendly = true;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.Center = Owner.Center + new Vector2(Owner.direction * 16, -16);
            if (Main.myPlayer == Projectile.owner)
            {
                //大概是手持法杖的角度
                Projectile.rotation = Rotate = Owner.direction * 0.3f - 1.57f + 0.785f;
            }
        }

        public override void AI()
        {
            /*
             * 先根据玩家方向以及手的方向得到初始位置，然后在初始位置稍微转两圈
             * 然后转向鼠标方向
             */
            if (Timer > 40)
                return;

            do
            {
                if (Timer < SWING_TIME)
                {
                    Projectile.Center = Owner.Center/* + new Vector2(Owner.direction * 4, -4)*/;
                    float sinProgress = MathF.Sin(7.852f * Timer / SWING_TIME);
                    //摇晃一下
                    Projectile.rotation = Rotate + sinProgress * -Owner.direction * 0.4f;

                    visualEffectScale += 0.5f / SWING_TIME;

                    break;
                }

                if (Timer == SWING_TIME)
                {
                    if (Main.myPlayer == Projectile.owner)
                    {
                        Rotate = (Main.MouseWorld - Projectile.Center).ToRotation() + 0.785f;
                        Lenth = (Projectile.Center - Owner.Center).Length();
                        Owner.direction = Main.MouseWorld.X > Owner.Center.X ? 1 : -1;
                        Projectile.netUpdate = true;
                    }
                }

                //旋转中心点，旋转角度，看上去像挥舞过去一样
                Projectile.rotation = Projectile.rotation.AngleLerp(Rotate, 0.2f);
                Lenth += 0.2f;
                Projectile.Center = Owner.Center + (Projectile.rotation-0.785f).ToRotationVector2() * Lenth;

                float factor = 1 - ((Timer - SWING_TIME) / 15);
                visualEffectScale = factor * 0.5f;

                if (Main.myPlayer == Projectile.owner && Timer == 40)
                {
                    Shoot = 1f;
                    Projectile.netUpdate = true;
                    Projectile.NewProjectile(Projectile.GetSource_FromAI(), Owner.Center, (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.One) * 14f,
                        ModContent.ProjectileType<IcicleMagicBall>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
                }

            } while (false);

            Owner.heldProj = Projectile.whoAmI;
            Owner.itemTime = Owner.itemAnimation = 2;
            Owner.itemRotation = Projectile.rotation + (Owner.direction > 0 ? -0.6f :2.2f);
            Projectile.timeLeft = 2;

            visualEffectRotate += 0.4f;
            Lighting.AddLight(Projectile.Center, 0.5f, 0.5f, 0.5f);
            Timer++;
        }

        public override bool? CanDamage() => false;

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D mainTex = TextureAssets.Projectile[Type].Value;

            Main.spriteBatch.Draw(mainTex, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, new Vector2(0, mainTex.Height), Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public void DrawNonPremultiplied(SpriteBatch spriteBatch)
        {
            //50.9 = 1.414*36,得到大概是法杖中心的位置
            Vector2 center = Projectile.Center + (Projectile.rotation - 0.785f).ToRotationVector2() * 50.9f * Projectile.scale;
            Texture2D visualEffectTex = ModContent.Request<Texture2D>(AssetDirectory.IcicleProjectiles + "IceLight").Value;

            spriteBatch.Draw(visualEffectTex, center - Main.screenPosition, null, Color.White, visualEffectRotate, new Vector2(visualEffectTex.Width / 2, visualEffectTex.Height / 2), visualEffectScale, SpriteEffects.None, 0f);
        }
    }
}