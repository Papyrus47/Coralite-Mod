using Coralite.Content.Items.Mushroom;
using Coralite.Core;
using Coralite.Core.Systems.YujianSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Buffs
{
    public class YujianBuff : ModBuff
    {
        public override string Texture => AssetDirectory.MinionBuffs + Name;

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            bool hasYuJian = false;
            for (int i = 0; i < 1000; i++)
                if (Main.projectile[i].active && Main.projectile[i].ModProjectile is BaseYujianProj && Main.projectile[i].owner == Main.myPlayer)
                {
                    hasYuJian = true;
                    break;
                }

            if (hasYuJian)
                player.buffTime[buffIndex] = 18000;
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
        public override bool RightClick(int buffIndex)
        {
            for (int i = 0; i < 1000; i++)
                if (Main.projectile[i].active && Main.projectile[i].ModProjectile is BaseYujianProj && Main.projectile[i].owner == Main.myPlayer)
                    Main.projectile[i].Kill();

            return true;
        }
    }
}
