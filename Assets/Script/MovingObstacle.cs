using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public Vector3 moveDirection = Vector3.right;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = startPos + moveDirection.normalized * offset;
    }
}
