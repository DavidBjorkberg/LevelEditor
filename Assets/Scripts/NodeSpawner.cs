using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class NodeSpawner : MonoBehaviour
{
    public GameObject Node;
    Vector3 Direction;
    [SerializeField]
    internal List<GameObject> nodes = new List<GameObject>();
    public void SpawnNode()
    {
        GameObject spawned;
        if (nodes.Count <= 1)
        {
            spawned = Instantiate(Node
                , transform.position + new Vector3(0, nodes.Count + 1, 0)
                , Quaternion.identity);
        }
        else
        {
            Direction = nodes[nodes.Count - 1].transform.position - nodes[nodes.Count - 2].transform.position;
            Direction.Normalize();
            spawned = Instantiate(Node
                , nodes[nodes.Count - 1].transform.position + Direction
                , Quaternion.identity);
        }
        nodes.Add(spawned);
        spawned.name = (nodes.Count).ToString();
        spawned.GetComponent<Node>().spawner = this;
        spawned.transform.parent = GameObject.Find("Nodes").transform;
    }
    public void DespawnNode()
    {
        DestroyImmediate(nodes[nodes.Count - 1]);
    }

    private void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            int count = nodes.Count;
            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(nodes[0]);
            }
        }
    }
}
