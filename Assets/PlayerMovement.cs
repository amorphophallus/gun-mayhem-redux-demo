using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;  // public �������� Unity �������ʾ
    public float jumpForce;
    public Transform ceilingCheck1;  // 4 ��λ�õ�
    public Transform ceilingCheck2;
    public Transform groundCheck1;
    public Transform groundCheck2;
    public LayerMask groundObjects;
    public float checkRadius;  // �컨��͵������ײ���뾶
    public int maxJumpCount;  // ������������������ٴ�

    private Rigidbody2D rb;
    private bool facingRight = true;  // �����ұ�Ϊ true���������Ϊ false
    private float moveDirection;
    private bool isJumping;
    private bool isGrounded;
    private int jumpCount;  // Ŀǰ�����������ٴ�

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // will look for a component on this GameObject (what the script is attached to) of type RigidBody2D
    }

    // �ڵ�һ�ε��� Update ֮ǰִ�У����г�ʼ��
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
        moveDirection = Input.GetAxis("Horizontal");   // ����� Unity->Edit->Project Settings->Input ����������ü�λ
        if (Input.GetButtonDown("Jump") && jumpCount > 0)  // ������Ծ�����Ұ�����Ծ����������
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
            rb.AddForce(new Vector2(0f, jumpForce));  // x ����û������y ���ϼ��� jumpForce ����
            jumpCount--;
        }
        isJumping = false;
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);  // ���� y ����ת 180 ��
    }
}
