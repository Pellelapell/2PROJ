using UnityEngine;

namespace SupKonQuest
{
    public class CampSelection : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private LayerMask campLayerMask;

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (mainCamera == null) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, campLayerMask))
            {
                Camp camp = hit.collider.GetComponent<Camp>();
                if (camp == null) return;

                Debug.Log("Camp clicked: " + camp.name);

                CampUIManager ui = FindFirstObjectByType<CampUIManager>();
                if (ui != null)
                {
                    ui.SelectCamp(camp);
                }
                else
                {
                    Debug.LogError("CampUIManager not found in scene");
                }
            }
        }
    }
}