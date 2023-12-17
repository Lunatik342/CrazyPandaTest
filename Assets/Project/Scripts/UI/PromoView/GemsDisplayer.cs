using System;
using Grace.DependencyInjection.Attributes;
using RedPanda.Project.Services;
using RedPanda.Project.Services.Interfaces;
using TMPro;
using UnityEngine;

namespace RedPanda.Project.UI.PromoView
{
    public class GemsDisplayer: MonoBehaviour
    {
        [SerializeField] private TMP_Text _gemsCount;

        private IObservableCurrency _observableCurrency;
        
        [Import]
        public void Inject(IObservableCurrency observableCurrency)
        {
            _observableCurrency = observableCurrency;
        }

        private void Start()
        {
            _observableCurrency.CurrencyAmountChanged += DisplayCurrency;
            DisplayCurrency(_observableCurrency.Currency);
        }

        private void DisplayCurrency(int count)
        {
            _gemsCount.text = count.ToString();
        }

        private void OnDestroy()
        {
            _observableCurrency.CurrencyAmountChanged -= DisplayCurrency;
        }
    }
}