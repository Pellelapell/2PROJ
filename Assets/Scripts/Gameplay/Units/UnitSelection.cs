using UnityEngine;

namespace SupKonQuest
{
    public class UnitSelection : MonoBehaviour
    {
        public Camera mainCamera;
        private UnitMovement selectedUnit;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectUnit();
            }

            if (Input.GetMouseButtonDown(1))
            {
                MoveSelectedUnit();
            }
        }

        private void SelectUnit()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                selectedUnit = hit.collider.GetComponent<UnitMovement>();
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