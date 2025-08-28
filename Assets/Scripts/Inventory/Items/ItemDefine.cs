using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 인벤토리 클래스
public class Inventory
{
    List<IItem> itemSlot; // 아이템 슬롯 배열
    public List<IItem> GetInvenTory { get { return itemSlot; } } // 전체 인벤토리 접근자

    // 인덱서: 인벤토리 슬롯에 직접 접근 가능
    public IItem this[int index]
    {
        get { return itemSlot[index]; }
        set { itemSlot[index] = value; }
    }

    // 인벤토리 크기 설정
    public Inventory(int index)
    {
        this.itemSlot = new List<IItem>(index);
    }

    // 아이템 획득 (빈 슬롯에 아이템 추가)
    public void ItemGet(IItem item)
    {
        itemSlot.Add(item);
        UIManager.GetInstance.updateInven?.Invoke(); // UI 
    }
}

// 장착된 아이템 정보
public class Equiped
{
    public Weapon weapon;
    public Gloves gloves;
    public Helmet helmet;
    public ChestArmor chestArmor;
}

// 아이템 공통 인터페이스
public interface IItem
{
    public int ItemValue { get; }      // 능력치 값
    public Sprite IconSprite { get; } // 아이콘 이미지
    public bool IsEquiped { get; }     // 장착 여부
    public void Equip();               // 장착
    public void UnEquip();             // 해제
}

// 무기 아이템
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
        GameManager.GetInstance.player.stat.ATK += ItemValue; // 공격력 증가
        isEquiped = true;

        if (GameManager.GetInstance.player.equiped.weapon != null) // 기존 무기 해제
            GameManager.GetInstance.player.equiped.weapon.UnEquip();

        GameManager.GetInstance.player.equiped.weapon = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.ATK -= ItemValue; // 공격력 감소
        isEquiped = false;
        GameManager.GetInstance.player.equiped.weapon = null;
    }
}

// 장갑 아이템
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
        GameManager.GetInstance.player.stat.CRIT += ItemValue; // 치명타율 증가

        if (GameManager.GetInstance.player.equiped.gloves != null) // 기존 장갑 해제
            GameManager.GetInstance.player.equiped.gloves.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.gloves = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.CRIT -= ItemValue; // 치명타율 감소
        isEquiped = false;
        GameManager.GetInstance.player.equiped.gloves = null;
    }
}

// 헬멧 아이템
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
        GameManager.GetInstance.player.stat.DEF += ItemValue; // 방어력 증가

        if (GameManager.GetInstance.player.equiped.helmet != null) // 기존 헬멧 해제
            GameManager.GetInstance.player.equiped.helmet.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.helmet = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.DEF -= ItemValue; // 방어력 감소
        isEquiped = false;
        GameManager.GetInstance.player.equiped.helmet = null;
    }
}

// 갑옷 아이템
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
        GameManager.GetInstance.player.stat.HP += ItemValue; // 체력 증가

        if (GameManager.GetInstance.player.equiped.chestArmor != null) // 기존 갑옷 해제
            GameManager.GetInstance.player.equiped.chestArmor.UnEquip();

        isEquiped = true;
        GameManager.GetInstance.player.equiped.chestArmor = this;
    }

    public void UnEquip()
    {
        GameManager.GetInstance.player.stat.HP -= ItemValue; // 체력 감소
        isEquiped = false;
        GameManager.GetInstance.player.equiped.chestArmor = null;
    }
}
