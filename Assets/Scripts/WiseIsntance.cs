using UnityEngine;

public class WiseIsntance : MonoBehaviour
{
    public static WiseIsntance instance;
    protected void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
