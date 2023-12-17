using System;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IObservableCurrency
    {
        public int Currency { get; }
        public event Action<int> CurrencyAmountChanged;
    }
}