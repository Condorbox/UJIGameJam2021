using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;

//[CreateAssetMenu(fileName = "New Weapon", menuName = "Gun")]
[CreateAssetMenu(menuName ="New Weapon")]
public class WeaponData : ScriptableObject
{
    protected const string LEFT_VERTICAL_GROUP = "Split/Left";
    protected const string RIGHT_VERTICAL_GROUP = "Split/Right";
    protected const string STATS_BOX_GROUP = "Split/Left/Stats";
    protected const string GENERAL_SETTINGS_VERTICAL_GROUP = "Split/Left/General Settings/Split/Right";

    [HideLabel, PreviewField(55)]
    [VerticalGroup(LEFT_VERTICAL_GROUP)]
    [HorizontalGroup(LEFT_VERTICAL_GROUP + "/General Settings/Split", 60, LabelWidth = 100)]
    public Sprite icon;

    [BoxGroup(LEFT_VERTICAL_GROUP + "/General Settings")]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    public string Name;

    [BoxGroup("Split/Right/Description")]
    [HideLabel, TextArea(4, 14)]
    public string Description;

    [HorizontalGroup("Split", 0.5f, MarginLeft = 5, LabelWidth = 130)]
    [BoxGroup("Split/Right/Notes")]
    [HideLabel, TextArea(4, 9)]
    public string Notes;

    [VerticalGroup("Split/Right")]
    [BoxGroup("Split/Right/Pool")]
    public PoolInfo poolInfo;

    [AssetsOnly]
    [VerticalGroup(GENERAL_SETTINGS_VERTICAL_GROUP)]
    public GameObject bullet;

    [Title("Basic Stats")]
    [BoxGroup(STATS_BOX_GROUP)]
    public float velocity;
    [BoxGroup(STATS_BOX_GROUP)]
    public float damage;
    [BoxGroup(STATS_BOX_GROUP)]

    [Title("Transform Stats")]
    [BoxGroup(STATS_BOX_GROUP)]
    public float minTrasnformX;
    [BoxGroup(STATS_BOX_GROUP)]
    public float maxTransformX;
    [BoxGroup(STATS_BOX_GROUP)]
    public float minTransformY;
    [BoxGroup(STATS_BOX_GROUP)]
    public float maxTransformY;

    [Title("N Bullets Stats")]
    [BoxGroup(STATS_BOX_GROUP)]
    public float numOfBullet;
    [BoxGroup(STATS_BOX_GROUP)]
    public float timeBtwBullet;
    [BoxGroup(STATS_BOX_GROUP)]
    public float timeBtwShoot;

    [Title("Time To Destroy")]
    [BoxGroup(STATS_BOX_GROUP)]
    public float timeToDestroy; 
}
