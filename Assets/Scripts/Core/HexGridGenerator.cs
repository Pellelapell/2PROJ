using UnityEngine;
using Unity.AI.Navigation;

public class HexGridGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] hexPrefabs;

    [Header("Grid Size")]
    public int width = 8;
    public int height = 8;

    [Header("Randomness")]
    public int seed = 0;

    [Header("Scale")]
    [Tooltip("Facteur de réduction de la taille des hexagones")]
    public float hexScale = 0.5f; // 0.5 = moitié de la taille originale

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
        float hexWidth = r.bounds.size.x * hexScale;
        float hexDepth = r.bounds.size.z * hexScale;
        Destroy(temp);

        float colSpacing = hexWidth * 0.75f;
        float rowSpacing = hexDepth * 1f;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float xPos = x * colSpacing;
                float zOffset = (x % 2 == 1) ? rowSpacing * 0.5f : 0f;
                float zPos = z * rowSpacing + zOffset;

                GameObject prefab = hexPrefabs[Random.Range(0, hexPrefabs.Length)];
                GameObject instance = Instantiate(prefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);

                // applique le scale pour réduire la taille
                instance.transform.localScale = Vector3.one * hexScale;

                AssignArea(instance);
            }
        }

        BuildNavMesh();
    }

    void AssignArea(GameObject hex)
    {
        NavMeshModifier modifier = hex.AddComponent<NavMeshModifier>();
        modifier.overrideArea = true;

        int rand = Random.Range(0, 3);

        if (rand == 0)
            modifier.area = UnityEngine.AI.NavMesh.GetAreaFromName("Walkable");
        else if (rand == 1)
            modifier.area = UnityEngine.AI.NavMesh.GetAreaFromName("Not Walkable");
        else
            modifier.area = UnityEngine.AI.NavMesh.GetAreaFromName("Jump");
    }

    void BuildNavMesh()
    {
        NavMeshSurface surface = gameObject.AddComponent<NavMeshSurface>();
        surface.collectObjects = CollectObjects.Children;
        surface.BuildNavMesh();
    }
}