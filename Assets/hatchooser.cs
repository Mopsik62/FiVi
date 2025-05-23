using UnityEngine;

public class hatchooser : MonoBehaviour
{
   public GameObject one;
   public GameObject two;
   public GameObject three;
   public GameObject four;

    private GameObject _active;

    private void OnEnable()
    {
        int index = Random.Range(0, 4);
        switch (index)
        {
            case 0: _active = one; break;
            case 1: _active = two; break;
            case 2: _active = three; break;
            case 3: _active = four; break;
        }

        if (_active != null)
            _active.SetActive(true);
    }

    private void OnDisable()
    {
        if (_active != null)
            _active.SetActive(false);
    }

}
