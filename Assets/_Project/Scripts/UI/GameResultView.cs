using TMPro;
using UnityEngine;

public class GameResultView : MonoBehaviour
{
    [SerializeField] private TMP_Text _messageText;

    public void Show(string message)
    {
        gameObject.SetActive(true);
        _messageText.text = message;
    }
}