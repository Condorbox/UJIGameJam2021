using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameData gameData;
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private int soul;
    [SerializeField] private GoldSoulText goldSoulText;

    void OnEnable(){
        button.onClick.AddListener(() => BuyWeapon(weaponData, soul));

        if(gameData.weapons.Contains(weaponData)){
            button.gameObject.SetActive(false);
        }
    }
    void OnDisable(){
        button.onClick.RemoveAllListeners();
    }

    public void BuyWeapon(WeaponData weapon, int soul){
        if(gameData.soul >= soul && !gameData.weapons.Contains(weapon)){
            gameData.weapons.Add(weapon);
            gameData.soul -= soul;

            AddToPool(weapon.poolInfo);
            goldSoulText.UpdateSoul();

            button.gameObject.SetActive(false);
        }
    }

    private void AddToPool(PoolInfo poolInfo){
        GameObject cont = GameObject.FindGameObjectWithTag("Containers");
        GameObject ob = new GameObject("cont" + poolInfo.type);
        ob.transform.parent = cont.transform;
        poolInfo.container = ob;
        poolManager.listOfPool.Add(poolInfo);
        poolManager.FillPool(poolInfo);
    }

    /*private void FillPool(PoolInfo info){
        for(int i=0; i < info.amount; i++)
        {
            GameObject obInstance = null;
            obInstance = Instantiate(info.prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);
            obInstance.transform.position =  new Vector3(-100, -100, -100);;
            if(obInstance != null)
                info.pool.Add(obInstance);
        }
    }*/
}
