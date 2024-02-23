using RogueHelper.Characters.ICharacterBase;
using UnityEngine;

namespace RogueHelper.Characters.ETGCharacterController
{
    public class ETGCharacterAnimator : ICharacterAnimator
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _shootHash = Animator.StringToHash("IsShooting");

        private readonly Animator _characterAnimator;

        public ETGCharacterAnimator(Animator characterAnimator)
        {
            _characterAnimator = characterAnimator;
        }

        public void SetWalk(float x)
        {
            _characterAnimator.SetFloat(_speedHash, x);
        }

        public void SetShoot(bool isShooting)
        {
            _characterAnimator.SetBool(_shootHash, isShooting);
        }
    }
}