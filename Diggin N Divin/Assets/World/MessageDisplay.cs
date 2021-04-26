using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private const float MessageDecayTime = 2.5f;

    public TextMeshProUGUI Message;
    public bool IsPaused;

    private float _decay;

    void Start()
    {
        Message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Time.time > _decay && !IsPaused)
            Message.text = string.Empty;
        else if(IsPaused)
            Message.text = "Paused";
    }

    public void ShowMessage(string message)
    {
        Message.text = message;
        _decay = Time.time + MessageDecayTime;
    }
}