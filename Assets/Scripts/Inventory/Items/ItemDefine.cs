using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    IItem[] itemSlot;
    public IItem[] GetInvenTory { get { return itemSlot; } }
    public IItem this[int index]
    {
        get
        {
            return itemSlot[index];
        }
        set
        {
            itemSlot[index] = value;
        }
    }

    public Inventory(int index)
    {
        this.itemSlot = new IItem[index];
    }
    
    public void ItemGet(IItem item)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i] == null)
            {
                itemSlot[i] = item;
                UIManager.GetInstance.updateInven?.Invoke();
                break;
            }
        }
    }
}
public class Equiped
{
    public Weapon weapon;
    public Gloves gloves;
    public Helmet helmet;
    public ChestArmor chestArmor;
}


public interface IItem
{
    public int ItemValue { get;}
    public Sprite IconSprite { get;}
    public bool IsEquiped { get; }
    public void Equip();
    public void UnEquip();
}
public class Weapon : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    private bool isEquiped = false;
    public bool IsEquiped => isEquiped;

    public void Equip()
    {
        GameManager.GetInstance.player.stat.ATK += ItemValue;
        isEquiped = true;
        GameManager.GetInstance.player.equiped.weapon = this;
    }
    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.ATK -= ItemValue;
        isEquiped = false;
        GameManager.GetInstance.player.equiped.weapon = null;
    }
}
public class Gloves : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;
    public void Equip()
    {
        GameManager.GetInstance.player.stat.CRIT += ItemValue;
        isEquiped = true;
        GameManager.GetInstance.player.equiped.gloves = this;
    }
    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.CRIT -= ItemValue;
        isEquiped = false;
        GameManager.GetInstance.player.equiped.gloves = null;
    }
}
public class Helmet : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;
    public void Equip()
    {
        GameManager.GetInstance.player.stat.DEF += ItemValue;
        isEquiped = true;
        GameManager.GetInstance.player.equiped.helmet = this;
    }
    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.DEF -= ItemValue;
        isEquiped = false;
        GameManager.GetInstance.player.equiped.helmet = null;
    }
}
public class ChestArmor : IItem
{
    private int itemValue;
    public int ItemValue { get { return itemValue; } }
    private Sprite iconSprite;
    public Sprite IconSprite { get { return iconSprite; } }
    public bool IsEquiped => isEquiped;
    private bool isEquiped = false;
    public void Equip()
    {
        GameManager.GetInstance.player.stat.HP += ItemValue;
        isEquiped = true;
        GameManager.GetInstance.player.equiped.chestArmor = this;
    }
    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.HP -= ItemValue;
        isEquiped = false;
        GameManager.GetInstance.player.equiped.chestArmor = null;
    }
}