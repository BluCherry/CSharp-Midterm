using UnityEngine;

public class GetUsername : MonoBehaviour
{
    public static string username;

    public static void ReadStringInput(string input)
    {
        if (input == null || input == "")
        {
            throw new MissingComponentException("username cannot be null or empty");
        }

        username = input;
        Debug.Log(username);
    }
}

