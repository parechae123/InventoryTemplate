using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �κ��丮 Ŭ����
public class Inventory
{
    List<IItem> itemSlot; // ������ ���� �迭
    public List<IItem> GetInvenTory { get { return itemSlot; } } // ��ü �κ��丮 ������

    // �ε���: �κ��丮 ���Կ� ���� ���� ����
    public IItem this[int index]
    {
        get { return itemSlot[index]; }
        set { itemSlot[index] = value; }
    }

    // �κ��丮 ũ�� ����
    public Inventory(int index)
    {
        this.itemSlot = new List<IItem>(index);
    }

    // ������ ȹ�� (�� ���Կ� ������ �߰�)
    public void ItemGet(IItem item)
    {
        itemSlot.Add(item);
        UIManager.GetInstance.updateInven?.Invoke(); // UI 
    }
}
