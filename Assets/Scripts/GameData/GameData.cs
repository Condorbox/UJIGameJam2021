using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New GameData", menuName = "GameData")]
public class GameData : ScriptableObject
{
    [Header("Movement")]
    public float moveSpeed;
    public float timeBtwDash;
    public bool canDash;

    [Header("Health")]
    public bool armor;

    [Header("Attack")]
    public float extraDamage;

    [Header("Weapons")]
    public List<WeaponData> weapons;
    public int index;

    [Header("Score")]
    public float score;
    public float maxScore;
    public float wave;

    [Header("Shoop")]
    public long money;
    public int soul;
    public int soulsToCollect;
}