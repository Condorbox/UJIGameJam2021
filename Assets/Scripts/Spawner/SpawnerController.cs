using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class SpawnerController : Singleton<SpawnerController>
{
    [SerializeField, ReadOnly]  private SpawnState state;
    [SerializeField] WaveData[] wavesEasy;
    [SerializeField] WaveData[] wavesMedium;
    [SerializeField] WaveData[] wavesHard;
    [SerializeField, ReadOnly] int wave = 0;
    [SerializeField] private float timeBtwWave;
    private float counter;
    [SerializeField] private float[] spawnZoneX = new float[2];
    [SerializeField] private float[] spawnZoneY = new float[2];
    private Vector2 spawnZone;
    [SerializeField] private SpawnEnemy spawner;
    [SerializeField] Transform target;
    [SerializeField] GameData gameData;

    private EnemyController[] enemys;
    [SerializeField] private GameObject tp;

    private float checkTime = 1.5f;
    private float checkCounter;

    [SerializeField] private TMP_Text waveText;

    void Start()
    {
        counter = 1f;
        state = SpawnState.Spawn;
        tp.SetActive(false);
        waveText.text = "Wave " + 1;
        waveText.gameObject.SetActive(true);
    }
    void Update(){
        switch(state){
            case SpawnState.Spawn:
                Spawn();
                break;
            case SpawnState.Rest:
                Rest();
                break;
        }
    }

    private void Spawn(){
        counter -= Time.deltaTime;
        if(counter <= 0){
            SpawnWave(WaveToSpawn(whatWaveDifficulty()));
            counter = timeBtwWave;
            wave++;
            gameData.wave = wave;
            waveText.text = "Wave " + wave;
        }

        if(wave!=0 && wave%5==0){
            checkCounter = checkTime;
            state = SpawnState.Rest;
        }
    }

    private WaveData[] whatWaveDifficulty(){
        float index = Random.Range(0,100);
        if(wave <= 10){
            return wavesEasy;
        }
        else if(wave <= 30){
            if(index <= 35) { return wavesEasy; }
            else { return wavesMedium; }
        }else{
            if(index <= 15) { return wavesEasy; }
            else if(index <= 35) { return wavesMedium; }
            else { return wavesHard; }
        }
    }

    private WaveData WaveToSpawn(WaveData[] waves){
        int max = waves.Length;
        int index = Random.Range(0, max);
        return waves[index];
    }

    private void SpawnWave(WaveData wave){
        foreach(KeyValuePair<EnemyController, int> enemy in wave.enemyToSpawn){
                for(int i = 0; i<enemy.Value; i++){
                    spawnZone = new Vector2(Random.Range(spawnZoneX[0], spawnZoneX[1]), Random.Range(spawnZoneY[0], spawnZoneY[1]));
                    PoolObjectType poolObjectType = PoolObjectType.Spawner;
                    GameObject newSpawn = PoolManager.Instance.GetPoolObject(poolObjectType);
                    newSpawn.transform.position = spawnZone;
                    newSpawn.GetComponent<SpawnEnemy>().SetEnemyToSpawn(enemy.Key);
                    newSpawn.GetComponent<SpawnEnemy>().SetTarget(target);
                    newSpawn.gameObject.SetActive(true);
                }
            }
    }

    private void Rest(){
        enemys = GameObject.FindObjectsOfType<EnemyController>();
            if(enemys.Length == 0){
                checkCounter -= Time.deltaTime;
                if(checkCounter <= 0){
                    waveText.gameObject.SetActive(false);
                    tp.SetActive(true);
                }
            }else{
                checkCounter = checkTime;
                waveText.gameObject.SetActive(true);
                tp.SetActive(false);
            }
    }

    public void RestToSpawn(){
        counter = timeBtwWave;
        wave++;
        tp.SetActive(false);
        waveText.text = "Wave " + wave;
        waveText.gameObject.SetActive(true);
        state = SpawnState.Spawn;
    }
}

public enum SpawnState{
    Spawn,
    Rest
}
