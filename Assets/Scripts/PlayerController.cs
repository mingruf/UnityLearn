using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirection),typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 10f;
    private Rigidbody2D rb;
    Animator animator;
    TouchingDirection touchingDirection;
    Vector2 moveInput;
    Damageable damageable;
    [SerializeField]
    private bool _isMoving = false;
    [SerializeField]
    private bool _isRunning = false;
    [SerializeField]
    private bool _isFacingRight = true;
   

    private  float currentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (touchingDirection.isGrounded)
                {
                    if (isMoving && !touchingDirection.isOnWall)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        
                        return 0;
                    }
                }
                else
                {
                    if (!touchingDirection.isOnWall)
                        return airWalkSpeed;
                    else
                        return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }
    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool IsRunning { 
        get
        {
            return _isRunning;
        }
        private set
        {
             _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }  
    }

    public bool isFacingRight { 
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if(_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    private bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive { get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }

    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable= GetComponent<Damageable>();
    }


    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
            moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            isMoving = moveInput != Vector2.zero;
            SetFaceDirection(moveInput);
        }
        else
        {
            isMoving = false;
        }   
        
    }

    private void SetFaceDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !isFacingRight)
        {
            isFacingRight = true;
        }
        else if(moveInput.x < 0 && isFacingRight)
        {
            isFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.isGrounded && CanMove) 
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y+rb.velocity.y);
    }

    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangeAttackTrigger);
        }
    }
}
