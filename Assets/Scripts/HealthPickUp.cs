using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int healCount = 20;
    public Vector3 spinRotationSpeed = new Vector3(0,180,0);
    public AudioSource pickUpSource;
    private void Awake()
    {
        pickUpSource= GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable= collision.GetComponent<Damageable>();
        if (damageable)
        {
            bool boolHeal = damageable.Heal(healCount);
            if (boolHeal)
            {
                if (pickUpSource)
                    AudioSource.PlayClipAtPoint(pickUpSource.clip, transform.position, pickUpSource.volume);
                Destroy(gameObject);
            }
                
        }

    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
