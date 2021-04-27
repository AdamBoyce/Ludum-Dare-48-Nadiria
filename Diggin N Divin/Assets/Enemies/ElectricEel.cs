using UnityEngine;

public class ElectricEel : MonoBehaviour
{
    public const float SwimForce = 3f;
    public const float MaximumVelocity = 2f;
    public const float ShockTime = 0.5f;
    public const float ShockFrequency = 3f;

    private Vector2 _leftLimit;
    private Vector2 _rightLimit;

    public float NextShock;

    public Transform LeftLimit;
    public Transform RightLimit;
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

        _leftLimit = LeftLimit.position;
        _rightLimit = RightLimit.position;

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

        if (Transform.position.x >= _rightLimit.x)
            Renderer.flipX = false;
        else if (Transform.position.x <= _leftLimit.x)
            Renderer.flipX = true;

        if (Renderer.flipX)
            Body.AddForce(Vector2.right * SwimForce);
        else
            Body.AddForce(Vector2.left * SwimForce);

        float velocityX = Mathf.Clamp(Body.velocity.x, -MaximumVelocity, MaximumVelocity);
        float velocityY = Mathf.Clamp(Body.velocity.y, -MaximumVelocity, MaximumVelocity);

        Body.velocity = Animator.GetBool("IsShocking") ? Vector2.zero : new Vector2(velocityX, velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Diver diver))
            diver.OnHit();
    }
}
