using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public float projectileSpeed;  // �ӵ��ٶ�
    public float recoilForce;  // ������������С
    public float fireRate;  // ��������ʱ����

    private Rigidbody2D rb;
    private float moveDirection;
    private bool facingRight = true;  // Player �ĳ���
    private float recoilDirection = -1;  // Player ����������
    private float nextFireTime = 0f;  // �´ο�ǹ��ʱ��

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // will look for a component on this GameObject (what the script is attached to) of type RigidBody2D
    }

    // Update is called once per frame
    void Update()
    {
        // ���� Player ����
        moveDirection = Input.GetAxis("Horizontal");
        if (moveDirection > 0)
        {
            facingRight = true;
            recoilDirection = -1;
        }
        else if (moveDirection < 0)
        {
            facingRight = false;
            recoilDirection = 1;
        }

        // ���� Fire ����
        if (Input.GetButton("Fire1") &&  Time.time >= nextFireTime)
        {
            // �����ӵ�
            GameObject bullet = Instantiate(projectile, firePosition.position, firePosition.rotation);  // �����ӵ���λ��
            Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
            if (bullet_rb != null)
            {
                bullet_rb.velocity = firePosition.right * projectileSpeed;  // �����ӵ��ٶ�
            }


            // ����������
            rb.AddForce(new Vector2(recoilDirection * recoilForce, 0f));  // x ���ϸ�һ����������y ����û����

            // �����´ο���ʱ��
            nextFireTime = Time.time + fireRate;
        }
    }
}
