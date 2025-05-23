using UnityEngine;

public class Golden : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Invincible(3f);
    }
}
