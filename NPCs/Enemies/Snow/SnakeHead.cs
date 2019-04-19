﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Snow
{
    public class SnakeHead : ModNPC
    {
        bool TailSpawned = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snow Serpent");
        }

        public override void SetDefaults()
        {
            npc.damage = 20;
            npc.npcSlots = 5f;
            npc.damage = 35;
            npc.width = 20;
            npc.height = 20;
            npc.defense = 13;
            npc.lifeMax = 250;
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            animationType = 10;
            npc.behindTiles = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit5;
            npc.DeathSound = SoundID.NPCDeath7;
            npc.netAlways = true;
            npc.value = Item.buyPrice(0, 0, 10, 0);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.ZoneSnow &&
                   NPC.downedBoss3 &&
                   !Main.dayTime
                ? .2f
                : 0f;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            BaseMod.BaseAI.AIWorm(npc,
                new int[]
                {
                    mod.NPCType("SnakeHead"), mod.NPCType("SnakeBody"), mod.NPCType("SnakeBody2"),
                    mod.NPCType("SnakeTail")
                }, 9, 8f, 12f, 0.1f, false, false);

            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            if (Main.expertMode)
            {
                player.AddBuff(BuffID.Chilled, 200, true);
            }
            else
            {
                player.AddBuff(BuffID.Chilled, 100, true);
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.IceDust>(), hitDirection, -1f, 0,
                    default(Color), 1f);
            }

            if (npc.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.SnowDustLight>(), hitDirection,
                        -1f, 0, default(Color), 1f);
                }
            }
        }

        public override void NPCLoot()
        {
            npc.DropLoot(mod.ItemType("SubzeroCrystal"));
        }
    }
}