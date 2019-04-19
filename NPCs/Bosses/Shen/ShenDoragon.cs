﻿using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shen
{
    [AutoloadBossHead]
    public class ShenDoragon : ModNPC
    {
        public float[] customAI = new float[6];

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short) customAI[0]);
                writer.Write((short) customAI[1]);
                writer.Write((short) customAI[2]);
                writer.Write((short) customAI[3]);
                writer.Write((short) customAI[4]);
                writer.Write((short) customAI[5]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
                customAI[4] = reader.ReadFloat();
                customAI[5] = reader.ReadFloat();
            }
        }

        public bool SpawnGrips = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shen Doragon; Discordian Doomsayer");
            Main.npcFrameCount[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.noTileCollide = true;
            npc.height = 100;
            npc.width = 444;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.knockBackResist = 0f;
            npc.damage = 180;
            npc.defense = 210;
            npc.lifeMax = 400000;
            if (Main.expertMode)
            {
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            else
            {
                npc.value = Item.buyPrice(10, 0, 0, 0);
            }

            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.alpha = 255;
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Shen");
            musicPriority = (MusicPriority) 11;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }

            npc.buffImmune[mod.BuffType<Buffs.Terrablaze>()] = false;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = npc.lifeMax;
            npc.defense = (int) (npc.defense * 1.2f);
            npc.damage = (int) (npc.damage * 1.2f);
            damageDiscordianInferno = (int) (damageDiscordianInferno * 1.2f);
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }

            npc.lifeRegen -= 48;
            if (damage < 10)
            {
                damage = 10;
            }
        }

        public bool Weakness = false;
        public bool spawnalpha = false;
        public bool isAwakened = false;
        public float _normalSpeed = 15f; //base for normal movement
        public float _chargeSpeed = 40f; //base for charge movement

        public float MoveSpeed
        {
            get
            {
                float playerRunAcceleration = 1f;
                if (Main.player[npc.target].active && !Main.player[npc.target].dead
                ) //if you have a target, speed up to keep up
                {
                    playerRunAcceleration = Math.Max(Math.Abs(Main.player[npc.target].moveSpeed),
                        Main.player[npc.target].runAcceleration);
                    if (playerRunAcceleration <= 1f) playerRunAcceleration = 1f;
                }

                if (Charging)
                {
                    return _chargeSpeed * playerRunAcceleration;
                }
                else
                {
                    return _normalSpeed * playerRunAcceleration;
                }
            }
        }

        public bool ChargePrep
        {
            get { return npc.ai[0] == 0.5f || customAI[3] == 0.5f; }
        }

        public bool Charging
        {
            get { return npc.ai[0] == 1; }
        }

        public bool
            SnapToPlayer //wether to 'snap' relative to a player's position. This forces the player to be unable to outrun the npc while this is true.
        {
            get
            {
                Player player = Main.player[npc.target];
                if (player == null || !player.active || player.dead) return false;

                if (ChargePrep) return true; //always snap when prepping a charge to prevent a stall

                return false;
            }
        }

        public int spawnTimerMax = 100; //time to sit when you spawn
        public int discordianInfernoTimerMax = 105; //shoot fireballs timer
        public int discordianInfernoPercent = 10; //the % amount to shoot fireballs
        public int discordianFirebombTimerMax = 105; //shoot firebombs timer
        public int discordianFirebombPercent = 30; //the % amount to shoot firebombs

        public int
            aiChangeRate =
                100; //the rate to jump to another ai. (in truth this is ai[2], this is what it is checked against by default.)

        public int
            aiTooLongCheck =
                60; //if he takes too long to change ai states this forces it to happen soon. smaller value == faster change.

        public int damageDiscordianInferno = 120; //how much damage the inferno fire does.
        public int damageDiscordianFirebomb = 140; //how much damage the firebomb does.

        //clientside stuff
        public Rectangle wingFrame = new Rectangle(0, 0, 444, 400); //the wing frame.
        public int wingFrameY = 400; //the frame height for the wings.
        public int frameY = 400; //the frame height for the body.
        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.

        public bool Roaring //wether or not he is roaring. only used clientside for frame visuals.
        {
            get { return roarTimer > 0; }
        }

        public bool LookAtPlayer
        {
            get { return ChargePrep || npc.ai[0] == 2; }
        }

        public int chargeWidth = 50;
        public int normalWidth = 444;


        public override void BossLoot(ref string name, ref int potionType)
        {
            if (Main.expertMode && !isAwakened)
            {
                potionType = 0;
                return;
            }

            potionType = mod.ItemType<Items.Potions.GrandHealingPotion>();
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }


        public bool Health4 = false;
        public bool Health3 = false;
        public bool Health2 = false;
        public bool Health1 = false;
        public bool Invisible = false;

        public override void AI()
        {
            #region preamble stuff

            if (isAwakened && npc.life > npc.lifeMax * 0.2f) //set awakened stats
            {
                _normalSpeed = 17f;
                _chargeSpeed = 45f;
                discordianInfernoPercent = 7;
                discordianFirebombPercent = 25;
                aiTooLongCheck = 50;
            }
            else if (isAwakened && npc.life <= npc.lifeMax * 0.2f)
            {
                _normalSpeed = 23f;
                _chargeSpeed = 55f;
                discordianInfernoPercent = 4;
                discordianFirebombPercent = 17;
                aiTooLongCheck = 45;
            }

            if (npc.alpha > 0 && !spawnalpha)
            {
                npc.alpha -= 5;
            }

            if (npc.alpha <= 0)
            {
                npc.alpha = 0;
                spawnalpha = true;
            }

            if (Roaring) roarTimer--;

            if (Charging)
            {
                if (npc.width != chargeWidth)
                {
                    Vector2 center = npc.Center;
                    npc.width = chargeWidth;
                    npc.Center = center;
                    npc.netUpdate = true;
                }
            }
            else if (npc.width != normalWidth)
            {
                Vector2 center = npc.Center;
                npc.width = normalWidth;
                npc.Center = center;
                npc.netUpdate = true;
            }

            #endregion

            Player player = Main.player[npc.target];

            customAI[5]++;

            if (npc.HasBuff(mod.BuffType<Buffs.Terrablaze>()) && !Weakness && !AAWorld.downedShen && !isAwakened)
            {
                BaseUtility.Chat("TERRA MAGIC?! NO! I THOUGHT IT WAS WIPED FROM THE EARTH!", Color.DarkMagenta.R,
                    Color.DarkMagenta.G, Color.DarkMagenta.B);
                Weakness = true;
            }

            if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
            {
                npc.TargetClosest(true);
                player = Main.player[npc.target];
                npc.netUpdate = true;
            }

            if (player.dead)
            {
                npc.velocity.Y = npc.velocity.Y - 0.4f;
                if (npc.timeLeft > 150)
                {
                    npc.timeLeft = 150;
                }

                npc.ai[0] = 0f;
                npc.ai[2] = 0f;
            }
            else if (npc.timeLeft > 1800)
            {
                npc.timeLeft = 1800;
            }

            if (npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                npc.alpha = 255;
                if (Main.netMode != 1)
                {
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                }
            }

            if (npc.ai[0] == -1f) //initial spawn effects
            {
                npc.chaseable = false;
                npc.velocity *= 0.98f;
                if (npc.ai[2] > 20f)
                {
                    npc.velocity.Y = -2f;
                }

                if (npc.ai[2] == discordianFirebombTimerMax - 30)
                {
                    Roar(roarTimerMax, false);
                }

                npc.ai[2] += 1f;
                if (npc.ai[2] >= spawnTimerMax)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.netUpdate = true;
                    return;
                }
            }
            else if (npc.ai[0] == 0f && !player.dead) //move to default point / pick new AI
            {
                npc.chaseable = true;
                bool playerPointInRange = false;
                if (customAI[3] != 0.5f) //be sure we aren't waiting on a prep state!
                {
                    if (npc.ai[1] == 0f)
                    {
                        npc.ai[1] = 400 * Math.Sign((npc.Center - player.Center).X);
                        npc.netUpdate = true;
                    }

                    Vector2 playerPoint = player.Center + new Vector2(npc.ai[1], -300f);
                    MoveToPoint(playerPoint);
                    playerPointInRange = (playerPoint - npc.Center).Length() < 100f;
                }

                npc.ai[2] += 1f;
                if (playerPointInRange && npc.ai[2] < (aiChangeRate - aiTooLongCheck))
                {
                    npc.ai[2] = aiChangeRate - aiTooLongCheck;
                }

                if (npc.ai[2] >= aiChangeRate)
                {
                    float aiChoice = 0;
                    if (customAI[3] == 0.5f) //prep goes directly into charge
                    {
                        aiChoice = 1f;
                    }
                    else
                    {
                        switch ((int) npc.ai[3]) //switch for attack modes
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                aiChoice = 0.5f;
                                break;
                            case 5:
                                npc.ai[3] = 1f;
                                aiChoice = 2f;
                                break;
                            case 6:
                                npc.ai[3] = 0f;
                                aiChoice = 3f;
                                break;
                        }
                    }

                    npc.ai[0] = aiChoice;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    customAI[3] = 0f;
                    if (aiChoice == 1f)
                    {
                        Vector2 vel = player.Center - npc.Center;
                        vel = Vector2.Normalize(vel) * 500f;
                        customAI[0] = player.Center.X + vel.X;
                        customAI[1] = player.Center.Y + vel.Y;
                    }

                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[0] == 0.5f) //charge attack prep
            {
                npc.chaseable = true;
                float chargePrepSpot = 550;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = chargePrepSpot * Math.Sign((npc.Center - player.Center).X);
                    npc.netUpdate = true;
                }

                Vector2 playerPoint = player.Center + new Vector2(npc.ai[1], -chargePrepSpot);
                MoveToPoint(playerPoint);
                if (Main.netMode != 1 && (playerPoint - npc.Center).Length() < 100f)
                {
                    SwitchToAI(0f, 0f, aiChangeRate - 15, npc.ai[3]);
                }
            }
            else if (npc.ai[0] == 1f) //charge attack
            {
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[1] == 0f)
                {
                    npc.ai[1] = 500 * -Math.Sign((npc.Center - player.Center).X);
                    npc.netUpdate = true;
                }

                Vector2 point = player.Center + new Vector2(npc.ai[1], 500f);
                MoveToPoint(point);

                if (Main.netMode != 1 && (point - npc.Center).Length() < 100f)
                {
                    SwitchToAI(0f, 0f, 40f, npc.ai[3] + 2f);
                }
            }
            else if (npc.ai[0] == 2f) //fire discordian infernos
            {
                Vector2 playerPoint =
                    player.Center + new Vector2(Math.Sign((npc.Center - player.Center).X) * 500, -350);
                MoveToPoint(playerPoint);
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                if (npc.ai[2] == 0f)
                {
                    Roar(roarTimerMax, false);
                }

                if (npc.ai[2] % discordianInfernoPercent == 0f)
                {
                    Roar(8, true);
                    if (Main.netMode != 1)
                    {
                        Vector2 infernoPos = new Vector2(200f, (npc.direction == 1 ? 65f : -45f));
                        Vector2 vel = new Vector2(MathHelper.Lerp(6f, 8f, (float) Main.rand.NextDouble()),
                            MathHelper.Lerp(-5f, 5f, (float) Main.rand.NextDouble()));

                        if (player.active && !player.dead)
                        {
                            float rot = BaseUtility.RotationTo(npc.Center, player.Center);
                            infernoPos = BaseUtility.RotateVector(Vector2.Zero, infernoPos, rot);
                            vel = BaseUtility.RotateVector(Vector2.Zero, vel, rot);
                            vel *= (MoveSpeed / _normalSpeed); //to compensate for players running away
                            int dir = (npc.Center.X < player.Center.X ? 1 : -1);
                            if ((dir == -1 && npc.velocity.X < 0) || (dir == 1 && npc.velocity.X > 0))
                                vel.X += npc.velocity.X;
                            vel.Y += npc.velocity.Y;
                            infernoPos += npc.Center;
                            infernoPos.Y -= 60;
                        }

                        //REMEMBER: PROJECTILES DOUBLE DAMAGE so to get an accurate damage count you divide it by 2!
                        int projectile = Projectile.NewProjectile((int) infernoPos.X, (int) infernoPos.Y, 0f, 0f,
                            mod.ProjectileType("DiscordianInferno"), damageDiscordianInferno / 2, 0f, Main.myPlayer, 0f,
                            0f);
                        Main.projectile[projectile].velocity = vel;
                        Main.projectile[projectile].netUpdate = true;
                    }
                }

                npc.ai[2] += 1f;
                if (npc.ai[2] >= discordianInfernoTimerMax)
                {
                    SwitchToAI(0f, 0f, -40f, 0f);
                }
            }
            else if (npc.ai[0] == 3f) //Fire firebombs
            {
                int shootThis = mod.ProjectileType("ShenFirebomb");
                if (customAI[2] == 0)
                {
                    shootThis = mod.ProjectileType("ShenStorm");
                }

                Vector2 playerPoint = player.Center + new Vector2(Math.Sign((npc.Center - player.Center).X) * 400, -50);
                MoveToPoint(playerPoint);
                if (npc.ai[2] % discordianFirebombPercent == 0)
                {
                    Roar(roarTimerMax, true);
                    if (Main.netMode != 1)
                    {
                        for (int m = 0; m < 3; m++)
                        {
                            Vector2 infernoPos = new Vector2(200f, (npc.direction == -1 ? 65f : -45f));
                            Vector2 vel = new Vector2(MathHelper.Lerp(12f, 15f, (float) Main.rand.NextDouble()),
                                MathHelper.Lerp(-4f, 4f, (float) Main.rand.NextDouble()));

                            if (player.active && !player.dead)
                            {
                                float rot = BaseUtility.RotationTo(npc.Center, player.Center);
                                infernoPos = BaseUtility.RotateVector(Vector2.Zero, infernoPos, rot);
                                vel = BaseUtility.RotateVector(Vector2.Zero, vel, rot);
                                vel *= (MoveSpeed / _normalSpeed); //to compensate for players running away
                                int dir = (npc.Center.X < player.Center.X ? 1 : -1);
                                if ((dir == -1 && npc.velocity.X < 0) || (dir == 1 && npc.velocity.X > 0))
                                    vel.X += npc.velocity.X;
                                vel.Y += npc.velocity.Y;
                                infernoPos += npc.Center;
                                infernoPos.Y -= 60;
                            }
                            //REMEMBER: PROJECTILES DOUBLE DAMAGE so to get an accurate damage count you divide it by 2!

                            int projectile = Projectile.NewProjectile((int) infernoPos.X, (int) infernoPos.Y, 0f, 0f,
                                shootThis, damageDiscordianFirebomb / 2, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[projectile].velocity = vel;
                            Main.projectile[projectile].netUpdate = true;
                        }
                    }
                }

                npc.ai[2] += 1f;
                if (npc.ai[2] >= discordianFirebombTimerMax)
                {
                    SwitchToAI(0f, 0f, 0f, 1f);
                }
            }

            if (SnapToPlayer)
            {
                npc.position += (player.position - player.oldPosition);
            }

            HandleFrames(player);
            HandleRotations(player);
        }

        public void SwitchToAI(float ai0, float ai1, float ai2, float ai3)
        {
            customAI[2] = -1;
            customAI[3] = npc.ai[0]; //last AI
            npc.ai[0] = ai0; //handles AI state (charging, prep, fire, etc.)
            npc.ai[1] = ai1; //handles X movement for some AI states
            npc.ai[2] = ai2; //handles timers for the AI state
            npc.ai[3] = ai3; //handles the next AI choice
            npc.netUpdate = true;
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                Main.PlaySound(4, (int) npc.Center.X, (int) npc.Center.Y, 60);
            }
            else
            {
                Main.PlaySound(mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/ShenRoar"),
                    npc.Center);
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist.Length();
            if (length < MoveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, dist.Length() / MoveSpeed);
            }

            npc.velocity = Vector2.Normalize(point - npc.Center);
            npc.velocity *= MoveSpeed;
            npc.velocity *= velMultiplier;
            if (!Charging)
            {
                if (length < 200f)
                {
                    npc.velocity *= 0.9f;
                }

                if (length < 150f)
                {
                    npc.velocity *= 0.9f;
                }

                if (length < 100f)
                {
                    npc.velocity *= 0.8f;
                }

                if (length < 50f)
                {
                    npc.velocity *= 0.8f;
                }
            }
        }

        public void HandleFrames(Player player)
        {
            npc.frame = new Rectangle(0, (Roaring ? frameY : 0), 444, frameY);
            if (Charging)
            {
                npc.frameCounter = 0;
                wingFrame.Y = wingFrameY;
            }
            else
            {
                npc.frameCounter++;
                if (npc.frameCounter >= 5)
                {
                    npc.frameCounter = 0;
                    wingFrame.Y += wingFrameY;
                    if (wingFrame.Y > (wingFrameY * 4))
                    {
                        npc.frameCounter = 0;
                        wingFrame.Y = 0;
                    }
                }
            }

            npc.direction = (npc.Center.X < player.Center.X ? 1 : -1);
        }

        public void HandleRotations(Player player)
        {
            if (LookAtPlayer)
            {
                Vector2 diff = player.Center - npc.Center;
                BaseAI.LookAt(npc.Center - diff, npc, 3, 0f, 0.12f, false);
            }
            else if (Charging)
            {
                BaseAI.LookAt(npc.Center - npc.velocity, npc, 0, 0f, 0f, false);
            }
            else
            {
                BaseAI.LookAt(npc.Center + new Vector2(-npc.direction * 200, 0f), npc, 0, 0f, 0.05f, false);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            Player player = Main.player[npc.target];
            if (npc.life <= npc.lifeMax / 2 && !SpawnGrips && !isAwakened)
            {
                SpawnGrips = true;
                Main.NewText("Grips! Assist me!", Color.DarkMagenta);
                SpawnBoss(player, "AbyssGrip", "");
                SpawnBoss(player, "BlazeGrip", "");
                Main.PlaySound(SoundID.Roar, player.position, 0);
            }

            if (npc.life <= npc.lifeMax / 2 && !SpawnGrips && isAwakened)
            {
                SpawnGrips = true;

                if (AAWorld.downedShen)
                {
                    Main.NewText("Ashe? Haruka? I need your assistance again..!", Color.DarkMagenta);
                    Main.NewText("On it, Papa~!", new Color(102, 20, 48));
                    Main.NewText("Again..?", new Color(72, 78, 117));
                }
                else
                {
                    Main.NewText("Girls..? Help your father with this insignificant mortal.", Color.DarkMagenta);
                    Main.NewText("With pleasure, Papa~!", new Color(102, 20, 48));
                    Main.NewText("Yes, father.", new Color(72, 78, 117));
                }

                SpawnBoss(player, "FuryAshe", "");
                SpawnBoss(player, "WrathHaruka", "");
            }

            if (npc.life <= npc.lifeMax * 0.80f && !Health4 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    BaseUtility.Chat("You are quite persistent, Child. I like that.", Color.DarkMagenta.R,
                        Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    BaseUtility.Chat("What's this? Competence? I would have never expected...", Color.DarkMagenta.R,
                        Color.DarkMagenta.G, Color.DarkMagenta.B);
                }

                Health4 = true;
                npc.netUpdate = true;
            }

            if (npc.life <= npc.lifeMax * 0.66f && !Health3 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    BaseUtility.Chat("True warriors don't show mercy! I won't and neither will you!",
                        Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    BaseUtility.Chat("Give up, child. The world will always fall into chaos!", Color.DarkMagenta.R,
                        Color.DarkMagenta.G, Color.DarkMagenta.B);
                }

                Health3 = true;
                npc.netUpdate = true;
            }

            if (npc.life <= npc.lifeMax * 0.30f && !Health1 && !isAwakened)
            {
                if (AAWorld.downedShen)
                {
                    BaseUtility.Chat("SHOW NO MERCY!", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    BaseUtility.Chat("What? You're still fighting? Why?!", Color.DarkMagenta.R, Color.DarkMagenta.G,
                        Color.DarkMagenta.B);
                }

                Health1 = true;
                npc.netUpdate = true;
            }
        }


        public void SpawnBoss(Player player, string name, string displayName)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType))
                {
                    return;
                } //don't spawn if there's already a boss!

                int npcID = NPC.NewNPC((int) player.Center.X, (int) player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center -
                                         new Vector2(MathHelper.Lerp(-300f, 300f, (float) Main.rand.NextDouble()),
                                             300f);
                Main.npc[npcID].netUpdate2 = true;
            }
        }

        public override void NPCLoot()
        {
            if (isAwakened)
            {
                if (Main.expertMode)
                {
                    BaseAI.DropItem(npc, mod.ItemType("ShenATrophy"), 1, 1, 15, true);
                    NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, mod.NPCType<ShenDeath>());
                    npc.DropBossBags();
                    AAWorld.downedShen = true;
                }
            }
            else
            {
                if (!Main.expertMode)
                {
                    AAWorld.downedShen = true;
                    npc.DropLoot(mod.ItemType("ChaosScale"), 20, 30);
                    string[] lootTable = {"ChaosSlayer", "MeteorStrike", "Skyfall"};
                    int loot = Main.rand.Next(lootTable.Length);
                    npc.DropLoot(mod.ItemType(lootTable[loot]));
                    BaseAI.DropItem(npc, mod.ItemType("ShenTrophy"), 1, 1, 15, true);
                    Main.NewText(
                        "Heh, alright. I’ll leave you alone I guess. But if you come back stronger, I’ll show you the power of true unyielding chaos…",
                        Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                }
                else
                {
                    NPC.NewNPC((int) npc.Center.X, (int) npc.Center.Y, mod.NPCType<ShenTransition>());
                }

                BaseAI.DropItem(npc, mod.ItemType("ShenTrophy"), 1, 1, 15, true);
                npc.value = 0f;
                npc.boss = false;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Texture2D currentTex = (npc.spriteDirection == 1
                ? mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlue")
                : Main.npcTexture[npc.type]);
            Texture2D currentWingTex = (npc.spriteDirection == 1
                ? mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonBlueWings")
                : mod.GetTexture("NPCs/Bosses/Shen/ShenDoragonWings"));

            //offset
            npc.position.Y += 130f;

            //draw body/charge afterimage
            if (Charging)
            {
                BaseDrawing.DrawAfterimage(sb, currentTex, 0, npc, 1.5f, 1f, 3, false, 0f, 0f,
                    new Color(drawColor.R, drawColor.G, drawColor.B, 150));
            }

            BaseDrawing.DrawTexture(sb, currentTex, 0, npc, npc.GetAlpha(drawColor), false);
            //draw wings
            BaseDrawing.DrawTexture(sb, currentWingTex, 0, npc.position + new Vector2(0, npc.gfxOffY), npc.width,
                npc.height, npc.scale, npc.rotation, npc.spriteDirection, 5, wingFrame, npc.GetAlpha(drawColor), false);

            //deoffset
            npc.position.Y -= 130f;
            return false;
        }
    }
}