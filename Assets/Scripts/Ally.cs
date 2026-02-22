using UnityEngine;

public class Ally : BaseShip
{
    private void Start()
    {
        Health = 100;
    }

    protected override void Die()
    {
        base.Die();
    }
}
