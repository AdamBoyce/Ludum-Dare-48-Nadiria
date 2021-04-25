using UnityEngine;

public class Diver : MonoBehaviour
{
    public const float SwimForce = 3f;
    public const float MaximumVelocity = 6f;
    public const float DashVelocity = 18f;
    public const float DashOffset = 0.583f;
    public const float DashCooldown = 1.583f;

    public Rigidbody2D Body;
    public Animator Animator;
    public SpriteRenderer Renderer;
    public bool IsDashing;
    public bool CanDash;
    public float DashEnd;
    public float DashDelay;
    public Vector2 Trajectory;
    public Vector2 DashVector;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Trajectory = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        CanDash = !IsDashing && Time.time >= DashDelay;

        if (Input.GetButtonDown("Jump") && CanDash)
        {
            DashEnd = Time.time + DashOffset;
            DashDelay = Time.time + DashCooldown;
            Animator.SetTrigger("Dash");

            if (Trajectory == Vector2.zero)
                Trajectory = Renderer.flipX ? Vector2.right : Vector2.left;

            DashVector = Trajectory * DashVelocity;
        }
    }

    void FixedUpdate()
    {
        IsDashing = Time.time <= DashEnd;

        if (Trajectory.x > 0)
            Renderer.flipX = true;
        else if (Trajectory.x < 0)
            Renderer.flipX = false;

        if (!IsDashing)
        {
            Animator.SetBool("IsMoving", Trajectory != Vector2.zero);
            Body.AddForce(Trajectory * SwimForce);

            float velocityX = Mathf.Clamp(Body.velocity.x, -MaximumVelocity, MaximumVelocity);
            float velocityY = Mathf.Clamp(Body.velocity.y, -MaximumVelocity, MaximumVelocity);

            Body.velocity = new Vector2(velocityX, velocityY);
        }
        else
            Body.velocity = DashVector;

    }

    public void Defeat()
    {
        Animator.SetTrigger("Defeated");
    }
}
