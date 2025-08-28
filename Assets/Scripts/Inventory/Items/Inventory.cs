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
