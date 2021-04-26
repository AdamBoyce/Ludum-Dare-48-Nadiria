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
    public AudioClip CheckpointActivatedClip;
    public AudioSource AudioSource;

    public event EventHandler CheckpointActivated;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
        Renderer.sprite = Inactive;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver) && !IsOn)
        {
            CheckpointActivated.Invoke(this, EventArgs.Empty);
            AudioSource.clip = CheckpointActivatedClip;
            AudioSource.Play();
        }
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
