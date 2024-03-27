using RogueHelper.Characters.ICharacterBase;
using RogueHelper.Enemies;
using RogueHelper.Enemies.IEnemyBase;
using RogueHelper.Services;
using System.Collections;
using UnityEngine;

namespace RogueHelper.Characters.ETGCharacterController
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ETGBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _lifeTime;
        [SerializeField] private Rigidbody2D _rb;

        [SerializeField] private LayerMask _characterLayer;
        [SerializeField] private LayerMask _enemyLayer;

        private void Awake()
        {
            if(_rb == null)
                _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rb.velocity = transform.up * _speed;
        }

        private void OnEnable()
        {
            StartCoroutine(LifeTime());
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == gameObject.layer || LayerService.CheckLayersEquality(collision.gameObject.layer, _characterLayer))
                return;

            if(LayerService.CheckLayersEquality(collision.gameObject.layer, _enemyLayer))
            {
                if(collision.gameObject.TryGetComponent(out IEnemy enemy))
                {
                    enemy.TakeDamage(_damage);
                }

            }

            gameObject.SetActive(false);
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(_lifeTime);

            gameObject.SetActive(false);
        }
    }
}