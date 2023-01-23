using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCoildersRemain;
    public List<Collider2D> Collider2D = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col= GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Collider2D.Remove(collision);
        if(Collider2D.Count <= 0)
        {
            noCoildersRemain.Invoke();
        }
    }
}
