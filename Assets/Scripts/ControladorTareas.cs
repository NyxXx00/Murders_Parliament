using UnityEngine;

public class ControladorTareas : MonoBehaviour
{
    public GameObject PanelTareas;

    public void TogglePanelTareas() {

       if (PanelTareas != null) {
            PanelTareas.SetActive(!PanelTareas.activeSelf);
        }
    }
}
