using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{

    public static Pooler SharedInstance;
    List<GameObject> pool;
    [SerializeField] GameObject item;
    int poolAmount = 64;
    private void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            CreateItem();
        }
    }

    private void CreateItem()
    {
        GameObject _item = Instantiate(item);
        _item.SetActive(false);
        pool.Add(_item);
    }

    public GameObject PoolItem()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public GameObject PoolItem(int _extension)
    {
        if (poolAmount < _extension)
        {
            int _oldPool = poolAmount;
            for (int i = _oldPool; i < _extension; i++)
            {
                CreateItem();
            }
            poolAmount = _extension;
        }


        for (int i = 0; i < poolAmount; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                return pool[i];
            }
        }
        return null;
    }

    public void ResetPool()
    {
        for (int i = 0; i < poolAmount; i++)
        {
                pool[i].SetActive(false);
        }
    }


}
