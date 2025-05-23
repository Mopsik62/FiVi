using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public int Price;
    public Image image;

    public virtual void Consume()
    {
       //Destroy(gameObject);
    }
}
