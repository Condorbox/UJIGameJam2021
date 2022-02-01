using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New wave", menuName = "Wave")]
public class WaveData : SerializedScriptableObject{
    public Dictionary<EnemyController, int> enemyToSpawn = new Dictionary<EnemyController, int>();
}
