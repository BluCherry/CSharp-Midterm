using UnityEngine;

public class QuestionInput : MonoBehaviour
{
    public static string userAnswer;

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
    }
}
