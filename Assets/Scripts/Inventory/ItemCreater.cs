using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCreater : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => UIManager.GetInstance.IsLoad);
        yield return new WaitUntil(() => ResourceManager.GetInstance.GetAtlas != null);
        yield return new WaitUntil(() => GameManager.GetInstance.player != null);
        for (int i = 0; i < 10; i++)
        {
            GameManager.GetInstance.player.inven.ItemGet(new Weapon(10, ResourceManager.GetInstance.GetAtlas.GetSprite("axe")));
            GameManager.GetInstance.player.inven.ItemGet(new Helmet(10, ResourceManager.GetInstance.GetAtlas.GetSprite("helmets")));
            GameManager.GetInstance.player.inven.ItemGet(new ChestArmor(10, ResourceManager.GetInstance.GetAtlas.GetSprite("armor")));
            GameManager.GetInstance.player.inven.ItemGet(new Gloves(10, ResourceManager.GetInstance.GetAtlas.GetSprite("gloves")));
        }
        UIManager.GetInstance.SetPlayerNameText = GameManager.GetInstance.player.name;
        UIManager.GetInstance.SetPlayerLevelText = GameManager.GetInstance.player.stat.Level.ToString();
    }
    
}
