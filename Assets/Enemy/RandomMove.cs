using UnityEngine;

public class RandomMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveRadious = 5f;
    public float ChangeTargetTime = 3f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Move();

        if (timer >= ChangeTargetTime || Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            SetRandomTarget();
        }
    }
    void Move()
    {
        Vector3 dir = (targetPos - transform.position).normalized;
        Vector3 lookdir = dir;
        lookdir.y = 0f;
        transform.position += dir * moveSpeed * Time.deltaTime;

        if (lookdir != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(lookdir),
                Time.deltaTime * 5f
            );
        }
    }

    void SetRandomTarget()
    {
        timer = 0f;

        Vector2 rand = Random.insideUnitCircle * moveRadious;
        targetPos = startPos + new Vector3(rand.x, 0, rand.y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(startPos, moveRadious);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(targetPos, 0.2f);
    }
}
