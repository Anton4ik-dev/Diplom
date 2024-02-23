using RogueHelper.Characters.ICharacterBase;
using UnityEngine;

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
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private Animator _animator;

        public void Initialize(IInputListener etgInputListener)
        {
            etgInputListener.Initialize(new ETGCharacterController(_shootDelay, _bullet, _shootPoint,
                _moveSpeed, _rb, Camera.main, new ETGCharacterAnimator(_animator), _autoExpand, _poolLength));
        }
    }
}