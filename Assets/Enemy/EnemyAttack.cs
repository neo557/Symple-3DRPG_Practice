using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCoolDown = 1.5f;

    private float timer;
    private Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float dist = Vector3.Distance(transform.position, player.position);

        if (dist <= attackRange && timer >= attackCoolDown)
        {
            Attack();
            timer = 0f;
        }
    }
    void Attack()
    {
        Debug.Log("Enemy Attack!");

        PlayerStatus ps = player.GetComponentInParent<PlayerStatus>();
        if (ps == null)
        {
            Debug.Log("PlayerStatus not found");
            return;
        }
        ps.TakeDamage(damage);

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.ApplyKnockBack(transform.position);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
