﻿using Coralite.Core;
using Coralite.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Coralite.Content.Bosses.ShadowBalls
{
    /// <summary>
    ///                                               马赛克
    ///           ○○○○○○○○○○ ○                        l   l  
    ///       ○○○○○○○○○○○○○○○○○ ○                     l   l
    ///     ○○○○○○○○○○○○○○○○○○○○○ ○              _ _  l   l_  
    ///    ○○○○○{影}○○○○○○○○○○○{球}○ ○          !  !  l   l l ˉl
    ///   ○○○○○{影影影}○○子○○○{球球球}○ ○        l               l
    /// ○○○○○○○○{影}○○○○○○○○○○○{球}○○○ ○        l               l
    /// ○○○○○○○○○○○○○○○○○○○○○○○○○○○○○ ○          l             l
    ///  ○○○○○○==○○○○○○○○○○○○○○○○○○○ ○           l            l
    ///    ○○○○○○==○○○○○○○○○○○○○○○○ ○            l           l
    ///     ○○○○○○○=========○○○○○ ○              l           l
    ///       ○○○○○○○○○○○○○○○○○ ○
    ///           ○○○○○○○○○○ ○
    /// 
    ///             就贼搁赤玉灵嗷，别让我在影之城看见你嗷，
    ///                 抓到你，指定没你好果汁吃
    ///                     你记住我说的话嗷！
    /// 
    /// </summary>
    public class ShadowBall : ModNPC
    {
        public override string Texture => AssetDirectory.ShadowBalls + "SmallShadowBall";

        internal ref float Phase => ref NPC.ai[0];
        internal ref float State => ref NPC.ai[1];
        internal ref float SonState => ref NPC.ai[2];
        internal ref float Timer => ref NPC.ai[3];

        public Player Target => Main.player[NPC.target];

        public bool SpawnedSmallBalls;
        public List<NPC> smallBalls;

        #region tmlHooks

        public override void SetStaticDefaults()
        {
            NPCID.Sets.MPAllowedEnemies[Type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.damage = 50;
            NPC.defense = 6;
            NPC.lifeMax = 4500;
            NPC.knockBackResist = 0f;
            //NPC.scale = 1.2f;
            NPC.aiStyle = -1;
            NPC.npcSlots = 10f;
            NPC.value = Item.buyPrice(0, 10, 0, 0);

            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.boss = true;

            //NPC.BossBar = GetInstance<BabyIceDragonBossBar>();

            //BGM：冰结寒流
            if (!Main.dedServ)
                Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/IcyColdStream");
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
        {
            if (Helper.GetJourneyModeStrangth(out float journeyScale, out NPCStrengthHelper nPCStrengthHelper))
            {
                if (nPCStrengthHelper.IsExpertMode)
                {
                    NPC.lifeMax = (int)((3820 + numPlayers * 1750) / journeyScale);
                    NPC.damage = 35;
                    NPC.defense = 12;
                }

                if (nPCStrengthHelper.IsMasterMode)
                {
                    NPC.lifeMax = (int)((4720 + numPlayers * 2100) / journeyScale);
                    NPC.damage = 60;
                    NPC.defense = 15;
                }

                if (Main.getGoodWorld)
                {
                    NPC.damage = 80;
                    NPC.defense = 15;
                }

                if (Main.zenithWorld)
                {
                    NPC.scale = 0.4f;
                }

                return;
            }

            NPC.lifeMax = 3820 + numPlayers * 1750;
            NPC.damage = 35;
            NPC.defense = 12;

            if (Main.masterMode)
            {
                NPC.lifeMax = 4720 + numPlayers * 2100;
                NPC.damage = 60;
                NPC.defense = 15;
            }

            if (Main.getGoodWorld)
            {
                NPC.lifeMax = 5320 + numPlayers * 2200;
                NPC.damage = 80;
                NPC.defense = 15;
            }

            if (Main.zenithWorld)
            {
                NPC.scale = 0.4f;
            }
        }

        public override bool CheckDead()
        {
            //if ((int)State != (int)AIStates.onKillAnim)
            //{
            //    State = (int)AIStates.onKillAnim;
            //    Timer = 0;
            //    NPC.dontTakeDamage = true;
            //    NPC.life = 1;
            //    return false;
            //}

            return true;
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            //npcLoot.Add(ItemDropRule.MasterModeCommonDrop(ItemType<BabyIceDragonRelic>()));
            //npcLoot.Add(ItemDropRule.BossBag(ItemType<BabyIceDragonBossBag>()));
            //npcLoot.Add(ItemDropRule.Common(ItemType<BabyIceDragonTrophy>(), 10));

            //LeadingConditionRule notExpertRule = new LeadingConditionRule(new Conditions.NotExpert());
            //notExpertRule.OnSuccess(ItemDropRule.Common(ItemType<IcicleCrystal>(), 1, 3, 5));
            //npcLoot.Add(notExpertRule);
        }


        #endregion

        #region AI

        public enum AIPhases
        {
            WithSmallBalls,
            ShadowPlayer,
            BigBallSmash
        }

        public enum AIStates
        {
            OnSpawnAnmi,
            /// <summary> 狂暴，为出框惩罚 </summary>
            Rampage,
            /// <summary> 一阶段和2阶段的切换，使用在2阶段 </summary>
            P1ToP2Exchange,

        }

        public override void AI()
        {
            if (NPC.target < 0 || NPC.target == 255 || Target.dead || !Target.active || /*Target.Distance(NPC.Center) > 4800 ||*/ Main.dayTime) //世花也是4800
            {
                NPC.TargetClosest();

                do
                {
                    if (!Main.dayTime)
                    {
                        State = (int)AIStates.Rampage; //狂暴的AI
                        break;
                    }

                    if (Target.dead || !Target.active)
                    {
                        NPC.EncourageDespawn(10);
                        NPC.dontTakeDamage = true;  //脱战无敌
                        NPC.velocity.Y += 0.25f;

                        return;
                    }
                    //else
                    //    ResetStates();
                } while (false);
            }

            switch (Phase)
            {
                default:
                case (int)AIPhases.WithSmallBalls:
                    {
                        if (!SpawnedSmallBalls)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                               int index= NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<SmallShadowBall>(), NPC.whoAmI);
                                Main.npc[index].realLife = NPC.whoAmI;
                            }
                            SpawnedSmallBalls = true;
                        }

                        if (!GetSmallBalls())
                        {
                            //切换状态
                            return;
                        }

                        switch (State)
                        {
                            default:
                            case (int)AIStates.OnSpawnAnmi:
                                {

                                }
                                break;
                        }
                    }
                    break;
                case (int)AIPhases.ShadowPlayer:
                    {

                    }
                    break;
                case (int)AIPhases.BigBallSmash:
                    break;
            }
        }

        #endregion

        #region HelperMethods

        public bool GetSmallBalls()
        {
            smallBalls.Clear();
            int count=0;
            for (int i = 0; i < 200; i++)
                if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<SmallShadowBall>())
                {
                    smallBalls.Add(Main.npc[i]);
                    count++;
                    if (count>=5)
                    {
                        break;
                    }
                }

            if (count==0)
                return false;

            return true;
        }

        #endregion

        #region Draw

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return true;
        }

        #endregion
    }
}
