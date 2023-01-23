using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    public float groundDistance = 0.09f;
    public float wallCheckDistance = 0.09f;
    public float ceilingCheckDistance = 0.09f;
    CapsuleCollider2D  capsuleCollider;
    Animator animator;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    [SerializeField]
    private bool _isGround = true;
    public bool isGrounded { get
        {
            return _isGround;
        }
        private set
        {
            _isGround = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }
    }

    [SerializeField]
    private bool _isOnWall = true;
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            animator.SetBool(AnimationStrings.isOnWall, value);
        }
    }

    [SerializeField]
    private bool _isOnCeiling = true;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right:Vector2.left;

    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            animator.SetBool(AnimationStrings.isOnCeiling, value);
        }
    }
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        isGrounded = capsuleCollider.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        isOnWall = capsuleCollider.Cast(wallCheckDirection,contactFilter, wallHits, wallCheckDistance) > 0;
        isOnCeiling = capsuleCollider.Cast(Vector2.up, contactFilter, ceilingHits, ceilingCheckDistance) > 0;
    }
}
