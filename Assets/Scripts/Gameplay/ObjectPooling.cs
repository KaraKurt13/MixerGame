using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    private List<GameObject> pooledObjects;

    void Start()
    {
        pooledObjects = new List<GameObject>();

        GameObject poolObject;

        for(int i=0;i<30;i++)
        {
            poolObject = Instantiate(objectToPool, this.gameObject.transform);
            poolObject.SetActive(false);
            pooledObjects.Add(poolObject);
        }
    }

    public void SpawnObjectFromPool()
    {
        for(int i=0;i<pooledObjects.Count;i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                pooledObjects[i].GetComponent<ColorObject>().PlayAppearAnimation();
                return;
            }
        }
    }
}
