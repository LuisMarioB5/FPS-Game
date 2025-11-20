using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int vidaMaxima = 2;
    public int puntosQueDa = 50;
    
    private int vidaActual;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Sumar puntos al GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.SumarPuntos(puntosQueDa);
        }
        
        // Destruir al robot
        Destroy(gameObject);

        GameManager.instance.RestarEnemigo();
    }
}
