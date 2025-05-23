using UnityEngine;
using UnityEngine.UI;


public class FoodButton : MonoBehaviour
{
    public Food food;

    void Start()
    {
        Button btn = GetComponent<Button>();

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager не найден!");
            return;
        }

        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => GameManager.instance.BuyFood(food));
    }
}
