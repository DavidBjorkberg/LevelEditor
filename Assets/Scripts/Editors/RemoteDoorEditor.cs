using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine.Events;

[CustomEditor(typeof(RemoteDoor)), CanEditMultipleObjects]
public class RemoteDoorEditor : Editor
{
    ButtonScript addingButton;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Add to button", new GUILayoutOption[] { GUILayout.Height(35) }))
        {
            foreach (RemoteDoor door in targets)
            {
                foreach (ButtonScript button in FindObjectsOfType<ButtonScript>())
                {
                    if (button.adding)
                    {
                        addingButton = button;
                        break;
                    }
                }
                if (addingButton == null)
                {
                    Debug.Log("Pick a button first");
                    break;
                }
                bool add = true;
                //Check if the door is already added to the button
                for (int i = 0; i < addingButton.OnButtonPress[0].GetPersistentEventCount(); i++)
                {
                    if (addingButton.OnButtonPress[0].GetPersistentTarget(i) != null
                        && addingButton.OnButtonPress[0].GetPersistentTarget(i).GetInstanceID() == door.GetInstanceID())
                    {
                        add = false;
                    }
                }
                //If a button is selected and it isn't already linked to the door, add the doors OnButtonClick function
                if (add)
                {
                    UnityAction<float> action = new UnityAction<float>(door.OnButtonClick);
                    UnityEventTools.AddFloatPersistentListener(addingButton.OnButtonPress[0], action, door.OpenTime);
                    EditorUtility.SetDirty(addingButton);
                }
            }
        }
    }
}
