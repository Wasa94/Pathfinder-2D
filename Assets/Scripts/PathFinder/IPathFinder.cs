﻿using System.Collections.Generic;

public interface IPathFinder
{
    IEnumerable<Node> FindPath(Node[,] map, Node start, Node target, out int countOfCheckedNodes, out string algName);
}