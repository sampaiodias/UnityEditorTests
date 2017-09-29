using UnityEngine;
using UnityEditor;

public class CreateGameObject : EditorWindow {

    bool addScript;
    int value;

    bool specialMaterial;
    Color color;

    string prefabPath = "CapsulePrefabExample";

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

        specialMaterial = EditorGUILayout.BeginToggleGroup("New Material?", specialMaterial);
        shader = EditorGUILayout.TextField("Shader Name ", shader);
        color = EditorGUILayout.ColorField("Object Color ", color);
        EditorGUILayout.EndToggleGroup();
        GUILayout.Space(10);

        addScript = EditorGUILayout.BeginToggleGroup("Add SimpleScript.cs?", addScript);
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
        GUILayout.Label("Press the button below with the prefab selected", EditorStyles.label);
        //prefabPath = EditorGUILayout.TextField("Prefab Path ", prefabPath);
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
        GameObject newObject;

        switch (type)
        {
            default:
                newObject = new GameObject();
                break;
            case ("Cube"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case ("Capsule"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                break;
            case ("Sphere"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            case ("Cylinder"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                break;
            case ("Plane"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
                break;
            case ("Quad"):
                newObject = GameObject.CreatePrimitive(PrimitiveType.Quad);
                break;
            case ("Prefab"):
                //Object prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Resources/CapsulePrefabExample", typeof(GameObject));
                //newObject = (GameObject)Instantiate(Resources.Load(prefabPath));

                newObject = (GameObject)PrefabUtility.InstantiatePrefab(Selection.activeObject as GameObject);
                break;
        }

        newObject.name = myName;

        if (addScript)
        {
            newObject.AddComponent<SimpleScript>();
            SimpleScript script = newObject.GetComponent<SimpleScript>();
            script.myValue = value;
        }

        if (specialMaterial)
        {
            Material material = new Material(Shader.Find(shader));
            AssetDatabase.CreateAsset(material, "Assets/Materials/" + myName + ".mat");          

            Renderer renderer = newObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = material;
                renderer.sharedMaterial.color = color;
            }
        }

        Selection.activeObject = newObject;

        Debug.Log("Object created!");
    }
}
