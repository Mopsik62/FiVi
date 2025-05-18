using UnityEngine;

public class Skeleton : Fighter
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
