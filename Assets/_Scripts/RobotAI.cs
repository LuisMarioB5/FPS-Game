using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    [Header("Configuración IA")]
    public float detectionRange = 10f; 
    public int damage = 1;
    
    [Header("Animación Procedimental")]
    public Transform modelTransform; 
    public float bobSpeed = 3f;
    public float bobHeight = 0.05f;
    public float leanAmount = 15f;   

    private Transform player;
    private NavMeshAgent agent;
    private float defaultY;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

        if (modelTransform != null) defaultY = modelTransform.localPosition.y;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
            
            // --- NUEVO: OBLIGAR A MIRAR AL JUGADOR ---
            FacePlayer(); 
        }

        AnimateRobot();
    }

    // Función nueva para corregir la rotación
    void FacePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        
        direction.y = 0; 

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void AnimateRobot()
    {
        if (modelTransform == null) return;

        // FLOTAR (Bobbing)
        float newY = defaultY + (Mathf.Sin(Time.time * bobSpeed) * bobHeight);
        
        // INCLINARSE (Leaning)
        float currentSpeed = agent.velocity.magnitude;
        float leanAngle = Mathf.Lerp(0, leanAmount, currentSpeed / agent.speed);

        modelTransform.localPosition = new Vector3(modelTransform.localPosition.x, newY, modelTransform.localPosition.z);
        modelTransform.localRotation = Quaternion.Euler(leanAngle, 0, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Robot hit the player, dealing " + damage + " damage.");
        }
    }
}