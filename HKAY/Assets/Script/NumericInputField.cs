using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumericInputField : MonoBehaviour
{
    private TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
        inputField.onValidateInput += OnValidateInput;
    }

    private char OnValidateInput(string text, int charIndex, char addedChar)
    {
        if (!char.IsDigit(addedChar))
        {
            return '\0'; // Boþ karakter döndürerek harf giriþini engeller
        }

        return addedChar;
    }
}