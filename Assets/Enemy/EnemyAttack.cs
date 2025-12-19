using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public enum EnemyAttackState
    {
        Idle,
        Preparing,
        Attacking,
        Cooldown
    }
    public float attackRange = 1.5f;
    public int damage = 10;
    public float attackCoolDown = 1.5f;

    public bool IsBusy =>
    state == EnemyAttackState.Preparing ||
    state == EnemyAttackState.Attacking ||
    state == EnemyAttackState.Cooldown;

    public float prepareTime = 0.6f;
    private float timer;
    private Transform player;
    EnemyAttackState state = EnemyAttackState.Idle;
    Renderer enemyRender;
    Color originalColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        enemyRender = GetComponentInChildren<Renderer>();
        originalColor = enemyRender.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player not found!");
            return;
        }
        float distance = Vector3.Distance(transform.position, player.position);

        if (state == EnemyAttackState.Idle)
        {
            GetComponent<TrackingAI>().enabled = true;
            if (distance <= attackRange)
            {
                StartCoroutine(PrepareAttack());
            }
        }


    }
    IEnumerator PrepareAttack()
    {
        state = EnemyAttackState.Preparing;

        //予備動作
        GetComponent<TrackingAI>().enabled = false;

        //見た目予備動作
        enemyRender.material.color = Color.yellow;
        yield return new WaitForSeconds(prepareTime);
        enemyRender.material.color = originalColor;
        /*Vector3 backPos = transform.position - transform.forward * 0.3f;
        float t = 0f;

        while (t < prepareTime)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                backPos,
                Time.deltaTime * 5f
            );
            t += Time.deltaTime;
            yield return null;
        }*/

        Attack();
    }
    void Attack()
    {
        if (state != EnemyAttackState.Preparing) return;
        state = EnemyAttackState.Attacking;
        Debug.Log("Enemy Attack!");

        PlayerStatus ps = player.GetComponentInParent<PlayerStatus>();
        if (ps != null)
        {

            ps.TakeDamage(damage);
        }
        else
        {
            Debug.Log("PlayerStatus not found");
        }

        PlayerController pc = player.GetComponentInParent<PlayerController>();
        if (pc != null)
        {
            pc.ApplyKnockBack(transform.position);
        }
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        state = EnemyAttackState.Cooldown;
        yield return new WaitForSeconds(attackCoolDown);

        GetComponent<TrackingAI>().enabled = true;
        state = EnemyAttackState.Idle;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
