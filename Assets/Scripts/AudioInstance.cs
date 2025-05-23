using UnityEngine;

public class AudioInstance : MonoBehaviour
{
    public static AudioInstance instance;
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
