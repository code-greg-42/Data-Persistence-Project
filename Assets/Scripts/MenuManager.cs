using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TMP_InputField nameField;

    private void Start()
    {
        if (DataManager.Instance.currentPlayerName != "")
        {
            // update player name and high score
            bestScoreText.text = DataManager.Instance.currentPlayerName + "'s high score: " + DataManager.Instance.currentPlayerBestScore;
        }
        else
        {
            bestScoreText.text = "brickbreaker!";
        }
    }

    public void StartGame()
    {
        if (nameField != null && nameField.text != "")
        {
            DataManager.Instance.LoadPlayerData(nameField.text.ToLower());
            SceneManager.LoadScene(1);
        }
        else
        {
            TextMeshProUGUI placeholderText = nameField.placeholder as TextMeshProUGUI;
            if (placeholderText != null )
            {
                placeholderText.text = "Must enter name...";
            }
        }
    }

    public void ExitGame()
    {
        EditorApplication.ExitPlaymode();
    }
}
