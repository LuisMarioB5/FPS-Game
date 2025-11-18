using UnityEngine;

public class TargetZone : MonoBehaviour
{
    // Esta variable aparecerá en el Inspector.
    // Aquí escribirás cuánto vale ESTE cilindro específico (1, 2, 10, 100, etc.)
    public int scoreValue = 10;

    // Función que llamaremos cuando le disparen
    public void HandleHit()
    {
        // 1. Imprimir en consola (para probar)
        Debug.Log("¡Acierto! Puntos ganados: " + scoreValue);
        
        // 2. AQUÍ conectaremos con el sistema de puntuación global más tarde.
        // Ej: GameManager.instance.AddScore(scoreValue);
        
        // 3. Opcional: Destruir toda la diana si le das al centro (si quieres)
        // if (scoreValue == 10) Destroy(transform.root.gameObject);
    }
}