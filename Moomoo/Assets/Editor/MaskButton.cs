using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaskDataUpdater))]
public class MaskButton : Editor
{
    public override void OnInspectorGUI()
    {
        MaskDataUpdater maskDataUpdater = (MaskDataUpdater)target;
        if (GUILayout.Button("Enable"))
            maskDataUpdater.Enable();
        if (GUILayout.Button("Disable"))
            maskDataUpdater.Disable();

        DrawDefaultInspector();
    }
}
