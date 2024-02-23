using RogueHelper.Characters.ICharacterBase;
using UnityEngine;

namespace RogueHelper.Characters.ETGCharacterController
{
    public class ETGInputListener : MonoBehaviour, IInputListener
    {
        private ICharacterController _characterController;
        private Coroutine _activeCoroutine;

        public void Initialize(ICharacterController characterActions)
        {
            _characterController = characterActions;
        }

        private void Update()
        {
            _characterController.Rotate();
            Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetMouseButtonDown(0))
            {
                StartShoot();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                StopShoot();
            }
        }

        private void StartShoot()
        {
            _activeCoroutine = StartCoroutine(_characterController.ShootDelay());
        }

        private void StopShoot()
        {
            StopCoroutine(_activeCoroutine);
            _characterController.ShootAnimate(false);
        }

        private void Move(float x, float y)
        {
            _characterController.Move(x, y);
        }
    }
}