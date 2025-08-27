using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �÷��̾� Ŭ����
public class Player
{
    public PlayerStat stat = new PlayerStat(40, 30, 100, 10); // �÷��̾� �⺻ �ɷ�ġ
    public Inventory inven = new Inventory(120);           // �÷��̾� �κ��丮 (�ִ� 120ĭ)
    public Equiped equiped = new Equiped();                // ���� ������ ����
}

// �÷��̾� �ɷ�ġ Ŭ����
public class PlayerStat
{
    int atk = 40;  // ���ݷ�
    public int ATK
    {
        get { return atk; }
        set
        {
            atk = value;
            UIManager.GetInstance.SetATKText = value.ToString(); // UI�� ���ݷ� ǥ�� ����
        }
    }

    int def = 30;  // ����
    public int DEF
    {
        get { return def; }
        set
        {
            def = value;
            UIManager.GetInstance.SetDefText = value.ToString(); // UI�� ���� ǥ�� ����
        }
    }

    int hp = 100;  // ü��
    public int HP
    {
        get { return hp; }
        set
        {
            hp = value;
            UIManager.GetInstance.SetHPText = value.ToString(); // UI�� ü�� ǥ�� ����
        }
    }

    int crit = 10; // ġ��Ÿ Ȯ��
    public int CRIT
    {
        get { return crit; }
        set
        {
            crit = value;
            UIManager.GetInstance.SetCritRateText = value.ToString(); // UI�� ġ��Ÿ Ȯ�� ǥ�� ����
        }
    }

    // ������ (�⺻ �ɷ�ġ �ʱ�ȭ)
    public PlayerStat(int atk, int def, int hp, int crit)
    {
        this.atk = atk;
        this.def = def;
        this.hp = hp;
        this.crit = crit;
    }
}
