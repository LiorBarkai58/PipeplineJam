using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Audio;
using UnityEngine;

namespace Player.FishWithArms
{
    public class ArmsHitbox : MonoBehaviour
    {
        [SerializeField] private Collider2D hitbox;

        [SerializeField] private AudioClip hitSound;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Enemy))
            {
                Destroy(other.gameObject);
                SfxManager.Instance?.PlaySFX(hitSound, 0.6f);
            }
        }
    }
}