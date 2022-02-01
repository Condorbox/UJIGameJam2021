using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private List<WeaponData> weapons;

    public void LoadLevel(string level){
        ResetGameData();
        SceneManager.LoadScene(level);
    }

    private void ResetGameData(){
        gameData.timeBtwDash = 3f;
        gameData.armor = true;
        gameData.weapons = weapons;
        gameData.index = 0;
        gameData.extraDamage = 0;
        gameData.wave = 0;
        gameData.score = 0;
        gameData.money = 0;
        gameData.soul += gameData.soulsToCollect;
        gameData.soulsToCollect = 0;
    }

    public void Exit(){
        Application.Quit();
    }
}
