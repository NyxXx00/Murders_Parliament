using TMPro;
using UnityEngine;

public class ClicMensj : MonoBehaviour {


    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public string texto = "";
    public float showTime = 3f;

    void OnMouseDown() {
        messageText.text = texto;
        messagePanel.SetActive(true);
        CancelInvoke(nameof(HidePanel));
        Invoke(nameof(HidePanel), showTime);
    }

    void HidePanel() {
        messagePanel.SetActive(false);
    }
}


