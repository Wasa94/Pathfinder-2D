using UnityEngine;

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
            tileSize = gameManager.tileSize;

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
    }

    public Node[,] GetGrid()
    {
        return grid;
    }
}
