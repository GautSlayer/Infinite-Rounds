using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {

    public float movementSpeed = 1;
    public float minDistance = 1;

    private Rigidbody2D myRb;
    private GameObject player;
    // Use this for initialization
    void Start () {
        myRb = GetComponent<Rigidbody2D>();
        player = GameManager.instance.player;
    }

    private void FixedUpdate()
    {
        if (myRb == null)
        {
            Debug.Log("Enemy is missing a rigidbody2D");
            return;
        }
        if (player == null)
        {
            Debug.Log("Player (target) is null");
            return;
        }

        //Move the enemy toward player's position with rigidbody.MovePosition()
        Vector2 diff = player.transform.position - this.transform.position;
        //If I am to far from the player, I move towards it
        if (diff.magnitude >= minDistance)
        {
            Vector2 movement = (diff.normalized) * (movementSpeed / 1000);
            Vector2 startPos = transform.position;
            myRb.MovePosition(startPos + movement);
        }
        else //I stay in place
        {
            myRb.MovePosition(this.transform.position);
        }
    }
}
