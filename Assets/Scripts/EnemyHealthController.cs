using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    [SerializeField] private float MaxHealth = 100;
    public float CurrentHealth;

    private Animator animator;
    private Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        enemy.getCollider().enabled = false;
        //enemy.GetRigidbody2D().velocity = Vector2.zero;
        //enemy.GetRigidbody2D().gravityScale = 0;
        animator.SetTrigger("IsDead");
        Destroy(gameObject, 0.767f);
    }
}