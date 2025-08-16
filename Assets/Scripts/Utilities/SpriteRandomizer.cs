using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public class SpriteRandomizer : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        [SerializeField] private List<Sprite> sprites;

        private void Start()
        {
            spriteRenderer.sprite = sprites[UnityEngine.Random.Range(0, sprites.Count)];
        }
    }
}