using UnityEngine;

public class Round : MonoBehaviour
{
    public class PixelPerfectClamp : MonoBehaviour
    {
        public float pixelsPerUnit = 16f;

        void LateUpdate()
        {
            var pos = transform.position;
            float unit = 1f / pixelsPerUnit;
            pos.x = Mathf.Round(pos.x / unit) * unit;
            pos.y = Mathf.Round(pos.y / unit) * unit;
            transform.position = pos;
        }
    }
}
