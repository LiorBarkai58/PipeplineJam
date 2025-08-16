using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class ScaleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float bigScale = 1.2f;
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(bigScale, 0.2f).SetEase(Ease.OutQuad);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(1f, 0.2f).SetEase(Ease.OutQuad);
        }
    }
}