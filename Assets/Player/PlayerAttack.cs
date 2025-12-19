using UnityEngine;
using System;


public class PlayerAttack : MonoBehaviour
{
    public GameObject AttackPoint;
    public float attackDuration = 0.2f;
    public float attackCoolDown = 0.5f;

    private bool isAttackable = false;
    private float CoolDownTimer = 0f;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AttackPoint.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CoolDownTimer -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && !isAttackable && CoolDownTimer <= 0f)
        {
            StartCoroutine(Attack());
            animator.SetTrigger("Attack");
        }
    }

    System.Collections.IEnumerator Attack()
    {
        isAttackable = true;
        CoolDownTimer = attackCoolDown;

        AttackPoint.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        AttackPoint.SetActive(false);
        isAttackable = false;
    }
}
