using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirection),typeof(Damageable))]

public class Knight : MonoBehaviour
{
    public float walkAcceleration = 3f;
    public float maxSpeed = 3f;
    public enum WalkAbleDirection {Right, Left};
    private WalkAbleDirection _walkDirection;
    public WalkAbleDirection WalkDirection
    {
        get { return _walkDirection; }
        set {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale= new Vector2(gameObject.transform.localScale.x * -1,
                                                             gameObject.transform.localScale.y);
                if(value == WalkAbleDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }else if(value == WalkAbleDirection.Left) {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value; }
    }

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

    public bool CanMove { get {
            return animator.GetBool(AnimationStrings.canMove);
        } }

    public bool IsAlive { get {
            return animator.GetBool(AnimationStrings.isAlive);
        } }

    public float attackCooldown { 
        get 
        {
            return animator.GetFloat(AnimationStrings.cooldown);
        }  
        set 
        {
            animator.SetFloat(AnimationStrings.cooldown, Mathf.Max(value,0));
        } 
    }

    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    public DetectionZone attackZone;
    public DetectionZone groundDetectionZone;
    Animator animator;
    Damageable damageable;
    private Vector2 walkDirectionVector = Vector2.right;
    public float walkSpeedRate = 0.05f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    private void Update()
    {
        HasTarget = attackZone.Collider2D.Count> 0;
        if(attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        if (touchingDirection.isGrounded && touchingDirection.isOnWall)
        {
            FlipDirection();
        }
        if (!damageable.LockVelocity)
        {
            if (CanMove && touchingDirection.isGrounded)
            {
                rb.velocity = new Vector2(
                    Mathf.Clamp(rb.velocity.x+(walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime),-maxSpeed,maxSpeed),
                    rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkSpeedRate), rb.velocity.y);
            }
        }
       
     }

    private void FlipDirection()
    {
        if(WalkDirection == WalkAbleDirection.Right)
        {
            WalkDirection = WalkAbleDirection.Left;
        }else if(WalkDirection == WalkAbleDirection.Left)
        {
            WalkDirection = WalkAbleDirection.Right;
        }
        else
        {
            Debug.LogError("·½Ïò´íÎó");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirection.isGrounded)
        {
            FlipDirection();
        }
    }
}
