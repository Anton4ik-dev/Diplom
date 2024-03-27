using RogueHelper.Characters.ETGCharacterController;
using RogueHelper.Enemies.IEnemyBase;
using RogueHelper.Services;
using System.Collections;
using UnityEngine;

namespace RogueHelper.Enemies.BaseEnemy
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float health;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float damage;
        [SerializeField] private float timeToDamage;

        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Collider2D col;
        [SerializeField] private Animator animator;

        [SerializeField] private LayerMask characterLayer;

        public bool IsDead { get; set; }

        private bool canDamage = true;
        private IEnemyAnimator _enemyAnimator;
        private IEnemyController _enemyController;

        private void Start()
        {
            IsDead = false;
            _enemyAnimator = new EnemyAnimator(animator);
            _enemyController = new EnemyController(health, moveSpeed, transform, rb, col, _enemyAnimator,
                this);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, characterLayer))
                _enemyController.MoveToCharacter(collision.transform);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, characterLayer))
                _enemyController.StopMove();
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, characterLayer))
            {
                if (canDamage)
                    StartCoroutine(DamageCharacter(collision));
            }
        }

        public IEnumerator DamageCharacter(Collision2D collision)
        {
            canDamage = false;
            if (collision.gameObject.TryGetComponent(out ETGCharacter character))
                character.TakeDamage(damage);
            yield return new WaitForSeconds(timeToDamage);
            canDamage = true;
        }

        public void TakeDamage(float damage)
        {
            _enemyController.TakeDamage(damage);
        }
    }
}