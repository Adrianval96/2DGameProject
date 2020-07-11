using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    
    private static readonly int TakeHit = Animator.StringToHash("Take_hit");

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        if (other.CompareTag("SwordDamage"))
        {
            anim.SetTrigger(TakeHit);
        }
    }

    /*
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other);
        if (other.gameObject.CompareTag("SwordDamage"))
        {
            anim.SetTrigger(TakeHit);
        }
    }*/
}
