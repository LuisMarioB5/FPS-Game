using UnityEngine;

public class Target : MonoBehaviour
{
    // Esta es una función pública que nuestro script de disparo llamará
    public void TakeHit()
    {
        // El profesor pide que el objeto se destruya
        Destroy(gameObject);
    }
}