using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Node node = (Node)target;
        base.OnInspectorGUI();
        if (node.PreviousDistance != 0)
        {
            EditorGUILayout.LabelField("Previous distance", node.PreviousDistance.ToString());
        }
        if (node.NextDistance != 0)
        {
            EditorGUILayout.LabelField("Next distance", node.NextDistance.ToString());
        }

    }
}
