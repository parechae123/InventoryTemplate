using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenPagePannel : MonoBehaviour
{
    Button[] buttons;                // �κ��丮 ĭ(��ư)
    TextMeshProUGUI[] texts;         // ��ư ���� ǥ�õǴ� �ؽ�Ʈ (��: "E")
    int currIndex = 0;               // ���� ������ �ε��� (0���������� ����)

    // ���� �������� �ּ� �ε��� (ex: ������ 0 �� 0, ������ 1 �� ��ư �� ��ŭ ���� ��)
    int GetCurrMinIndex { get { return currIndex * buttons.Length; } }
    // ���� �������� �ִ� �ε��� (�ּ� �ε��� + ��ư ����)
    int GetCurrMaxIndex { get { return currIndex * buttons.Length + buttons.Length; } }

    IEnumerator Start()
    {
        // �ڽ� ������Ʈ�� ��ȸ�ϸ� ��ư, �ؽ�Ʈ �ʱ�ȭ
        buttons = new Button[transform.childCount];
        texts = new TextMeshProUGUI[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
            texts[i] = transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        // UI �� ���� ������ �ε� ���
        yield return new WaitUntil(() => UIManager.GetInstance.IsLoad);
        yield return new WaitUntil(() => ResourceManager.GetInstance.GetAtlas != null);
        yield return new WaitUntil(() => GameManager.GetInstance.player != null);

        // ���� ����
        UIManager.GetInstance.StatUpdate();

        // �κ��丮 �г� ��Ȱ��ȭ
        transform.parent.parent.gameObject.SetActive(false);

        // ���� ������ ��ư�� ����
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }

        // �κ��丮 ������Ʈ �̺�Ʈ ����
        UIManager.GetInstance.updateInven += OnEnable;
    }

    // ������ ����
    public void OnEquip(int btnIndex)
    {
        int dataIndex = GetCurrMinIndex + btnIndex; // ���� �κ��丮 ������ �ε��� ���
        texts[btnIndex].text = "E"; // ���� ǥ��
        buttons[btnIndex].onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
        buttons[btnIndex].onClick.AddListener(() => { OnUnEquip(btnIndex); }); // ���� ���� �̺�Ʈ ���
        GameManager.GetInstance.player.inven[dataIndex]?.Equip(); // ���� ������ Equip ȣ��
        UIManager.GetInstance.updateInven?.Invoke(); // �κ��丮 ����
    }

    // ������ ���� ����
    private void OnUnEquip(int btnIndex)
    {
        int dataIndex = GetCurrMinIndex + btnIndex;
        texts[btnIndex].text = string.Empty; // ���� ǥ�� ����
        buttons[btnIndex].onClick.RemoveAllListeners();
        buttons[btnIndex].onClick.AddListener(() => { OnEquip(btnIndex); }); // ���� �̺�Ʈ ���
        GameManager.GetInstance.player.inven[dataIndex]?.UnEquip(); // ���� ������ UnEquip ȣ��
        UIManager.GetInstance.updateInven?.Invoke();
    }

    // ��ư �̹��� �� Ŭ�� �̺�Ʈ ����
    private void SetButtonImage(int btnIndex, int dataIndex)
    {
        texts[btnIndex].text = string.Empty;
        
        buttons[btnIndex].image.sprite = null;
        buttons[btnIndex].image.enabled = true;
        buttons[btnIndex].onClick.RemoveAllListeners();

        // �κ��丮 ���� üũ
        if (dataIndex < GameManager.GetInstance.player.inven.GetInvenTory.Count)
        {
            // �ش� ĭ�� �������� ���� ���
            if (GameManager.GetInstance.player.inven[dataIndex] != null)
            {
                buttons[btnIndex].image.sprite = GameManager.GetInstance.player.inven[dataIndex].IconSprite;

                // ���� ���ο� ���� ��ư ����
                if (GameManager.GetInstance.player.inven[dataIndex].IsEquiped)
                {
                    texts[btnIndex].text = "E";
                    buttons[btnIndex].onClick.AddListener(() => { OnUnEquip(btnIndex); });
                }
                else
                {
                    texts[btnIndex].text = string.Empty;
                    buttons[btnIndex].onClick.AddListener(() => { OnEquip(btnIndex); });
                }
            }
            else
            {
                // �������� ���� ���
                buttons[btnIndex].image.sprite = null;
            }
        }
        else
        {
            // �κ��丮 ������ ���� ��� ��ư ��Ȱ��ȭ
            buttons[btnIndex].image.sprite = null;
            buttons[btnIndex].image.enabled = false;
        }
    }

    // ������ ������ �̵�
    public void OnFowardBTN()
    {
        int maxIndex = GameManager.GetInstance.player.inven.GetInvenTory.Count;
        if (GetCurrMaxIndex >= maxIndex) return; // �� �Ѿ �������� ������ ����
        currIndex++;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }

    // ������ �ڷ� �̵�
    public void OnBackBTN()
    {
        if (currIndex == 0) return; // ù �������� ����
        currIndex--;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }

    // �κ��丮 UI ���� (enable�� ������ ȣ���)
    private void OnEnable()
    {
        if (!UIManager.GetInstance.IsLoad || GameManager.GetInstance.player == null || buttons == null) return;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }
}
