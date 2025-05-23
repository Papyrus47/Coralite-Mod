using Coralite.Content.Items.FlyingShields;
using Coralite.Core.Prefabs.Projectiles;
using Coralite.Core.Systems.YujianSystem;
using Coralite.Core.Systems.YujianSystem.YujianAIs;
using Coralite.Helpers;
using InnoVault.Trails;
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
                new YujianAI_PreciseSlash(startTime: 100,
                    slashWidth: 150,
                    slashTime: 70,
                    startAngle: -2f,
                    totalAngle: 3f,
                    turnSpeed: 2.2f,
                    roughlyVelocity: 0.9f,
                    halfShortAxis: 1f,
                    halfLongAxis: 1.5f,
                    Coralite.Instance.HeavySmootherInstance),
            },
            yujianAIsRandom:new int[] { 2, 6 },
            powerfulAI: new YujianAI_TaiJiSlash(),
            //powerfulAI:null,
            PowerfulAttackCost: 100,
            attackLength: 400,
            width: 30,height: 58,
            color1: new Color(200,230,220), color2: new Color(10,5,9),
            trailCacheLength: 18)
        { }
    }
    public class YujianAI_TaiJiSlash : YujianAI_DoubleSlash
    {
        public YujianAI_TaiJiSlash() : base(180, 150, 150, -MathHelper.TwoPi,MathHelper.TwoPi, 1.2f, 0.9f, 1f, 1.2f, Coralite.Instance.HeavySmootherInstance)
        {
        }
        public override void Reset()
        {
            StartTime = 130;

            SlashTime = 100;
            StartAngle = 2.5f;

            halfShortAxis = 1.8f;
            halfLongAxis = 1f;
        }

        public override void Init()
        {
            StartTime = 90;

            SlashTime = 40;
            StartAngle = -2.5f;

            halfShortAxis = 1.3f;
            halfLongAxis = 1f;
        }
    }
}
