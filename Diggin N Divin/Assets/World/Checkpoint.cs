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
    public SaveDataManager SaveManager;
    public MessageDisplay MessageDisplay;

    private void Start()
    {
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>();
        Renderer = GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
        Renderer.sprite = Inactive;
        MessageDisplay = GameObject.Find("Canvas").GetComponent<MessageDisplay>();

        if (SaveManager.Data.ActiveCheckpoint == Index)
            Activate();
    }

    private void Update()
    {
        if (SaveManager.Data.ActiveCheckpoint != Index)
            Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver) && !IsOn)
        {
            AudioSource.clip = CheckpointActivatedClip;
            AudioSource.Play();
            MessageDisplay.ShowMessage("Checkpoint!");
            SaveManager.Checkpoint(Index);
            Activate();
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
