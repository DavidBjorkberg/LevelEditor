using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
[CustomEditor(typeof(BoxScript)), CanEditMultipleObjects]
public class BoxEditor : Editor
{
    GameObject[] weapons;
    private void OnEnable()
    {
        weapons = Resources.LoadAll("Weapons", typeof(GameObject)).Cast<GameObject>().ToArray();

    }
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        foreach (var weapon in weapons)
        {
            if (GUILayout.Button("Add " + weapon.name, new GUILayoutOption[] { GUILayout.Height(21) }))
            {
                foreach (BoxScript Box in targets)
                {
                    Box.DropItems.Add(weapon);
                }
            }
        }
        if (GUILayout.Button("Remove latest", new GUILayoutOption[] { GUILayout.Height(25) }))
        {
            foreach (BoxScript Box in targets)
            {
                Box.DropItems.RemoveAt(Box.DropItems.Count - 1);
            }
        }
        if (GUILayout.Button("Clear all", new GUILayoutOption[] { GUILayout.Height(30) }))
        {
            foreach (BoxScript Box in targets)
            {
                Box.DropItems.Clear();
            }
        }
        EditorUtility.SetDirty(target);
    }
}
