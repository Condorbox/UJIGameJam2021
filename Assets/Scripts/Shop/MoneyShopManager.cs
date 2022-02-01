using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MoneyShopManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] Button armorButton;
    [SerializeField] TMP_Text armorCostText;
    [SerializeField] private int startArmorCost;
    private long armorCost;
    [SerializeField] TMP_Text extraDamageCostText;
    [SerializeField] private int startExtraDamageCost;
    private long extraDamageCost;
    [SerializeField] Button dashButton;
    [SerializeField] TMP_Text dashTimeCostText;
    [SerializeField] private int startDashTimeCost;
    private long dashTimeCost;
    [SerializeField] private GoldSoulText goldSoulText;

    void Awake(){
        armorCost = startArmorCost;
        extraDamageCost = startExtraDamageCost;
        dashTimeCost = startDashTimeCost;

        armorCostText.text = armorCost.ToString();
        extraDamageCostText.text = extraDamageCost.ToString();
        dashTimeCostText.text = dashTimeCost.ToString();
    }

    void OnEnable(){
        if(gameData.armor){
            armorButton.gameObject.SetActive(false);
        }else{
            armorButton.gameObject.SetActive(true);
        }

        if(gameData.timeBtwDash < .5f){
            dashButton.gameObject.SetActive(false);
        }
    }

    public long GetArmorCost(){
        return armorCost;
    }

    public long GetExtraDamageCost(){
        return extraDamageCost;
    }

    public long GetDashTimeCost(){
        return dashTimeCost;
    }

    public void BuyArmor(){
        if(!gameData.armor && gameData.money >= armorCost){
            gameData.armor = true;
            gameData.money -= armorCost;
            armorCost = (int)Math.Pow(armorCost, 1.05);
            armorCost = Math.Abs(armorCost);

            armorCostText.text = armorCost.ToString();
            goldSoulText.UpdateGold();
            armorButton.gameObject.SetActive(false);
        }
    }

    public void BuyExtraDamage(){
        if(gameData.money >= extraDamageCost){
            gameData.extraDamage += 0.05f;
            gameData.money -= extraDamageCost;
            extraDamageCost = (int)Math.Pow(extraDamageCost, 1.009);
            extraDamageCost = Math.Abs(extraDamageCost);

            extraDamageCostText.text = extraDamageCost.ToString();
            goldSoulText.UpdateGold();
        }
    }

    public void BuyDashTime(){
        if(gameData.timeBtwDash > .5f && gameData.money >= dashTimeCost){
            gameData.timeBtwDash -= 0.05f;
            gameData.money -= dashTimeCost;
            dashTimeCost = (int)Math.Pow(dashTimeCost, 1.03);
            dashTimeCost = Math.Abs(dashTimeCost);

            if(gameData.timeBtwDash < .5f){
                dashButton.gameObject.SetActive(false);
            }

            dashTimeCostText.text = dashTimeCost.ToString();
            goldSoulText.UpdateGold();
        }
    }
}
