using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField]
    private Fighter[] _enemies;

    private void Start()
    {


        Debug.Log(GameManager.instance.CurrentMoney);
    }
}
