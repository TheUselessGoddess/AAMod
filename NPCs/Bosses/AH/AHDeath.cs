using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.AH
{
    public class AHDeath : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sisters Defear");
        }

        public override void SetDefaults()
        {
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.width = 100;
            npc.height = 100;
            npc.friendly = false;
            npc.lifeMax = 1;
            npc.dontTakeDamage = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.timeLeft = 10;

            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            npc.ai[1]++;
            npc.TargetClosest();
            Player player = Main.player[npc.target];

            npc.Center = player.Center;

            AAMod.AHIntro = true;

            if (npc.ai[1] == 100) //if the timer has gotten to 7.5 seconds, this happens (60 = 1 second)
            {
                if (AAWorld.downedSisters)
                {
                    Main.NewText("RRRRRRRRRGH! NOT AGAIN!!!", new Color(102, 20, 48));
                }
                else
                {
                    Main.NewText("You see Ashe? I told you this was a stupid idea, but YOU didn't listen...",
                        new Color(72, 78, 117));
                }
            }

            if (npc.ai[1] == 300)
            {
                if (AAWorld.downedSisters)
                {
                    Main.NewText("Why do I keep going with you..? I should honestly just let you fight them yourself.",
                        new Color(72, 78, 117));
                }
                else
                {
                    Main.NewText(
                        "Shut up! I thought if we ganged up on " + (player.Male ? "him" : "her") +
                        ", we could just beat the daylights out of 'em!", new Color(102, 20, 48));
                }
            }

            if (npc.ai[1] == 500)
            {
                if (AAWorld.downedSisters)
                {
                    Main.NewText("Rgh..! Shut up..!", new Color(102, 20, 48));
                    AAMod.AHIntro = false;
                    npc.active = false;
                    AAWorld.downedSisters = true;
                }
                else
                {
                    Main.NewText("Whatever...I'm going back home. SOMEONE has to tell dad about this kid.",
                        new Color(72, 78, 117));
                }
            }

            if (npc.ai[1] == 700)
            {
                Main.NewText("Hmpf..! Fine! Be that way! I'm going back to the inferno!", new Color(102, 20, 48));
                AAMod.AHIntro = false;
                npc.active = false;
                AAWorld.downedSisters = true;
            }
        }
    }
}