using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Zero
{
    public class RiftShredder : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rift Shredder");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.width = 40;
            npc.height = 70;
            npc.damage = 100;
            npc.defense = 30;
            npc.lifeMax = 37500;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            animationType = NPCID.PrimeVice;
            npc.noTileCollide = true;
            npc.knockBackResist = 0.0f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.lavaImmune = true;
            npc.netAlways = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(mod.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.velocity.Y == 0.0)
                npc.spriteDirection = npc.direction;
            ++npc.frameCounter;
            if (npc.frameCounter >= 8.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y / frameHeight >= 2)
                    npc.frame.Y = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool flag = (npc.life <= 0 || (!npc.active && NPC.AnyNPCs(mod.NPCType<Zero>())));
            if (flag && Main.netMode != 1)
            {
                int ind = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("TeslaHand"), npc.whoAmI, 1.5f, npc.ai[1], 0f, 0f, byte.MaxValue);
                Main.npc[ind].life = 1;
                Main.npc[ind].rotation = npc.rotation;
                Main.npc[ind].velocity = npc.velocity;
                Main.npc[ind].netUpdate = true;
                Main.npc[(int)npc.ai[1]].ai[3]++;
                Main.npc[(int)npc.ai[1]].netUpdate = true;
            }
        }

        public override void AI()
        {
            Vector2 vector2_1 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
            float num1 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_1.X;
            float num2 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_1.Y;
            float num3 = (float)Math.Sqrt((num1 * (double)num1) + (num2 * (double)num2));
            if (npc.ai[2] != 99.0)
            {
                if (num3 > 800.0)
                    npc.ai[2] = 99f;
            }
            else if (num3 < 400.0)
                npc.ai[2] = 0.0f;
            npc.spriteDirection = -(int)npc.ai[0];
            if (!Main.npc[(int)npc.ai[1]].active)
            {
                npc.ai[2] += 10f;
                if (npc.ai[2] > 50.0 || Main.netMode != 2)
                {
                    npc.life = -1;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                }
            }
            if (Main.player[npc.target].GetModPlayer<AAPlayer>().ZoneVoid == false)
            {
                npc.defense = 999999999;
            }
            else
            {
                npc.defense = 70;
            }
            if (npc.ai[2] == 99.0)
            {
                if (npc.position.Y > (double)Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y -= 0.1f;
                    if (npc.velocity.Y > 8.0)
                        npc.velocity.Y = 8f;
                }
                else if (npc.position.Y < (double)Main.npc[(int)npc.ai[1]].position.Y)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.96f;
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y < -8.0)
                        npc.velocity.Y = -8f;
                }
                if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.96f;
                    npc.velocity.X -= 0.5f;
                    if (npc.velocity.X > 12.0)
                        npc.velocity.X = 12f;
                }
                if (npc.position.X + (double)(npc.width / 2) >= Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                    return;
                if (npc.velocity.X < 0.0)
                    npc.velocity.X *= 0.96f;
                npc.velocity.X += 0.5f;
                if (npc.velocity.X >= -12.0)
                    return;
                npc.velocity.X = -12f;
            }
            else if (npc.ai[2] == 0.0 || npc.ai[2] == 3.0)
            {
                if (Main.npc[(int)npc.ai[1]].ai[1] == 3.0 && npc.timeLeft > 10)
                    npc.timeLeft = 10;
                if (Main.npc[(int)npc.ai[1]].ai[1] != 0.0)
                {
                    npc.TargetClosest(true);
                    if (Main.player[npc.target].dead)
                    {
                        npc.velocity.Y += 0.1f;
                        if (npc.velocity.Y > 16.0)
                            npc.velocity.Y = 16f;
                    }
                    else
                    {
                        Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                        float num4 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                        float num5 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                        float num6 = 7f / (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                        float num7 = num4 * num6;
                        float num8 = num5 * num6;
                        npc.rotation = (float)Math.Atan2(num8, num7) - 1.57f;
                        if (npc.velocity.X > (double)num7)
                        {
                            if (npc.velocity.X > 0.0)
                                npc.velocity.X *= 0.97f;
                            npc.velocity.X -= 0.05f;
                        }
                        if (npc.velocity.X < (double)num7)
                        {
                            if (npc.velocity.X < 0.0)
                                npc.velocity.X *= 0.97f;
                            npc.velocity.X += 0.05f;
                        }
                        if (npc.velocity.Y > (double)num8)
                        {
                            if (npc.velocity.Y > 0.0)
                                npc.velocity.Y *= 0.97f;
                            npc.velocity.Y -= 0.05f;
                        }
                        if (npc.velocity.Y < (double)num8)
                        {
                            if (npc.velocity.Y < 0.0)
                                npc.velocity.Y *= 0.97f;
                            npc.velocity.Y += 0.05f;
                        }
                    }
                    ++npc.ai[3];
                    if (npc.ai[3] >= 600.0)
                    {
                        npc.ai[2] = 0.0f;
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    ++npc.ai[3];
                    if (npc.ai[3] >= 300.0)
                    {
                        ++npc.ai[2];
                        npc.ai[3] = 0.0f;
                        npc.netUpdate = true;
                    }
                    if (npc.position.Y > Main.npc[(int)npc.ai[1]].position.Y + 320.0)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y -= 0.04f;
                        if (npc.velocity.Y > 3.0)
                            npc.velocity.Y = 3f;
                    }
                    else if (npc.position.Y < Main.npc[(int)npc.ai[1]].position.Y + 260.0)
                    {
                        if (npc.velocity.Y < 0.0)
                            npc.velocity.Y *= 0.96f;
                        npc.velocity.Y += 0.04f;
                        if (npc.velocity.Y < -3.0)
                            npc.velocity.Y = -3f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) > Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X -= 0.3f;
                        if (npc.velocity.X > 12.0)
                            npc.velocity.X = 12f;
                    }
                    if (npc.position.X + (double)(npc.width / 2) < Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - 250.0)
                    {
                        if (npc.velocity.X < 0.0)
                            npc.velocity.X *= 0.96f;
                        npc.velocity.X += 0.3f;
                        if (npc.velocity.X < -12.0)
                            npc.velocity.X = -12f;
                    }
                }
                Vector2 vector2_3 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num9 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_3.X;
                float num10 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_3.Y;
                Math.Sqrt((num9 * (double)num9) + (num10 * (double)num10));
                npc.rotation = (float)Math.Atan2(num10, num9) + 1.57f;
            }
            else if (npc.ai[2] == 1.0)
            {
                Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num4 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_2.X;
                float num5 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_2.Y;
                float num6 = (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                npc.rotation = (float)Math.Atan2(num5, num4) + 1.57f;
                npc.velocity.X *= 0.95f;
                npc.velocity.Y -= 0.1f;
                if (npc.velocity.Y < -8.0)
                    npc.velocity.Y = -8f;
                if (npc.position.Y >= Main.npc[(int)npc.ai[1]].position.Y - 200.0)
                    return;
                npc.TargetClosest(true);
                npc.ai[2] = 2f;
                vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num7 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                float num8 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                float num9 = 22f / (float)Math.Sqrt((num7 * (double)num7) + (num8 * (double)num8));
                npc.velocity.X = num7 * num9;
                npc.velocity.Y = num8 * num9;
                npc.netUpdate = true;
            }
            else if (npc.ai[2] == 2.0)
            {
                if (npc.position.Y <= (double)Main.player[npc.target].position.Y && npc.velocity.Y >= 0.0)
                    return;
                npc.ai[2] = 3f;
            }
            else if (npc.ai[2] == 4.0)
            {
                npc.TargetClosest(true);
                Vector2 vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num4 = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - vector2_2.X;
                float num5 = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - vector2_2.Y;
                float num6 = 7f / (float)Math.Sqrt((num4 * (double)num4) + (num5 * (double)num5));
                float num7 = num4 * num6;
                float num8 = num5 * num6;
                if (npc.velocity.X > (double)num7)
                {
                    if (npc.velocity.X > 0.0)
                        npc.velocity.X *= 0.97f;
                    npc.velocity.X -= 0.05f;
                }
                if (npc.velocity.X < (double)num7)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X *= 0.97f;
                    npc.velocity.X += 0.05f;
                }
                if (npc.velocity.Y > (double)num8)
                {
                    if (npc.velocity.Y > 0.0)
                        npc.velocity.Y *= 0.97f;
                    npc.velocity.Y -= 0.05f;
                }
                if (npc.velocity.Y < (double)num8)
                {
                    if (npc.velocity.Y < 0.0)
                        npc.velocity.Y *= 0.97f;
                    npc.velocity.Y += 0.05f;
                }
                ++npc.ai[3];
                if (npc.ai[3] >= 600.0)
                {
                    npc.ai[2] = 0.0f;
                    npc.ai[3] = 0.0f;
                    npc.netUpdate = true;
                }
                vector2_2 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
                float num9 = (float)(Main.npc[(int)npc.ai[1]].position.X + (double)(Main.npc[(int)npc.ai[1]].width / 2) - (200.0 * npc.ai[0])) - vector2_2.X;
                float num10 = Main.npc[(int)npc.ai[1]].position.Y + 230f - vector2_2.Y;
                float num11 = (float)Math.Sqrt((num9 * (double)num9) + (num10 * (double)num10));
                npc.rotation = (float)Math.Atan2(num10, num9) + 1.57f;
            }
            else
            {
                if (npc.ai[2] != 5.0 || (npc.velocity.X <= 0.0 || npc.position.X + (double)(npc.width / 2) <= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2)) && (npc.velocity.X >= 0.0 || npc.position.X + (double)(npc.width / 2) >= Main.player[npc.target].position.X + (double)(Main.player[npc.target].width / 2)))
                    return;
                npc.ai[2] = 0.0f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Zero.DrawArm(mod, npc, spriteBatch, drawColor);
            return true;
        }


        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/RiftShredderZ");
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, npc, GenericUtils.COLOR_GLOWPULSE);
        }
    }
}