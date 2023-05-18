using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

//exp 크리티컬 mp / 스킬 추가
class FightAbleUnit
{
    protected string NAME = "NONE";
    protected int AT = 10;
    protected int HP = 50;
    protected int MAXHP = 100;
    protected int ARMOR = 20;
    protected int LV = 1;
    protected int SPEED = 10;
}

class Player : FightAbleUnit
{
    int MAXAT = 30;
    int EXP = 0;
    int MXEXP = 20;

    string JOB = "Beginner";
    public Player()
    {
        NAME = "초보자";
    }

    public void ATUp()
    {
        if (AT < MAXAT)
        {
            this.AT = AT + 10;
        }
        else Console.WriteLine("더이상 공격력을 올릴 수 없습니다.");
    }

    public void Heal()
    {
        if (HP < MAXHP)
        {
            this.HP = MAXHP;
        }
        else Console.WriteLine("이미 체력이 모두 채워져있습니다.");
    }
    public void PlayerStatusRender()
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("이름 : " + NAME);
        Console.WriteLine("레벨 : " + LV);
        Console.WriteLine("방어도 : " + ARMOR);
        Console.WriteLine("공격력 : " + AT);
        Console.WriteLine("스피드 : " + SPEED);
        Console.WriteLine("체력 : " + HP + "/" + MAXHP);
        Console.WriteLine("-----------------------------------");
    }

    public int PlayerGiveDamage()
    {
        Console.WriteLine("공격하였습니다...");
        Console.WriteLine(this.AT + "의 데미지를 주었습니다...");
        /*Monster.MonsterDamaged(this.AT);*/

        return this.AT;
    }
    public void PlayerDamaged(int _OtherAT)
    {
        this.HP -= _OtherAT;
    }


    public bool IsPlayerAlive()
    {
        if (HP > 0)
        {
            return true;
        }
        else return false;
    }
}

class Monster : FightAbleUnit
{

    public void SetGoblin()
    {
        MAXHP = 70;
        HP = 70;
        AT = 10;
        SPEED = 30;
        ARMOR = 10;
    }
    public void SetOrc()
    {
        MAXHP = 110;
        HP = 110;
        AT = 20;
        SPEED = 5;
        ARMOR = 30;
    }

    public void MonsterStatusRender()
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("이름 : " + NAME);
        Console.WriteLine("레벨 : " + LV);
        Console.WriteLine("방어도 : " + ARMOR);
        Console.WriteLine("공격력 : " + AT);
        Console.WriteLine("스피드 : " + SPEED);
        Console.WriteLine("체력 : " + HP + "/" + MAXHP);
        Console.WriteLine("-----------------------------------");
    }
    public bool IsMonsetrAlive()
    {
        if (HP > 0)
        {
            return true;
        }
        else return false;
    }

    public Monster(String _Type)
    {
        NAME = _Type;
    }
    public int MonsterGiveDamage()
    {
        Console.WriteLine("공격당하였습니다...");
        Console.WriteLine(this.AT + "의 데미지를 받았습니다...");
        /*Monster.MonsterDamaged(this.AT);*/

        return this.AT;
    }

    public void MonsterDamaged(int _OtherAT)
    {
        this.HP -= _OtherAT;
    }

    public void SetMonsterHP()
    {
        this.HP = MAXHP;
    }

    public string ReturnName()
    {
        return this.NAME;
    }
}

enum STARTSELECT
{
    TOWNSELECT,
    BATTLESELECT,
    NONESELECT
}

namespace TextRPG
{
    class Program
    {
        static STARTSELECT StartSelect()
        {
            Console.Clear();
            Console.WriteLine("1. 마을 입장");
            Console.WriteLine("2. 몬스터와 배틀");
            Console.WriteLine("어디로 가시겠습니까?");

            ConsoleKeyInfo CKI = Console.ReadKey();

            /*Console.WriteLine("");*/

            switch (CKI.Key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Console.WriteLine("");
                    Console.WriteLine("마을로 이동합니다...");
                    Console.ReadKey();
                    return STARTSELECT.TOWNSELECT;

                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Console.WriteLine("");
                    Console.ReadKey();
                    return STARTSELECT.BATTLESELECT;

                default:
                    Console.WriteLine("");
                    Console.WriteLine("잘못된 선택입니다...");
                    Console.ReadKey();
                    return STARTSELECT.NONESELECT;

            }


        }

        static void Town(Player _Player)
        {
            while (true)
            {
                Console.Clear();
                _Player.PlayerStatusRender();
                Console.WriteLine("마을에 입장했습니다...무슨 일을 하시겠습니까?");
                Console.WriteLine("1. 치료하기");
                Console.WriteLine("2. 무기 강화하기");
                Console.WriteLine("3. 상점 방문하기");
                Console.WriteLine("4. 마을 떠나기");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("");
                        Console.WriteLine("치료를 시작합니다...");
                        _Player.Heal();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("");
                        Console.WriteLine("무기를 강화합니다...");
                        _Player.ATUp();
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.WriteLine("");
                        Console.WriteLine("상점문이 닫혀있습니다..."); //상점 만들면서 없애기
                        Console.ReadKey();
                        break;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("마을을 떠납니다...");
                        Console.ReadKey();
                        return;
                }
            }

        }

        static void Battle(Player _Player, Monster _Monster)
        {
            if (_Monster.ReturnName().Equals("고블린")) { _Monster.SetGoblin(); }
            if (_Monster.ReturnName().Equals("오크")) { _Monster.SetOrc(); }
            _Monster.SetMonsterHP();
            Console.WriteLine("전투를 시작합니다...");
            _Player.PlayerStatusRender();
            _Monster.MonsterStatusRender();
            Console.ReadKey();

            while (_Player.IsPlayerAlive() == true && _Monster.IsMonsetrAlive() == true)
            {
                Console.Clear();
                Console.WriteLine("무엇을 하시겠습니까");
                Console.WriteLine("1. 공격하기");
                Console.WriteLine("2. 도망치기");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine("");
                        _Monster.MonsterDamaged(_Player.PlayerGiveDamage());
                        Console.WriteLine("");
                        Console.ReadKey();
                        _Player.PlayerDamaged(_Monster.MonsterGiveDamage());
                        Console.WriteLine("");
                        if (_Player.IsPlayerAlive() == false) { Console.WriteLine("플레이어가 쓰러졌습니다."); };
                        if (_Monster.IsMonsetrAlive() == false) { Console.WriteLine("몬스터가 쓰러졌습니다."); };
                        Console.ReadKey();
                        break;

                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.WriteLine("");
                        Console.WriteLine("무사히 도망쳤습니다...");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("");
                        Console.WriteLine("잘못된 선택입니다...");
                        Console.ReadKey();
                        return;
                }
            }
        }


        static void Main(string[] strings)
        {
            Random r = new Random();

            Player player = new Player();

            Monster orc = new Monster("오크");
            Monster goblin = new Monster("고블린");

            while (true)
            {
                STARTSELECT SelectCheck = StartSelect();

                switch (SelectCheck)
                {
                    case STARTSELECT.TOWNSELECT:
                        Town(player);
                        break;
                    case STARTSELECT.BATTLESELECT:
                        int randomcreate = r.Next(1, 3);
                        if (randomcreate == 1) { Battle(player, orc); }
                        else if (randomcreate == 2) { Battle(player, goblin); }
                        break;
                    default: break;
                }
            }
        }
    }
}