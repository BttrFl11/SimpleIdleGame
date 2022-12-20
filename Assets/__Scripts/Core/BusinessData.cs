using UnityEngine;

[CreateAssetMenu(menuName = "SO/Business")]
public class BusinessData : ScriptableObject
{
    [SerializeField] private float _profitDelay;
    [SerializeField] private float _baseProfit;
    [SerializeField] private float _baseCost;

    public BusinessUpgrade BusinessUpgrade1;
    public BusinessUpgrade BusinessUpgrade2;

    public float ProfitDelay => _profitDelay;
    public float BaseProfit => _baseProfit;
    public float BaseCost => _baseCost;
}