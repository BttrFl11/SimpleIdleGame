using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using View;
using Model;
using Data;

namespace Composite
{
    public class BusinessCompositeRoot : CompositeRoot, IData
    {
        [SerializeField] private float _startMoney;
        [SerializeField] private BusinessFactory _businessFactory;
        [SerializeField] private WalletView _walletView;

        private List<Business> _businesses = new();
        private Wallet _wallet;

        private IEnumerable<Business> Businesses => _businesses;

        public static Action OnBusinessPurchase;
        public static Action<float> OnProfit;

        private void OnEnable()
        {
            OnBusinessPurchase += UnlockBusiness;
            OnProfit += Wallet.AddMoney;
        }

        private void OnDisable()
        {
            OnBusinessPurchase -= UnlockBusiness;
            OnProfit -= Wallet.AddMoney;
        }

        public override void Compose() { }

        public void NewGame()
        {
            UnlockBusiness();

            _wallet = new Wallet(_startMoney);
            _walletView.Init(_wallet);
        }

        private void UnlockBusiness()
        {
            var business = _businessFactory.UnlockBusiness(_businesses.Count);

            if (business == null)
            {
                Debug.Log("All businesses was unlocked!");
                return;
            }

            _businesses.Add(business);
        }

        private void Update()
        {
            foreach (var business in _businesses)
            {
                business.Update(Time.deltaTime);
            }
        }

        public void SaveData(GameData gameData)
        {
            gameData.Money = Wallet.Money;
            gameData.Businesses = Businesses.ToArray();
        }

        public void LoadData(GameData gameData)
        {
            _wallet = new(gameData.Money);
            _walletView.Init(_wallet);
            _businesses = new(gameData.Businesses);

            foreach (var business in _businesses)
            {
                _businessFactory.InitializeBusiness(business);
            }
        }
    }
}