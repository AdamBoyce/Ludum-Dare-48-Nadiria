using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Checkpoint[] Checkpoints;
    public SaveDataManager SaveManager;
    public Transform Diver;

    private void Start()
    {
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>(); 
        Checkpoints = GetComponentsInChildren<Checkpoint>();
        Diver = GameObject.Find("Diver").GetComponent<Transform>();

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

        activeCheckpoint.Activate();
        SaveManager.Checkpoint(activeCheckpoint.Index);
    }
}