using UnityEngine;

public class ElectricEel : MonoBehaviour
{
    public const float SwimForce = 3f;
    public const float MaximumVelocity = 2f;
    public const float ShockTime = 0.5f;
    public const float ShockFrequency = 3f;

    public Vector2 PointAOffset;
    public Vector2 PointBOffset;
    public float NextShock;

    public Rigidbody2D Body;
    public Transform Transform;
    public AudioSource AudioSource;
    public AudioClip EelJolt;
    public SpriteRenderer Renderer;
    public BoxCollider2D BoxCollider;
    public PolygonCollider2D PolyCollider;
    public Animator Animator;
    public Diver Diver;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
        AudioSource = GetComponent<AudioSource>();
        Renderer = GetComponent<SpriteRenderer>();
        BoxCollider = GetComponent<BoxCollider2D>();
        PolyCollider = GetComponent<PolygonCollider2D>();
        Animator = GetComponent<Animator>();
        Diver = GameObject.Find("Diver").GetComponent<Diver>();
        PointAOffset = new Vector2(Transform.position.x + 4f, Transform.position.y);
        PointBOffset = new Vector2(Transform.position.x - 4f, Transform.position.y);

        BoxCollider.enabled = true;
        PolyCollider.enabled = false;
        NextShock = Time.time + ShockFrequency;

    }

    void Update()
    {
        if(Time.time >= NextShock && Time.time < NextShock + ShockTime)
        {
            Animator.SetBool("IsShocking", true);
            
            if(!AudioSource.isPlaying)
                AudioSource.Play();
        }
        else if(Time.time >= NextShock + ShockTime)
        {
            Animator.SetBool("IsShocking", false);
            NextShock = Time.time + ShockFrequency;
        }
    }

    void FixedUpdate()
    {
        PolyCollider.enabled = Animator.GetBool("IsShocking") && !Diver.IsDashing;
        BoxCollider.enabled = !Animator.GetBool("IsShocking") && !Diver.IsDashing;

        if (Transform.position.x >= PointAOffset.x)
            Renderer.flipX = false;
        else if (Transform.position.x <= PointBOffset.x)
            Renderer.flipX = true;

        if (Renderer.flipX)
            Body.AddForce(Vector2.right * SwimForce);
        else
            Body.AddForce(Vector2.left * SwimForce);

        float velocityX = Mathf.Clamp(Body.velocity.x, -MaximumVelocity, MaximumVelocity);
        float velocityY = Mathf.Clamp(Body.velocity.y, -MaximumVelocity, MaximumVelocity);

        Body.velocity = Animator.GetBool("IsShocking") ? Vector2.zero : new Vector2(velocityX, velocityY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver))
            diver.OnHit();
    }
}
