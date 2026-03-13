using UnityEngine;

public class HexGridGenerator : MonoBehaviour
{
    public GameObject hexPrefab;

    public int width = 8;
    public int height = 8;

    void Start()
    {
        Renderer r = hexPrefab.GetComponentInChildren<Renderer>();
        float hexWidth = r.bounds.size.x;
        float hexHeight = r.bounds.size.z;

        // Pour flat-top : espacement horizontal = 3/4 de la largeur
        float colSpacing = hexWidth * 0.75f;

        // Espacement vertical = hauteur complète
        float rowSpacing = hexHeight;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                float xPos = x * colSpacing;

                // Décalage vertical pour les colonnes impaires
                float zOffset = (x % 2 == 1) ? rowSpacing * 0.5f : 0f;
                float zPos = z * rowSpacing + zOffset;

                Instantiate(
                    hexPrefab,
                    new Vector3(xPos, 0, zPos),
                    Quaternion.identity,
                    transform
                );
            }
        }
    }
}