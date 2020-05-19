using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    [SerializeField]
    private SceneFader sceneFader = null;

    [SerializeField]
    private GameManager gameManager = null;

    [Header("Map")]
    [SerializeField]
    private InputField inputFieldHeight = null;
    [SerializeField]
    private InputField inputFieldWidth = null;
    [SerializeField]
    private InputField inputFieldTileSize = null;

    [Header("Start")]
    [SerializeField]
    private InputField inputFieldStartX = null;
    [SerializeField]
    private InputField inputFieldStartY = null;

    [Header("End")]
    [SerializeField]
    private InputField inputFieldEndX = null;
    [SerializeField]
    private InputField inputFieldEndY = null;

    [Header("Algorithms")]
    [SerializeField]
    private Toggle toggleDfs = null;
    [SerializeField]
    private Toggle toggleBfs = null;
    [SerializeField]
    private Toggle toggleDijkstra = null;
    [SerializeField]
    private Toggle toggleAStar = null;

    [Header("Other")]
    [SerializeField]
    private Toggle toggleAutoStart = null;
    [SerializeField]
    private Text textError = null;

    void Start()
    {
        inputFieldWidth.text = gameManager.mapWidth.ToString();
        inputFieldHeight.text = gameManager.mapHeight.ToString();
        inputFieldTileSize.text = gameManager.tileSize.ToString();

        inputFieldStartX.text = gameManager.startX.ToString();
        inputFieldStartY.text = gameManager.startY.ToString();

        inputFieldEndX.text = gameManager.endX.ToString();
        inputFieldEndY.text = gameManager.endY.ToString();

        toggleDfs.isOn = gameManager.dfs;
        toggleBfs.isOn = gameManager.bfs;
        toggleDijkstra.isOn = gameManager.dijkstra;
        toggleAStar.isOn = gameManager.aStar;

        toggleAutoStart.isOn = gameManager.autoStart;
        textError.text = gameManager.CheckValues();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Play()
    {
        bool res = int.TryParse(inputFieldWidth.text, out gameManager.mapWidth);
        if(!res)
        {
            textError.text = "Map width is not valid!";
            return;
        }
        res = int.TryParse(inputFieldHeight.text, out gameManager.mapHeight);
        if (!res)
        {
            textError.text = "Map height is not valid!";
            return;
        }
        res = float.TryParse(inputFieldTileSize.text, out gameManager.tileSize);
        if (!res)
        {
            textError.text = "Tile size is not valid!";
            return;
        }

        res = int.TryParse(inputFieldStartX.text, out gameManager.startX);
        if (!res)
        {
            textError.text = "Start position is not valid!";
            return;
        }
        res = int.TryParse(inputFieldStartY.text, out gameManager.startY);
        if (!res)
        {
            textError.text = "Start position is not valid!";
            return;
        }

        res = int.TryParse(inputFieldEndX.text, out gameManager.endX);
        if (!res)
        {
            textError.text = "End position is not valid!";
            return;
        }
        res = int.TryParse(inputFieldEndY.text, out gameManager.endY);
        if (!res)
        {
            textError.text = "End position is not valid!";
            return;
        }

        gameManager.dfs = toggleDfs.isOn;
        gameManager.bfs = toggleBfs.isOn;
        gameManager.dijkstra = toggleDijkstra.isOn;
        gameManager.aStar = toggleAStar.isOn;

        gameManager.autoStart = toggleAutoStart.isOn;

        string msg = gameManager.CheckValues();

        if (msg == string.Empty)
        {
            textError.text = string.Empty;
            sceneFader.FadeTo("GameScene");
        }
        else
        {
            textError.text = msg;
        }
    }
}
