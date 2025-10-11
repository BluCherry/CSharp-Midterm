using TMPro;
using UnityEngine;

public class WinOrLose : MonoBehaviour
{
    public TextMeshProUGUI outputText;

    void Start()
    {
        if (QuestionOutput.gameComplete == true)
        {
            outputText.text = "You Win!";
        }
        else
        {
            outputText.text = "You Lose!";
        }
    }
}
