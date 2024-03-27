using UnityEngine;

namespace RogueHelper.Enemies.IEnemyBase
{
    public interface IEnemyController
    {
        public void TakeDamage(float damage);
        public void MoveToCharacter(Transform characterTransform);
        public void StopMove();
    }
}