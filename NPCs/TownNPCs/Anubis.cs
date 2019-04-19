using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Anubis : ModNPC
    {
        public override string Texture
        {
            get { return "AAMod/NPCs/TownNPCs/Anubis"; }
        }

        public override bool Autoload(ref string name)
        {
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            npc.dontTakeDamageFromHostiles = true;
            NPCID.Sets.ExtraFramesCount[npc.type] = 10;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 20;
            NPCID.Sets.HatOffsetY[npc.type] = 2;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 56;
            npc.aiStyle = 7;
            npc.damage = 10;
            npc.defense = 68;
            npc.lifeMax = 160000;
            npc.HitSound = SoundID.NPCHit23;
            npc.DeathSound = SoundID.NPCDeath39;
            npc.knockBackResist = 0f;
            animationType = NPCID.Guide;
            npc.lavaImmune = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active)
                {
                    return true;
                }
            }

            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(4))
            {
                default:
                    return "Anubis";
            }
        }

        public static bool SwitchInfo = false;
        public static bool DoNext = false;
        public static bool Mushroom = false;
        public static bool Glowshroom = false;
        public static bool Grips = false;
        public static bool Brood = false;
        public static bool Hydra = false;
        public static bool Djinn = false;
        public static bool Serpent = false;
        public static bool Retriever = false;
        public static bool Raider = false;
        public static bool Orthrus = false;
        public static bool Equinox = false;
        public static bool AnubisB = false;
        public static bool Sisters = false;
        public static bool Akuma = false;
        public static bool Yamata = false;
        public static bool Zero = false;
        public static bool Shen = false;
        public static bool BaseChat = false;
        public static int ChatNumber = 0;

        public override void ResetEffects()
        {
            SwitchInfo = false;
            DoNext = false;
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            Raider = false;
            Retriever = false;
            Orthrus = false;
            Equinox = false;
            AnubisB = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            string SwitchInfoT = "Switch Info";

            string DoNextT = "What Do I do now?";

            string MushT = "Mushrooms. I'm serious.";

            string GlowT = "More Mushrooms.";

            string GripT = "Get a Grip";

            string BroodT = "Deadweight Dragon";

            string HydraT = "Three-Headed Freak";

            string DjinnT = "3 Wishes";

            string SerpentT = "Snakes. Why is it always snakes?";

            string RetT = "Gotcha'";

            string RaidT = "Mechanical Mass";

            string OrthrusT = "Two heads; zero brains";

            string EquinoxT = "More worm bosses god dammit.";

            string AnubisT = "Ancient of Judgement";

            string SistersT = "Terrible Twins";

            string AkumaT = "Ancient of Fury";

            string YamataT = "Ancient of Wrath";

            string ZeroT = "Ancient of Null";

            string ShenT = "Discordian Death";

            Player player = Main.player[Main.myPlayer];

            button = SwitchInfoT;

            if (ChatNumber == 0)
            {
                button2 = DoNextT;
                DoNext = true;
            }
            else if (ChatNumber == 1)
            {
                button2 = MushT;
                Mushroom = true;
            }
            else if (ChatNumber == 2)
            {
                button2 = GlowT;
                Glowshroom = true;
            }
            else if (ChatNumber == 3)
            {
                button2 = GripT;
                Grips = true;
            }
            else if (ChatNumber == 4)
            {
                button2 = BroodT;
                Brood = true;
            }
            else if (ChatNumber == 5)
            {
                button2 = HydraT;
                Hydra = true;
            }
            else if (ChatNumber == 6 && NPC.downedBoss3)
            {
                button2 = DjinnT;
                Djinn = true;
            }
            else if (ChatNumber == 7 && NPC.downedBoss3)
            {
                button2 = SerpentT;
                Serpent = true;
            }
            else if (ChatNumber == 8 && Main.hardMode)
            {
                button2 = RetT;
                Retriever = true;
            }
            else if (ChatNumber == 9 && Main.hardMode)
            {
                button2 = RaidT;
                Raider = true;
            }
            else if (ChatNumber == 10 && Main.hardMode)
            {
                button2 = OrthrusT;
                Orthrus = true;
            }
            else if (ChatNumber == 11 && NPC.downedMoonlord)
            {
                button2 = EquinoxT;
                Equinox = true;
            }
            else if (ChatNumber == 12 && NPC.downedMoonlord)
            {
                button2 = AnubisT;
                AnubisB = true;
            }
            else if (ChatNumber == 13 && NPC.downedMoonlord && AAWorld.downedEquinox)
            {
                button2 = SistersT;
                Sisters = true;
            }
            else if (ChatNumber == 14 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = AkumaT;
                Akuma = true;
            }
            else if (ChatNumber == 15 && NPC.downedMoonlord && AAWorld.downedSisters)
            {
                button2 = YamataT;
                Yamata = true;
            }
            else if (ChatNumber == 16 && NPC.downedMoonlord && AAWorld.downedNC)
            {
                button2 = ZeroT;
                Zero = true;
            }
            else if (ChatNumber == 17 && AAWorld.downedAllAncients)
            {
                button2 = SistersT;
                Zero = true;
            }
            else
            {
                ChatNumber = 0;
                button2 = DoNextT;
                DoNext = true;
            }
        }

        public void ResetBools()
        {
            Mushroom = false;
            Glowshroom = false;
            Grips = false;
            Brood = false;
            Hydra = false;
            Djinn = false;
            Serpent = false;
            Raider = false;
            Retriever = false;
            Orthrus = false;
            Equinox = false;
            DoNext = false;
            Sisters = false;
            Akuma = false;
            Yamata = false;
            Zero = false;
            Shen = false;
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                ResetBools();
                ChatNumber += 1;
                if (ChatNumber > 17)
                {
                    ChatNumber = 0;
                }
            }
            else
            {
                Player player = Main.player[Main.myPlayer];
                Main.npcChatText = BossChat();
            }
        }

        public static string BossChat()
        {
            if (Mushroom)
            {
                return AAWorld.downedMonarch
                    ? "...that was it?"
                    : "Hey, you know all these little red mushrooms growing everywhere? I hear if you squish a bunch of them together and wave them around, their king or something will come and attempt to run you down. I gotta see that.";
            }
            else if (Glowshroom)
            {
                return AAWorld.downedFungus
                    ? "Nice work. Now be honest, how high are you right now?"
                    : "The glowing mushroom caves always make me feel loopy for some reason. Anyways, you want better magic abilities? There's a big mushroom monster that has some great magic abilities infused into it. Just plug your nose while your down there.";
            }
            else if (Grips)
            {
                return AAWorld.downedGrips
                    ? "Nice job taking down those giant hands. Maybe the little ones will finally leave me alone for once."
                    : "Those flying claws at night are a nightmare to deal with, and they freak me out. In my travels, I've come across these two REEEEEEEEALLY big ones. Maybe if you kill them, the little ones will bugger off. Maybe killing a few of them and showing that you have in some way will call them down.";
            }
            else if (Brood)
            {
                return AAWorld.downedBrood
                    ? "That was the fattest dragon I've ever seen in my life."
                    : "Like dragons? No? Too bad. You want better gear? You gotta go kill of this HUGE dragon made of lava. She's big, scary, and WAAAAAAAAAAAY too fat to fly, but she does anyways.";
            }
            else if (Hydra)
            {
                return AAWorld.downedHydra
                    ? "Good thing is that those Hydras are not like in Greek mythology. They are not totally immortal like Lernean Hydra."
                    : "Mire is a native place for creatures, named Hydras. They are 3 headed, very poisonous reptiles. And they also laying a lot of eggs. Go and kill some. Maybe Mire will become a bit friendlier after that.";
            }
            else if (Djinn)
            {
                return AAWorld.downedDjinn
                    ? "You actually found and defeated one? And you even didn't try to make a wish? What a shame..."
                    : "I am sure you heard some Arabian fairy tales before. Often, they have a character, named Djinn or Genie. It mostly showed there like almighty and kind spirit, which may present 3 wishes to his liberator. In reality, however, they are smart and evil creatures, whos only purpose is to create chaos. If you will see one, do not try to talk, just kill it.";
            }
            else if (Serpent)
            {
                return AAWorld.downedSerpent
                    ? "How are your frostbites? I hope that they are not as deep as they look."
                    : "Normal serpents are usually hibernate while it is cold around. But this exact kind of serpents behaves diffirently. They are very active during cold times. But the strangest thing is that they are capable to fly and breathe subzero frost. I am sure you can find something useful in their lair, so go and kill one.";
            }
            else if (Retriever)
            {
                return AAWorld.downedRetriever
                    ? "My only hope is that Fulgarians will not send another one and cause time paradox."
                    : "You heard about the probe which floats somewhere in night time? Nobody knows what it is doing here. It also looks pretty dangerous. It would be better if you will stop it forever.";
            }
            else if (Raider)
            {
                return AAWorld.downedRaider
                    ? "So it was a giant robot after all? Well, sometimes people just overthinking the problem. Also, maybe you should show you wounds to Nurse?"
                    : "People said that they saw strange shade in one of the nights. It was so big that it even closed the moon for a moment. You should discover this mystery and do something.";
            }
            else if (Orthrus)
            {
                return AAWorld.downedOrthrus
                    ? "I guess orthrus is what it eats, now."
                    : "Remeber the Hydra? There's a bigger one out there. And it's a robot. And it shoots Electricity. So uh...good luck!";
            }
            else if (Equinox)
            {
                return AAWorld.downedEquinox
                    ? "Nice job taking out the Equinox worms. I could tell you did because it's like a week later now. I hope I didn't miss my nurse's appointment..."
                    : "Like worms? Me neither, but guess what? There are 2 big ones that control the flow of day and night, and they're tough buggers. Good luck.";
            }
            else if (AnubisB)
            {
                return
                    "I hear there’s this Anubis guy that’s really jacked and handsome, and all the ladies love him for his amazing soul-judging abilities. What a guy.";
            }
            else if (Sisters)
            {
                return AAWorld.downedSisters
                    ? "Nice, you taught those two spoiled brats a lesson! Those two didn't see it coming!"
                    : "Remember ol' Brood and Hydra? Well, those two have daughters. And MAN they're annoying..! Every time I go into the chaoses, those two are just waiting to ruin my day! Can you go give em' the ol' one-two?";
            }
            else if (Akuma)
            {
                return AAWorld.downedAkuma
                    ? "Akuma thinks he's edgy. To me, he just comes across as trying to be way too cool and failing. Anyways, might wanna run some water through your hair. You got a little singed up there."
                    : "Why would anyone call a sun serpent a demon? I have no idea personally...but Akuma has got to go. He always glasses my deserts with his fire and it pisses me off.";
            }
            else if (Yamata)
            {
                return AAWorld.downedYamata
                    ? "Thanks for shutting up that 7-headed sissy. He makes me want to tear my fur out."
                    : "Yamata, the whiny nit! He complains about everything, and he WONT SHUT UP! LIKE SERIOUSLY, YOU try and deal with seven obnoxiously loud dragon heads that chatter constantly and talk over eachother!";
            }
            else if (Zero)
            {
                return AAWorld.downedZero
                    ? "...I'll be honest. I don't like what that thing said after it died."
                    : "You know the void? Those spooky floating islands to the east? There's a BIG scary machine there that's always just floating there. Anyways, after you slammed the moon lord, I heard a massive shockwave come from the void. Could you check it out for me?";
            }
            else if (Shen)
            {
                return AAWorld.downedShen
                    ? "Holy-- You BEAT Shen Doragon?! ...I'm suddenly a lot more intimidated by you, kid."
                    : "Akuma and Yamata...you know, those two were once one being. And hot dang, that guy was powerful. He leveled 2 civilizations one time. Anyways, so what was it that you needed?";
            }
            else
            {
                return "I dunno? Ask me about any strong creatures you can fight currently.";
            }
        }

        public override string GetChat()
        {
            Mod GRealm = ModLoader.GetMod("Grealm");
            Mod Fargos = ModLoader.GetMod("Fargowiltas");
            Mod Alchemist = ModLoader.GetMod("AlchemistNPC");
            Mod AlchemistLite = ModLoader.GetMod("AlchemistNPCLite");
            Mod Redemption = ModLoader.GetMod("Redemption");
            Mod Thorium = ModLoader.GetMod("ThoriumMod");

            int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(GRealm.NPCType("HordeZombie"));
            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(Fargos.NPCType("Mutant"));
            int Newb = Redemption == null ? -1 : NPC.FindFirstNPC(Redemption.NPCType("Newb"));
            int Cobbler = Thorium == null ? -1 : NPC.FindFirstNPC(Thorium.NPCType("Cobbler"));
            int ConfusedZombie = Thorium == null ? -1 : NPC.FindFirstNPC(Thorium.NPCType("ConfusedZombie"));

            WeightedRandom<string> chat = new WeightedRandom<string>();

            chat.Add("You wouldn’t happen to be good at belly rubs would you?");
            chat.Add(
                "You know that awful feeling of getting sand in your swim trunks after going to the beach? Imagine having that all the time. Welcome to my life.");
            chat.Add("For the thousandth time, I AM NOT A FURRY!");
            chat.Add("Hey, I got this really bad itch on my back. Could ya get it for me?");
            chat.Add(@"No. I won’t wear a flea collar.
 
*Scratch Scratch*");
            chat.Add("Everyone asks me who's a good boy, but I'm upset because they never tell me who it is.");
            chat.Add("Have you seen my tail? I need to teach it a thing or two.");
            chat.Add("The Desert Djinn may be ripped but he's got nothing on me! Check it!");
            chat.Add(
                "Thanks for letting me crash here by the way. Walking around the desert for a couple thousand years really tuckers ya out.");
            chat.Add(
                "I wrote the Terraria Historia, yes. But I also wrote another great book. 'The Life and Epic Adventures of Anubis the Wonder Dog!' Want a copy?");
            chat.Add("Don't you hate it when " + (WorldGen.crimson ? "red fleshy crap" : "purple muggy crap") +
                     " takes over your biome? it's disgusting.");
            chat.Add(
                "What creature do I hate most? Oh that's easy, King Slime. If that thing lands on you, good luck washing the slime out of your clothes or fur without a blowtorch.");

            Player player = Main.player[Main.myPlayer];


            int FemaleNPC = NPC.FindFirstNPC(FindFemaleNPC());


            if (Main.bloodMoon && FemaleNPC != NPCID.PartyGirl)
            {
                chat.Add("Geeze, I tried hitting on " + Main.npc[FemaleNPC].GivenName +
                         " earlier and they kicked the bajeezus out of me. What is it with these ladies and blood moons?");
            }
            else if (Main.bloodMoon && FemaleNPC == NPCID.PartyGirl)
            {
                chat.Add("I tried hitting on " + Main.npc[FemaleNPC].GivenName +
                         " and she was totally polite to me. That's...odd...especially during a blood moon.");
            }

            if (player.head == 200 && player.body == 198 && player.legs == 142)
            {
                chat.Add("Hey, nice outfit.");
            }

            if (AAWorld.DiscoBall > 0)
            {
                chat.Add("Disco is still popular with you terrarians, right? I can do a mean boogie.");
            }

            if (HordeZombie >= 0)
            {
                chat.Add("Hey uh...if anyone asks, " + Main.npc[HordeZombie].GivenName +
                         " got all of his info from me, got it?");
            }

            if (Mutant >= 0)
            {
                chat.Add("That " + Main.npc[Mutant].GivenName +
                         " is pretty chill. Knows a lot about bosses too...actually I hope he isn't stalking me or anything.");
            }

            if (Newb >= 0)
            {
                chat.Add("I think " + Main.npc[Newb].GivenName +
                         " isn't letting in on how smart he actually is. I mean look at that face. Pure. Raw. Genius.");
            }

            if (Cobbler >= 0)
            {
                chat.Add(Main.npc[Cobbler].GivenName +
                         " keeps yelling at me for eating all the shoes he makes. It'snot my fault he makes them with gourmet lether.");
            }

            if (ConfusedZombie >= 0)
            {
                chat.Add("Don't ask " + Main.npc[ConfusedZombie].GivenName + " where he gets his merch. It's nasty.");
            }

            return chat;
        }

        public static string WHATTHEFUCKDOIDOANUBIS()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();

            return chat;
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<JudgementNPC>();
            attackDelay = 5;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection,
            ref float randomOffset)
        {
            multiplier = 4f;

            randomOffset = 2f;
        }

        public int FindFemaleNPC()
        {
            int FemaleNPC = Main.rand.Next(6);
            switch (FemaleNPC)
            {
                case 0:
                    FemaleNPC = NPCID.Nurse;
                    break;
                case 1:
                    FemaleNPC = NPCID.Dryad;
                    break;
                case 2:
                    FemaleNPC = NPCID.Stylist;
                    break;
                case 3:
                    FemaleNPC = NPCID.Mechanic;
                    break;
                case 4:
                    FemaleNPC = NPCID.Steampunker;
                    break;
                default:
                    FemaleNPC = NPCID.PartyGirl;
                    break;
            }

            return FemaleNPC;
        }
    }
}