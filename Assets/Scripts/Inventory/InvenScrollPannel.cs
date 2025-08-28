using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InvenScrollPannel : MonoBehaviour
{
    List<Button> buttons;                // 인벤토리 칸(버튼)
    List<TextMeshProUGUI> texts;         // 버튼 위에 표시되는 텍스트 (예: "E")
    [SerializeField] RectTransform contentTR;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GridLayoutGroup grid;
    // 현재 페이지의 최소 인덱스 (ex: 페이지 0 → 0, 페이지 1 → 버튼 수 만큼 곱한 값)

    IEnumerator Start()
    {
        // 자식 오브젝트를 순회하며 버튼, 텍스트 초기화
        buttons = new List<Button>();
        texts = new List<TextMeshProUGUI>();

        // UI 및 게임 데이터 로드 대기
        yield return new WaitUntil(() => UIManager.GetInstance.IsLoad);
        yield return new WaitUntil(() => ResourceManager.GetInstance.GetAtlas != null);
        yield return new WaitUntil(() => GameManager.GetInstance.player != null);

        // 스탯 갱신
        UIManager.GetInstance.StatUpdate();

        // 인벤토리 패널 비활성화
        transform.parent.parent.gameObject.SetActive(false);

        // 현재 페이지 버튼들 세팅
        for (int i = 0; i < GameManager.GetInstance.player.inven.GetInvenTory.Count; i++)
        {
            SetButtonImage(i);
        }

        // 인벤토리 업데이트 이벤트 연결
        UIManager.GetInstance.updateInven += OnEnable;
    }

    // 아이템 장착
    public void OnEquip(int btnIndex)
    {
        texts[btnIndex].text = "E"; // 장착 표시
        buttons[btnIndex].onClick.RemoveAllListeners(); // 기존 이벤트 제거
        buttons[btnIndex].onClick.AddListener(() => { OnUnEquip(btnIndex); }); // 장착 해제 이벤트 등록
        GameManager.GetInstance.player.inven[btnIndex]?.Equip(); // 실제 아이템 Equip 호출
        UIManager.GetInstance.updateInven?.Invoke(); // 인벤토리 갱신
    }

    // 아이템 장착 해제
    private void OnUnEquip(int btnIndex)
    {
        int dataIndex = btnIndex;
        texts[btnIndex].text = string.Empty; // 장착 표시 제거
        buttons[btnIndex].onClick.RemoveAllListeners();
        buttons[btnIndex].onClick.AddListener(() => { OnEquip(btnIndex); }); // 장착 이벤트 등록
        GameManager.GetInstance.player.inven[dataIndex]?.UnEquip(); // 실제 아이템 UnEquip 호출
        UIManager.GetInstance.updateInven?.Invoke();
    }

    // 버튼 이미지 및 클릭 이벤트 세팅
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
        
        // 인벤토리 범위 체크
        if (btnIndex < GameManager.GetInstance.player.inven.GetInvenTory.Count)
        {
            // 해당 칸에 아이템이 있을 경우
            if (GameManager.GetInstance.player.inven[btnIndex] != null)
            {
                buttons[btnIndex].image.sprite = GameManager.GetInstance.player.inven[btnIndex].IconSprite;

                // 장착 여부에 따라 버튼 세팅
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
                // 아이템이 없을 경우
                buttons[btnIndex].image.sprite = null;
            }
        }
    }
    // 인벤토리 UI 갱신 (enable될 때마다 호출됨)
    private void OnEnable()
    {
        if (!UIManager.GetInstance.IsLoad || GameManager.GetInstance.player == null || buttons == null) return;
        for (int i = 0; i < GameManager.GetInstance.player.inven.GetInvenTory.Count; i++)
        {
            SetButtonImage(i);
        }
    }
}
