using UnityEngine;
using Unity.AI.Navigation;
using System.Collections.Generic;

public class HexGridGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] hexPrefabs;

    [Header("Grid Size")]
    public int width = 8;
    public int height = 8;

    [Header("Randomness")]
    public int seed = 0;

    void Start()
    {
        if (hexPrefabs == null || hexPrefabs.Length == 0)
        {
            Debug.LogError("Aucun prefab hexagonal assigné !");
            return;
        }

        if (seed != 0)
            Random.InitState(seed);

        GameObject temp = Instantiate(hexPrefabs[0]);
        Renderer r = temp.GetComponentInChildren<Renderer>();
        float hexWidth = r.bounds.size.x;
        float hexDepth = r.bounds.size.z;
        Destroy(temp);

        float colSpacing = hexWidth * 0.98f;
        float rowSpacing = hexDepth * 0.98f;

        // Stocke les instances ET leurs meshFilters
        List<GameObject> instances = new List<GameObject>();
        List<MeshFilter> meshFilters = new List<MeshFilter>();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float xPos = x * colSpacing;
                float zOffset = (x % 2 == 1) ? rowSpacing * 0.5f : 0f;
                float zPos = z * rowSpacing + zOffset;

                GameObject prefab = hexPrefabs[Random.Range(0, hexPrefabs.Length)];
                GameObject instance = Instantiate(prefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);

                MeshFilter mf = instance.GetComponentInChildren<MeshFilter>();
                if (mf != null)
                {
                    meshFilters.Add(mf);
                    instances.Add(instance);
                }
            }
        }

        CombineMeshes(meshFilters, instances);
    }

    void CombineMeshes(List<MeshFilter> meshFilters, List<GameObject> instances)
    {
        // Crée le mesh combiné
        GameObject combined = new GameObject("HexMap_Combined");
        MeshFilter combinedMF = combined.AddComponent<MeshFilter>();
        MeshRenderer combinedMR = combined.AddComponent<MeshRenderer>();

        // Récupère le matériau AVANT de détruire quoi que ce soit
        Material mat = meshFilters[0].GetComponent<MeshRenderer>() != null
            ? meshFilters[0].GetComponent<MeshRenderer>().sharedMaterial
            : meshFilters[0].GetComponentInParent<MeshRenderer>().sharedMaterial;

        combinedMR.material = mat;

        // Combine les meshes
        CombineInstance[] combine = new CombineInstance[meshFilters.Count];
        for (int i = 0; i < meshFilters.Count; i++)
        {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        }

        Mesh finalMesh = new Mesh();
        finalMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        finalMesh.CombineMeshes(combine, true, true);
        combinedMF.mesh = finalMesh;

        // Ajoute le collider
        MeshCollider mc = combined.AddComponent<MeshCollider>();
        mc.sharedMesh = finalMesh;

        // Supprime les instances originales APRES la combinaison
        foreach (GameObject go in instances)
            Destroy(go);

       // NavMesh
        NavMeshSurface surface = combined.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Volume;
        surface.BuildNavMesh();

        Debug.Log("HexMap combinée et NavMesh généré !");
    }
}