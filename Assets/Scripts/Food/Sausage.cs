using UnityEngine;

public class Sausage : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Heal(10);
    }
}
