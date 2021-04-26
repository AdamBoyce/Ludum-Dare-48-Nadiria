using TMPro;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] Checkpoints;
    public SaveDataManager SaveManager;
    public Transform Diver;
    public TextMeshProUGUI Deaths;
    public MessageDisplay MessageDisplay;

    private void Start()
    {
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>(); 
        Checkpoints = GetComponentsInChildren<Checkpoint>();
        Diver = GameObject.Find("Diver").GetComponent<Transform>();
        MessageDisplay = GameObject.Find("Canvas").GetComponent<MessageDisplay>();

        if ((GameObject.Find("DeathCount")?.TryGetComponent(out Deaths)).GetValueOrDefault())
            Deaths.text = $"Deaths: {SaveManager.Data.Deaths}";

        for (int i = 0; i < Checkpoints.Length; ++i)
        { 
            Checkpoints[i].CheckpointActivated += OnCheckpointActivated;
            Checkpoints[i].Index = i;
        }

        Checkpoint activeCheckpoint = Checkpoints[SaveManager.Data.ActiveCheckpoint];
        activeCheckpoint.Activate();
        Vector2 checkpointPosition = activeCheckpoint.LoadPoint.position;

        Diver.position = checkpointPosition;
    }

    private void OnCheckpointActivated(object sender, System.EventArgs e)
    {
        foreach (Checkpoint checkpoint in Checkpoints)
            checkpoint.Deactivate();

        Checkpoint activeCheckpoint = ((Checkpoint)sender);
        MessageDisplay.ShowMessage("Checkpoint!");
        activeCheckpoint.Activate();
        SaveManager.Checkpoint(activeCheckpoint.Index);
    }
}