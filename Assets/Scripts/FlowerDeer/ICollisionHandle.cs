namespace FlowerDeer
{
    interface ICollisionHandle
    {
        void OnTouchedDamageObject(float value);
        void OnTouchedGround();
    }
}