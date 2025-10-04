using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;

    public List<GameObject> bulletPool;

    public GameObject bulletPrefab;

    public int numPool = 5;

    void Awake()
    {
        SharedInstance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bulletPool = new List<GameObject>();

        for (int i = 0; i < numPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            GameObject obj = bulletPool[i];

            if (obj.activeInHierarchy == false)
            {
                return obj;
            }
        }

        IncreasePool();
        return GetPooledObject();
    }
    
    private void IncreasePool()
    {
        for (int j = 0; j < numPool; j++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
    }

}
