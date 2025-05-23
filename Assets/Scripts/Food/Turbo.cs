using UnityEngine;

public class Turbo : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Haste(20f);
    }
}
