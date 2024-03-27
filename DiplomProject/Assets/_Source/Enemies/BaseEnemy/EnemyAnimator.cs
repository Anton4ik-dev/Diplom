using RogueHelper.Enemies.IEnemyBase;
using UnityEngine;

namespace RogueHelper.Enemies.BaseEnemy
{
    public class EnemyAnimator : IEnemyAnimator
    {
        private readonly int _speedHash = Animator.StringToHash("Speed");
        private readonly int _deadHash = Animator.StringToHash("IsDead");

        private readonly Animator _characterAnimator;

        public EnemyAnimator(Animator characterAnimator)
        {
            _characterAnimator = characterAnimator;
        }

        public void SetWalk(float x)
        {
            _characterAnimator.SetFloat(_speedHash, x);
        }

        public void SetDead()
        {
            _characterAnimator.SetTrigger(_deadHash);
        }
    }
}