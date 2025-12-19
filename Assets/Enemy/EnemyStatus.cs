using System.Collections;
using System.Timers;
using UnityEngine;


public class EnemyStatus : MonoBehaviour
{
    private int maxHp = 5;
    private int currentHp;
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
        controller = GetComponent<CharacterController>();
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"{gameObject.name} Hp;{currentHp}");

        if (currentHp <= 0)

        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} Dead");

        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        float duration = 0.5f;
        float elapse = 0f;

        Vector3 startScalse = transform.localScale;

        while (elapse < duration)
        {
            transform.localScale = Vector3.Lerp(startScalse,
            Vector3.zero,
            elapse / duration);
            elapse += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);

    }
    // Update is called once per frame
    void Update()
    {

    }
}
