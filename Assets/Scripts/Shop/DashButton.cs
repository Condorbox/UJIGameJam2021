using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashButton : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private Button button;
    [SerializeField] private GoldSoulText goldSoulText;

    void OnEnable(){
        if(gameData.canDash){
            button.gameObject.SetActive(false);
        }
    }

    public void BuyDash(){
        if(!gameData.canDash && gameData.soul >= 1){ 
            gameData.canDash = true;
            gameData.soul -= 1;

            goldSoulText.UpdateSoul();

            button.gameObject.SetActive(false);
        }
    }
}
