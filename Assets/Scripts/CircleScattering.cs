using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(CircleScattering))]
public class CircleScatteringEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CircleScattering myScript = (CircleScattering)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.DestroyObjects();
            myScript.ScatterObjects();
        }
    }
}
public class CircleScattering : MonoBehaviour
{
    public GameObject Prefab;
    public List<GameObject> InstantiatedPrefabs;
    public int InstantiateNumber;
    public float Distance;
    public Transform Parent;

    public void ScatterObjects()
    {
        if (InstantiatedPrefabs.Count != InstantiateNumber)
        {
            for (int i = InstantiatedPrefabs.Count; i < InstantiateNumber; i++)
            {
                InstantiatedPrefabs.Add(Instantiate(Prefab, Parent));
            }
        }
        for (int i = 0; i < InstantiatedPrefabs.Count; i++)
        {
            InstantiatedPrefabs[i].transform.position = CircleDistribution(i, Distance);
        }
    }

    public void DestroyObjects()
    {
        InstantiatedPrefabs.Clear();
        foreach (Transform child in Parent)
        {
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    private Vector3 CircleDistribution(int index, float space)
    {
        var pos = new Vector3();
        if (index == 0)
            return pos;
        int finall = 0;
        int temp = index;
        while (temp > 0)
        {
            finall++;
            temp -= 6 * finall;
        }
        float angle = (2 * 3.14f) / (6 * finall) * index;
        pos = new Vector3(Mathf.Cos(angle) * space * finall, Mathf.Sin(angle) * space * finall);

        return pos;
    }
}