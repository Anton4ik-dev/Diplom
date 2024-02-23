using RogueHelper.Core;
using UnityEditor;
using UnityEngine;

public class RogueMenu
{
    [MenuItem("GameObject/RogueHelper/RogueHelperContext", false, 10)]
    static void CreateCustomGameObject(MenuCommand menuCommand)
    {
        GameObject go = new GameObject("RogueHelperContext");
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Undo.AddComponent(go, typeof(RogueHelperContext));
        Selection.activeObject = go;
    }
}