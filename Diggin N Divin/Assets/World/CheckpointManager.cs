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

        if ((GameObject.Find("DeathCount")?.TryGetComponent(out Deaths)).GetValueOrDefault())
            Deaths.text = $"Deaths: {SaveManager.Data.Deaths}";

        //foreach (Checkpoint checkpoint in Checkpoints)
        //{
        //    checkpoint.CheckpointActivated += OnCheckpointActivated;
            
        //    if(checkpoint.Index == SaveManager.Data.ActiveCheckpoint)
        //    {
        //        checkpoint.Activate();
        //        Diver.position = checkpoint.LoadPoint.position; 
        //    }           
        //}        
    }

    //private void OnCheckpointActivated(object sender, System.EventArgs e)
    //{
    //    foreach (Checkpoint checkpoint in Checkpoints)
    //        checkpoint.Deactivate();

    //    Checkpoint activeCheckpoint = ((Checkpoint)sender);
    //    MessageDisplay.ShowMessage("Checkpoint!");
    //    activeCheckpoint.Activate();
    //    SaveManager.Checkpoint(activeCheckpoint.Index);
    //}
}