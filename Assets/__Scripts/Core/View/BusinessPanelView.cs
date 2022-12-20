using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class BusinessPanelView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _businessName;
        [SerializeField] private TextMeshProUGUI _businessLevel;
        [SerializeField] private TextMeshProUGUI _businessIncome;
        [SerializeField] private TextMeshProUGUI _upgradeText;
        [SerializeField] private TextMeshProUGUI _oneTimeUpgradeText1;
        [SerializeField] private TextMeshProUGUI _oneTimeUpgradeText2;
        [SerializeField] private Image _businessScaleImage;
        [SerializeField] private Button _upgradeButton1;
        [SerializeField] private Button _upgradeButton2;
        [SerializeField] private Button _upgradeButton;



        private Business _business;
        public Business Business => _business;

        public Button UpgradeButton1 => _upgradeButton1;
        public Button UpgradeButton2 => _upgradeButton2;
        public Button UpgradeButton => _upgradeButton;

        public void Init(Business business)
        {
            _business = business;

            _business.OnUpgrade += UpdateUI;
            _business.OnUnlock += UpdateUI;
            Wallet.OnMoneyChanged += UpdateButtonInteractable;

            UpdateUI();
            UpdateButtonInteractable(Wallet.Money);
        }

        private void OnDisable()
        {
            _business.OnUpgrade -= UpdateUI;
            _business.OnUnlock -= UpdateUI;
            Wallet.OnMoneyChanged -= UpdateButtonInteractable;
        }

        private void Update()
        {
            if (_business == null)
                return;

            UpdateScaleImage();
        }

        private void UpdateScaleImage()
        {
            _businessScaleImage.fillAmount = _business.FillAmount;
        }

        private void UpdateButtonInteractable(float currentMoney)
        {
            _upgradeButton.interactable = currentMoney >= Business.GetCost();
            _upgradeButton1.interactable = currentMoney >= Business.BusinessData.BusinessUpgrade1.Cost && Business.Upgraded1 == false;
            _upgradeButton2.interactable = currentMoney >= Business.BusinessData.BusinessUpgrade2.Cost && Business.Upgraded2 == false;
        }

        public void UpdateUpgradeButtons()
        {
            var upgrade1 = Business.BusinessData.BusinessUpgrade1;
            var upgrade2 = Business.BusinessData.BusinessUpgrade2;

            string state1 = Business.Upgraded1 == true ? "Done" : $"Price: {upgrade1.Cost}$";
            string state2 = Business.Upgraded2 == true ? "Done" : $"Price: {upgrade2.Cost}$";
            _oneTimeUpgradeText1.text = $"{upgrade1.Name}" +
                $"\nIncome: + {(upgrade1.ProfitMult * 100):0}%" +
                $"\n{state1}";

            _oneTimeUpgradeText2.text = $"{upgrade2.Name}" +
                $"\nIncome: + {(upgrade2.ProfitMult * 100):0}%" +
                $"\n{state2}";
        }

        private void UpdateUI()
        {
            _businessName.text = Business.BusinessData.name;
            _businessIncome.text = $"Income\n{Business.GetProfit()}$";
            _businessLevel.text = $"LVL\n {Business.Level}";
            _upgradeText.text = $"Upgrade\nPrice: {Business.GetCost()}$";

            UpdateUpgradeButtons();
        }
    }
}