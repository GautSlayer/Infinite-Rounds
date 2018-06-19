using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardPlayer : MonoBehaviour {

    public float movementSpeed = 1;
    public float minDistance = 1;
    //public float minSwitchDirTimer = 0.2f;
    //public float maxSwitchDirTimer = 1f;

    private Rigidbody2D myRb;
    private GameObject player;
    //private float nextSwitchDir;
    //private Vector2 movement;

    // Use this for initialization
    void Start () {
        myRb = GetComponent<Rigidbody2D>();
        player = GameManager.instance.player;
        //nextSwitchDir = 0;
        //movement = Vector2.zero;
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
            /*
            if(Time.time > nextSwitchDir)
            {
                nextSwitchDir = Time.time + Random.Range(minSwitchDirTimer, maxSwitchDirTimer);

                if (Mathf.Abs(diff.x) >= Mathf.Abs(diff.y))
                {
                    movement = (new Vector2(diff.x, 0).normalized) * (movementSpeed / 1000);
                }
                else
                {
                    movement = (new Vector2(0, diff.y).normalized) * (movementSpeed / 1000);
                }
            }
            */
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
