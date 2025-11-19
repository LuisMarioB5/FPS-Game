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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        balasActuales = balasIniciales;
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
            TerminarEntrenamiento();
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
    }

    void TerminarEntrenamiento()
    {
        Debug.Log("Entrenamiento Terminado. Puntuación Final: " + scoreActual);
        
        if (panelResultados != null)
        {
            textoMunicion.gameObject.SetActive(false);
            textoScore.gameObject.SetActive(false);
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

        Debug.Log("Dirigiendo al Menú Principal ('MainMenu')...");

        // SceneManager.LoadScene("MainMenu");
    }

    public void IrAlNivel1()
    {
        Time.timeScale = 1f;

        Debug.Log("Dirigiendo al Nivel 1 ('Nivel1')...");
        // SceneManager.LoadScene("Nivel1");
    }
}