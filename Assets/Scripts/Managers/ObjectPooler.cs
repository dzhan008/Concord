using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour {

    // Setting variables
    public GameObject pooledObject;
    public int pooledAmount;

    private GameObject poolContainer;
    List<GameObject> pooledObjects = new List<GameObject>();

	// Use this for initialization
	void Start () {
        poolContainer = new GameObject();
        poolContainer.name = pooledObject.name + " Container";
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = poolContainer.transform;
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects != null) 
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        GameObject obj = (GameObject)Instantiate(pooledObject);
        obj.transform.parent = poolContainer.transform;
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
