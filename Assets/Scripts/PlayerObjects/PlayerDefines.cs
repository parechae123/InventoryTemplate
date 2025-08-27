using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 클래스
public class Player
{
    public PlayerStat stat = new PlayerStat(40, 30, 100, 10); // 플레이어 기본 능력치
    public Inventory inven = new Inventory(120);           // 플레이어 인벤토리 (최대 120칸)
    public Equiped equiped = new Equiped();                // 장착 아이템 정보
}

// 플레이어 능력치 클래스
public class PlayerStat
{
    int atk = 40;  // 공격력
    public int ATK
    {
        get { return atk; }
        set
        {
            atk = value;
            UIManager.GetInstance.SetATKText = value.ToString(); // UI에 공격력 표시 갱신
        }
    }

    int def = 30;  // 방어력
    public int DEF
    {
        get { return def; }
        set
        {
            def = value;
            UIManager.GetInstance.SetDefText = value.ToString(); // UI에 방어력 표시 갱신
        }
    }

    int hp = 100;  // 체력
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            UIManager.GetInstance.SetHPText = value.ToString(); // UI에 체력 표시 갱신
        }
    }

    int crit = 10; // 치명타 확률
    public int CRIT
    {
        get { return crit; }
        set
        {
            crit = value;
            UIManager.GetInstance.SetCritRateText = value.ToString(); // UI에 치명타 확률 표시 갱신
        }
    }

    // 생성자 (기본 능력치 초기화)
    public PlayerStat(int atk, int def, int hp, int crit)
    {
        this.atk = atk;
        this.def = def;
        this.hp = hp;
        this.crit = crit;
    }
}
