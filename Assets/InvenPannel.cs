using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenPannel : MonoBehaviour
{
    Button[] buttons;
    TextMeshProUGUI[] texts;
    int currIndex = 0;
    int GetCurrMinIndex { get { return currIndex * buttons.Length; } }
    int GetCurrMaxIndex { get { return currIndex * buttons.Length+ buttons.Length; } }
    
    IEnumerator Start()
    {
        if (buttons != null) yield break;
        buttons = new Button[transform.childCount];
        texts = new TextMeshProUGUI[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i).GetComponent<Button>();
            texts[i] = transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        
        yield return new WaitUntil(() => UIManager.GetInstance.IsLoad);
        yield return new WaitUntil(() => GameManager.GetInstance.player != null);
        UIManager.GetInstance.StatUpdate();
        transform.parent.parent.gameObject.SetActive(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
        UIManager.GetInstance.updateInven += OnEnable;
    }

    public void OnEquip(int btnIndex) 
    {
        int dataIndex = GetCurrMinIndex + btnIndex;
        texts[btnIndex].text = "E";
        buttons[btnIndex].onClick.RemoveAllListeners();
        buttons[btnIndex].onClick.AddListener(() => { OnUnEquip(btnIndex); });
        GameManager.GetInstance.player.inven[dataIndex]?.Equip();
        UIManager.GetInstance.updateInven?.Invoke();
    }

    private void OnUnEquip(int btnIndex)
    {
        int dataIndex = GetCurrMinIndex + btnIndex;
        texts[btnIndex].text = string.Empty;
        buttons[btnIndex].onClick.RemoveAllListeners();
        buttons[btnIndex].onClick.AddListener(() => { OnEquip(btnIndex); });
        GameManager.GetInstance.player.inven[dataIndex]?.UnEquip();
        UIManager.GetInstance.updateInven?.Invoke();
    }

    private void SetButtonImage(int btnIndex, int dataIndex)
    {
        texts[btnIndex].text = string.Empty;
        buttons[btnIndex].image.sprite = null;
        buttons[btnIndex].image.enabled = true;
        buttons[btnIndex].onClick.RemoveAllListeners();
        if (dataIndex < GameManager.GetInstance.player.inven.GetInvenTory.Length)
        {
            if (GameManager.GetInstance.player.inven[dataIndex] != null)
            {
                buttons[btnIndex].image.sprite = GameManager.GetInstance.player.inven[dataIndex].IconSprite;
                if (GameManager.GetInstance.player.inven[dataIndex].IsEquiped)
                {
                    texts[btnIndex].text = "E";
                    //클로저이슈 관찰 시 변경필요
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
                buttons[btnIndex].image.sprite = null;
            }

        }
        else
        {
            buttons[btnIndex].image.sprite = null;
            buttons[btnIndex].image.enabled = false;
        }
    }

    public void OnFowardBTN()
    {
        int maxIndex = GameManager.GetInstance.player.inven.GetInvenTory.Length;
        if (GetCurrMaxIndex >= maxIndex) return;
        currIndex++;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }

    public void OnBackBTN()
    {
        if (currIndex == 0) return;
        currIndex--;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }
    private void OnEnable()
    {
        if (UIManager.GetInstance.IsLoad || GameManager.GetInstance.player == null) return;
        for (int i = 0; i < buttons.Length; i++)
        {
            SetButtonImage(i, i + GetCurrMinIndex);
        }
    }
}
