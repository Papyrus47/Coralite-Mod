﻿using Coralite.Content.Bosses.VanillaReinforce.NightmarePlantera;
using Coralite.Content.ModPlayers;
using Coralite.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Bosses.GodOfWind
{
    public class GOWTester : ModItem
    {
        public override string Texture => AssetDirectory.GodOfWind + "AosSiHead";

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = Item.useAnimation = 20;
            Item.shoot = ModContent.ProjectileType<GOWTestProj>();
        }

        public override bool CanUseItem(Player player)
        {
            //Filters.Scene.Activate("NightmareScreen", player.position);
            //if (NightmareScreen.size > 0)
            //{
            //    NightmareScreen.size -= 0.05f;
            //}

            if (player.TryGetModPlayer(out CoralitePlayer cp))
                cp.nightmareCount = 0;

            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //return false;
            Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<NightmareSparkle_Normal>(), 1, 1);
            return false;
        }
    }

    public class GOWTestProj : ModProjectile
    {
        public override string Texture => AssetDirectory.GodOfWind + "AosSiHead";

        public override void SetDefaults()
        {
            Projectile.timeLeft = 120;
            Projectile.aiStyle = -1;
        }

        public override void AI()
        {

        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture2 = TextureAssets.Projectile[Projectile.type].Value;

            Texture2D texture = ModContent.Request<Texture2D>(AssetDirectory.GodOfWind + "CycloneSample").Value;
            Effect shader = Filters.Scene["GlowingMarblingBlack"].GetShader().Shader;

            //shader.Parameters["uColor"].SetValue(new Vector3(0.6f, 0.3f, 0.3f));
            //shader.Parameters["uOpacity"].SetValue(1.5f*MathF.Sin(MathHelper.Pi*Projectile.timeLeft / 120f));
            //shader.Parameters["uRotateSpeed"].SetValue(0.15f);
            shader.Parameters["uTime"].SetValue(Main.GlobalTimeWrappedHourly);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap,
                            Main.spriteBatch.GraphicsDevice.DepthStencilState, RasterizerState.CullNone, shader, Main.GameViewMatrix.TransformationMatrix);

            float scale =( Main.screenWidth) / texture.Width;
            Vector2 vector2 = new Vector2(1, texture.Width / (float)texture.Height);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, 0, texture.Size() / 2, scale, 0, 0);

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, Main.spriteBatch.GraphicsDevice.BlendState, Main.spriteBatch.GraphicsDevice.SamplerStates[0],
                            Main.spriteBatch.GraphicsDevice.DepthStencilState, Main.spriteBatch.GraphicsDevice.RasterizerState, null, Main.Transform);

            return false;
        }
    }
}
