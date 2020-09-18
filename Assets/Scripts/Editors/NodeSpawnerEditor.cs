using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NodeSpawner))]
public class NodeSpawnerEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        NodeSpawner nodespawner = (NodeSpawner)target;
        if (GUILayout.Button("Spawn node", new GUILayoutOption[] { GUILayout.Height(25) }))
        {
            nodespawner.SpawnNode();
        }
        if (GUILayout.Button("Despawn node", new GUILayoutOption[] { GUILayout.Height(25) }))
        {
            nodespawner.DespawnNode();
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
