using UnityEngine;

public class InventarioUI : MonoBehaviour {

    public GameObject panelInventario;

    public void ToggleInventario() {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }
}
