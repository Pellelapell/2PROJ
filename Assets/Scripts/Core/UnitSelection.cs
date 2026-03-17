using UnityEngine;

namespace SupKonQuest
{
    public class UnitSelection : MonoBehaviour
    {
        public Camera mainCamera;

        private UnitMovement selectedUnit;
        private Camp selectedCamp;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleLeftClick();
            }

            if (Input.GetMouseButtonDown(1))
            {
                MoveSelectedUnit();
            }
        }

        private void HandleLeftClick()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ~0, QueryTriggerInteraction.Collide))
            {
                Debug.Log("Hit: " + hit.collider.name);

                UnitMovement unit = hit.collider.GetComponent<UnitMovement>();
                if (unit != null)
                {
                    selectedUnit = unit;
                    selectedCamp = null;
                    Debug.Log("Unit selected");
                    return;
                }

                Camp camp = hit.collider.GetComponent<Camp>();
                if (camp != null)
                {
                    selectedCamp = camp;
                    selectedUnit = null;

                    Debug.Log("Camp selected: " + camp.name);

                    CampUIManager ui = FindFirstObjectByType<CampUIManager>();
                    if (ui != null)
                    {
                        ui.SelectCamp(camp);
                    }

                    return;
                }
            }
        }

        private void MoveSelectedUnit()
        {
            if (selectedUnit == null) return;

            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedUnit.MoveTo(hit.point);
            }
        }
    }
}