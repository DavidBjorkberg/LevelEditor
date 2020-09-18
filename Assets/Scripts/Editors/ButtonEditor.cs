using UnityEditor;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;
[CustomEditor(typeof(ButtonScript))]
public class ButtonEditor : Editor
{
    string text;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ButtonScript button = (ButtonScript)target;
        EditorUtility.SetDirty(button);
        button.adding = GUILayout.Toggle(button.adding, text, "Button"
            , new GUILayoutOption[] {GUILayout.Height(35) });
        text = button.adding ? "Ready to add" : "Click to start adding";
        if (button.adding)
        {
            foreach (ButtonScript Button in FindObjectsOfType<ButtonScript>())
            {
                if (Button != button)
                {
                    Button.StopAdding();
                }
            }
        }
        SlowTimeButton(button);
        SpeedUpTimeButton(button);
        EditorUtility.SetDirty(button);
    }
    void SlowTimeButton(ButtonScript button)
    {
        if (GUILayout.Button("Slow time", new GUILayoutOption[] { GUILayout.Height(35) }))
        {
            foreach (ButtonScript Button in targets)
            {
                UnityAction<float> action_ = new UnityAction<float>(Button.SlowTime);
                UnityEventTools.AddFloatPersistentListener(Button.OnButtonPress[0], action_, Button.SlowDuration);
                EditorUtility.SetDirty(Button);
            }
        }
        button.SlowDuration = EditorGUILayout.FloatField("Slow duration", button.SlowDuration);
        EditorUtility.SetDirty(button);
    }
    void SpeedUpTimeButton(ButtonScript button)
    {
        if (GUILayout.Button("Speed up time", new GUILayoutOption[] { GUILayout.Height(35) }))
        {
            foreach (ButtonScript Button in targets)
            {
                UnityAction<float> action_ = new UnityAction<float>(Button.SpeedUpTime);
                UnityEventTools.AddFloatPersistentListener(Button.OnButtonPress[0], action_, Button.SpeedUpDuration);
                EditorUtility.SetDirty(Button);
            }
        }
        button.SpeedUpDuration = EditorGUILayout.FloatField("Speed-up duration", button.SpeedUpDuration);
        EditorUtility.SetDirty(button);
    }
}
