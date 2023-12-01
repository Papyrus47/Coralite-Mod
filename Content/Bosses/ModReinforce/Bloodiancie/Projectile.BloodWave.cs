﻿using Coralite.Content.Bosses.Rediancie;
using Coralite.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Coralite.Content.Bosses.ModReinforce.Bloodiancie
{
    public class BloodWave:ModProjectile
    {
        public override string Texture => AssetDirectory.Blank;

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;

            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.netImportant = true;
            Projectile.scale = 1.6f;
            Projectile.aiStyle = -1;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = false;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) => false;

        public override void AI()
        {
            if ((int)Projectile.ai[0]%10==0)
            {
                int type = Main.rand.NextFromList(ModContent.ProjectileType<Rediancie_Explosion>(), ModContent.ProjectileType<Rediancie_BigBoom>());

                Projectile.NewProjectile(Projectile.GetSource_FromAI(),
                    Projectile.Center + new Vector2(Main.rand.NextFloat(-16, 16), Main.rand.NextFloat(-80, 80)), Vector2.Zero, type,
                    50, 8, Projectile.owner);
            }

            if (Projectile.ai[0]>200)
            {
                Projectile.NewProjectile(Projectile.GetSource_FromAI(), Projectile.Center ,
                    Vector2.Zero, ModContent.ProjectileType<Bloodiancie_BigBoom>(), 55, 8f);

                Projectile.Kill();
            }

            Projectile.ai[0]++;
        }

        public override void OnKill(int timeLeft)
        {
            if (Main.myPlayer == Projectile.owner)
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<Rediancie_Explosion>(), (int)(Projectile.damage * 0.8f), Projectile.knockBack, Projectile.owner);
        }

        public override bool PreDraw(ref Color lightColor) => false;
    }
}
