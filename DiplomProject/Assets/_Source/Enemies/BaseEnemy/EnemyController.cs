using RogueHelper.Enemies.IEnemyBase;
using UnityEngine;

namespace RogueHelper.Enemies.BaseEnemy
{
    public class EnemyController : IEnemyController
    {
        private float _health;
        private float _moveSpeed;
        private Transform _enemyTransform;

        private Rigidbody2D _rb;
        private Collider2D _col;
        private IEnemyAnimator _enemyAnimator;
        private Enemy _enemy;

        public EnemyController(float health, float moveSpeed, Transform enemyTransform, 
            Rigidbody2D rb, Collider2D col, IEnemyAnimator enemyAnimator, Enemy enemy)
        {
            _health = health;
            _moveSpeed = moveSpeed;
            _enemyTransform = enemyTransform;
            _rb = rb;
            _col = col;
            _enemyAnimator = enemyAnimator;
            _enemy = enemy;
        }

        private void Rotate(Transform characterTransform)
        {
            Vector2 moveDirection = (Vector2)characterTransform.position - _rb.position;
            float aimAngle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = aimAngle;
        }

        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                _rb.isKinematic = true;
                _col.isTrigger = true;
                _enemyAnimator.SetDead();
                _enemy.IsDead = true;
                GameObject.Destroy(_enemy.gameObject, 1f);
                GameObject.Destroy(_enemy);
            }
        }

        public void MoveToCharacter(Transform characterTransform)
        {
            _enemyTransform.position = Vector2.MoveTowards(_enemyTransform.position,
                characterTransform.position, _moveSpeed * Time.deltaTime);

            Rotate(characterTransform);

            _enemyAnimator.SetWalk(_enemyTransform.position.magnitude);
        }

        public void StopMove()
        {
            _enemyAnimator.SetWalk(0);
        }
    }
}