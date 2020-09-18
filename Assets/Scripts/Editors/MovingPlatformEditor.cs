using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor.Events;

[CustomEditor(typeof(MovingPlatform)), CanEditMultipleObjects]
public class MovingPlatformEditor : Editor
{
    ButtonScript Button;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MovingPlatform platform = (MovingPlatform)target;
        platform.MoveOnGameStart = EditorGUILayout.Toggle("Move on game start", platform.MoveOnGameStart);
        EditorUtility.SetDirty(platform);

        if (!platform.MoveOnGameStart)
        {
            platform.MoveTime = EditorGUILayout.FloatField("Move time:", platform.MoveTime);
            if (GUILayout.Button("Add to button", new GUILayoutOption[] { GUILayout.Height(35) }))
            {

                foreach (MovingPlatform Platform in targets)
                {
                    foreach (ButtonScript button in FindObjectsOfType<ButtonScript>())
                    {
                        if (button.adding)
                        {
                            Button = button;
                        }
                    }
                    bool Add = true;
                    if (Button == null)
                    {
                        Debug.Log("Pick a button first");
                        break;
                    }
                    if (Button.OnButtonPress[0].GetPersistentEventCount() > 0)
                    {
                        for (int i = 0; i < Button.OnButtonPress[0].GetPersistentEventCount(); i++)
                        {
                            if (Button.OnButtonPress[0].GetPersistentTarget(i) != null)
                            {
                                if (Button.OnButtonPress[0].GetPersistentTarget(i).GetInstanceID() == Platform.GetInstanceID())
                                {
                                    Add = false;
                                }
                            }
                        }
                    }
                    if (!Platform.MoveOnGameStart)
                    {

                        if (Add)
                        {
                            UnityAction<float> action = new UnityAction<float>(Platform.StartMoving);
                            UnityEventTools.AddFloatPersistentListener(Button.OnButtonPress[0], action, Platform.MoveTime);
                            EditorUtility.SetDirty(Button);

                        }
                    }

                }
            }




        }

    }
}

