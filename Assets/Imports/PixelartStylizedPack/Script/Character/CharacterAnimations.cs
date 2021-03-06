using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {

        //Movement 4direction's

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Up", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Down", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("Down", true);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("RunLeft", true);
        }
        else
        {
            anim.SetBool("RunLeft", false);
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("RunRight", true);
        }
        else
        {
            anim.SetBool("RunRight", false);
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("RunUp", true);
        }
        else
        {
            anim.SetBool("RunUp", false);
        }
        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("RunDown", true);
        }
        else
        {
            anim.SetBool("RunDown", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("ShootRight", true);
        }
        else
        {
            anim.SetBool("ShootRight", false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("ShootUp", true);
        }
        else
        {
            anim.SetBool("ShootUp", false);

        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("ShootDown", true);
        }
        else
        {
            anim.SetBool("ShootDown", false);
        }
    }
}
