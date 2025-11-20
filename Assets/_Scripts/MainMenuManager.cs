using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void CargarEntrenamiento()
    {
        SceneManager.LoadScene("01 Galeria de tiro");
    }

    public void CargarCombate()
    {
        SceneManager.LoadScene("02 Nivel Enemigos"); 
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }
}