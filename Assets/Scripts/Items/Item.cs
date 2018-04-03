using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    
    public bool held = false;
    public ItemSO ItemData;

    private void Start() {
        Collider [] colliders = GetComponents<Collider>();
        held = (transform.parent != null && transform.parent.name == "WeaponGrip") ? true : false;
        foreach (Collider col in colliders) {
            col.isTrigger = (held) ? false : true;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Player hit_player = other.GetComponent<Player>();
        if (hit_player && hit_player.AddItem(this)) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

}