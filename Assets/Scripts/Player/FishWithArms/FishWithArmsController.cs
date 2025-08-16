using System.Collections;
using UnityEngine;

namespace Player.FishWithArms
{
    public class FishWithArmsController : CharacterMovement
    {
        private static readonly int Smash = Animator.StringToHash("Smash");
        private static readonly int Hit = Animator.StringToHash("Hit");
        [Header("Arms Specific")]
        [SerializeField] private ArmsHitbox armsHitbox;

        [SerializeField] private Animator effectAnimator;
        [SerializeField] private float hitCooldown = 0.5f;
        private bool _canAttack = true;
        protected override void Start()
        {
            base.Start();
            input.Action += StartHit;
            armsHitbox.gameObject.SetActive(false);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            input.Action -= StartHit;
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
            animator.SetTrigger(Smash);
            effectAnimator.SetTrigger(Hit);
            _canAttack = false;
            rb.linearVelocityY = 3;
            yield return new WaitForSeconds(0.3f);
            armsHitbox.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            armsHitbox.gameObject.SetActive(false);
            yield return new WaitForSeconds(hitCooldown);
            _canAttack = true;

        }
    }
}