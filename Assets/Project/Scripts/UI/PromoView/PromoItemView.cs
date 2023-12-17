using AYellowpaper.SerializedCollections;
using Grace.DependencyInjection.Attributes;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using RedPanda.Project.UI.GenericComponents;
using RedPanda.Project.UI.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI.PromoView
{
    public class PromoItemView : MonoBehaviour
    {
        [SerializeField] private SerializedDictionary<PromoRarity, Sprite> _backgroundImages;
        [SerializeField] private SerializedDictionary<PromoRarity, Color> _topMarkColours;
        [SerializeField] private SerializedDictionary<PromoRarity, Color> _textColours;
        
        [SerializeField] private Image _background;
        [SerializeField] private Image _topMark;
        
        [SerializeField] private DynamicallySetImage _icon;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _buyButton;

        private IPromoPurchaseService _promoPurchaseService;

        private IPromoModel _promoModel;
        
        [Import]
        public void Inject(IPromoPurchaseService promoPurchaseService)
        {
            _promoPurchaseService = promoPurchaseService;
        }

        private void Start()
        {
            _buyButton.onClick.AddListener(BuyPromo);
        }

        public void Initialize(IPromoModel promoModel)
        {
            _promoModel = promoModel;

            SetVisualsBasedOnRarity(promoModel.Rarity);
            SetPromoInformation(promoModel);
        }

        private void SetVisualsBasedOnRarity(PromoRarity rarity)
        {
            _background.sprite = _backgroundImages[rarity];
            _topMark.color = _topMarkColours[rarity];
            _title.color = _textColours[rarity];
        }

        private void SetPromoInformation(IPromoModel promoModel)
        {
            _cost.text = StringFormatter.ToCostString(promoModel.Cost);
            _title.text = promoModel.Title;
            _icon.SetSprite(promoModel.GetIcon());
        }

        private void BuyPromo()
        {
            _promoPurchaseService.BuyPromo(_promoModel);
        }
    }
}
