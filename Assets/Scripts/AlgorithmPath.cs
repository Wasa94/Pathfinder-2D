using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmPath : MonoBehaviour
{
    public List<Node> path = null;
    public Color color = Color.white;
    public Node start = null;
    public GridManager gridManager = null;

    private bool move = false;
    private bool started = false;
    private GameObject runner = null;

    public void StartMoving()
    {
        if (!gridManager.IsFinished() || move)
            return;

        runner = (GameObject)Instantiate(Resources.Load("runner"), GameObject.Find("GridHolder").transform);
        runner.GetComponent<SpriteRenderer>().color = color;
        runner.transform.position = new Vector3(start.transform.position.x, start.transform.position.y, -0.2f);
        runner.transform.localScale = start.transform.localScale * 0.2f;
        move = true;
    }

    private IEnumerator MoveObject(List<Node> path, GameObject obj, Node start)
    {
        foreach (var node in path)
        {
            obj.transform.position = new Vector3(node.transform.position.x, node.transform.position.y, -0.2f);

            yield return new WaitForSeconds(1f);
        }

        move = false;
        started = false;
        Destroy(runner);
    }

    private void Update()
    {
        if (move && !started)
        {
            started = true;
            StartCoroutine(MoveObject(path, runner, start));
        }
    }
}
