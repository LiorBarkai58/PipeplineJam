using System;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PauloScript : MonoBehaviour
    {
        [SerializeField] private RectTransform targetPos;
        [SerializeField] private RectTransform startingPos;

        [SerializeField] private RectTransform paulo;
        private void Start()
        {
            paulo.DOMove(targetPos.position, 4f).SetEase(Ease.Linear);
        }
    }
}