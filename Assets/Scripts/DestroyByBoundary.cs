using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Get the root gameobject, and destroy it
        GameObject collisionGO = collision.transform.root.gameObject;
        Destroy(collisionGO);
    }
}
