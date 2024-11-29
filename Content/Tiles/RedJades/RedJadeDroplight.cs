﻿using Coralite.Core;
using Coralite.Core.Prefabs.Tiles;
using Terraria.ID;

namespace Coralite.Content.Tiles.RedJades
{
    public class RedJadeDroplight : ModTile
    {
        public override string Texture => AssetDirectory.RedJadeTiles + Name;

        public override void SetStaticDefaults()
        {
            this.DropLightPrefab(1, new int[] { 22 }, DustID.GemRuby, Coralite.RedJadeRed);
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1.9f;
            g = 0.8f;
            b = 0.8f;
        }
    }
}
