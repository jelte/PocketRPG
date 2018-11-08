namespace RPG
{
    public interface IInteractable : Targetable
    {
        void Interact(Character character);
    }
}