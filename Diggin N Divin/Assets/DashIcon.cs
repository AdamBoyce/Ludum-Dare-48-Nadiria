using UnityEngine;
using UnityEngine.UI;

public class DashIcon : MonoBehaviour
{
    public RuntimeAnimatorController RuntimeController;
    public Image Image;
    public SpriteRenderer Renderer;
    public Animator Animator;

    void Start()
    {
        Image = GetComponent<Image>();
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.enabled = false;
        Animator = GetComponent<Animator>();
        Animator.runtimeAnimatorController = RuntimeController;
    }

    void Update()
    {
        if (Animator.runtimeAnimatorController)
            Image.sprite = Renderer.sprite;
    }
}
