using UnityEngine;
using UnityEngine.UI;

public class LevelDataMono : MonoBehaviour
{
    public LevelData levelData = null;
    public GameObject algorithmGrid = null;
    public GridManager gridManager = null;

    public void SetAlgorithms()
    {
        int i = 0;
        foreach(AlgorithmData algorithmData in levelData.algorithms)
        {
            GameObject algGridElement;
            if (algorithmGrid.transform.childCount < levelData.algorithms.Count)
                algGridElement = (GameObject)Instantiate(Resources.Load("PanelAlgorithm"), algorithmGrid.transform);
            else
                algGridElement = algorithmGrid.transform.GetChild(i).gameObject;
            algGridElement.transform.Find("TextName").GetComponent<Text>().text = "Name: " + algorithmData.name;
            algGridElement.transform.Find("TextFieldCount").GetComponent<Text>().text = "Checked tiles: " + algorithmData.checkedNodesCount;
            algGridElement.transform.Find("TextTime").GetComponent<Text>().text = "Time: " + algorithmData.time.ToString("0.000") + " ms";

            AlgorithmPath algRun = algGridElement.transform.GetComponentInChildren<AlgorithmPath>();
            algRun.path = algorithmData.path;
            algRun.start = gridManager.GetStart();
            algRun.color = GetRunnerColor(algorithmData.name);
            algRun.gridManager = gridManager;

            i++;
        }

        gridManager.SetMap(levelData.blocked);
    }

    private Color GetRunnerColor(string algName)
    {
        switch(algName)
        {
            case "DFS": return Color.white;
            case "BFS": return Color.yellow;
            case "Dijkstra": return Color.green;
            case "A*": return Color.cyan;
            default: return Color.red;
        }
    }

}
