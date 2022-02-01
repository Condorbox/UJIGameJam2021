using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text maxScoreText;
    [SerializeField] TMP_Text soulText;
    [SerializeField] Animator anim;

    void OnEnable(){
        scoreText.text = "Score: " + gameData.score.ToString();
        if(gameData.score > gameData.maxScore){
            gameData.maxScore = gameData.score;
        }
        maxScoreText.text = "Max Score: " + gameData.maxScore.ToString();
        soulText.text = gameData.soulsToCollect.ToString() + " this round";

        ActiveAnimTrigger();
    }

    private void ActiveAnimTrigger(){
        anim.SetTrigger("Die");
    }
}
