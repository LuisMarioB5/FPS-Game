using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [Header("Configuración")]
    public int scoreValue = 10; // Puntos de esta zona

    // NUEVO: Aquí arrastraremos el objeto "ArcheryTarget" (el papá de todos)
    [SerializeField] private GameObject dianaPadre; 

    public void HandleHit()
    {
        // 1. Mensaje de consola (luego será la puntuación real)
        Debug.Log("¡Puntos Ganados: " + scoreValue + "!");

        // 2. DESTRUCCIÓN SEGURA
        // Si le asignaste un objeto en el Inspector, destruye ese.
        if (dianaPadre != null)
        {
            Destroy(dianaPadre);
        }
        else
        {
            // PLAN B: Si se te olvidó asignarlo, intenta destruir la raíz absoluta del objeto
            // (Esto busca al padre más alto de todos en la jerarquía)
            Destroy(transform.root.gameObject);
        }
    }
}