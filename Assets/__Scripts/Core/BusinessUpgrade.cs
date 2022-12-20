using UnityEngine;

[System.Serializable]
public struct BusinessUpgrade
{
    [SerializeField] private string _name;
    [SerializeField] private float _cost;
    [SerializeField] private float _profitMult;

    public float Cost => _cost;
    public float ProfitMult => _profitMult;
    public string Name => _name;
}
