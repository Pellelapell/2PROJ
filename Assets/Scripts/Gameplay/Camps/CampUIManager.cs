using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SupKonQuest
{
    public class CampUIManager : MonoBehaviour
    {
        [Header("Panel")]
        public GameObject panel;
        public TMP_Text titleText;

        [Header("Buttons")]
        public Button infantryButton;
        public Button rangeButton;
        public Button heavyButton;
        public Button transportButton;
        public Button frigateButton;
        public Button destroyerButton;

        private Camp selectedCamp;
        private CampProduction selectedProduction;

        private void Start()
        {
            HideUI();

            infantryButton.onClick.AddListener(() => Produce(UnitType.Infantry));
            rangeButton.onClick.AddListener(() => Produce(UnitType.Range));
            heavyButton.onClick.AddListener(() => Produce(UnitType.Heavy));
            transportButton.onClick.AddListener(() => Produce(UnitType.Transport));
            frigateButton.onClick.AddListener(() => Produce(UnitType.Frigate));
            destroyerButton.onClick.AddListener(() => Produce(UnitType.Destroyer));
        }

        public void SelectCamp(Camp camp)
        {
            selectedCamp = camp;
            selectedProduction = camp.GetComponent<CampProduction>();

            if (selectedCamp == null || selectedProduction == null)
            {
                HideUI();
                return;
            }

            panel.SetActive(true);
            titleText.text = selectedCamp.name + " - " + selectedCamp.campType;

            RefreshButtons();
        }

        public void HideUI()
        {
            selectedCamp = null;
            selectedProduction = null;
            panel.SetActive(false);
        }

        private void RefreshButtons()
        {
            infantryButton.gameObject.SetActive(false);
            rangeButton.gameObject.SetActive(false);
            heavyButton.gameObject.SetActive(false);
            transportButton.gameObject.SetActive(false);
            frigateButton.gameObject.SetActive(false);
            destroyerButton.gameObject.SetActive(false);

            if (selectedCamp == null) return;

            switch (selectedCamp.campType)
            {
                case CampType.Normal:
                    infantryButton.gameObject.SetActive(true);
                    rangeButton.gameObject.SetActive(true);
                    heavyButton.gameObject.SetActive(true);
                    break;

                case CampType.Port:
                    infantryButton.gameObject.SetActive(true);
                    transportButton.gameObject.SetActive(true);
                    frigateButton.gameObject.SetActive(true);
                    destroyerButton.gameObject.SetActive(true);
                    break;

                case CampType.NeutralSpecial:
                    infantryButton.gameObject.SetActive(true);
                    rangeButton.gameObject.SetActive(true);
                    heavyButton.gameObject.SetActive(true);
                    break;
            }
        }

        private void Produce(UnitType type)
        {
            if (selectedProduction == null) return;
            selectedProduction.Produce(type);   
        }
    }
}