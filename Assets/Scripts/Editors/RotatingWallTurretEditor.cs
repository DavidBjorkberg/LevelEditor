using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditor.Events;
[CustomEditor(typeof(RotatingWallTurret)), CanEditMultipleObjects]
public class RotatingWallTurretEditor : Editor
{
    ButtonScript activatedButton;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add to button", new GUILayoutOption[] { GUILayout.Height(35) }))
        {
            foreach (RotatingWallTurret wallturret_ in targets)
            {
                if (!wallturret_.ShootOnGameStart)
                {
                    foreach (ButtonScript button in FindObjectsOfType<ButtonScript>())
                    {
                        if (button.adding)
                        {
                            activatedButton = button;
                        }
                    }
                    if (activatedButton == null)
                    {
                        Debug.Log("Pick a button first");
                        break;
                    }
                    //Prevent same object being added twice to the same button
                    bool add = true;
                    for (int i = 0; i < activatedButton.OnButtonPress[0].GetPersistentEventCount(); i++)
                    {
                        if (activatedButton.OnButtonPress[0].GetPersistentTarget(i) != null
                            && activatedButton.OnButtonPress[0].GetPersistentTarget(i).GetInstanceID() == wallturret_.GetInstanceID())
                        {
                            add = false;
                        }
                    }
                    if (add)
                    {
                        UnityAction<float> action = new UnityAction<float>(wallturret_.OnButtonClick);
                        UnityEventTools.AddFloatPersistentListener(activatedButton.OnButtonPress[0], action, wallturret_.ShootTime);
                        EditorUtility.SetDirty(activatedButton);
                    }
                }
            }
        }
        if (GUILayout.Button("Change vision area", new GUILayoutOption[] { GUILayout.Height(35) }))
        {
            RotatingWallTurret wallturretTarget = (RotatingWallTurret)target;
            Selection.activeGameObject = wallturretTarget.transform.GetChild(2).gameObject;
        }
    }
}
