using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : SingleTon<UIManager>
{
    TextMeshProUGUI atkText;
    TextMeshProUGUI defText;
    TextMeshProUGUI hpText;
    TextMeshProUGUI cirtRateText;
    public string SetATKText
    {
        set { atkText.text = $"ATK \n{value}"; }
    }
    public string SetDefText
    {
        set { defText.text = $"DEF \n{value}"; }
    }
    public string SetHPText
    {
        set { hpText.text = $"HP \n{value}"; }
    }
    public string SetCritRateText
    {
        set { cirtRateText.text = $"critRate \n{value}"; }
    }


    protected override void Init()
    {
        Transform statPanel = GameObject.Find("StatPannel").transform;
        atkText = statPanel.Find("ATK").Find("ATKText").GetComponent<TextMeshProUGUI>();
        defText = statPanel.Find("DEF").Find("DEFText").GetComponent<TextMeshProUGUI>();
        hpText = statPanel.Find("HP").Find("HPText").GetComponent<TextMeshProUGUI>();
        cirtRateText = statPanel.Find("CritRate").Find("CritText").GetComponent<TextMeshProUGUI>();
    }
}
