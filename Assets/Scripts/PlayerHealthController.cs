using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    private Player player;
    
    public static event Action<float> onPlayerHealthChange; 

    public float MaxHealth = 100;

    public float CurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("DamageSource"))
        {
            float dmg = other.gameObject.GetComponent<Enemy>().damage;
            TakeDamage(dmg);
        }
    }

    private void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        
        // Lanza el evento TakeDamage y luego ya HealthBar se suscribe al evento
        onPlayerHealthChange?.Invoke(CurrentHealth / MaxHealth);
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            player.Respawn();
        }
    }
    
    
}
