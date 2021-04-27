using UnityEngine;

public class GiantSquid : MonoBehaviour
{
    public const float UpSpeed = 8f;
    public const float DownSpeed = 2f;

    public Vector2 Base;
    public Vector2 Top;
    public float Offset;
    public bool IsSwimming;
    public bool StartFromTheBottom;

    public Rigidbody2D Body;
    public Transform Transform;
    public Animator Animator;
    public Diver Diver;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
        Transform = GetComponent<Transform>();
        Animator = GetComponent<Animator>();
        Diver = GameObject.Find("Diver").GetComponent<Diver>();
        Base = Transform.position;
        Top = new Vector2(Base.x, Base.y + Offset);
        IsSwimming = false;

        if (!StartFromTheBottom)
            Transform.position = Top;
    }

    void FixedUpdate()
    {
        if (Transform.position.y >= Top.y)
        {
            Body.velocity = new Vector2(0f, -DownSpeed);
            Animator.SetTrigger("EndSwim");
        }
        else if (Transform.position.y <= Base.y && Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            Animator.SetTrigger("StartSwim");
        else if (Transform.position.y <= Base.y && Animator.GetCurrentAnimatorStateInfo(0).IsName("Swim"))
            Body.velocity = new Vector2(0f, UpSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.TryGetComponent(out Diver diver) && !diver.IsDashing)
            diver.OnHit();
    }
}
