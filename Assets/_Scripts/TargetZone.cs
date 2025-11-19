using UnityEngine;

public class TargetZone : MonoBehaviour
{
    [Header("Configuración")]
    public int scoreValue = 10; // Puntos de esta zona

    [SerializeField] private GameObject dianaPadre; 

    public void HandleHit()
    {
        GameManager.instance.SumarPuntos(scoreValue);
        
        if (dianaPadre != null)
        {
            Destroy(dianaPadre);
        }
        else
        {
            Destroy(transform.root.gameObject);
        }
    }
}