using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class Node : MonoBehaviour
{
    [SerializeField]
    internal NodeSpawner spawner;
    [HideInInspector]
    public float NextDistance;
    internal GameObject NextNode;
    [HideInInspector]
    public float PreviousDistance;
    internal GameObject PreviousNode;
    private void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            if (spawner != null)
            {
                spawner.nodes.Remove(gameObject);
                EditorUtility.SetDirty(spawner);
            }
        }
    }
    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (spawner.nodes.Count > 1)
            {
                int This = spawner.nodes.IndexOf(gameObject);
                bool IsLastNode = This + 1 == spawner.nodes.Count;
                bool IsFirstNode = This - 1 == -1;
                if (!IsLastNode && !IsFirstNode)
                {
                    NextNode = spawner.nodes[This + 1];
                    PreviousNode = spawner.nodes[This - 1];
                }
                else if (IsLastNode)
                {
                    NextNode = spawner.nodes[0];
                    PreviousNode = spawner.nodes[This - 1];
                }
                else if (IsFirstNode)
                {
                    NextNode = spawner.nodes[This + 1];
                    PreviousNode = spawner.nodes[spawner.nodes.Count - 1];

                }
                NextDistance = Vector3.Distance(transform.position, NextNode.transform.position);
                PreviousDistance = Vector3.Distance(transform.position, PreviousNode.transform.position);
            }
        }
    }
}