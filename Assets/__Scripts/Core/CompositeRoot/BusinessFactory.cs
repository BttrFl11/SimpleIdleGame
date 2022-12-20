using UnityEngine;
using View;
using Model;

namespace Composite
{
    public class BusinessFactory : MonoBehaviour
    {
        [SerializeField] private BusinessData[] _availableBusinesses;
        [SerializeField] private Transform _businessPanelParent;
        [SerializeField] private BusinessPanelView _businessPanelPrefab;

        public Business UnlockBusiness(int businessCount)
        {
            if (businessCount == _availableBusinesses.Length)
                return null;

            var business = new Business(_availableBusinesses[businessCount]);

            InitializeBusiness(business);

            return business;
        }

        public void InitializeBusiness(Business business)
        {
            var businessView = Instantiate(_businessPanelPrefab, _businessPanelParent);
            businessView.Init(business);
        }
    }
}