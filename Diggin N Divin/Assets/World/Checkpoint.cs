using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool IsOn;
    public SpriteRenderer Renderer;
    public Sprite Inactive;
    public Sprite Active;
    public Transform LoadPoint;
    public int Index;

    public event EventHandler CheckpointActivated;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.sprite = Inactive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver))        
            CheckpointActivated.Invoke(this, EventArgs.Empty);        
    }

    public void Activate()
    {
        IsOn = true;
        Renderer.sprite = Active;
    }

    public void Deactivate()
    {
        IsOn = false;
        Renderer.sprite = Inactive;
    }
}
