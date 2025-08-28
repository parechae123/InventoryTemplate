using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenScrollPannel : MonoBehaviour
{
    List<Button> buttons;                // �κ��丮 ĭ(��ư)
    List<TextMeshProUGUI> texts;         // ��ư ���� ǥ�õǴ� �ؽ�Ʈ (��: "E")
    [SerializeField] RectTransform contentTR;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GridLayoutGroup grid;
    // ���� �������� �ּ� �ε��� (ex: ������ 0 �� 0, ������ 1 �� ��ư �� ��ŭ ���� ��)

    IEnumerator Start()
    {
        // �ڽ� ������Ʈ�� ��ȸ�ϸ� ��ư, �ؽ�Ʈ �ʱ�ȭ
        buttons = new List<Button>();
        texts = new List<TextMeshProUGUI>();

        // UI �� ���� ������ �ε� ���
        yield return new WaitUntil(() => UIManager.GetInstance.IsLoad);
        yield return new WaitUntil(() => ResourceManager.GetInstance.GetAtlas != null);
        yield return new WaitUntil(() => GameManager.GetInstance.player != null);

        // ���� ����
        UIManager.GetInstance.StatUpdate();

        // �κ��丮 �г� ��Ȱ��ȭ
        transform.parent.parent.gameObject.SetActive(false);

        // ���� ������ ��ư�� ����
        for (int i = 0; i < GameManager.GetInstance.player.inven.GetInvenTory.Count; i++)
        {
            SetButtonImage(i);
        }

        // �κ��丮 ������Ʈ �̺�Ʈ ����
        UIManager.GetInstance.updateInven += OnEnable;
    }

    // ������ ����
    public void OnEquip(int btnIndex)
    {
        texts[btnIndex].text = "E"; // ���� ǥ��
        buttons[btnIndex].onClick.RemoveAllListeners(); // ���� �̺�Ʈ ����
        buttons[btnIndex].onClick.AddListener(() => { OnUnEquip(btnIndex); }); // ���� ���� �̺�Ʈ ���
        GameManager.GetInstance.player.inven[btnIndex]?.Equip(); // ���� ������ Equip ȣ��
        UIManager.GetInstance.updateInven?.Invoke(); // �κ��丮 ����
    }

    // ������ ���� ����
    private void OnUnEquip(int btnIndex)
    {
        int dataIndex = btnIndex;
        texts[btnIndex].text = string.Empty; // ���� ǥ�� ����
        buttons[btnIndex].onClick.RemoveAllListeners();
        buttons[btnIndex].onClick.AddListener(() => { OnEquip(btnIndex); }); // ���� �̺�Ʈ ���
        GameManager.GetInstance.player.inven[dataIndex]?.UnEquip(); // ���� ������ UnEquip ȣ��
        UIManager.GetInstance.updateInven?.Invoke();
    }

    // ��ư �̹��� �� Ŭ�� �̺�Ʈ ����
    private void SetButtonImage(int btnIndex)
    {
        if(btnIndex >= buttons.Count)
        {
            GameObject clonedPrefab = GameObject.Instantiate(buttonPrefab);
            clonedPrefab.transform.SetParent(contentTR);
            buttons.Add(clonedPrefab.GetComponent<Button>());
            texts.Add(clonedPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
            int xCount = (int)(contentTR.rect.size.x/ (grid.spacing.x + grid.cellSize.x));
            int yCount = (int)(contentTR.rect.size.y/ (grid.spacing.y + grid.cellSize.y));
            if(xCount*yCount < btnIndex)
            {
                contentTR.sizeDelta += new Vector2(0, grid.spacing.y + grid.cellSize.y);
            }
        }
        texts[btnIndex].text = string.Empty;
        
        buttons[btnIndex].image.sprite = null;
        buttons[btnIndex].image.enabled = true;
        buttons[btnIndex].onClick.RemoveAllListeners();
        
        // �κ��丮 ���� üũ
        if (btnIndex < GameManager.GetInstance.player.inven.GetInvenTory.Count)
        {
            // �ش� ĭ�� �������� ���� ���
            if (GameManager.GetInstance.player.inven[btnIndex] != null)
            {
                buttons[btnIndex].image.sprite = GameManager.GetInstance.player.inven[btnIndex].IconSprite;

                // ���� ���ο� ���� ��ư ����
                if (GameManager.GetInstance.player.inven[btnIndex].IsEquiped)
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
    }
    // �κ��丮 UI ���� (enable�� ������ ȣ���)
    private void OnEnable()
    {
        if (!UIManager.GetInstance.IsLoad || GameManager.GetInstance.player == null || buttons == null) return;
        for (int i = 0; i < GameManager.GetInstance.player.inven.GetInvenTory.Count; i++)
        {
            SetButtonImage(i);
        }
    }
}
