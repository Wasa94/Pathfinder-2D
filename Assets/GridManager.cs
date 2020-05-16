using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private int rows = 10;
    [SerializeField]
    private int cols = 10;
    [SerializeField]
    private float tileSize = 1;

    private GameObject[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        grid = new GameObject[rows, cols];

        GameObject referenceTile1 = (GameObject)Instantiate(Resources.Load("tile1"));
        GameObject referenceTile2 = (GameObject)Instantiate(Resources.Load("tile2"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject referenceTile = Random.value < 0.5 ? referenceTile1 : referenceTile2;
                GameObject tile = (GameObject)Instantiate(referenceTile, transform);

                tile.transform.localScale = new Vector2(tile.transform.localScale.x * tileSize, tile.transform.localScale.y * tileSize);

                float posX = col * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);

                grid[row, col] = tile;
            }
        }

        Destroy(referenceTile1);
        Destroy(referenceTile2);

        float gridW = cols * tileSize;
        float gridH = rows * tileSize;
        transform.position = new Vector2(-gridW / 2 + tileSize / 2, gridH / 2 - tileSize / 2);
    }

    public GameObject[,] GetGrid()
    {
        return grid;
    }
}
