using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class ItemTareas : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Toggle toggle;

    private string taskID;
    private bool completado = false;

    public void Initialize(string id, string descripcion) {
        taskID = id;
        text.text = descripcion;
        toggle.isOn = false;
        toggle.interactable = false;
    }

    public void Complete() {
        if (completado) return;

        completado = true;
        toggle.isOn = true;
        text.text = text.text;
        text.color = Color.gray;
    }
}
