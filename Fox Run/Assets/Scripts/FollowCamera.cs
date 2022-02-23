using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector2 center;
    public Vector2 size;

    private float m_height;
    private float m_width;

    private void Awake()
    {
        target = GameObject.Find("Player").transform;
    }

    void Start()
    {
        m_height = Camera.main.orthographicSize;
        m_width = m_height * Screen.width / Screen.height;
    }

    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 1, -10);

        float lx = size.x * 0.5f - m_width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly= size.y * 0.5f - m_height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
