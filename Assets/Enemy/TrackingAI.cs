using UnityEngine;

public class TrackingAI : MonoBehaviour
{
    enum EnemyState
    {
        Wander,
        Chase
    }

    public Transform player;
    public float moveSpeed = 3.5f;
    public float chaseSpeed = 3.5f;
    public float chaseDistance = 6f;
    public float moveRadious = 5f;
    public float ChangeTargetTime = 3f;

    private EnemyState state;
    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        state = EnemyState.Wander;
        SetRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist < chaseDistance)
            state = EnemyState.Chase;
        else
            state = EnemyState.Wander;

        switch (state)
        {
            case EnemyState.Wander:
                Wander();
                break;

            case EnemyState.Chase:
                Chase();
                break;
        }
    }

    void Wander()
    {
        timer += Time.deltaTime;
        Move(targetPos, moveSpeed);

        if (timer >= ChangeTargetTime ||
        Vector3.Distance(transform.position, targetPos) < 0.2f)
        {
            SetRandomTarget();
        }
    }
    void Chase()
    {
        Move(player.position, chaseSpeed);
    }

    void Move(Vector3 target, float speed)
    {
        Vector3 dir = (target - transform.position);
        dir.y = 0f;

        if (dir == Vector3.zero) return;

        transform.position += dir.normalized * speed * Time.deltaTime;

        transform.rotation = Quaternion.Slerp(transform.rotation,
        Quaternion.LookRotation(dir),
        Time.deltaTime * 5f);
    }

    void SetRandomTarget()
    {
        timer = 0f;
        Vector2 rand = Random.insideUnitCircle * moveRadious;
        targetPos = startPos + new Vector3(rand.x, 0, rand.y);
    }
}
