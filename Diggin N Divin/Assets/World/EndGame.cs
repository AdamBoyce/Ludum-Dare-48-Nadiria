using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public SaveDataManager SaveManager;
    public AudioSource AudioSource;
    public TextMeshProUGUI Results;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>();
        AudioSource.Play();
        AudioSource.time = 30f;
        Results = GameObject.Find("Results").GetComponent<TextMeshProUGUI>();

        int bonusTreasures = 0;
        if (SaveManager.Data.TreasureOne)
            bonusTreasures++;
        if (SaveManager.Data.TreasureTwo)
            bonusTreasures++;
        if (SaveManager.Data.TreasureThree)
            bonusTreasures++;

        Results.text = string.Format(Results.text, SaveManager.Data.Deaths, bonusTreasures);
    }

    public void OnNewGame()
    {
        SaveManager.NewGame();
        SceneManager.LoadScene(1);
    }

    public void OnExit()
    {
        Application.Quit();
    }
}
