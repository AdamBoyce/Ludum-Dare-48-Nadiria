using UnityEngine;

public class DashBarrier : MonoBehaviour
{
    public const float Bounce = 1f;

    public Diver Diver;
    public BoxCollider2D Collider;

    void Start()
    {
        Diver = GameObject.Find("Diver").GetComponent<Diver>();
        Collider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Collider.enabled = !Diver.IsDashing;        
    }
}
