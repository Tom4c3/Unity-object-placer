using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class CircleObjectPlacer : EditorWindow
{
    GameObject objectToPlace;
    float radius = 5f;
    int objectCount = 10;
    bool faceCenter = true;
    List<GameObject> placedObjects = new List<GameObject>();

    [MenuItem("Tools/Circle Object Placer")]
    public static void ShowWindow()
    {
        GetWindow<CircleObjectPlacer>("Circle Object Placer");
    }

    void OnGUI()
    {
        GUILayout.Label("Place Objects in a Circle", EditorStyles.boldLabel);

        objectToPlace = (GameObject)EditorGUILayout.ObjectField("Object to Place", objectToPlace, typeof(GameObject), true);
        radius = EditorGUILayout.FloatField("Radius", radius);
        objectCount = EditorGUILayout.IntField("Object Count", objectCount);
        faceCenter = EditorGUILayout.Toggle("Face Center", faceCenter);

        if (GUILayout.Button("Place Objects"))
        {
            Undo.RegisterCompleteObjectUndo(this, "Circle Object Placer Change");
            ClearObjects();
            PlaceObjects();
        }

        if (GUILayout.Button("Clear Placed Objects"))
        {
            Undo.RegisterCompleteObjectUndo(this, "Circle Object Placer Clear");
            ClearObjects();
        }
    }

    void PlaceObjects()
    {
        for (int i = 0; i < objectCount; i++)
        {
            float angle = i * Mathf.PI * 2 / objectCount;
            Vector3 position = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
            GameObject newObj = Instantiate(objectToPlace, position, Quaternion.identity);
            placedObjects.Add(newObj);

            if (faceCenter)
            {
                newObj.transform.LookAt(Vector3.zero);
            }
        }
    }

    void ClearObjects()
    {
        foreach (var obj in placedObjects)
        {
            if (obj != null)
            {
                DestroyImmediate(obj);
            }
        }
        placedObjects.Clear();
    }
}
