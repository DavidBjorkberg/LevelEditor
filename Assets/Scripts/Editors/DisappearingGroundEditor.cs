using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditor.Events;
[CustomEditor(typeof(DisappearingGround)), CanEditMultipleObjects]
public class DisappearingGroundEditor : Editor
{
    ButtonScript Button;


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DisappearingGround platform = (DisappearingGround)target;
        if (platform.GetComponentInParent<DisappearingMovingPlatform>() == null)
        {
            if (GUILayout.Button("Add to button", new GUILayoutOption[] { GUILayout.Height(35) }))
            {
                foreach (DisappearingGround Platform in targets)
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

                    if (Add)
                    {
                        UnityAction<float> action_ = new UnityAction<float>(Platform.GetComponent<DisappearingGround>().Activate);
                        UnityEventTools.AddFloatPersistentListener(Button.OnButtonPress[0], action_, Platform.DisappearTime);
                        EditorUtility.SetDirty(Button);

                    }

                }


            }
        }
    }
}


