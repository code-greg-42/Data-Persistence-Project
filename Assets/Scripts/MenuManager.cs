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
        if (DataManager.Instance.playerName != "")
        {
            // update player name and high score
            bestScoreText.text = DataManager.Instance.playerName + "'s High Score: " + DataManager.Instance.bestScore;
        }
        else
        {
            bestScoreText.text = "BrickBreaker!";
        }
    }

    public void StartGame()
    {
        if (nameField != null && nameField.text != "")
        {
            DataManager.Instance.playerName = nameField.text;
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
