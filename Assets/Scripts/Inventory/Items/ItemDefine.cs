using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ������ ����
public class Equiped
{
    public Weapon weapon;
    public Gloves gloves;
    public Helmet helmet;
    public ChestArmor chestArmor;
}

// ������ ���� �������̽�
public interface IItem
{
    public int ItemValue { get; }      // �ɷ�ġ ��
    public Sprite IconSprite { get; } // ������ �̹���
    public bool IsEquiped { get; }     // ���� ����
    public void Equip();               // ����
    public void UnEquip();             // ����
}

// ���� ������
public class Weapon : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    private bool isEquiped = false;
    public bool IsEquiped => isEquiped;

    public Weapon(int itemValue, Sprite iconSprite)
    {
        this.itemValue = itemValue;
        this.iconSprite = iconSprite;
        this.isEquiped = false;
    }

    public void Equip()
    {
        GameManager.GetInstance.player.stat.ATK += ItemValue; // ���ݷ� ����
        isEquiped = true;

        if (GameManager.GetInstance.player.equiped.weapon != null) // ���� ���� ����
            GameManager.GetInstance.player.equiped.weapon.UnEquip();

        GameManager.GetInstance.player.equiped.weapon = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.ATK -= ItemValue; // ���ݷ� ����
        isEquiped = false;
        GameManager.GetInstance.player.equiped.weapon = null;
    }
}

// �尩 ������
public class Gloves : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;

    public Gloves(int itemValue, Sprite iconSprite)
    {
        this.itemValue = itemValue;
        this.iconSprite = iconSprite;
        this.isEquiped = false;
    }

    public void Equip()
    {
        GameManager.GetInstance.player.stat.CRIT += ItemValue; // ġ��Ÿ�� ����

        if (GameManager.GetInstance.player.equiped.gloves != null) // ���� �尩 ����
            GameManager.GetInstance.player.equiped.gloves.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.gloves = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.CRIT -= ItemValue; // ġ��Ÿ�� ����
        isEquiped = false;
        GameManager.GetInstance.player.equiped.gloves = null;
    }
}

// ��� ������
public class Helmet : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;

    public Helmet(int itemValue, Sprite iconSprite)
    {
        this.itemValue = itemValue;
        this.iconSprite = iconSprite;
        this.isEquiped = false;
    }

    public void Equip()
    {
        GameManager.GetInstance.player.stat.DEF += ItemValue; // ���� ����

        if (GameManager.GetInstance.player.equiped.helmet != null) // ���� ��� ����
            GameManager.GetInstance.player.equiped.helmet.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.helmet = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.DEF -= ItemValue; // ���� ����
        isEquiped = false;
        GameManager.GetInstance.player.equiped.helmet = null;
    }
}

// ���� ������
public class ChestArmor : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;

    public ChestArmor(int itemValue, Sprite iconSprite)
    {
        this.itemValue = itemValue;
        this.iconSprite = iconSprite;
        this.isEquiped = false;
    }

    public void Equip()
    {
        GameManager.GetInstance.player.stat.HP += ItemValue; // ü�� ����

        if (GameManager.GetInstance.player.equiped.chestArmor != null) // ���� ���� ����
            GameManager.GetInstance.player.equiped.chestArmor.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.chestArmor = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.HP -= ItemValue; // ü�� ����
        isEquiped = false;
        GameManager.GetInstance.player.equiped.chestArmor = null;
    }
}
