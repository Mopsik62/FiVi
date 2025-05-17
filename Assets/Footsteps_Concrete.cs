using UnityEngine;
using AK.Wwise;
public class Footsteps_Concrete : MonoBehaviour
{
    public AK.Wwise.Event footstepConcreteEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        footstepConcreteEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
