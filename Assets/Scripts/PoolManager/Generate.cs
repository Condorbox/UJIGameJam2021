using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generate : MonoBehaviour
{
    [SerializeField] List<PoolInfo> listOfSmt = new List<PoolInfo>();

    public void Click()
    {
        int random = Random.Range(0, 2);
        switch (random)
        {
            case 0:
                StartCoroutine(GenerateRoutine(PoolObjectType.Normal));
                break;
            case 1:
                StartCoroutine(GenerateRoutine(PoolObjectType.OneShoot));
                break;
        }
    }

    private IEnumerator GenerateRoutine(PoolObjectType type)
    {
        GameObject ob = PoolManager.Instance.GetPoolObject(type);

        ob.transform.position = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
        ob.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        PoolManager.Instance.CoolObject(ob, type);
    }

    public List<PoolInfo> GetList()
    {
        return listOfSmt;
    }
}
