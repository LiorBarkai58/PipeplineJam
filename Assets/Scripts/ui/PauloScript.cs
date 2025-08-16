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
            MoveToEnd();
        }

        private void MoveToEnd()
        {
            transform.localScale = new Vector3(1, 1, 1);
            paulo.DOMove(targetPos.position, 4f).SetEase(Ease.Linear).OnComplete(MoveToStart);
        }

        private void MoveToStart()
        {
            transform.localScale = new Vector3(-1, 1, 1);
            
            paulo.DOMove(startingPos.position, 4f).SetEase(Ease.Linear).OnComplete(MoveToEnd);
        }
    }
}