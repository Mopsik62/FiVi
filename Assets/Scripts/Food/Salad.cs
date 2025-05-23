using UnityEngine;

public class Salad : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Heal(10);
    }
}
