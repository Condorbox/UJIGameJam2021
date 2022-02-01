using System.Runtime.InteropServices;
using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    private List<WeaponData> weapons;
    private WeaponData currentWeapon;
    public Transform shootPoint;
    private float startTimeBtwAttack;
    float timeBtwAttack = 0;
    [SerializeField] private SpriteRenderer sr;

    [Header("ChangeWeapon")]
    private int index = 0;

    [SerializeField] private GameData gameData;

    private Queue<Boolean> inputBuffer;

    private void Awake()
    {
        //gameData = Resources.Load<GameData>(typeof(GameData).Name);

        weapons = gameData.weapons;

        if (weapons.Count == 0) { Debug.LogError("Player doesn't have any weapon"); return; }
        currentWeapon = weapons[gameData.index];
        startTimeBtwAttack = currentWeapon.timeBtwShoot;
        sr.sprite = currentWeapon.icon;
        index = gameData.index;
    }

    // Start is called before the first frame update
    void Start()
    {
        inputBuffer = new Queue<Boolean>();
    }

    private void Update()
    {
        timeBtwAttack -= Time.deltaTime;
        if(Input.GetButtonDown("Fire1")){
            ShootBuffer();
        }

        if(Input.GetKeyDown(KeyCode.E)){
            ChangeWeapon();
        }
        if(Input.GetKeyDown(KeyCode.Q)){
            ChangeWeaponNegative();
        }

        if (inputBuffer.Count > 0){
            Shoot();
            QuitAction();
        }
    }

    private void ShootBuffer()
    {
        inputBuffer.Enqueue(true);
        Invoke("QuitAction", 0.5f);
    }

    private void QuitAction()
    {
        if (inputBuffer.Count > 0)
        {
            inputBuffer.Dequeue();
        }
    }

    void Shoot(){
        if (timeBtwAttack <= 0){
            StartCoroutine(ShootEnumerator());
            timeBtwAttack = startTimeBtwAttack;
            QuitAction();
        }
    }

    IEnumerator ShootEnumerator(){
        for (int i = 0; i < currentWeapon.numOfBullet; i++){
                //var newBullet = Instantiate(currentWeapon.bullet, shootPoint.position, shootPoint.rotation);
                PoolObjectType poolObjectType = currentWeapon.poolInfo.type;
                GameObject newBullet = PoolManager.Instance.GetPoolObject(poolObjectType);
                newBullet.transform.position = shootPoint.position;
                newBullet.transform.rotation = shootPoint.rotation;
                newBullet.gameObject.SetActive(true);


                var transformVector = new Vector2(UnityEngine.Random.Range(currentWeapon.minTrasnformX, currentWeapon.maxTransformX), UnityEngine.Random.Range(currentWeapon.minTransformY, currentWeapon.maxTransformY));

                //newBullet.GetComponent<Rigidbody2D>().velocity = transformVector * currentWeapon.velocity * transform.localScale.x;
                Rigidbody2D rbBullet = newBullet.GetComponent<Rigidbody2D>();
                rbBullet.AddForce((shootPoint.up * transformVector) * currentWeapon.velocity, ForceMode2D.Impulse);

                //Destroy(newBullet, currentWeapon.timeToDestroy);
                //yield return new WaitForSeconds(currentWeapon.timeToDestroy);
                //PoolManager.Instance.CoolObject(newBullet, poolObjectType);

                yield return new WaitForSeconds(currentWeapon.timeBtwBullet);
            }
    }

    private void ChangeWeapon(){
        if(weapons.Count > 1){
            if(index< weapons.Count - 1){
                index++;
                currentWeapon = weapons[index];
                startTimeBtwAttack = weapons[index].timeBtwShoot;
                sr.sprite = currentWeapon.icon;

                gameData.index = index;
            }else{
                index = 0;
                currentWeapon = weapons[index];
                startTimeBtwAttack = weapons[index].timeBtwShoot;
                sr.sprite = currentWeapon.icon;

                gameData.index = index;
            }
        }
    }

    private void ChangeWeaponNegative(){
        if(weapons.Count > 1){
            if (index > 0){
                index--;
                currentWeapon = weapons[index];
                startTimeBtwAttack = weapons[index].timeBtwShoot;
                sr.sprite = currentWeapon.icon;

                gameData.index = index;
            }else{
                index = weapons.Count - 1;
                currentWeapon = weapons[index];
                startTimeBtwAttack = weapons[index].timeBtwShoot;
                sr.sprite = currentWeapon.icon;

                gameData.index = index;
            }
        }
    }

    public void Addweapon(WeaponData weapon)
    {
        weapons.Add(weapon);
    }

    public List<PoolInfo> GeneratePoolList()
    {
        List<PoolInfo> poolInfo = new List<PoolInfo>();

        foreach(WeaponData weapon in weapons)
        {
            if(weapon.poolInfo.pool.Count != 0)
            {
                weapon.poolInfo.pool = new List<GameObject>(0);
            }
            
            poolInfo.Add(weapon.poolInfo);
        }

        return poolInfo;
    }
}
