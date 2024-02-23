using System.Collections;

namespace RogueHelper.Characters.ICharacterBase
{
    public interface ICharacterController
    {
        public void Rotate();
        public void Move(float x, float y);
        public IEnumerator ShootDelay();
        public void ShootAnimate(bool isShoot);
    }
}