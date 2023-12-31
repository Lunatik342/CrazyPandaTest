using System.Collections.Generic;
using Grace.DependencyInjection.Attributes;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

namespace RedPanda.Project.UI.PromoView
{
    public class PromoSection : MonoBehaviour
    {
        [SerializeField] private Transform _promosParent;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private ScrollConflictManager _scrollRect;

        private IGameObjectFactory _gameObjectFactory;
        
        [Import]
        public void Inject(IGameObjectFactory gameObjectFactory)
        {
            _gameObjectFactory = gameObjectFactory;
        }

        public void Initialize(IEnumerable<IPromoModel> promoModels, PromoSectionParameters parameters, ScrollRect parentScrollRect)
        {
            _scrollRect.SetParentScrollRect(parentScrollRect);
            _title.text = parameters.Title;
            SpawnPromoViews(promoModels, parameters.Prefab);
        }

        private void SpawnPromoViews(IEnumerable<IPromoModel> promoModels, PromoItemView promoViewPrefab)
        {
            int itemsCount = 0;
            
            foreach (var promoModel in promoModels)
            {
                var promoView = _gameObjectFactory.Create(promoViewPrefab, _promosParent);
                promoView.Initialize(promoModel);
                itemsCount++;
            }

            SetInvisibleIfEmpty(itemsCount);
        }

        private void SetInvisibleIfEmpty(int itemsCount)
        {
            if (itemsCount == 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
