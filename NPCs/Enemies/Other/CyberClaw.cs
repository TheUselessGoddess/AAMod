using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Other
{
    public class CyberClaw : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cyber Claw");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 24;
            npc.friendly = false;
            npc.damage = 35;
            npc.defense = 4;
            npc.lifeMax = 300;
            npc.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            npc.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            npc.value = 1f;
            npc.knockBackResist = 0.6f;
        }

        public override void AI()
        {
            BaseMod.BaseAI.AIEye(npc, ref npc.ai, true, false, 0.1f, 0.04f, 6f, 3f, 1f, 1f);
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (AAWorld.downedRetriever)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.12f;
            }
            else
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0f;
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CyberClawGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CyberClawGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CyberClawGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CyberClawGore4"), 1f);
            }
        }
    }
}