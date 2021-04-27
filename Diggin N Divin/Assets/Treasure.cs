using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Treasure : MonoBehaviour
{
    public int TreasureNumber;
    public SaveDataManager SaveManager;
    public AudioSource AudioSource;
    public MessageDisplay MessageDisplay;

    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        SaveManager = GameObject.Find("SaveData").GetComponent<SaveDataManager>();
        MessageDisplay = GameObject.Find("Canvas").GetComponent<MessageDisplay>();

        if ((TreasureNumber == 1 && SaveManager.Data.TreasureOne) ||
            (TreasureNumber == 2 && SaveManager.Data.TreasureTwo) ||
            (TreasureNumber == 3 && SaveManager.Data.TreasureThree) ||
            (TreasureNumber == 4 && SaveManager.Data.TreasureFour))
            Destroy(gameObject);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Diver diver))
        {
            switch (TreasureNumber)
            {
                case 1:
                    SaveManager.GetTreasureOne();
                    break;
                case 2:
                    SaveManager.GetTreasureTwo();
                    break;
                case 3:
                    SaveManager.GetTreasureThree();
                    break;
                case 4:
                    StartCoroutine("EndGame");
                    break;
            }

            MessageDisplay.ShowMessage("Treasure!");
            AudioSource.Play();
            Destroy(gameObject);
        }
    }

    IEnumerator EndGame()
    {
        SaveManager.GetTreasureFour();
        MessageDisplay.ShowMessage("YOU DID IT!");
        AudioSource.Play();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(2);
    }
}
