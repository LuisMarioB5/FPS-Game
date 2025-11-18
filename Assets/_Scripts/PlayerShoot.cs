using UnityEngine;
using UnityEngine.InputSystem; // <-- CAMBIO: Añadimos esto para el nuevo sistema

public class PlayerShoot : MonoBehaviour
{
    // 'Update' se llama una vez por cada frame
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {

            Debug.DrawRay(transform.position, transform.forward * 100f, Color.green, 0.2f);

            RaycastHit hit; 

            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                Debug.Log("Le di a: " + hit.transform.name);

                if (hit.transform.CompareTag("Target"))
                {
                    hit.transform.GetComponent<Target>().TakeHit();
                }
            }
        }
    }
}