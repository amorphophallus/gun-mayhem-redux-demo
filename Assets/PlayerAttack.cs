using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePosition;
    public GameObject projectile;
    public float projectileSpeed;  // 子弹速度
    public float recoilForce;  // 武器后坐力大小
    public float fireRate;  // 武器开火时间间隔

    private Rigidbody2D rb;
    private float moveDirection;
    private bool facingRight = true;  // Player 的朝向
    private float recoilDirection = -1;  // Player 后坐力方向
    private float nextFireTime = 0f;  // 下次开枪的时间

    // Awake is called after all objects are initialized. Called in a random order.
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();  // will look for a component on this GameObject (what the script is attached to) of type RigidBody2D
    }

    // Update is called once per frame
    void Update()
    {
        // 处理 Player 朝向
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

        // 处理 Fire 输入
        if (Input.GetButton("Fire1") &&  Time.time >= nextFireTime)
        {
            // 产生子弹
            GameObject bullet = Instantiate(projectile, firePosition.position, firePosition.rotation);  // 产生子弹的位置
            Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
            if (bullet_rb != null)
            {
                bullet_rb.velocity = firePosition.right * projectileSpeed;  // 设置子弹速度
            }


            // 产生后坐力
            rb.AddForce(new Vector2(recoilDirection * recoilForce, 0f));  // x 轴上给一个后坐力，y 轴上没有力

            // 计算下次开火时间
            nextFireTime = Time.time + fireRate;
        }
    }
}
