using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldSoulText : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Text soulText;
    [SerializeField] TMP_Text goldText;

    void OnEnable(){
        UpdateSoul();
        UpdateGold();
    }

    public void UpdateSoul(){
        soulText.text = gameData.soul.ToString();
    }

    public void UpdateGold(){
        goldText.text = gameData.money.ToString();
    }
}
