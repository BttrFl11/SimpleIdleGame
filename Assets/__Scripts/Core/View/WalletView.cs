using Model;
using TMPro;
using UnityEngine;

namespace View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _moneyText;

        public void Init(Wallet wallet)
        {
            UpdateUI(Wallet.Money);

            Wallet.OnMoneyChanged += UpdateUI;
        }

        private void OnDisable()
        {
            Wallet.OnMoneyChanged -= UpdateUI;
        }

        private void UpdateUI(float money)
        {
            _moneyText.text = $"{money:0}$";
        }
    }
}