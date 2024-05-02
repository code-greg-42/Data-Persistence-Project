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

    public void StartGame()
    {
        if (nameField != null && nameField.text != "")
        {
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
