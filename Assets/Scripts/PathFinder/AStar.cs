using System.Collections.Generic;
using System.Linq;
using System.Numerics;

public class AStar : IPathFinder
{
    public IEnumerable<Node> FindPath(Node[,] map, Node start, Node target)
    {
        try
        {
            if (map == null || start == null || target == null)
                return null;

            int mapSize = map.GetLength(0);

            bool[,] visited = new bool[mapSize, mapSize];
            float[,] distance = new float[mapSize, mapSize];
            float[,] rootDistance = new float[mapSize, mapSize];
            float[,] targetDistance = new float[mapSize, mapSize];

            Vector2 targetVector = new Vector2(target.x, target.y);
            for (int i = 0; i < mapSize; i++)
                for (int j = 0; j < mapSize; j++)
                {
                    distance[i, j] = float.MaxValue;
                    rootDistance[i, j] = float.MaxValue;
                    targetDistance[i, j] = 2 * Vector2.Distance(new Vector2(map[i, j].x, map[i, j].y), targetVector);
                }

            HashSet<Node> set = new HashSet<Node>();
            Node[,] pathMatrix = new Node[mapSize, mapSize];

            rootDistance[start.x, start.y] = 0;
            distance[start.x, start.y] = targetDistance[start.x, start.y];
            set.Add(start);

            while (set.Count > 0)
            {
                float min = set.Min(n => distance[n.x, n.y]);
                Node tmp = set.First(n => distance[n.x, n.y] == min);
                set.Remove(tmp);
                visited[tmp.x, tmp.y] = true;

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
                    rootDistance[tmp.x - 1, tmp.y] = System.Math.Min(rootDistance[tmp.x, tmp.y] + 1, rootDistance[tmp.x - 1, tmp.y]);
                    float minDistance = System.Math.Min(distance[tmp.x - 1, tmp.y], rootDistance[tmp.x - 1, tmp.y] + targetDistance[tmp.x - 1, tmp.y]);
                    if (minDistance != distance[tmp.x - 1, tmp.y])
                    {
                        set.Add(map[tmp.x - 1, tmp.y]);
                        pathMatrix[tmp.x - 1, tmp.y] = tmp;
                        distance[tmp.x - 1, tmp.y] = minDistance;
                    }
                }

                if (tmp.y > 0 && !visited[tmp.x, tmp.y - 1] && !map[tmp.x, tmp.y - 1].isBlocked)
                {
                    rootDistance[tmp.x, tmp.y - 1] = System.Math.Min(rootDistance[tmp.x, tmp.y] + 1, rootDistance[tmp.x, tmp.y - 1]);
                    float minDistance = System.Math.Min(distance[tmp.x, tmp.y - 1], rootDistance[tmp.x, tmp.y - 1] + targetDistance[tmp.x, tmp.y - 1]);
                    if (minDistance != distance[tmp.x, tmp.y - 1])
                    {
                        set.Add(map[tmp.x, tmp.y - 1]);
                        pathMatrix[tmp.x, tmp.y - 1] = tmp;
                        distance[tmp.x, tmp.y - 1] = minDistance;
                    }
                }

                if (tmp.x < mapSize - 1 && !visited[tmp.x + 1, tmp.y] && !map[tmp.x + 1, tmp.y].isBlocked)
                {
                    rootDistance[tmp.x + 1, tmp.y] = System.Math.Min(rootDistance[tmp.x, tmp.y] + 1, rootDistance[tmp.x + 1, tmp.y]);
                    float minDistance = System.Math.Min(distance[tmp.x + 1, tmp.y], rootDistance[tmp.x + 1, tmp.y] + targetDistance[tmp.x + 1, tmp.y]);
                    if (minDistance != distance[tmp.x + 1, tmp.y])
                    {
                        set.Add(map[tmp.x + 1, tmp.y]);
                        pathMatrix[tmp.x + 1, tmp.y] = tmp;
                        distance[tmp.x + 1, tmp.y] = minDistance;
                    }
                }

                if (tmp.y < mapSize - 1 && !visited[tmp.x, tmp.y + 1] && !map[tmp.x, tmp.y + 1].isBlocked)
                {
                    rootDistance[tmp.x, tmp.y + 1] = System.Math.Min(rootDistance[tmp.x, tmp.y] + 1, rootDistance[tmp.x, tmp.y + 1]);
                    float minDistance = System.Math.Min(distance[tmp.x, tmp.y + 1], rootDistance[tmp.x, tmp.y + 1] + targetDistance[tmp.x, tmp.y + 1]);
                    if (minDistance != distance[tmp.x, tmp.y + 1])
                    {
                        set.Add(map[tmp.x, tmp.y + 1]);
                        pathMatrix[tmp.x, tmp.y + 1] = tmp;
                        distance[tmp.x, tmp.y + 1] = minDistance;
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

