using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerStat stat = new PlayerStat(40,30,100,10);
    public Inventory inven = new Inventory(120);
    public Equiped equiped = new Equiped();
}
public class PlayerStat
{
    int atk = 40;
    public int ATK { get { return atk; }  set { atk = value; UIManager.GetInstance.SetATKText = value.ToString(); } }
    int def = 30;
    public int DEF { get { return def; } set { def = value; UIManager.GetInstance.SetDefText = value.ToString(); } }
    int hp = 100;
    public int HP { get { return hp; } set { hp = value; UIManager.GetInstance.SetHPText = value.ToString(); } }
    int crit = 10;
    public int CRIT { get { return crit; } set { crit = value; UIManager.GetInstance.SetCritRateText = value.ToString(); } }
    public PlayerStat(int atk, int def,int hp, int crit)
    {
        this.atk = atk;
        this.def = def;
        this.hp = hp;
        this.crit = crit;
    }
}