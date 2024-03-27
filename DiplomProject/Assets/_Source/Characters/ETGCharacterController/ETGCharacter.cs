using RogueHelper.Characters.ICharacterBase;
using RogueHelper.Services;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RogueHelper.Characters.ETGCharacterController
{
    public class ETGCharacter : MonoBehaviour, ICharacter
    {
        [Header("ShootingSettings")]
        [SerializeField] private float _shootDelay;
        [SerializeField] private bool _autoExpand;
        [SerializeField] private int _poolLength;
        [SerializeField] private ETGBullet _bullet;
        [SerializeField] private Transform _shootPoint;

        [Header("CharacterSettings")]
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _maxHitPoints;
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private Text _hitPointsText;
        [SerializeField] private LayerMask _teleportLayer;

        private float _hitPoints;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (LayerService.CheckLayersEquality(collision.gameObject.layer, _teleportLayer))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }

        public void Initialize(IInputListener etgInputListener)
        {
            _hitPoints = _maxHitPoints;
            ChangeHitPointsText();
            etgInputListener.Initialize(new ETGCharacterController(_shootDelay, _bullet, _shootPoint,
                _moveSpeed, _rb, Camera.main, new ETGCharacterAnimator(_animator), _autoExpand, _poolLength));
        }

        private void ChangeHitPointsText()
        {
            _hitPointsText.text = $"HP: {_hitPoints}/{_maxHitPoints}";
        }

        private void CheckDeath()
        {
            if(_hitPoints <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void TakeDamage(float damage)
        {
            _hitPoints -= damage;
            ChangeHitPointsText();
            CheckDeath();
        }
    }
}