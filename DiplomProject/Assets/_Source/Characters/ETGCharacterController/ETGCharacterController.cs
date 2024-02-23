using RogueHelper.Characters.ICharacterBase;
using System.Collections;
using UnityEngine;

namespace RogueHelper.Characters.ETGCharacterController
{
    public class ETGCharacterController : ICharacterController
    {
        private float _shootDelay;
        private Transform _shootPoint;
        private ICharacterAnimator _characterAnimator;

        private float _moveSpeed;
        private Rigidbody2D _rb;
        private Camera _mainCamera;

        private BulletPool<ETGBullet> _pool;

        public ETGCharacterController(float shootDelay, 
            ETGBullet bullet, Transform shootPoint, 
            float moveSpeed, Rigidbody2D rb, 
            Camera mainCamera, 
            ETGCharacterAnimator characterAnimator, 
            bool autoExpand,
            int poolLength)
        {
            _shootDelay = shootDelay;
            _shootPoint = shootPoint;
            _moveSpeed = moveSpeed;
            _rb = rb;
            _mainCamera = mainCamera;
            _characterAnimator = characterAnimator;

            _pool = new BulletPool<ETGBullet>(bullet, autoExpand, _shootPoint, poolLength);
        }

        private void Shoot()
        {
            _pool.GetFreeElement();
            ShootAnimate(true);
        }

        public void Rotate()
        {
            Vector2 mousePosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 aimDirection = mousePosition - _rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            _rb.rotation = aimAngle;
        }

        public void Move(float moveX, float moveY)
        {
            Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
            _rb.velocity = new Vector2(moveDirection.x * _moveSpeed, moveDirection.y * _moveSpeed);
            _characterAnimator.SetWalk(_rb.velocity.magnitude);
        }

        public IEnumerator ShootDelay()
        {
            while(true)
            {
                Shoot();
                yield return new WaitForSeconds(_shootDelay);
            }
        }

        public void ShootAnimate(bool isShoot)
        {
            _characterAnimator.SetShoot(isShoot);
        }
    }
}