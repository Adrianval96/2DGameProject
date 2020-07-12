using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;

    private float boxSizeY;
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private Player player;

    private EnemyHealthController healthController;
    /*
    public LayerMask groundMask;
    public bool isGrounded;
    public float groundDistanceCheck = 0.2f;
    public float jumpForce = 3;
    public float thrust = 1.0f;
    */
    public float impulseX = 50;
    public float impulseY = 50;
    
    public float damage = 20;
    public int killReward = 20;
    
    public int facing;


    private static readonly int TakeHit = Animator.StringToHash("Take_hit");

    private void Start()

    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthController = GetComponent<EnemyHealthController>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other);
        if (other.CompareTag("SwordDamage"))
        {
            player = other.GetComponentInParent<Player>();
            anim.SetTrigger(TakeHit);
            healthController.TakeDamage(other.GetComponentInParent<Player>().damage);
            TakeImpulseAfterDamage();
        }
    }
    
    // Quiero que el enemigo se translade ligeramente hacia atras con un saltito cuando sea atacado
    void TakeImpulseAfterDamage()
    {
        //rb.velocity += new Vector2(10, 10);
        Vector2 impulse = new Vector2(impulseX * (-facing), impulseY);
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    public Collider2D getCollider()
    {
        return collider;
    }

    public Rigidbody2D GetRigidbody2D()
    {
        return rb;
    }

    public void Die()
    {
        player.AddToScore(killReward);
        getCollider().enabled = false;
        //enemy.GetRigidbody2D().velocity = Vector2.zero;
        //enemy.GetRigidbody2D().gravityScale = 0;
        anim.SetTrigger("IsDead");
        Destroy(gameObject, 0.767f);
    }
}
