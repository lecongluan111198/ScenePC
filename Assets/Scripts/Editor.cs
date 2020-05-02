using UnityEditor;
using UnityEngine;

public class Editor : EditorWindow
{
    private float scale;
    private float speed;
    private float x, y, z;
    Color color;
    [MenuItem("Window/Colorizer")]
   
   public static void ShowWindow()
    {
       GetWindow<Editor>("Colorizer");
    }
    void OnGUI()
    {
        GUILayout.Label("Color the selected object", EditorStyles.boldLabel);
        color = EditorGUILayout.ColorField("Color", color);
        if(GUILayout.Button("COLORIZE"))
        {
            Colorize();
        }
        GUILayout.Label("Position the selected object", EditorStyles.boldLabel);
        //string myString = EditorGUILayout.TextField("Name", myString);
        x = EditorGUILayout.FloatField("x", x);
        y = EditorGUILayout.FloatField("y", y);
        z = EditorGUILayout.FloatField("z", z);
        if (GUILayout.Button("POSITION"))
        {
            Position();
        }
        GUILayout.Label("Rotation the selected object", EditorStyles.boldLabel);
        speed = EditorGUILayout.FloatField("Speed", speed);
        if (GUILayout.Button("ROTATION"))
        {
            Rotation();
        }
        GUILayout.Label("Scale the selected object", EditorStyles.boldLabel);
        scale = EditorGUILayout.FloatField("Scale", scale);
        if (GUILayout.Button("SCALE"))
        {
            Scale();
        }
    }
    void Colorize()
    {
        foreach (GameObject obj in Selection.gameObjects)   
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            Debug.Log(renderer);
            if (renderer != null)
            {
                //renderer.material.color = color;
                renderer.sharedMaterial.color = color;
            }
        }
    }
    void Position()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.position = new Vector3(x, y, z);
        }
    }
    void Rotation()
    {
        foreach(GameObject obj in Selection.gameObjects)
        {
            obj.transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }
    void Scale()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.localScale = Vector3.one * scale;
        }
    }
}