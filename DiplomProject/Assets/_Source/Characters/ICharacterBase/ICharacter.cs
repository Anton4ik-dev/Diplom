namespace RogueHelper.Characters.ICharacterBase
{
    public interface ICharacter
    {
        public void Initialize(IInputListener etgInputListener);
        public void TakeDamage(float damage);
    }
}