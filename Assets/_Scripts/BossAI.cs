using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    [Header("Configuración")]
    public float detectionRange = 40f;
    public float attackRange = 15f;
    public float fireRate = 1.5f; // Dispara cada 1.5 segundos

    [Header("Referencias")]
    public GameObject projectilePrefab; // La bala roja
    public Transform firePoint;         // La punta del arma

    private Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private float nextFireTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        // Desactivamos la rotación automática para controlarla nosotros
        agent.updateRotation = false;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Siempre mirar al jugador si está cerca
        if (distance <= detectionRange)
        {
            FacePlayer();
        }

        // --- ESTADOS ---

        // 1. PERSEGUIR (Lejos pero visible)
        if (distance > attackRange && distance <= detectionRange)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            
            // Animaciones
            if(animator) 
            {
                animator.SetBool("isMoving", true);
                animator.SetBool("isAttacking", false);
            }
        }
        // 2. ATACAR (Cerca)
        else if (distance <= attackRange)
        {
            agent.isStopped = true; // Se detiene para disparar
            
            // Animaciones
            if(animator)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", true);
            }

            // Disparo
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;
            }
        }
        // 3. IDLE (Nadie cerca)
        else
        {
            agent.isStopped = true;
            if(animator)
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", false);
            }
        }
    }

    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; 
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            // Instanciar la bala
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}