using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trigger), typeof(ObjectPooler))]
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject objectToSpawn;

    [SerializeField]
    private Transform locationToSpawn;

    private Trigger trigger;
    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>();
        trigger = GetComponent<Trigger>();
        trigger.Initialize(Spawn);
    }

    private void Spawn()
    {
        GameObject go = pooler.GetPooledObject();
        go.transform.position = locationToSpawn.position;
    }
}
