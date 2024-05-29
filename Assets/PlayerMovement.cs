using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;  // public 参数会在 Unity 面板中显示
    public float jumpForce;
    public Transform ceilingCheck1;  // 4 个位置点
    public Transform ceilingCheck2;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask groundObjects;
    public float checkRadius;  // 天花板和地面的碰撞检查半径
    public int maxJumpCount;  // 参数：最多能连跳多少次

    private Rigidbody2D rb;
    private bool facingRight = true;  // 面向右边为 true，面向左边为 false
    private float moveDirection;
    private bool isJumping;
    private bool isGrounded;
    private int jumpCount;  // 目前还能再跳多少次

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // will look for a component on this GameObject (what the script is attached to) of type RigidBody2D
    }

    // 在第一次调用 Update 之前执行，进行初始化
    private void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        // Get inputs
        ProcessInputs();

        // Animate
        Animate();
    }

    // Better for handling physics, can be called multiple times per update frame.
    private void FixedUpdate()
    {
        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck1.position, checkRadius, groundObjects)
                  || Physics2D.OverlapCircle(groundCheck2.position, checkRadius, groundObjects);
        if (isGrounded)
        {
            jumpCount = maxJumpCount;
        }

        // Move
        Move();
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");   // 这个在 Unity->Edit->Project Settings->Input 里面可以设置键位
        if (Input.GetButtonDown("Jump") && jumpCount > 0)  // 还有跳跃次数且按下跳跃键才能起跳
        {
            isJumping = true;
        }
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight || moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f, jumpForce));  // x 轴上没有力，y 轴上加上 jumpForce 的力
            jumpCount--;
        }
        isJumping = false;
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);  // 绕着 y 轴旋转 180 度
    }
}
