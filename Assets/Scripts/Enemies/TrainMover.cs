using UnityEngine;

public class TrainMover : MonoBehaviour
{
    private Vector2 _direction;
    private float _speed;

    public void Init(Vector2 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
}
