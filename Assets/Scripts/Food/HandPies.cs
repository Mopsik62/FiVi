using UnityEngine;

public class HandPies : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Heal(5);
    }
}
