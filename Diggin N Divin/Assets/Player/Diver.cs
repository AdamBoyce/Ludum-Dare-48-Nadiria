using TMPro;
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
    public Transform Transform;
    public AudioSource AudioSource;
    public AudioClip FinDash;
    public AudioClip Defeat;
    public MessageDisplay MessageDisplay;
    public SaveDataManager SaveManager;

    public bool IsDashing;
    public bool CanDash;
    public float DashEnd;
    public float DashDelay;
    public bool IsPaused;
    public Vector2 Trajectory;
    public Vector2 DashVector;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        Transform = GetComponent<Transform>();
        AudioSource = GetComponent<AudioSource>();
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>();
        MessageDisplay = GameObject.Find("Canvas").GetComponent<MessageDisplay>();
        IsPaused = false;
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

            float angle = Mathf.Atan2(Trajectory.x, Trajectory.y) * Mathf.Rad2Deg;
            angle = Renderer.flipX || Trajectory.y > 0 ? angle : -angle;
            DashVector = Trajectory * DashVelocity;            

            Transform.rotation = Quaternion.Euler(0f, 0f, angle);
            AudioSource.clip = FinDash;
            AudioSource.Play();
        }

        if (Input.GetButtonDown("Pause"))
        {
            IsPaused = !IsPaused;
            MessageDisplay.IsPaused = IsPaused;
            Time.timeScale = IsPaused ? 0f : 1f;
        }
    }

    void FixedUpdate()
    {
        IsDashing = Time.time <= DashEnd;

        if (!IsDashing)
            Transform.eulerAngles = new Vector3(0f, 0f, 0f);

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

    public void OnHit()
    {
        SaveManager.OnDefeated();
        Animator.SetTrigger("Defeated");
        AudioSource.clip = Defeat;
        AudioSource.Play();
    }
}
