using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RedPanda.Project.UI.GenericComponents
{
    public class ButtonScaleEffect: MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private float _targetScale = 0.9f;
        [SerializeField] private float _duration = 0.1f;
        [SerializeField] private Ease _ease;
        
        private Tween _tween;

        public void OnPointerClick(PointerEventData eventData)
        {
            _tween?.Kill(true);
            _tween = transform.DOScale(_targetScale, _duration).SetLoops(2, LoopType.Yoyo).SetEase(_ease);
        }

        private void OnDestroy()
        {
            _tween?.Kill();
        }
    }
}