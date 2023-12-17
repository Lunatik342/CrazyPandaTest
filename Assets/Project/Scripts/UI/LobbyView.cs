using RedPanda.Project.UI.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI
{
    public sealed class LobbyView : View
    {
        [SerializeField] private Button _startButton;
        
        private void Awake()
        {
            _startButton.onClick.AddListener(OpenPromoView);
        }

        private void OpenPromoView()
        {
            UIService.Show(ViewsNames.PromoView);
        }
    }
}