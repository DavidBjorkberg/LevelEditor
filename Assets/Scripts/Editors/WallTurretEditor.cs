using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor.Events;

[CustomEditor(typeof(WallTurret)), CanEditMultipleObjects]
public class WallTurretEditor : Editor
{
    ButtonScript Button;
    List<WallTurret> wallturrets = new List<WallTurret>();
    public override void OnInspectorGUI()
    {
        foreach (var target_ in targets)
        {
            WallTurret Target_ = (WallTurret)target;
            EditorUtility.SetDirty(target_);
            wallturrets.Add(Target_);
        }

        base.OnInspectorGUI();
        WallTurret wallturret = (WallTurret)target;
        if (!wallturret.ShootOnGameStart)
        {
            if (GUILayout.Button("Add to button", new GUILayoutOption[] { GUILayout.Height(35) }))
            {
                foreach (WallTurret wallturret_ in targets)
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
                                if (Button.OnButtonPress[0].GetPersistentTarget(i).GetInstanceID() == wallturret_.GetInstanceID())
                                {
                                    Add = false;
                                }
                            }
                        }
                    }
                    if (!wallturret_.ShootOnGameStart)
                    {
                        if (Add)
                        {
                            UnityAction<float> action = new UnityAction<float>(wallturret_.OnButtonClick);
                            UnityEventTools.AddFloatPersistentListener(Button.OnButtonPress[0], action, wallturret_.ShootTime);
                            EditorUtility.SetDirty(Button);

                        }
                    }

                }
            }


        }
    }
}

