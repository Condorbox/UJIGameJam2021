using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public enum PoolObjectType{
    Normal,
    ShootGun,
    SnakeKiss,
    OneShoot,
    Gutlin,
    BulletEffect,
    Spawner,
    MoveEnemy,
    PillarEnemy,
    MoveCircleEnemy,
    DashingEnemy,
    ShootEnemy,
    EnemyBullet,
    TwinGun,
    HexBullet,
    HexEnemy
}

[System.Serializable]
public class PoolInfo
{
    public PoolObjectType type;
    public int amount = 0;
    [AssetSelector]
    [AssetsOnly]
    public GameObject prefab;
    public GameObject container;

    [ReadOnly]
    public List<GameObject> pool = new List<GameObject>();
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] public List<PoolInfo> listOfPool;

    [SerializeField] WeaponController weaponController;
    private Vector3 defaultPos = new Vector3(-100, -100, -100);

    // Start is called before the first frame update
    void Start()
    {
        listOfPool = MergeList(weaponController.GeneratePoolList());

        GameObject cont = new GameObject("Containers");
        cont.tag = "Containers";

        for(int i = 0; i < listOfPool.Count; i++)
        {
            if(listOfPool[i].container == null)
            {
                GameObject ob = new GameObject("cont" + listOfPool[i].type);
                ob.transform.parent = cont.transform;
                listOfPool[i].container = ob;
            }
            FillPool(listOfPool[i]);
        }
    }

    public void FillPool(PoolInfo info)
    {
        for(int i=0; i < info.amount; i++)
        {
            GameObject obInstance = null;
            obInstance = Instantiate(info.prefab, info.container.transform);
            obInstance.gameObject.SetActive(false);
            obInstance.transform.position = defaultPos;
            if(obInstance != null)
                info.pool.Add(obInstance);
        }
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        GameObject obInstance = null;
        if (pool.Count > 0)
        {
            obInstance = pool[pool.Count - 1];
            pool.Remove(obInstance);
        }
        else
        {
            obInstance = Instantiate(selected.prefab, selected.container.transform);
        }

        return obInstance;
    }

    public void CoolObject(GameObject ob, PoolObjectType type)
    {
        ob.SetActive(false);
        ob.transform.position = defaultPos;

        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;

        if (!pool.Contains(ob))
            pool.Add(ob);
    }

    private PoolInfo GetPoolByType(PoolObjectType type)
    {
        for(int i = 0; i < listOfPool.Count; i++)
        {
            if (type == listOfPool[i].type)
                return listOfPool[i];
        }

        return null;
    }

    private List<PoolInfo> MergeList(List<PoolInfo> list)
    {
        var newList = listOfPool.Concat(list).ToList();

        return newList.ToList();
    }

    public List<PoolInfo> GetListOfPool()
    {
        return listOfPool;
    }
}
