using System.Collections;
using UnityEngine;

namespace RogueHelper.Enemies.IEnemyBase
{
    public interface IEnemy
    {
        public bool IsDead { get; set; }
        public IEnumerator DamageCharacter(Collision2D collision);
        public void TakeDamage(float damage);
    }
}