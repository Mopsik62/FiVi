using UnityEngine;

public class Pizza : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Heal(10);
    }
}
