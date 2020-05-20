using System.Collections.Generic;
using System.Numerics;

public class AStar : IPathFinder
{
    public IEnumerable<Node> FindPath(Node[,] map, Node start, Node target, out int countOfCheckedNodes, out string algName)
    {
        countOfCheckedNodes = 0;
        algName = "A*";

        try
        {
            if (map == null || start == null || target == null)
                return null;

            int mapHeight = map.GetLength(0);
            int mapWidth = map.GetLength(1);

            bool[,] visited = new bool[mapHeight, mapWidth];

            Vector2 targetVector = new Vector2(target.x, target.y);
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                {
                    map[i, j].gCost = float.MaxValue;
                    map[i, j].hCost = Vector2.Distance(new Vector2(map[i, j].x, map[i, j].y), targetVector);
                }

            Heap<Node> heap = new Heap<Node>(mapHeight * mapWidth);
            Node[,] pathMatrix = new Node[mapHeight, mapWidth];

            map[start.x, start.y].gCost = 0;
            heap.Add(start);

            while (heap.Count > 0)
            {
                Node tmp = heap.RemoveFirst();
                visited[tmp.x, tmp.y] = true;
                countOfCheckedNodes++;

                if (tmp == target)
                {
                    List<Node> path = new List<Node>();
                    Node hlp = pathMatrix[target.x, target.y];

                    while (hlp.x != start.x || hlp.y != start.y)
                    {
                        path.Add(hlp);
                        hlp = pathMatrix[hlp.x, hlp.y];
                    }

                    path.Reverse();
                    path.Add(target);

                    return path;
                }

                if (tmp.x > 0 && !visited[tmp.x - 1, tmp.y] && !map[tmp.x - 1, tmp.y].isBlocked)
                {
                    if ((map[tmp.x, tmp.y].gCost + 1) < map[tmp.x - 1, tmp.y].gCost || !heap.Contains(map[tmp.x - 1, tmp.y]))
                    {
                        pathMatrix[tmp.x - 1, tmp.y] = tmp;
                        map[tmp.x - 1, tmp.y].gCost = map[tmp.x, tmp.y].gCost + 1;
                        if (!heap.Contains(map[tmp.x - 1, tmp.y]))
                            heap.Add(map[tmp.x - 1, tmp.y]);
                        else
                            heap.UpdateItem(map[tmp.x - 1, tmp.y]);
                    }
                }

                if (tmp.y > 0 && !visited[tmp.x, tmp.y - 1] && !map[tmp.x, tmp.y - 1].isBlocked)
                {
                    if ((map[tmp.x, tmp.y].gCost + 1) < map[tmp.x, tmp.y - 1].gCost || !heap.Contains(map[tmp.x, tmp.y - 1]))
                    {
                        pathMatrix[tmp.x, tmp.y - 1] = tmp;
                        map[tmp.x, tmp.y - 1].gCost = map[tmp.x, tmp.y].gCost + 1;
                        if (!heap.Contains(map[tmp.x, tmp.y - 1]))
                            heap.Add(map[tmp.x, tmp.y - 1]);
                        else
                            heap.UpdateItem(map[tmp.x, tmp.y - 1]);
                    }
                }

                if (tmp.x < mapHeight - 1 && !visited[tmp.x + 1, tmp.y] && !map[tmp.x + 1, tmp.y].isBlocked)
                {
                    if ((map[tmp.x, tmp.y].gCost + 1) < map[tmp.x + 1, tmp.y].gCost || !heap.Contains(map[tmp.x + 1, tmp.y]))
                    {
                        pathMatrix[tmp.x + 1, tmp.y] = tmp;
                        map[tmp.x + 1, tmp.y].gCost = map[tmp.x, tmp.y].gCost + 1;
                        if (!heap.Contains(map[tmp.x + 1, tmp.y]))
                            heap.Add(map[tmp.x + 1, tmp.y]);
                        else
                            heap.UpdateItem(map[tmp.x + 1, tmp.y]);
                    }
                }

                if (tmp.y < mapWidth - 1 && !visited[tmp.x, tmp.y + 1] && !map[tmp.x, tmp.y + 1].isBlocked)
                {
                    if ((map[tmp.x, tmp.y].gCost + 1) < map[tmp.x, tmp.y + 1].gCost || !heap.Contains(map[tmp.x, tmp.y + 1]))
                    {
                        pathMatrix[tmp.x, tmp.y + 1] = tmp;
                        map[tmp.x, tmp.y + 1].gCost = map[tmp.x, tmp.y].gCost + 1;
                        if (!heap.Contains(map[tmp.x, tmp.y + 1]))
                            heap.Add(map[tmp.x, tmp.y + 1]);
                        else
                            heap.UpdateItem(map[tmp.x, tmp.y + 1]);
                    }
                }
            }

            return null;
        }
        catch
        {
            return null;
        }
    }
}

