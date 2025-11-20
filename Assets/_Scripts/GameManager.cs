using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuración")]
    public int balasIniciales = 10;
    
    [Header("UI")]
    public TextMeshProUGUI textoMunicion;
    public TextMeshProUGUI textoScore;
    public GameObject panelResultados;
    public TextMeshProUGUI textoScoreResultados;

    [Header("Salud Jugador")]
    public TextMeshProUGUI textoVida;
    public int saludMaximaJugador = 100;
    private int saludActualJugador;

    private int balasActuales;
    private int scoreActual = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Asegurarse de que el tiempo esté normal al iniciar
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        balasActuales = balasIniciales;
        saludActualJugador = saludMaximaJugador;
        ActualizarUI();
    }

    public void SumarPuntos(int puntos)
    {
        scoreActual += puntos;
        ActualizarUI();
    }

    public void GastarBala()
    {
        balasActuales--;
        ActualizarUI();

        if (balasActuales <= 0)
        {
            TerminarJuego();
        }
    }

    public bool PuedeDisparar()
    {
        return balasActuales > 0;
    }


    void ActualizarUI()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = "Balas: " + balasActuales;
        }
        
        if (textoScore != null)
        {
            textoScore.text = "Puntos: " + scoreActual;
        }

        if (textoVida != null) 
        {
            textoVida.text = "Salud: " + saludActualJugador;
        }
    }

    void TerminarJuego()
    {
        // Detener el tiempo del juego
        Time.timeScale = 0f;

        Debug.Log("Entrenamiento Terminado. Puntuación Final: " + scoreActual);

        
        if (panelResultados != null)
        {
            textoMunicion.gameObject.SetActive(false);
            textoScore.gameObject.SetActive(false);
            textoVida.gameObject.SetActive(false);
            panelResultados.SetActive(true);
        }

        if (textoScoreResultados != null)
        {
            textoScoreResultados.text = "Puntuación Final: " + scoreActual;
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player != null)
        {
            var controller = player.GetComponent<StarterAssets.FirstPersonController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
        }
    }

    public void RecibirDañoJugador(int daño)
    {
        saludActualJugador -= daño;
        ActualizarUI();

        if (saludActualJugador <= 0)
        {
            TerminarJuego();
        }
    }

    // Función para el botón "Reiniciar"
    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;

        // Recargar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void IrAlNivel1()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("02 Nivel Enemigos"); 
    }

    public void IrAlNivel2()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("03 Nivel Jefe"); 
    }
}