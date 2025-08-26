using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerStat stat;
}
public class PlayerStat
{
    int atk;
    public int ATK { get { return atk; }  set { atk = value; UIManager.GetInstance.SetATKText = value.ToString(); } }
    int def;
    public int DEF { get { return def; } set { def = value; UIManager.GetInstance.SetDefText = value.ToString(); } }
    int hp;
    public int HP { get { return hp; } set { hp = value; UIManager.GetInstance.SetHPText = value.ToString(); } }
    int crit;
    public int CRIT { get { return crit; } set { crit = value; UIManager.GetInstance.SetCritRateText = value.ToString(); } }

}