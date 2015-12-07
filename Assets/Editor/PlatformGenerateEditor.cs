using UnityEditor;
using UnityEngine;
using System.Collections;

public class PlatformGenerateEditor : EditorWindow
{
    private static EditorWindow _window;
    private static PlatformGenerator _platformGenerator;

    private int _height;
    private int _width;
    private int _layer = -1;
    private string _name = "Platform";
    private Vector3 _position = new Vector3(0, 0, 0);

    [MenuItem("PlatformGenerator/Show Window %i")]
    public static void ShowWindow()
    {

        _window = EditorWindow.GetWindow(typeof(PlatformGenerateEditor));
        _platformGenerator = new PlatformGenerator();
        _window.title = "GeneratePlatform";
    }

    private void OnGUI()
    {
        GUILayout.Label("This will create a new platform with height " + "\n" + "and width in tiles.");
        _height = EditorGUILayout.IntField("height: ", _height);
        _width = EditorGUILayout.IntField("width: ", _width);
        _layer = EditorGUILayout.IntField("Layer: ", _layer);
        _name = EditorGUILayout.TextField("Name: ", _name);
        _position = EditorGUILayout.Vector3Field("position: ",_position);

        if (GUILayout.Button("Generate Platform", GUILayout.Width(250)))
        {
            if (_platformGenerator == null)
                _platformGenerator = new PlatformGenerator();

            _platformGenerator.GeneratePlatform(_width, _height, _position, _layer, _name);
        }
    }
}
