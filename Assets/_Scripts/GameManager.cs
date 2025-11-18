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
    }

    void TerminarJuego()
    {
        Debug.Log("Juego Terminado. Score Final: " + scoreActual);
        
        if (panelResultados != null)
        {
            panelResultados.SetActive(true);
        }

        if (textoScoreResultados != null)
        {
            textoScoreResultados.text = "Puntuación Final: " + scoreActual;
        }
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}