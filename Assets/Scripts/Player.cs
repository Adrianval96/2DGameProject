using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collider;
    private float boxSizeY;
    private Animator animator;
    private SpriteRenderer sprite;
    
    public LayerMask groundMask;

    public float moveSpeed = 10;
    public float jumpForce = 10;
    public float gravity = 50;
    public float groundLimit = -200;
    public Transform spawnPosition;

    public float groundDistanceCheck = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        
        
        boxSizeY = collider.size.y / 2 + groundDistanceCheck;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector2 playerDirection = new Vector2(x, y); // Vector de direcciones pasado por input
        // playerDirection.y -= gravity * Time.deltaTime; // Gravedad
        
        move(playerDirection);
        jump();

        if (transform.position.y < groundLimit)
        {
            rb.velocity = Vector2.zero;
            transform.position = spawnPosition.position;
        }
    }

    private void LateUpdate()
    {
        // Comprueba condicion para la transicion de animaciones entre idle y running 
        bool isRunning = rb.velocity.x != 0;
        animator.SetBool("isRunning", isRunning);
    }

    void move(Vector2 pd)
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

    void jump()
    {
        Ray ray = new Ray(transform.position, Vector2.down);
        
        bool onGround = Physics2D.Raycast(ray.origin, ray.direction, boxSizeY, groundMask);
        
        //Debug.DrawLine(transform.position, Vector2.down * boxSizeY, Color.magenta);
        Debug.DrawRay(ray.origin, ray.direction * boxSizeY);
        Debug.Log(onGround);
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += Vector2.up * jumpForce;
        }
    }
}
