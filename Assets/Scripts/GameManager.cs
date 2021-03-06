﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int mapWidth = 10;
    public int mapHeight = 10;

    public int startX = 0;
    public int startY = 4;

    public int endX = 9;
    public int endY = 4;

    public bool dfs = true;
    public bool bfs = true;
    public bool dijkstra = true;
    public bool aStar = true;

    public bool autoStart = true;

    public string CheckValues()
    {
        if (mapWidth < 1) return "Map width must be positive integer!";
        if (mapHeight < 1) return "Map height must be positive integer!";

        if (startX < 0 || startX >= mapHeight) return "Start position must be valid!";
        if (startY < 0 || startY >= mapWidth) return "Start position must be valid!";

        if (endX < 0 || endX >= mapHeight) return "End position must be valid!";
        if (endY < 0 || endY >= mapWidth) return "End position must be valid!";

        if (startX == endX && startY == endY) return "Start and end positions must be different!";

        if (!dfs && !bfs && !dijkstra && !aStar) return "At least one algorithm must be selected!";

        return string.Empty;
    }
}
