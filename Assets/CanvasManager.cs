using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager _instance;

    public TextMeshProUGUI wonText;
    public TextMeshProUGUI lostText;

    [Header("All canvases used in scene")] 
    public GameObject startPage;
    public GameObject instructionsPage;
    public GameObject hud;
    public GameObject gameOverPage;
    
    [SerializeField] private GameObject[] _canvases;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
        ShowCanvas(0);
    }
    public void ShowCanvas(int index)
    {

        ClearCanvas();
        for (var i = 0; i < _canvases.Length; i++)
        {
            _canvases[i].SetActive(i == index);
        }
    }

    public void ShowCanvas(string canvasName)
    {
        ClearCanvas();
        foreach (var t in _canvases)
        {
            t.SetActive(t.name == canvasName);
        }
    }

    public void ShowCanvas(int index, bool hasWon)
    {
        ClearCanvas();
        ShowCanvas(index);
        if (hasWon)
        {
            wonText.gameObject.SetActive(true);
            lostText.gameObject.SetActive(false);
        }
        else
        {
            wonText.gameObject.SetActive(false);
            lostText.gameObject.SetActive(true);
        }
    }

    private void ClearCanvas()
    {
        foreach (var t in _canvases)
        {
            t.SetActive(false);
        }
    }

    public void TurnOnHUD()
    {
        ClearCanvas();
        hud.SetActive(true);
    }

    public void TurnOnInstructionsPage()
    {
        ClearCanvas();
        instructionsPage.SetActive(true);
    }

    public void TurnOnStartPage()
    {
        ClearCanvas();
        startPage.SetActive(true);
    }

}
