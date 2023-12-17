using System;
using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.Services
{
    public sealed class UserService : IUserService, IObservableCurrency
    {
        public int Currency { get; private set; }

        public event Action<int> CurrencyAmountChanged;

        public UserService()
        {
            Currency = 1000;
        }

        void IUserService.AddCurrency(int delta)
        {
            Currency += delta;
            CurrencyAmountChanged?.Invoke(Currency);
        }

        void IUserService.ReduceCurrency(int delta)
        {
            Currency -= delta;
            CurrencyAmountChanged?.Invoke(Currency);
        }
        
        bool IUserService.HasCurrency(int amount)
        {
            return Currency >= amount;
        }
    }
}