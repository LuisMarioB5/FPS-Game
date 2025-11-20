using UnityEngine;
using UnityEngine.AI;

public class RobotAI : MonoBehaviour
{
    [Header("Configuración IA")]
    public float detectionRange = 10f; 
    
    [Header("Animación Procedimental")]
    public Transform modelTransform; 
    public float bobSpeed = 3f;
    public float bobHeight = 0.05f;
    public float leanAmount = 15f;   

    [Header("Daño al Jugador")]
    public int dañoAlJugador = 10;
    public float tiempoEntreAtaques = 1.5f;
    public float RangoDeAtaque = 2.0f;
    private float siguienteAtaque = 0f;
    
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
            
            FacePlayer(); 
        }

        AnimateRobot();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer < RangoDeAtaque && Time.time >= siguienteAtaque)
        {
            AtacarJugador();
            siguienteAtaque = Time.time + tiempoEntreAtaques;
        }
    }

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

    void AtacarJugador()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.RecibirDañoJugador(dañoAlJugador);
            Debug.Log("¡El robot te golpeó!");
        }
    }
}