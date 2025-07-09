#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PictureEditor : EditorWindow
{
    public Texture2D texture2D;

    [MenuItem("Test/PngAlphaClearColor")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(PictureEditor));
    }

    private void OnGUI()
    {
        texture2D = EditorGUILayout.ObjectField(texture2D, typeof(Texture2D), false) as Texture2D;
        if (GUILayout.Button("Test"))
        {
            Test();
        }
    }

    public void Test()
    {
        string path = AssetDatabase.GetAssetPath(texture2D);
        TextureImporter ti = (TextureImporter)TextureImporter.GetAtPath(path);
        ti.isReadable = true;
        AssetDatabase.ImportAsset(path);
        for (int i = 0; i < texture2D.width; ++i)
        {
            for (int j = 0; j < texture2D.height; ++j)
            {
                Color color = texture2D.GetPixel(i, j);
                if (color.a == 0)
                {
                    texture2D.SetPixel(i, j, new Color(1, 1, 1, 0));
                }
            }
        }

        System.IO.File.WriteAllBytes(path, texture2D.EncodeToPNG());
        ti.isReadable = false;
        AssetDatabase.ImportAsset(path);
        AssetDatabase.Refresh();
    }
}

#endif