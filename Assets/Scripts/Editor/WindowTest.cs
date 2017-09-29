using UnityEngine;
using UnityEditor;

public class WindowTest : EditorWindow {

    string myString = "Exemplo";
    bool groupEnabled;
    bool subgroupEnabled;
    bool myBool = true;
    int myIntSliderValue = 0;
    float myFloat = 1.23f;

    // Add menu named "My Window" to the Window menu
    [MenuItem("Window/EditorTests/My Window")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        WindowTest window = (WindowTest)EditorWindow.GetWindow(typeof(WindowTest));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Polígono", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Nome", myString);
        
        groupEnabled = EditorGUILayout.BeginToggleGroup("Inimigo", groupEnabled);
        myIntSliderValue = EditorGUILayout.IntSlider("Tipo", myIntSliderValue, 0, 10);
        subgroupEnabled = EditorGUILayout.BeginToggleGroup("Dificuldade Padrão", subgroupEnabled);
        myFloat = EditorGUILayout.Slider("Dificuldade", myFloat, 0, 1);
        EditorGUILayout.EndToggleGroup();
        
        if (GUILayout.Button("Aplicar"))
        {
            Debug.Log("Aplicado");
            GameObject x = new GameObject(myString);
            Undo.RegisterCreatedObjectUndo(x, myString);
        }
        EditorGUILayout.EndToggleGroup();
        
    }
}
