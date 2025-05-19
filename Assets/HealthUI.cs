using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Sprite _fullHeart;
    [SerializeField]
    private Sprite _halfHeart;
    [SerializeField]
    private Sprite _emptyHeart;

    [SerializeField]
    private Image[] _hearts; 

    public void UpdateHearts(float currentHealth)
    {
        int fullHearts = (int)(currentHealth / 2);
        bool hasHalfHeart = currentHealth % 2 == 1;

        for (int i = 0; i < _hearts.Length; i++)
        {
            if (i < fullHearts)
                _hearts[i].sprite = _fullHeart;
            else if (i == fullHearts && hasHalfHeart)
                _hearts[i].sprite = _halfHeart;
            else
                _hearts[i].sprite = _emptyHeart;
        }
    }
}
