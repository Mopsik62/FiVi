using UnityEngine;

public class Soda : Food
{
    public override void Consume()
    {
        base.Consume();
        Player.instance.Haste(10f);
    }
}
