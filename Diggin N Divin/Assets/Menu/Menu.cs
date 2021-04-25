using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public SaveDataManager SaveManager;
    public Button NewGame;
    public Button Continue;

    void Start()
    {
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>();
        NewGame = GameObject.Find("New Game").GetComponent<Button>();
        Continue = GameObject.Find("Continue").GetComponent<Button>();

        if (!SaveManager.HasSavedGame())
        {
            Continue.enabled = false;
            Continue.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0);
        }
    }

    public void OnNewGame()
    {
        SaveManager.NewGame();
        SceneManager.LoadScene(1);
    }

    public void OnContinue()
    {
        SaveManager.Continue();
        SceneManager.LoadScene(1);
    }
}
