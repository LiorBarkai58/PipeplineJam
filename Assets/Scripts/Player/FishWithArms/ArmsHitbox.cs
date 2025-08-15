using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.FishWithArms
{
    public class ArmsHitbox : MonoBehaviour
    {
        [SerializeField] private Collider2D hitbox;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(Tags.Enemy))
            {
                Debug.Log("hit");
                Destroy(other.gameObject);
            }
        }
    }
}