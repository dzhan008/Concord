using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Trigger : MonoBehaviour
{
    Action action;
    private GameObject go;

    public void Initialize(Action action)
    {
        this.action = action;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Entity>() != null)
        {
            go = other.gameObject;
            action();
        }
    }
    public GameObject GetCollided()
    {
        return go;
    }
}
