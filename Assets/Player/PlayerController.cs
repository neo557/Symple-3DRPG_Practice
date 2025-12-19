using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    static float walkspeed = 12;
    public float gravity = -9.81f;
    public float fallVelocity = 0f;
    private CharacterController controller;
    private Vector3 direction;
    bool isKnockback;
    Vector3 knockbackVelocity;
    Animator animator;
    public float knockbackPower = 1f;
    public float knockbackDamping = 1f;
    float knockbackTimer;
    public float knockbackDuration = 0.08f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        direction = new Vector3(h, 0, v);

        bool isMoving = direction.magnitude > 0.1f;
        if (animator != null)
            animator.SetBool("isMoving", isMoving);

        if (Camera.main != null)
        {
            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            direction = camForward * v + camRight * h;

        }

        //Graviry-true
        if (controller.isGrounded)
        {
            fallVelocity = -1f;
        }
        else
        {
            fallVelocity += gravity * Time.deltaTime;
        }

        //長さを1にする
        if (direction.magnitude > 1f)
            direction.Normalize();

        Vector3 velocity = direction * walkspeed;
        velocity.y = fallVelocity;

        controller.Move(velocity * Time.deltaTime);

        //移動方向にキャラを向ける
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(direction),
            Time.deltaTime * 10f);
        }

        //ノックバック処理
        if (isKnockback && knockbackTimer > 0f)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
            }
            return;
        }
    }
    public void ApplyKnockBack(Vector3 fromPosition)
    {
        Vector3 dir = transform.position - fromPosition;
        dir.y = 0f;
        knockbackVelocity = dir.normalized * knockbackPower;
        knockbackTimer = knockbackDuration;
        isKnockback = true;
        Debug.Log($"KnockBack! dir:{dir} power:{knockbackPower}");
    }
}
