using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public Animator animator;
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> HealthChanged;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
   
    public int _health = 100;
    public int Health
    {
        get { return _health; }
        set {
            _health = value;
            HealthChanged?.Invoke(_health, MaxHealth);
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;


    private float timeSinceHit = 0;
    public float invinciblityTime = 0.25f;


    public bool IsAlive { 
        get {
            return _isAlive;
        } 
        private set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            if(value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invinciblityTime)
            {
                isInvincible= false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
            

        }
    }
    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }

    public bool Heal(int heal)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(0, MaxHealth-Health);
            int actualHeal = Mathf.Min(maxHeal, heal);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
