using Model;
using UnityEngine;

namespace View
{
    public class BusinessPanelActions : MonoBehaviour
    {
        private BusinessPanelView _businessView;

        private void Awake()
        {
            _businessView = GetComponent<BusinessPanelView>();
        }

        public void OnUpgradeButton()
        {
            if (_businessView.Business.GetCost() > Wallet.Money)
                return;

            _businessView.Business.Upgrade();
        }

        public void OnPurchaseBusinessUpgradeButton1()
        {
            if (_businessView.Business.BusinessData.BusinessUpgrade1.Cost > Wallet.Money)
                return;

            _businessView.Business.PurchaseUpgrade1();
            _businessView.UpdateUpgradeButtons();

            _businessView.UpgradeButton1.interactable = false;
        }

        public void OnPurchaseBusinessUpgradeButton2()
        {
            if (_businessView.Business.BusinessData.BusinessUpgrade2.Cost > Wallet.Money)
                return;

            _businessView.Business.PurchaseUpgrade2();
            _businessView.UpdateUpgradeButtons();

            _businessView.UpgradeButton2.interactable = false;
        }
    }
}