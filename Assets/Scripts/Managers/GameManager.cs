using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTon<GameManager>
{
    public Player player;
    protected override void Init()
    {
        base.Init();
        player = new Player("Player");
    }
    public void CallEmpty() { }
}
