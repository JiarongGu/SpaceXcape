public class MoneyShip: SpaceShip
{
    public override void Destroy()
    {
        if (!WithInBoundary()) {
            base.Destroy();
        }
    }
}
