using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    [Header("Map")]
    [SerializeField]
    private int rows = 10;
    [SerializeField]
    private int cols = 10;
    [SerializeField]
    private float tileSize = 90;

    [Header("Start")]
    [SerializeField]
    private int startX = 0;
    [SerializeField]
    private int startY = 4;

    [Header("End")]
    [SerializeField]
    private int endX = 9;
    [SerializeField]
    private int endY = 4;

    [Header("UI")]
    [SerializeField]
    private GameObject levelGrid = null;
    [SerializeField]
    private GameObject buttonCalculate = null;
    [SerializeField]
    private GameObject algorithmGrid = null;

    private Node[,] grid;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = GameObject.Find("GameManager");
        if (gameObject != null)
            gameManager = gameObject.GetComponent<GameManager>();

        if (gameManager != null)
        {
            rows = gameManager.mapHeight;
            cols = gameManager.mapWidth;

            float max = rows > cols ? rows : cols;
            tileSize = 90 * (10 / max);

            startX = gameManager.startX;
            startY = gameManager.startY;

            endX = gameManager.endX;
            endY = gameManager.endY;
        }

        GenerateGrid();
    }

    private void GenerateGrid()
    {
        float tileSizeScaled = tileSize / 90;

        grid = new Node[rows, cols];

        GameObject referenceTile1 = (GameObject)Instantiate(Resources.Load("tile1"));
        GameObject referenceTile2 = (GameObject)Instantiate(Resources.Load("tile2"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject referenceTile = Random.value < 0.7 ? referenceTile1 : referenceTile2;
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                tile.transform.localScale = new Vector2(tile.transform.localScale.x * tileSizeScaled, tile.transform.localScale.y * tileSizeScaled);

                float posX = col * tileSizeScaled;
                float posY = row * -tileSizeScaled;

                tile.transform.position = new Vector2(posX, posY);

                grid[row, col] = tile.GetComponent<Node>();

                grid[row, col].x = row;
                grid[row, col].y = col;
            }
        }

        Destroy(referenceTile1);
        Destroy(referenceTile2);

        float gridW = cols * tileSizeScaled;
        float gridH = rows * tileSizeScaled;
        transform.position = new Vector2(-gridW / 2 + tileSizeScaled / 2, gridH / 2 - tileSizeScaled / 2);

        GameObject start = (GameObject)Instantiate(Resources.Load("start"), transform);
        start.transform.localScale = new Vector2(grid[startX, startY].transform.localScale.x / 5, grid[startX, startY].transform.localScale.y / 5);
        start.transform.position = new Vector3(grid[startX, startY].transform.position.x, grid[startX, startY].transform.position.y, -0.1f);

        GameObject end = (GameObject)Instantiate(Resources.Load("end"), transform);
        end.transform.localScale = new Vector2(grid[endX, endY].transform.localScale.x / 5, grid[endX, endY].transform.localScale.y / 5);
        end.transform.position = new Vector3(grid[endX, endY].transform.position.x, grid[endX, endY].transform.position.y, -0.1f);

        availableForBlock = Flatten(grid).Where(n => !(n.x == startX && n.y == startY) && !(n.x == endX && n.y == endY)).ToList();

        if (gameManager.autoStart) buttonCalculate.SetActive(false);

        nextTime = Time.time + 3;
    }

    List<Node> availableForBlock;
    private int interval = 1;
    private float nextTime = 0;
    private int levelCounter = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            availableForBlock.Clear();
            buttonCalculate.SetActive(false);
        }

        if (availableForBlock.Count == 0 || !gameManager.autoStart)
            return;

        if (Time.time >= nextTime)
        {
            CalculatePaths();

            nextTime += interval;

        }
    }

    public void CalculatePaths()
    {
        if (gameManager.dfs) dfs = FindPath(new DFS());
        if (gameManager.bfs) bfs = FindPath(new BFS());
        if (gameManager.dijkstra) dijkstra = FindPath(new Dijkstra());
        if (gameManager.aStar) aStar = FindPath(new AStar());

        LevelData levelData = new LevelData
        {
            number = levelCounter++,
            blocked = Flatten(grid).Where(n => n.isBlocked).Select(n => new Vector2(n.x, n.y)).ToList(),
            algorithms = new List<AlgorithmData>()
        };

        if (gameManager.dfs && dfs != null) levelData.algorithms.Add(dfs);
        if (gameManager.bfs && bfs != null) levelData.algorithms.Add(bfs);
        if (gameManager.dijkstra && dijkstra != null) levelData.algorithms.Add(dijkstra);
        if (gameManager.aStar && aStar != null) levelData.algorithms.Add(aStar);

        dfs = bfs = dijkstra = aStar = null;

        AddLevel(levelData);

        Node newBlocked;
        do
        {
            int ind = (int)(UnityEngine.Random.value * (availableForBlock.Count - 1));
            newBlocked = availableForBlock[ind];
            availableForBlock.Remove(newBlocked);
            newBlocked.isBlocked = true;
            var path = new DFS().FindPath(grid, grid[startX, startY], grid[endX, endY], out int tmpCheckedNodesCount, out string tmpName);
            if (path == null)
            {
                newBlocked.isBlocked = false;
                newBlocked = null;
            }
            else
            {
                newBlocked.GetComponent<SpriteRenderer>().color = Color.black;
            }
        } while (newBlocked == null && availableForBlock.Count > 0);

        if (!gameManager.autoStart && availableForBlock.Count == 0) buttonCalculate.SetActive(false);
    }

    private void AddLevel(LevelData level)
    {
        GameObject levelGridElement = (GameObject)Instantiate(Resources.Load("GridElement"), levelGrid.transform);
        levelGridElement.GetComponentInChildren<Text>().text = "Level " + level.number;
        levelGridElement.GetComponentInChildren<LevelDataMono>().levelData = level;
        levelGridElement.GetComponentInChildren<LevelDataMono>().algorithmGrid = algorithmGrid;
    }

    private IEnumerable<T> Flatten<T>(T[,] map)
    {
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                yield return map[row, col];
            }
        }
    }

    AlgorithmData dfs;
    AlgorithmData bfs;
    AlgorithmData dijkstra;
    AlgorithmData aStar;

    private AlgorithmData FindPath(IPathFinder pathFinder)
    {
        AlgorithmData alg = new AlgorithmData();

        float startTime = Time.realtimeSinceStartup;
        alg.path = pathFinder.FindPath(grid, grid[startX, startY], grid[endX, endY], out alg.checkedNodesCount, out alg.name)?.ToList();
        alg.time = (Time.realtimeSinceStartup - startTime) * 1000;

        return alg;
    }
}
