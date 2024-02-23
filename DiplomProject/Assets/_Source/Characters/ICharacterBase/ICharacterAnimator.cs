namespace RogueHelper.Characters.ICharacterBase
{
    public interface ICharacterAnimator
    {
        public void SetShoot(bool isShooting);
        public void SetWalk(float speed);
    }
}