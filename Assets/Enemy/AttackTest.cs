using UnityEngine;

public class AttackTest : MonoBehaviour
{
    public int damege = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStatus status = other.GetComponent<EnemyStatus>();
            if (status != null)
            {
                status.TakeDamage(damege);
            }
        }
        Debug.Log("Hit: " + other.name);
    }
}
