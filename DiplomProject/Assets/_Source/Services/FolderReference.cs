using UnityEditor;

namespace RogueHelper.Services
{
    [System.Serializable]
    public class FolderReference
    {
        public string GUID;
        public string Path { 
            get {
                string path = AssetDatabase.GUIDToAssetPath(GUID);
                string[] newPath = path.Split('/');
                return newPath[newPath.Length - 1];
            }
            set {
                Path = value;
            }
        }
        //public DefaultAsset Asset => AssetDatabase.LoadAssetAtPath<DefaultAsset>(Path);
    }
}