namespace FlowerDeer.InputSystem
{
    public interface IInputHandle
    {
        void OnInputKey(KeyType keyType);
        void OnInputKeyDown(KeyType keyType);
        void OnInputKeyUp(KeyType keyType);
    }
}