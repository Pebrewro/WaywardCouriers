using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Targets")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform goods;
    [SerializeField] private float switchTargetAtDistance = 5f;

    [Header("Enemy Stats")]
    public int maxHealth = 10;
    public int currentHealth;
    public float detectionRadius = 10f;
    public float attackRange = 2f;
    public int damageAmount = 2;
    private bool isDead = false;

    [Header ("Attributes")]
    private Animator anim;
    private NavMeshAgent agent;
    private Transform currentTarget;
    private EnemyAttack enemyAttack;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        goods = GameObject.FindWithTag("Goods").transform;
        enemyAttack = GetComponent<EnemyAttack>();

        currentHealth = maxHealth;

    }

    private void Update()
    {
        if (isDead) return;

        if (currentTarget == null)
        {
            currentTarget = goods;
        }

        
        float playerDistance = Vector3.Distance(transform.position, player.position);
        float goodsDistance = Vector3.Distance(transform.position, goods.position);

        if (playerDistance <= switchTargetAtDistance && goodsDistance > playerDistance)
        {
            currentTarget = player;
        }
        else if (goodsDistance <= switchTargetAtDistance)
        {
            currentTarget = goods;
        }

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
            FaceTarget();
        }

        HandleMovementAnimation();
    }
    private void FaceTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        direction.y = 0;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }
    }

    private void HandleMovementAnimation()
    {
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f && speed < agent.acceleration) //When walking
        {
            anim.ResetTrigger("run");
            anim.SetTrigger("walk");
        }
        else if (speed >= agent.acceleration)  //When running
        {
            anim.ResetTrigger("walk");
            anim.SetTrigger("run");
        }
        else  //Reset
        {
            anim.ResetTrigger("walk");
            anim.ResetTrigger("run");
        }
    }

    void IDamageable.Damage(int amount)
    {
        if (isDead) return;

        anim.SetTrigger("hurt");
        Debug.Log($"Ow. Screw you! I just took {amount}");
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        agent.isStopped = true;

        Debug.Log("Enemy has died!");

        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 2f);
    }
}
