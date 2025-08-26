using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickDisable : MonoBehaviour
{
    public GameObject[] turnOffTargets;
    public GameObject[] turnOnTargets;
    public void OnClick()
    {
        for (int i = 0; i < turnOffTargets.Length; i++)
        {
            turnOffTargets[i].SetActive(false);
        }
        for (int i = 0; i < turnOnTargets.Length; i++)
        {
            turnOnTargets[i].SetActive(true);
        }
    }
}
