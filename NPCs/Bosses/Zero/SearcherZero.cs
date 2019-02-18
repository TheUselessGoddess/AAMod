using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Zero
{
	public class SearcherZero : ModNPC
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Searcher");	
		}		

        public override void SetDefaults()
        {
            npc.width = 35;
            npc.height = 35;
            npc.value = BaseMod.BaseUtility.CalcValue(0, 0, 5, 50);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 250;
            npc.defense = 30;
            npc.damage = 65;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
			npc.noGravity = true;
            
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            bool isDead = npc.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                int dustType = (mod.DustType<Dusts.VoidDust>());
                Dust.NewDust(npc.position, npc.width, npc.height, dustType, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, (isDead ? 2f : 1.1f));
            }
        }

        float shootAI = 0;
        public override void AI()
        {
            BaseMod.BaseAI.AISkull(npc, ref npc.ai, false, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[npc.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseMod.BaseAI.LookAt((playerActive ? player.Center : (npc.Center + npc.velocity)), npc, 0);
            if (Main.netMode != 1 && playerActive)
            {
                shootAI++;
                if (shootAI >= 90)
                {
                    shootAI = 0;
                    int projType = mod.ProjType("DeathLaser");
                    if (Collision.CanHit(npc.position, npc.width, npc.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, npc, projType, (int)(npc.damage * 0.25f), 0f, 2f);
                }
            }
        }

        public static Texture2D glowTex = null;
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/SearcherZero_Glow");
            }
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, npc, auraPercent, 1f, 0f, 0f, Color.Red);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, Color.Red);
            return false;
        }
	}
}