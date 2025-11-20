using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuración")]
    public int balasIniciales = 10;
    public int enemigosTotales = 3;
    
    [Header("UI")]
    public GameObject panelResultados;
    public TextMeshProUGUI textoMunicion;
    public TextMeshProUGUI textoScore;
    public TextMeshProUGUI textoVida;
    public TextMeshProUGUI textoEnemigosRestantes;
    public TextMeshProUGUI textoScoreResultados;

    [Header("Salud Jugador")]
    public int saludMaximaJugador = 100;
    private int saludActualJugador;

    private int balasActuales;
    private int enemigosActuales;
    private int scoreActual = 0;

    public bool juegoTerminado = false; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        
        juegoTerminado = false; 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        balasActuales = balasIniciales;
        enemigosActuales = enemigosTotales;
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
    
    public void RestarEnemigo()
    {
        enemigosActuales--;
        ActualizarUI();

        if (enemigosActuales <= 0)
        {
            TerminarJuego();
        }
    }

    public bool PuedeDisparar()
    {
        return balasActuales > 0 && !juegoTerminado;
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

        if (textoEnemigosRestantes != null) 
        {
            textoEnemigosRestantes.text = "Enemigos Restantes: " + enemigosActuales;
        }
    }

    void TerminarJuego()
    {
        juegoTerminado = true;

        Time.timeScale = 0f;

        Debug.Log("Entrenamiento Terminado. Puntuación Final: " + scoreActual);
        
        if (panelResultados != null)
        {
            if (textoMunicion != null) textoMunicion.gameObject.SetActive(false);
            if (textoScore != null) textoScore.gameObject.SetActive(false);
            if (textoVida != null) textoVida.gameObject.SetActive(false);
            if (textoEnemigosRestantes != null) textoEnemigosRestantes.gameObject.SetActive(false);
            
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
        if (juegoTerminado) return;

        saludActualJugador -= daño;
        ActualizarUI();

        if (saludActualJugador <= 0)
        {
            TerminarJuego();
        }
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f;
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