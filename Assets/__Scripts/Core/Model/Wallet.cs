using System;

namespace Model
{
    public class Wallet
    {
        private static float _money;

        public static float Money => _money;

        public static event Action<float> OnMoneyChanged;

        public Wallet(float startMoney)
        {
            _money = startMoney;

            OnMoneyChanged?.Invoke(_money);
        }

        public static void AddMoney(float amount)
        {
            _money += amount;

            OnMoneyChanged?.Invoke(_money);
        }

        public static void RemoveMoney(float amount)
        {
            _money -= amount;
            if (_money < 0)
                _money = 0;

            OnMoneyChanged?.Invoke(_money);
        }
    }
}
