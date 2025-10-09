using UnityEngine;

public class QuestionInput : MonoBehaviour
{
    public static string userAnswer;
    public TMP_InputField inputField;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ReadStringInput(string input)
    {
        userAnswer = input;
        Debug.Log(userAnswer);
        inputField.text = ""; // Clears the input field
        // Get a reference to the GameObject
        GameObject gameObject = GameObject.Find("QuestionOutputScript");

        // Send the message
        gameObject.SendMessage("CheckAnswer");
    }
}

