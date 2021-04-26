using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private const float MessageDecayTime = 2.5f;

    public TextMeshProUGUI Message;

    private float _decay;

    void Start()
    {
        Message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (Time.time > _decay)
            Message.text = string.Empty;
    }

    public void ShowMessage(string message)
    {
        Message.text = message;
        _decay = Time.time + MessageDecayTime;
    }
}