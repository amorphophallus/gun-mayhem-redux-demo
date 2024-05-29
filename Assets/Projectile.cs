using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float collisionForce;  // 子弹给敌人造成多大的推力

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 子弹撞上 Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);  // 销毁自己
            // 不用特意写碰撞加力，两个 collider 自己会碰撞
        }
        // 子弹撞上墙
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // 销毁自己
        }
    }
}
