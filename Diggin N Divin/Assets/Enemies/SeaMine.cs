using UnityEngine;

public class SeaMine : MonoBehaviour
{
    public Animator Animator;
    public AudioSource AudioSource;

    void Start()
    {
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver) && !diver.IsDashing)
        {
            diver.OnHit();
            Animator.SetTrigger("MineTripped");
            AudioSource.Play();
        }
    }
}
