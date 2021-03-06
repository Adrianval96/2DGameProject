﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private float boxSizeY;
    private Animator animator;
    private SpriteRenderer sprite;
    private PlayerHealthController healthController;
    
    
    public LayerMask groundMask;
    public float moveSpeed = 10;
    public float jumpForce = 10;
    public float gravity = 50;
    public float groundLimit = -50;
    public Transform spawnPosition;

    public float damage = 25;

    public float groundDistanceCheck = 0.2f;

    public int score;


    // Condiciones de transicion de animaciones
    public bool isRunning;
    public bool isJumping;
    public bool isFalling;
    public bool isGrounded;
    
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsFalling = Animator.StringToHash("isFalling");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int Attack1 = Animator.StringToHash("Attack1");
    
    public static event Action<int> onScoreChange; 


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        healthController = GetComponent<PlayerHealthController>();

        boxSizeY = collider.size.y / 2 + groundDistanceCheck;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 playerDirection = new Vector2(x, y); // Vector de direcciones pasado por input
        // playerDirection.y -= gravity * Time.deltaTime; // Gravedad
        
        Move(playerDirection);
        Jump();
        Attack();

        if (transform.position.y < groundLimit)
        {
            Respawn();
        }
    }

    private void LateUpdate()
    {
        // Comprueba condicion para la transicion de animaciones entre idle y running 
        isRunning = rb.velocity.x != 0;
        animator.SetBool(IsRunning, isRunning);

        isJumping = rb.velocity.y > 0;
        animator.SetBool(IsJumping, isJumping);

        isFalling = rb.velocity.y < 0;
        animator.SetBool(IsFalling, isFalling);

        animator.SetBool(IsGrounded, isGrounded);
    }

    void Move(Vector2 pd)
    {
        rb.velocity = new Vector2(pd.x * moveSpeed, rb.velocity.y); // 

        if (pd.x != 0)
        {
            if (pd.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                // sprite.flipX = true;
            }
            else if (pd.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                // sprite.flipX = false;
            }
        }
    }

    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        
        isGrounded = Physics2D.Raycast(ray.origin, ray.direction, boxSizeY, groundMask);
        
        //Debug.DrawLine(transform.position, Vector2.down * boxSizeY, Color.magenta);
        Debug.DrawRay(ray.origin, ray.direction * boxSizeY);
        //Debug.Log(isGrounded);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
        }
    }

    void Attack()
    {
        bool triggerAttack = Input.GetMouseButtonDown(0);

        if (triggerAttack)
        {
            animator.SetTrigger(Attack1);
        }
    }
    public void Respawn()
    {    
        /*
        rb.velocity = Vector2.zero;
        transform.position = spawnPosition.position;
        */

        SceneManager.LoadScene("SampleScene");
    }

    public void AddToScore(int reward)
    {
        score += reward;
        onScoreChange?.Invoke(score);
    }
}
