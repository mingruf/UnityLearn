using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public Vector2 moveSpeed = new Vector2(3f,0);
    public int damage = 10;

    Rigidbody2D rb;
    public Vector2 knockBack = new Vector2(0,0);

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            Vector2 deliverknockBack = transform.localScale.x > 0 ? knockBack : new Vector2(-knockBack.x, knockBack.y);
            bool damageHit = damageable.Hit(damage, deliverknockBack);
            if (damageHit)
            {
                Debug.Log("hit for" + damage);
                Destroy(gameObject);
            }
        }
    }
}
