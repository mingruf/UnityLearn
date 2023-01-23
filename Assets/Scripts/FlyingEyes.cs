using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEyes : MonoBehaviour
{
    public float flySpeed = 3f;
    public float wayPointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public Collider2D deathCollider;
    public List<Transform> wayPoints;

    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    Transform nextPoint;
    int wayPointNum = 0;

    public bool _HasTarget = false;
    

    public bool HasTarget
    {
        get
        {
            return _HasTarget;
        }
        private set
        {
            _HasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }


    private void Start()
    {
        nextPoint = wayPoints[wayPointNum];
    }

    private void Update()
    {
        HasTarget = biteDetectionZone.Collider2D.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
    }

    private void Flight()
    {
        Vector2 directionToPoint = (nextPoint.position - transform.position).normalized;
        float distance = Vector2.Distance(nextPoint.position, transform.position);
        rb.velocity = directionToPoint * flySpeed;
        UpdateDirection();
        if(distance <= wayPointReachedDistance)
        {
            wayPointNum = (wayPointNum+1)%wayPoints.Count;
            nextPoint = wayPoints[wayPointNum];
        }
    }

    private void UpdateDirection()
    {
        Vector3 localScale = transform.localScale;
        if(localScale.x > 0)
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
        else
        {
            if(rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
            }
        }
    }
    public void OnDeath()
    {
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }
}
