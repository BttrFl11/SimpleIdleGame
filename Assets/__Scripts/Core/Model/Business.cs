using Composite;
using System;
using UnityEngine;

namespace Model
{
    [System.Serializable]
    public class Business : IBusiness
    {
        [SerializeField] private BusinessData _businessData;
        [SerializeField] private int _level;
        [SerializeField] private float _fillAmount;
        [SerializeField] private float _seconds;

        [SerializeField] private float _upgradeMult1 = 0;
        [SerializeField] private float _upgradeMult2 = 0;
        [SerializeField] private bool _upgraded1;
        [SerializeField] private bool _upgraded2;

        public int Level => _level;
        public float FillAmount => _fillAmount;
        public BusinessData BusinessData => _businessData;

        public bool Upgraded1 => _upgraded1;
        public bool Upgraded2 => _upgraded2;

        public event Action OnUpgrade;
        public event Action OnUnlock;

        public Business(BusinessData businessData)
        {
            _businessData = businessData;

            OnUnlock?.Invoke();
        }

        public float GetProfit()
        {
            return _level * _businessData.BaseProfit * (1 + _upgradeMult1 + _upgradeMult2);
        }

        public float GetCost()
        {
            return (_level + 1) * _businessData.BaseCost;
        }

        public void Update(float deltaTime)
        {
            if (_level == 0)
                return;

            _seconds += deltaTime;
            _fillAmount = _seconds / _businessData.ProfitDelay;

            if (_seconds >= _businessData.ProfitDelay)
            {
                BusinessCompositeRoot.OnProfit?.Invoke(GetProfit());

                _seconds = 0;
                _fillAmount = 0;
            }
        }

        public void PurchaseUpgrade1()
        {
            _upgradeMult1 = _businessData.BusinessUpgrade1.ProfitMult;
            _upgraded1 = true;

            Wallet.RemoveMoney(_businessData.BusinessUpgrade1.Cost);

            OnUpgrade?.Invoke();
        }

        public void PurchaseUpgrade2()
        {
            _upgradeMult2 = _businessData.BusinessUpgrade2.ProfitMult;
            _upgraded2 = true;

            Wallet.RemoveMoney(_businessData.BusinessUpgrade2.Cost);

            OnUpgrade?.Invoke();
        }

        public void Upgrade()
        {
            Wallet.RemoveMoney(GetCost());
            _level++;

            OnUpgrade?.Invoke();

            if (_level == 1)
                BusinessCompositeRoot.OnBusinessPurchase?.Invoke();
        }
    }
}