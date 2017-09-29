using UnityEngine;
using UnityEditor;

public class CreateGameObject : EditorWindow {

    bool addScript;
    int value;

    bool specialMaterial;
    Color color;

    string prefabPath = "Prefabs/CapsulePrefabExample.prefab";

    string myName = "NewGameObject";
    string shader = "Standard";

    [MenuItem("Window/EditorTests/GameObject Creator")]
    public static void ShowWindow()
    {
        GetWindow<CreateGameObject>("Obj Creator");
    }

    void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Properties", EditorStyles.boldLabel);
        myName = EditorGUILayout.TextField("Object Name ", myName);
        GUILayout.Space(10);

        specialMaterial = EditorGUILayout.BeginToggleGroup("Change Material?", specialMaterial);
        shader = EditorGUILayout.TextField("Shader Name ", shader);
        color = EditorGUILayout.ColorField("Object Color ", color);
        EditorGUILayout.EndToggleGroup();
        GUILayout.Space(10);

        addScript = EditorGUILayout.BeginToggleGroup("Add SimpleScript?", addScript);
        value = EditorGUILayout.IntSlider("Value ", value, 0, 10);
        EditorGUILayout.EndToggleGroup();

        GUILayout.Space(20);

        GUILayout.Label("Create From Primitive Shape", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Capsule"))
        {
            Create("Capsule");
        }

        if (GUILayout.Button("Cube"))
        {
            Create("Cube");
        }

        if (GUILayout.Button("Sphere"))
        {
            Create("Sphere");
        }
        if (GUILayout.Button("Cylinder"))
        {
            Create("Cylinder");
        }

        if (GUILayout.Button("Plane"))
        {
            Create("Plane");
        }

        if (GUILayout.Button("Quad"))
        {
            Create("Quad");
        }

        GUILayout.EndHorizontal();

        GUILayout.Space(30);
        GUILayout.Label("Instantiate From Prefab", EditorStyles.boldLabel);
        prefabPath = EditorGUILayout.TextField("Prefab Path ", prefabPath);
        GUILayout.Space(10);
        if (GUILayout.Button("Prefab"))
        {
            Create("Prefab");
        }

        GUILayout.Space(30);
        GUILayout.Label("Game Object Creator - By Lucas Sampaio Dias", EditorStyles.centeredGreyMiniLabel);
    }

    void Create(string type)
    {
        GameObject obj;
        switch (type)
        {
            default:
                obj = new GameObject();
                break;
            case ("Cube"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case ("Capsule"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case ("Sphere"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case ("Cylinder"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            case ("Plane"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Plane);
                break;
            case ("Quad"):
                obj = GameObject.CreatePrimitive(PrimitiveType.Quad);
                break;
            case ("Prefab"):
                GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject));
                //obj = (GameObject)Instantiate(prefab);
                obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                break;
        }

        obj.name = myName;

        if (addScript)
        {
            obj.AddComponent<SimpleScript>();
            SimpleScript script = obj.GetComponent<SimpleScript>();
            script.myValue = value;
        }

        if (specialMaterial)
        {
            Material material = new Material(Shader.Find(shader));
            AssetDatabase.CreateAsset(material, "Assets/Materials/" + myName + ".mat");          

            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
                renderer.sharedMaterial.color = color;
            }
        }        
    }
}
