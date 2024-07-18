namespace TextRPG
{

    internal class Program
    {
        enum Scene { Select, Confirm, Town, Forest, Swarm, Inn, Shop, SecretOldRoom, Ruin, DragonNest, Gambling }
        enum Job { Warrior = 1, Mage, Rogue }
        enum Skill { PowerSlash = 1, FireBall, 암살 }
        enum Monster { 슬라임, 고블린, 골렘, 드래곤 }
        enum Item { OldKey = 1, SilverKey, flower, 골렘조각, 용의비늘, 용의이빨}
        // Item[0] = OldKey;
        // Item[1] = SilverKey;
        // Item[2] = flower;

        struct GameData
        {
            public bool running;

            public Scene scene;

            public string name;
            public Job job;
            public int curHP;
            public int maxHP;
            public int STR;
            public int INT;
            public int DEX;
            public int gold;
            public int minGold;
            public string[] inventory;
            public int level;
            public int maxExp;
            public int exp;
            public string[] skill;
        }

        static GameData data;

        static void Main(string[] args)
        {
            Start();

            while (data.running)
            {
                Run();
            }



            End();
        }


        static void Start()
        {
            data = new GameData();
            data.inventory = new string[10];
            data.skill = new string[1];
            data.level = 1;
            data.maxExp = 100;
            data.minGold = 0;
            data.running = true;

            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=           막 장 RPG              =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ReadKey();
        }

        static void End()
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=            게임 오버!            =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
        }

        static void Run()
        {
            Console.Clear();

            if (data.exp >= data.maxExp)
            {
                data.level += 1;
                data.exp = 0;

                data.maxHP += 100;
                data.curHP = data.maxHP;
                data.STR += 10;
                data.INT += 10;
                data.DEX += 10;
            }

            if (data.curHP >= data.maxHP)
            {
                data.curHP = data.maxHP;
            }

            if (data.gold <= data.minGold)
            {
                data.gold = data.minGold;
            }

            if (data.level == 3) // 3레벨 달성시 스킬 획득
            {
                if (data.job == Job.Warrior)
                {
                    data.skill[0] = Skill.PowerSlash.ToString();
                }
                if (data.job == Job.Mage)
                {
                    data.skill[0] = Skill.FireBall.ToString();
                }
                if (data.job == Job.Rogue)
                {
                    data.skill[0] = Skill.암살.ToString();
                }
            }

            switch (data.scene)
            {
                case Scene.Select:
                    SelectScene();
                    break;
                case Scene.Confirm:
                    ConfirmScene();
                    break;
                case Scene.Town:
                    TownScene();
                    break;
                case Scene.Forest:
                    ForestScene();
                    break;
                case Scene.Swarm:
                    SwarmScene();
                    break;
                case Scene.Inn:
                    InnScene();
                    break;
                case Scene.Shop:
                    ShopScene();
                    break;
                case Scene.SecretOldRoom:
                    SecretOldRoomScene();
                    break;
                case Scene.Ruin:
                    RuinScene();
                    break;
                case Scene.DragonNest:
                    DragonNestScene();
                    break;
                case Scene.Gambling:
                    GamblingScene();
                    break;

            }
        }

        static void PrintProfile()
        {
            Console.WriteLine("=====================================");
            Console.WriteLine($"이름 : {data.name,-6} 직업 : {data.job,-6}");
            Console.WriteLine($"체력 : {data.curHP,+3} / {data.maxHP}");
            Console.WriteLine($"힘 : {data.STR,-3} 지력 : {data.INT,-3} 민첩 : {data.DEX,-3}");
            Console.WriteLine($"소지금 : {data.gold,+5} G");
            Console.WriteLine($"경험치 : {data.exp,+5} exp  /  레벨 : {data.level,+5}");
            Console.WriteLine($"스킬 : {data.skill[0],+5} ");
            Console.WriteLine("=====================================");
            Console.WriteLine();
        }


        static void Wait(float seconds)
        {
            Thread.Sleep((int)(seconds * 1000));
        }

        static void SelectScene()
        {
            Console.Write("캐릭터의 이름을 입력하세요 : ");
            data.name = Console.ReadLine();
            if (data.name == "")
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            Console.WriteLine("직업을 선택하세요.");
            Console.WriteLine("1. 전사");
            Console.WriteLine("2. 마법사");
            Console.WriteLine("3. 도적");
            if (int.TryParse(Console.ReadLine(), out int select) == false)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }
            else if (Enum.IsDefined(typeof(Job), select) == false)
            {
                Console.WriteLine("잘못된 입력입니다.");
                return;
            }

            switch ((Job)select)
            {
                case Job.Warrior:
                    data.job = Job.Warrior;
                    data.maxHP = 200;
                    data.curHP = data.maxHP;
                    data.STR = 16;
                    data.INT = 8;
                    data.DEX = 12;
                    data.gold = 100;

                    break;
                case Job.Mage:
                    data.job = Job.Mage;
                    data.maxHP = 80;
                    data.curHP = data.maxHP;
                    data.STR = 6;
                    data.INT = 20;
                    data.DEX = 8;
                    data.gold = 300;

                    break;
                case Job.Rogue:
                    data.job = Job.Rogue;
                    data.maxHP = 120;
                    data.curHP = data.maxHP;
                    data.STR = 10;
                    data.INT = 10;
                    data.DEX = 16;
                    data.gold = 0;

                    break;
            }
            data.scene = Scene.Confirm;
        }

        static void ConfirmScene()
        {
            // Render
            Console.WriteLine("===================");
            Console.WriteLine($"이름 : {data.name}");
            Console.WriteLine($"직업 : {data.job}");
            Console.WriteLine($"체력 : {data.maxHP}");
            Console.WriteLine($"힘   : {data.STR}");
            Console.WriteLine($"지력 : {data.INT}");
            Console.WriteLine($"민첩 : {data.DEX}");
            Console.WriteLine($"소지금 : {data.gold}");
            Console.WriteLine($"레벨 : {data.level}");
            Console.WriteLine($"경험치 : {data.exp}");
            Console.WriteLine("===================");
            Console.WriteLine();
            Console.Write("이대로 플레이 하시겠습니까?(y/n) : ");

            // Input
            string input = Console.ReadLine();

            // Update
            switch (input)
            {
                case "Y":
                case "y":
                    Console.Clear();
                    Console.WriteLine("마을로 이동합니다...");
                    Wait(2);
                    data.scene = Scene.Town;
                    break;
                case "N":
                case "n":
                    data.scene = Scene.Select;
                    break;
                default:
                    data.scene = Scene.Confirm;
                    break;
            }
        }

        static void TownScene()
        {
            data.exp += 30;
            // Render
            PrintProfile();
            Console.WriteLine("따뜻한 마을이다. 사람들이 많다.");
            Console.WriteLine("어디로 이동하겠습니까?");
            Console.WriteLine();
            Console.WriteLine("1. 여관");
            Console.WriteLine();
            Console.WriteLine("2. 상점");
            Console.WriteLine();
            Console.WriteLine("3. 마을 밖 숲");
            Console.WriteLine();
            Console.WriteLine("4. 마을 뒤 늪");
            Console.WriteLine();
            Console.WriteLine("5. 유적지");
            Console.WriteLine();
            Console.WriteLine("6. 도박장 - 참가비 100G");
            Console.WriteLine();
            Console.WriteLine("7. 인벤토리");
            Console.WriteLine();
            Console.Write("선택 : ");

            // Input
            string input = Console.ReadLine();

            // Update
            switch (input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("여관으로 이동합니다...");
                    Wait(2);
                    data.scene = Scene.Inn;
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("상점으로 이동합니다...");
                    Wait(2);
                    data.scene = Scene.Shop;
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("마을 밖 숲으로 이동합니다...");
                    Wait(2);
                    data.scene = Scene.Forest;
                    break;
                case "4":
                    Console.Clear();
                    Console.WriteLine("마을 뒤 늪으로 이동합니다.");
                    Wait(2);
                    data.scene = Scene.Swarm;
                    break;
                case "5":
                    Console.Clear();
                    Console.WriteLine("유적지로 이동합니다.");
                    Wait(2);
                    data.scene = Scene.Ruin;
                    break;
                case "6":
                    Console.Clear();
                    Console.WriteLine("도박장으로 이동합니다.");
                    Wait(2);
                    data.scene = Scene.Gambling;
                    break;
                case "7":
                    Console.Clear();
                    Console.WriteLine("=인벤토리==");
                    Wait(2);
                    foreach (string itemName in data.inventory)
                    {
                        Console.WriteLine(itemName);
                    }

                    Console.WriteLine("아무키나 눌러 닫기");
                    Console.ReadKey();
                    break;

            }
        }

        static void ForestScene()
        {
            Console.WriteLine("적막한 숲입니다.");
            Wait(1);
            Console.WriteLine("갑작스럽게 당신 앞에 슬라임이 나타났습니다.");
            Wait(1);
            Console.WriteLine();
            Console.WriteLine("당신의 행동을 선택해주세요");
            Console.WriteLine("1. 슬라임을 공격한다.(힘)");
            Console.WriteLine("2. 슬라임을 주시하며 공격마법을 시전한다.(지력)");
            Console.WriteLine("3. 슬라임이 눈치채지 못하게 뒤로 접근하여 공격한다.(민첩)");
            Console.Write("선택 : ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (data.STR > 14)
                    {

                        Console.WriteLine("당신의 공격은 슬라임에게 치명적이었습니다!");
                        Wait(1);
                        Console.WriteLine("슬라임이 쓰러졌습니다!");
                        Wait(1);
                        Console.WriteLine("100 골드를 얻었습니다!");
                        Wait(1);
                        data.gold += 100;
                        data.exp += 100;
                    }
                    else
                    {
                        Console.WriteLine("당신의 공격은 슬라임에게 무의미 했습니다!");
                        Wait(1);
                        Console.WriteLine("슬라임이 반격했습니다!");
                        Wait(1);
                        Console.WriteLine("30의 체력 피해를 받았습니다!");
                        Wait(1);
                        data.curHP -= 30;
                    }
                    break;
                case "2":
                    if (data.INT > 12)
                    {
                        Console.WriteLine("당신의 마법은 슬라임에게 치명적이었습니다!");
                        Wait(1);
                        Console.WriteLine("슬라임이 쓰러졌습니다!");
                        Wait(1);
                        Console.WriteLine("100 골드를 얻었습니다!");
                        Wait(1);
                        data.gold += 100;
                        data.exp += 100;
                        Console.WriteLine("마법을 사용하여 체력 5 소모되었습니다.");
                        data.curHP -= 5;
                    }
                    else
                    {
                        Console.WriteLine("당신의 공격은 슬라임에게 무의미 했습니다!");
                        Wait(1);
                        Console.WriteLine("슬라임이 반격했습니다!");
                        Wait(1);
                        Console.WriteLine("30의 체력 피해를 받았습니다!");
                        Wait(1);
                        data.curHP -= 30;
                    }
                    break;
                case "3":
                    Console.WriteLine("당신은 슬라임의 뒤로 재빠르게 접근했습니다!");
                    Wait(1);
                    Console.WriteLine("하지만 슬라임은 앞뒤 구분이 되지 않았습니다.");
                    Wait(1);
                    Console.WriteLine("슬라임이 반격했습니다!");
                    Wait(1);
                    Console.WriteLine("30의 체력 피해를 받았습니다!");
                    Wait(1);
                    data.curHP -= 30;
                    break;
                default:
                    break;
            }

            Console.Clear();
            Console.Write("마을로 돌아가시겠습니까?(y/n) : ");
            string hiddenInput1 = Console.ReadLine();

            switch (hiddenInput1)
            {
                case "y":
                case "Y":
                    {
                        Console.WriteLine("마을로 돌아갑니다...");
                        data.scene = Scene.Town;
                        break;
                    }
                case "N":
                case "n":
                    {
                        Console.WriteLine("적막한 숲에서 더 깊숙한 곳으로 간다.");
                        Wait(1);
                        Console.WriteLine("허공에 낡은 문이 하나 서있다.");
                        Wait(1);
                        Console.WriteLine("열쇠 구멍이 있다.");
                        Wait(1);

                        bool isHavingKey = data.inventory[0] == Item.OldKey.ToString();
                        if (data.inventory[0] == Item.OldKey.ToString())
                        {
                            data.scene = Scene.SecretOldRoom;
                        }
                        else
                        {
                            Console.WriteLine("열쇠가 없어 열지 못한다..");
                            Wait(1);
                            Console.WriteLine("마을로 돌아가자.");
                            Wait(1);
                            data.scene = Scene.Town;
                        }
                        break;
                    }
            }

        }

        static void SecretOldRoomScene()
        {
            Console.WriteLine("낡은 열쇠를 이용하여 문을 열었다.");
            Wait(1);
            Console.WriteLine("보물상자가 있다. 열어보자!");
            Wait(1);
            Console.WriteLine("800골드와 100의 경험치를 얻었다.");
            Wait(1);

            data.gold += 800;
            data.exp += 100;
            data.inventory[0] = "";
            Console.WriteLine("마을로 돌아갑니다.");
            Wait(1);
            data.scene = Scene.Town;
        }

        static void SwarmScene()
        {
            Console.WriteLine("축축한 냄새가 나는 지역입니다.");
            if (data.DEX <= 12)
            {
                Wait(1);
                Console.WriteLine("늪이 당신을 잡아당기며 몸이 지치는 것이 느껴집니다.");
                Wait(1);
                Console.WriteLine("체력이 10 감소했습니다.");
                data.curHP -= 10;
            }

            Wait(2);
            Console.WriteLine("늪을 건너는 중 이상하게 생긴 식물을 발견했습니다.");

            if (data.INT >= 12)
            {
                Wait(1);
                Console.WriteLine("당신은 식물이 약초라는 것을 알았습니다.");
                Wait(1);
                Console.WriteLine("약초를 습득했습니다!");
                Wait(1);
                data.exp += 20;
                data.inventory[2] = Item.flower.ToString();

            }
            else
            {
                Wait(1);
                Console.WriteLine("당신은 식물에 대한 지식이 없습니다.");
                Wait(1);
                Console.WriteLine("지나쳐 버렸습니다...");
            }

            Wait(1);
            Console.WriteLine("늪지를 지나 다시 마을로 돌아갑니다...");

            data.scene = Scene.Town;
        }

        static void RuinScene()
        {
            Console.WriteLine("음산한 유적지입니다.");
            Wait(2);
            Console.WriteLine("굉음과 함께 골렘이 빠른 속도로 다가옵니다.");
            Wait(1);
            Console.WriteLine();
            Console.WriteLine("행동을 선택해주세요.");
            Console.WriteLine("1.골렘에게 다가가 물리공격을 한다.(힘)");
            Console.WriteLine("2.멀리 떨어져서 마법공격을 한다.(지능)");
            Console.WriteLine("3.달아나면서 골렘의 약점을 파악하여 공격한다.(민첩)");
            Console.WriteLine("4.스킬을 사용한다.(스킬 소지시)");
            Console.Write("선택 : ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        if (data.STR > 30)
                        {
                            Console.WriteLine("골렘에게 다가가 베었습니다.");
                            Wait(1);
                            Console.WriteLine("바위에 금이 가며 무너졌습니다.");
                            Wait(1);
                            Console.WriteLine("골렘을 잡았습니다.");
                            Wait(1);
                            Console.WriteLine("300 골드와 80의 경험치를 얻었습니다.");
                            Console.WriteLine($"{Item.골렘조각} 얻었습니다.");
                            Wait(1);
                            data.exp += 80;
                            data.gold += 300;
                            data.inventory[3] = Item.골렘조각.ToString();
                        }
                        else
                        {
                            Console.WriteLine("골렘에게 다가가 베었습니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 피식 웃었습니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 반격합니다.");
                            Wait(1);
                            Console.WriteLine("80의 피해를 받았습니다.");
                            Wait(1);
                            data.curHP -= 80;
                        }
                        break;
                    }
                case "2":
                    {
                        if (data.INT > 30)
                        {
                            Console.WriteLine("골렘에게 멀리 떨어져 마법공격을 합니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 파괴되었습니다.");
                            Wait(1);
                            Console.WriteLine("300 골드와 80의 경험치를 얻었습니다.");
                            Console.WriteLine($"{Item.골렘조각} 얻었습니다.");
                            Wait(1);
                            data.exp += 80;
                            data.gold += 300;
                            data.inventory[3] = Item.골렘조각.ToString();
                        }
                        else
                        {
                            Console.WriteLine("골렘에게 멀리 떨어져 마법공격을 합니다.");
                            Wait(1);
                            Console.WriteLine("마법공격이 튕겨져 나갑니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 반격합니다.");
                            Wait(1);
                            Console.WriteLine("80의 피해를 받았습니다.");
                            Wait(1);
                            data.curHP -= 80;
                        }
                        break;
                    }
                case "3":
                    {
                        if (data.DEX > 30)
                        {
                            Console.WriteLine("달아나면서 골렘의 약점을 파악합니다.");
                            Wait(1);
                            Console.WriteLine("눈이 약점인거 같습니다.");
                            Wait(1);
                            Console.WriteLine("공격하여 골렘을 쓰러트렸습니다.");
                            Wait(1);
                            Console.WriteLine("300 골드와 80의 경험치를 얻었습니다.");
                            Console.WriteLine($"{Item.골렘조각} 얻었습니다.");
                            Wait(1);
                            data.exp += 80;
                            data.gold += 300;
                            data.inventory[3] = Item.골렘조각.ToString();
                        }
                        else
                        {
                            Console.WriteLine("달아나면서 골렘의 약점을 파악합니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 너무 빨라 달아날수 없습니다.");
                            Wait(1);
                            Console.WriteLine("80의 피해를 받았습니다.");
                            Wait(1);
                            data.curHP -= 80;
                        }
                        break;
                    }
                case "4":
                    {
                        if (data.level >= 3)
                        {
                            Console.WriteLine("스킬을 사용합니다.");
                            Wait(1);
                            Console.WriteLine($"{data.skill[0]}!!");
                            Wait(1);
                            Console.WriteLine("골렘을 쓰러트렸습니다.");
                            Wait(1);
                            Console.WriteLine("300 골드와 80의 경험치를 얻었습니다.");
                            Console.WriteLine($"{Item.골렘조각} 얻었습니다.");
                            Wait(1);
                            data.exp += 80;
                            data.gold += 300;
                            data.inventory[3] = Item.골렘조각.ToString();
                        }
                        else
                        {
                            Console.WriteLine("스킬 이름을 외쳤으나 스킬이 발동하지 않았습니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 피식 웃습니다.");
                            Wait(1);
                            Console.WriteLine("골렘이 반격합니다.");
                            Wait(1);
                            Console.WriteLine("100의 피해를 받았습니다.");
                            Wait(1);
                            data.curHP -= 80;
                        }
                        break;
                    }
                default:
                    break;
            }
            Console.Write("마을로 돌아갑니다...");
            Wait(2);
            data.scene = Scene.Town;

        }

        static void InnScene()
        {
            Console.WriteLine("여관에 들어왔습니다");
            Wait(1);
            Console.WriteLine("여러 색의 문이 달린 방이 있습니다.");
            Wait(1);
            Console.WriteLine();
            Console.WriteLine("방을 선택해주세요.");
            Console.WriteLine("1.빨간색 문의 방");
            Console.WriteLine("2.파란색 문의 방");
            Console.WriteLine("3.노란색 문의 방");
            Console.WriteLine();
            Console.WriteLine("9.돌아간다");
            Console.Write("선택 : ");
            String input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        if (data.INT > 8)
                        {
                            Console.WriteLine("빨간색 문의 문고리를 잡았습니다.");
                            Wait(1);
                            Console.WriteLine("내부에서 사람의 소리가 들립니다.");
                            Wait(2);
                            Console.WriteLine("누군가 이미 방을 사용하고 있습니다.");
                            Wait(1);
                            Console.WriteLine("주인장에게 누군가 이미 방에 있다고 말합니다.");
                            Wait(1);
                            Console.WriteLine("주인장은 실수라고 사과하며 200골드와 다른 방을 주었습니다.");
                            Wait(1);
                            data.gold += 200;
                            data.curHP += 30;

                            Console.Clear();
                            Console.WriteLine("마을로 돌아갑니다...");
                            Wait(1);
                            data.scene = Scene.Town;
                        }
                        else
                        {
                            Console.WriteLine("빨간색 문을 열었습니다.");
                            Wait(1);
                            Console.WriteLine("누군가 이미 사용하고 있는 방이었습니다.");
                            Wait(1);
                            Console.WriteLine("따귀를 맞고 쫓겨납니다.");
                            Wait(1);
                            Console.WriteLine("5의 체력 피해를 입었습니다.");
                            Wait(1);
                            data.curHP -= 5;

                            Console.Clear();
                            Console.WriteLine("마을로 돌아갑니다...");
                            Wait(1);
                            data.scene = Scene.Town;
                        }
                        break;
                    }
                case "2":
                    Console.WriteLine("파란색 문을 열었습니다.");
                    Wait(1);
                    Console.WriteLine("방구석에 300G가 떨어져 있었습니다.");
                    Wait(1);
                    Console.WriteLine("300골드를 몰래 주머니에 넣습니다.");
                    Wait(1);
                    Console.WriteLine("기분 좋게 휴식을 취합니다.");
                    Wait(1);
                    data.curHP += 30;
                    data.gold += 300;

                    Console.Clear();
                    Console.WriteLine("마을로 돌아갑니다...");
                    Wait(1);
                    data.scene = Scene.Town;
                    break;

                case "3":
                    {
                        if (data.inventory[1] == Item.SilverKey.ToString())
                        {
                            Console.WriteLine("노란색 문을 열었습니다.");
                            Wait(1);
                            Console.WriteLine("옷을 정리하기 위해 옷장을 엽니다.");
                            Wait(1);
                            Console.WriteLine("옷장 안에 은 빛 문이 있습니다.");
                            Wait(1);
                            Console.WriteLine("열쇠로 열고 들어갑니다.");
                            Wait(1);
                            data.inventory[1] = "";
                            data.scene = Scene.DragonNest;
                        }
                        else
                        {
                            Console.WriteLine("노란색 문을 열었습니다.");
                            Wait(1);
                            Console.WriteLine("아담한 방입니다.");
                            Wait(1);
                            Console.WriteLine("기분 좋게 휴식을 취합니다.");
                            Wait(1);
                            Console.Clear();
                            Console.WriteLine("마을로 돌아갑니다...");
                            Wait(1);
                            data.scene = Scene.Town;
                            break;
                        }
                        break;
                    }

            }
            
        }

        static void ShopScene()
        {
            Console.WriteLine("상점에 들어왔습니다");
            Wait(1);
            Console.WriteLine("여러 아이템을 구매할 수 있습니다.");
            Wait(1);
            Console.WriteLine();
            Console.WriteLine("아이템을 선택해주세요.");
            Console.WriteLine("1.빨간 포션 (체력 40 회복) - 200G");
            Console.WriteLine("2.힘 포션(STR + 5) - 200G");
            Console.WriteLine("3.민첩 포션 (DEX + 5) - 200G");
            Console.WriteLine("4.지력 포션 (INT + 5) - 200G");
            Console.WriteLine("5.낡은 열쇠 - 300G");
            Console.WriteLine("6.은빛 열쇠 - 500G");
            Console.WriteLine();
            Console.WriteLine("9.돌아간다");

            Console.Write("선택 : ");
            String input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("빨간 포션을 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("체력이 40 회복 되었습니다.");
                    Wait(1);
                    data.gold -= 200;
                    data.curHP += 40;
                    break;
                case "2":
                    Console.WriteLine("힘 포션을 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("힘이 5 상승 되었습니다.");
                    Wait(1);
                    data.gold -= 200;
                    data.STR += 5;
                    break;
                case "3":
                    Console.WriteLine("민첩 포션을 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("민첩이 5 상승 되었습니다.");
                    Wait(1);
                    data.gold -= 200;
                    data.DEX += 5;
                    break;
                case "4":
                    Console.WriteLine("지능 포션을 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("지능이 5 상승 되었습니다.");
                    Wait(1);
                    data.gold -= 200;
                    data.INT += 5;
                    break;
                case "5":
                    Console.WriteLine("낡은 열쇠를 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("낡은 열쇠를 구매하였습니다.");
                    Wait(1);
                    data.gold -= 300;
                    data.inventory[0] = Item.OldKey.ToString();
                    break;
                case "6":
                    Console.WriteLine("은빛 열쇠를 선택하였습니다.");
                    Wait(1);
                    Console.WriteLine("은빛 열쇠를 구매하였습니다.");
                    Wait(1);
                    data.gold -= 500;
                    data.inventory[1] = Item.SilverKey.ToString();
                    break;
                case "9":
                    Console.WriteLine("상점을 나왔습니다.");
                    break;
            }
            Console.Clear();
            Console.WriteLine("마을로 돌아갑니다...");
            Wait(1);
            data.scene = Scene.Town;
        }

        static void DragonNestScene()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("용의 둥지에 들어왔습니다");
            Wait(2);
            Console.WriteLine("거대한 드래곤들이 날아다닙니다!!.");
            Wait(1);
            Console.WriteLine("드래곤 한마리가 달려듭니다.");
            Wait(1);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("당신의 행동을 선택해주세요. ");
            Console.WriteLine("1.삼십육계 줄행랑을 친다.");
            Console.WriteLine("2.맞서 싸운다");
            Console.WriteLine("3.공포에 떨어 자살한다.");
            Console.Write("선택 :  ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        Console.WriteLine("\"ㄷ..도망치자..! 죽고싶지 않아!..\" ");
                        Wait(1);
                        Console.WriteLine("도망치는 와중에 드래곤의 비닐 1개를 주웠습니다. ");
                        Wait(1);
                        data.inventory[4] = Item.용의비늘.ToString();
                        break;
                    }
                case "2":
                    {
                        if (data.level >= 3)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("아드레날린이 폭발하여 스킬을 난사합니다.");
                            Wait(1);
                            Console.WriteLine($"{data.skill[0]}!! {data.skill[0]}!! {data.skill[0]}!! {data.skill[0]}!! ");
                            Wait(1);
                            Console.WriteLine("드래곤이 쓰러졌습니다.");
                            Wait(1);
                            Console.WriteLine("용의 이빨과 함께 많은 양의 골드와 경험치를 얻습니다.");
                            Wait(1);
                            Console.ResetColor() ;  
                            data.inventory[4] = Item.용의이빨.ToString();
                            data.gold += 500;
                            data.exp += 700;
                        }
                        else
                        {
                            Console.WriteLine("스킬 이름을 외쳤으나 스킬이 발동하지 않았습니다.");
                            Wait(1);
                            Console.WriteLine("드래곤에게 물렸습니다.");
                            Wait(1);
                            Console.WriteLine("400의 피해를 받았습니다.");
                            Wait(1);
                            data.curHP = 0;
                        }
                        break;
                    }

                case "3":
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("극심한 공포에 떤 나머지...");
                        Wait(1);
                        Console.WriteLine("스스로 목숨을 끊었습니다.");
                        Wait(1);
                        Console.ResetColor();
                        data.curHP = 0;
                        break;
                    }
                   default:
                   break;
            }
           
            Console.WriteLine("마을로 돌아갑니다...");
            Wait(1);
            data.scene = Scene.Town;

        }

        static void GamblingScene()
        {
            Console.WriteLine("도박장에 들어왔습니다.");
            Wait(1);
            Console.WriteLine("참가비 100골드를 내고 게임을 시작합니다.");
            Wait(3);
            Console.WriteLine("참가비를 내주세요. (아무키를 누르기)");
            Console.ReadKey();
            Console.WriteLine();

            if (data.gold >= 100)
            {
                data.gold -= 100;
                Console.WriteLine("-100 G");
                Wait(1);

            }
            else
            {
                Console.WriteLine("참가비가 부족해 쫓겨납니다.");
                Wait(1);
                Console.WriteLine("마을로 돌아갑니다...");
                Wait(1);
                data.scene = Scene.Town;
                return;
            }

            Console.Clear();
            Console.WriteLine("주사위를 던져 숫자 1,6이 나오면 200골드를 얻고");
            Console.WriteLine("나머지 숫자가 나오면 50골드를 잃습니다.");
            Wait(3);
            Console.WriteLine();
            Console.WriteLine("주사위를 던져주세요. (아무키를 누르기)");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("주사위를 던집니다!");
            Console.WriteLine(".");
            Wait(1);
            Console.WriteLine(".");
            Wait(1);
            Console.WriteLine(".");
            Wait(1);

            Random random = new Random();
            int temp = random.Next(6);

            if (temp == 0)
            {
                Console.WriteLine($"{temp + 1}이 나왔습니다.");
                Wait(1);
                Console.WriteLine("축하합니다. 200골드를 획득하였습니다");
                Wait(1);
                data.gold += 200;
            }
            if (temp == 5)
            {
                Console.WriteLine($"{temp + 1}이 나왔습니다.");
                Wait(1);
                Console.WriteLine("축하합니다. 200골드를 획득하였습니다");
                Wait(3);
                data.gold += 200;
            }
            else
            {
                Console.WriteLine($"{temp + 1}이 나왔습니다.");
                Wait(1);
                Console.WriteLine("꽝! 다음 기회에 다시 도전하세요.");
                Wait(1);
                Console.WriteLine("50골드를 잃었습니다");
                Wait(3);
                data.gold -= 50;
            }


            Wait(1);
            Console.Clear();
            Console.WriteLine("마을로 돌아갑니다...");
            data.scene = Scene.Town;
        }
    }
}
