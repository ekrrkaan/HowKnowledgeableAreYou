using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LimitedCharacterInputField : MonoBehaviour
{
    private TMP_InputField inputField;
    private int maxCharacterCount = 12;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValidateInput += OnValidateInput;
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (text.Length >= maxCharacterCount)
        {
            return '\0'; // Boþ karakter döndürerek yeni karakter eklemeyi engeller
        }

        return addedChar;
    }
}

