using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private const float MessageDecayTime = 2.5f;

    public TextMeshProUGUI Message;
    public GameObject ExitButton;
    public GameObject ResumeButton;
    public bool IsPaused;

    private float _decay;

    void Start()
    {
        Message = GameObject.Find("Message").GetComponent<TextMeshProUGUI>();
        ExitButton.SetActive(false);
        ResumeButton.SetActive(false);
    }

    void Update()
    {
        if (Time.time > _decay && !IsPaused)
        {
            Message.text = string.Empty;
            ExitButton.SetActive(false);
            ResumeButton.SetActive(false);
        }        
        else if (IsPaused)
        {
            Message.text = "Paused";
            ExitButton.SetActive(true);
            ResumeButton.SetActive(true);
        }
    }

    public void ShowMessage(string message)
    {
        Message.text = message;
        _decay = Time.time + MessageDecayTime;
    }

    public void OnExit()
    {
        Application.Quit();
    }
}