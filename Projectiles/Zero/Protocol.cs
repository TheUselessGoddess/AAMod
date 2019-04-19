﻿using System;
using System.IO;
using AAMod;
using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Zero
{
    public class Protocol : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("D00M PR0T0C0L");
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.netImportant = true;
        }

        public float[] internalAI = new float[1];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write(internalAI[0]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }


        private float num633 = 700f;
        private float num634 = 800f;
        private float num635 = 1200f;
        private float num636 = 150f;

        private bool flag25 = false;

        public override void AI()
        {
            if (projectile.localAI[0] == 0f)
            {
                int num226 = 36;
                for (int num227 = 0; num227 < num226; num227++)
                {
                    Vector2 vector6 = Vector2.Normalize(projectile.velocity) *
                                      new Vector2((float) projectile.width / 2f, (float) projectile.height) * 0.75f;
                    vector6 = vector6.RotatedBy(
                                  (double) ((float) (num227 - (num226 / 2 - 1)) * 6.28318548f / (float) num226),
                                  default(Vector2)) + projectile.Center;
                    Vector2 vector7 = vector6 - projectile.Center;
                    int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 235, vector7.X * 1.75f, vector7.Y * 1.75f, 100,
                        default(Color), 1.1f);
                    Main.dust[num228].noGravity = true;
                    Main.dust[num228].velocity = vector7;
                }

                projectile.localAI[0] += 1f;
            }

            bool flag64 = projectile.type == mod.ProjectileType("Protocol");
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
            player.AddBuff(mod.BuffType("Protocol"), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.Protocol = false;
                }

                if (modPlayer.Protocol)
                {
                    projectile.timeLeft = 2;
                }
            }

            float num637 = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = (Main.projectile[num638].type == mod.ProjectileType("Protocol"));
                if (num638 != projectile.whoAmI && Main.projectile[num638].active &&
                    Main.projectile[num638].owner == projectile.owner && flag23 &&
                    Math.Abs(projectile.position.X - Main.projectile[num638].position.X) +
                    Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < (float) projectile.width)
                {
                    if (projectile.position.X < Main.projectile[num638].position.X)
                    {
                        projectile.velocity.X = projectile.velocity.X - num637;
                    }
                    else
                    {
                        projectile.velocity.X = projectile.velocity.X + num637;
                    }

                    if (projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num637;
                    }
                    else
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num637;
                    }
                }
            }

            for (int num645 = 0; num645 < 200; num645++)
            {
                NPC nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
                    if (((Vector2.Distance(projectile.Center, projectile.position) > num646 && num646 < num633) ||
                         !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height,
                            nPC2.position, nPC2.width, nPC2.height))
                    {
                        num633 = num646;
                        projectile.position = nPC2.Center;
                        flag25 = true;
                    }
                }

                if (Vector2.Distance(projectile.Center, nPC2.Center) < 300)
                {
                    internalAI[0] = 0;
                }
                else
                {
                    internalAI[0] = 1;
                }
            }

            if (projectile.ai[0] != 1f)
            {
                projectile.tileCollide = false;
            }

            if (projectile.tileCollide &&
                WorldGen.SolidTile(
                    Framing.GetTileSafely((int) projectile.Center.X / 16, (int) projectile.Center.Y / 16)))
            {
                projectile.tileCollide = false;
            }

            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }

            if (Vector2.Distance(player.Center, projectile.Center) > num647)
            {
                projectile.ai[0] = 1f;
                projectile.tileCollide = false;
                projectile.netUpdate = true;
            }

            if (flag25 && projectile.ai[0] == 0f)
            {
                Vector2 vector47 = projectile.position - projectile.Center;
                float num648 = vector47.Length();
                vector47.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector47 *= scaleFactor2;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    vector47 *= -num649;
                    projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = (projectile.ai[0] == 1f);
                }

                float num650 = 6f;
                if (flag26)
                {
                    num650 = 15f;
                }

                Vector2 center2 = projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 8f)
                {
                    num650 = 8f;
                }

                if (num651 < num636 && flag26 &&
                    !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
                {
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }

                if (num651 > 2000f)
                {
                    projectile.position.X = Main.player[projectile.owner].Center.X - (projectile.width / 2);
                    projectile.position.Y = Main.player[projectile.owner].Center.Y - (projectile.height / 2);
                    projectile.netUpdate = true;
                }

                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    projectile.velocity = (projectile.velocity * 40f + vector48) / 41f;
                }
                else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
                {
                    projectile.velocity.X = -0.15f;
                    projectile.velocity.Y = -0.05f;
                }
            }

            if (internalAI[0] == 0)
            {
                AI2(projectile, player);
            }
            else
            {
                AI1(projectile, player);
            }
        }

        public void AI1(Projectile projectile, Player player)
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }

            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }

            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += (float) Main.rand.Next(1, 4);
            }

            if (projectile.ai[1] > 90f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            if (projectile.ai[0] == 0f)
            {
                float scaleFactor3 = 8f;
                int num658 = mod.ProjectileType<RealityLaser>();
                if (flag25 && projectile.ai[1] == 0f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width,
                            projectile.height, projectile.position, 0, 0))
                    {
                        Vector2 value19 = projectile.position - projectile.Center;
                        value19.Normalize();
                        value19 *= scaleFactor3;
                        int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X,
                            value19.Y, num658, (int) ((float) projectile.damage * 0.8f), 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].timeLeft = 300;
                        projectile.netUpdate = true;
                    }
                }
            }
        }

        public void AI2(Projectile projectile, Player player)
        {
            bool flag24 = false;
            if (projectile.ai[0] == 2f)
            {
                projectile.ai[1] += 1f;
                projectile.extraUpdates = 1;
                projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
                projectile.frameCounter++;
                if (projectile.frameCounter > 1)
                {
                    projectile.frame++;
                    projectile.frameCounter = 0;
                }

                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }

                if (projectile.ai[1] > 40f)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.extraUpdates = 0;
                    projectile.numUpdates = 0;
                    projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }

            if (flag24)
            {
                return;
            }

            projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
            projectile.frameCounter++;
            if (projectile.frameCounter > 3)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }

            if (projectile.frame > 2)
            {
                projectile.frame = 0;
            }

            if (projectile.ai[1] > 0f)
            {
                projectile.ai[1] += (float) Main.rand.Next(1, 4);
            }

            if (projectile.ai[1] > 40f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            if (projectile.ai[0] == 0f)
            {
                if (projectile.ai[1] == 0f && flag25 && num633 < 500f)
                {
                    projectile.ai[1] += 1f;
                    if (Main.myPlayer == projectile.owner)
                    {
                        projectile.ai[0] = 2f;
                        Vector2 value20 = projectile.position - projectile.Center;
                        value20.Normalize();
                        projectile.velocity = value20 * 8f;
                        projectile.netUpdate = true;
                        return;
                    }
                }
            }
        }
    }
}