using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionInput : MonoBehaviour
{
    public static string userAnswer;
    public TMP_InputField inputField;

    public void ReadStringInput(string input)
    {
        if (input == null || input == "")
        {
            throw new MissingComponentException("input cannot be null or empty");
        }
        
        userAnswer = input;
        Debug.Log(userAnswer);
        inputField.text = ""; // Clears the input field
        // Get a reference to the GameObject
        GameObject gameObject = GameObject.Find("QuestionOutputScript");

        // Send the message
        gameObject.SendMessage("CheckAnswer");
    }
}

