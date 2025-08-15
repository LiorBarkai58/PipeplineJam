using System.Collections;
using UnityEngine;

namespace Player.FishWithArms
{
    public class FishWithArmsController : CharacterMovement
    {
        [Header("Arms Specific")]
        [SerializeField] private ArmsHitbox armsHitbox;


        protected override void Start()
        {
            base.Start();
            input.Action += StartHit;
            armsHitbox.gameObject.SetActive(false);
        }
        
        private void StartHit()
        {
            StartCoroutine(HitDuration());
        }

        private IEnumerator HitDuration()
        {
            armsHitbox.gameObject.SetActive(true);
            yield return new WaitForEndOfFrame();
            armsHitbox.gameObject.SetActive(false);

        }
    }
}