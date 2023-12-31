using System.Collections.Generic;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;

namespace RedPanda.Project.Services
{
    public sealed class PromoService : IPromoService, IPromoPurchaseService
    {
        private readonly IUserService _userService;
        private List<IPromoModel> _promos = new();
        
        public PromoService(IUserService userService)
        {
            _userService = userService;
            
            var data = new List<PromoData>()
            {
                new ("Common \nchest", PromoType.Chest, PromoRarity.Common, 10),
                new ("Rare \nchest", PromoType.Chest, PromoRarity.Rare, 30),
                new ("Epic \nchest", PromoType.Chest, PromoRarity.Epic, 100),
                new ("Сommon \ninapp", PromoType.InApp, PromoRarity.Common, 15),
                new ("Сommon \ninapp", PromoType.InApp, PromoRarity.Common, 25),
                new ("Rare \ninapp", PromoType.InApp, PromoRarity.Rare, 65),
                new ("Common \nspec", PromoType.Special, PromoRarity.Common, 25),
                new ("Rare \nspec", PromoType.Special, PromoRarity.Rare, 100),
                new ("Common \nspec", PromoType.Special, PromoRarity.Common, 35),
                new ("Epic \nspec", PromoType.Special, PromoRarity.Epic, 40)
            };

            foreach (var promoData in data)
            {
                _promos.Add(new PromoModel<PromoData>(promoData));
            }
        }
        
        IReadOnlyList<IPromoModel> IPromoService.GetPromos()
        {
            return _promos;
        }

        void IPromoPurchaseService.BuyPromo(IPromoModel promo)
        {
            if (!_userService.HasCurrency(promo.Cost))
            {
                Debug.LogError($"Not enough currency to purchase: {promo.Title}");
                return;
            }
            
            _userService.ReduceCurrency(promo.Cost);
            Debug.Log($"Purchase successful: {promo.Title}");
        }
    }
}
