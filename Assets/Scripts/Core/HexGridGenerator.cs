using UnityEngine;

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

        // Mesure automatique basée sur le premier prefab
        GameObject temp = Instantiate(hexPrefabs[0]);
        Renderer r = temp.GetComponentInChildren<Renderer>();
        float hexWidth = r.bounds.size.x;
        float hexDepth = r.bounds.size.z;
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
                Instantiate(prefab, new Vector3(xPos, 0, zPos), Quaternion.identity, transform);
            }
        }
    }
}