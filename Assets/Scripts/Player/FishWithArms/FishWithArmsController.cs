using System.Collections;
using UnityEngine;

namespace Player.FishWithArms
{
    public class FishWithArmsController : CharacterMovement
    {
        [Header("Arms Specific")]
        [SerializeField] private ArmsHitbox armsHitbox;

        [SerializeField] private float hitCooldown = 0.5f;
        private bool _canAttack = true;
        protected override void Start()
        {
            base.Start();
            input.Action += StartHit;
            armsHitbox.gameObject.SetActive(false);
        }
        
        private void StartHit()
        {
            if (_canAttack && _inputEnabled)
            {
                StartCoroutine(HitDuration());
            }
        }

        private IEnumerator HitDuration()
        {
            _canAttack = false;
            rb.linearVelocityY = 3;
            armsHitbox.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            armsHitbox.gameObject.SetActive(false);
            yield return new WaitForSeconds(hitCooldown);
            _canAttack = true;

        }
    }
}