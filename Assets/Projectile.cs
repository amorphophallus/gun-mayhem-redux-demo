using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float collisionForce;  // �ӵ���������ɶ�������

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �ӵ�ײ�� Enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);  // �����Լ�
            // ��������д��ײ���������� collider �Լ�����ײ
        }
        // �ӵ�ײ��ǽ
        else if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);  // �����Լ�
        }
    }
}
