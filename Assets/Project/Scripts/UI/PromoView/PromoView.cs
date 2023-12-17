using System;
using System.Collections.Generic;
using System.Linq;
using AYellowpaper.SerializedCollections;
using Grace.DependencyInjection.Attributes;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI.PromoView
{
    public class PromoView : View
    {
        [SerializeField] private SerializedDictionary<PromoType, PromoSectionParameters> _sectionsParameters;
        [SerializeField] private Transform _sectionsParent;
        [SerializeField] private PromoSection _sectionPrefab;
        [SerializeField] private ScrollRect _scrollRect;

        private IPromoService _promoService;
        private IGameObjectFactory _gameObjectFactory;
        
        [Import]
        public void Inject(IPromoService promoService, IGameObjectFactory gameObjectFactory)
        {
            _promoService = promoService;
            _gameObjectFactory = gameObjectFactory;
        }
        
        private void Start()
        {
            var allPromos = _promoService.GetPromos();

            foreach (var sectionParameters in _sectionsParameters)
            {
                var promoType = sectionParameters.Key;
                CreatePromoSection(allPromos, promoType, sectionParameters.Value);
            }
        }

        private void CreatePromoSection(IReadOnlyList<IPromoModel> allPromos, PromoType promoType, PromoSectionParameters parameters)
        {
            var promosOfType = GetOrderedPromosOfType(allPromos, promoType);
            var section = _gameObjectFactory.Create(_sectionPrefab, _sectionsParent);
            section.Initialize(promosOfType, parameters, _scrollRect);
        }

        private static IOrderedEnumerable<IPromoModel> GetOrderedPromosOfType(IReadOnlyList<IPromoModel> allPromos, PromoType promoType)
        {
            return allPromos.Where(model => model.Type == promoType).OrderByDescending(model => model.Rarity);
        }
    }

    [Serializable]
    public class PromoSectionParameters
    {
        [field: SerializeField] public PromoItemView Prefab { get; private set; }
        [field: SerializeField] public string Title { get; private set; }
    }
}
