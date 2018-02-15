using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public delegate void OnTrigger();
    private OnTrigger del;
    private GameObject go;

    public void Initialize(OnTrigger del)
    {
        this.del = del;
    }
    private void OnTriggerEnter(Collider other)
    {
        del();
        go = other.gameObject;
    }
    public GameObject GetCollided()
    {
        return go;
    }
}
