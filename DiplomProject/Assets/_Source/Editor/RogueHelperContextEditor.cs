using RogueHelper.Core;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RogueHelperContext))]
public class RogueHelperContextEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RogueHelperContext rogueHelperContext = (RogueHelperContext)target;

        if (GUILayout.Button("Generate level"))
        {
            rogueHelperContext.GenerateLevel(true);
        }
    }
}