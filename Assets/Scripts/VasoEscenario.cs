using UnityEngine;

public class VasoEscenario : MonoBehaviour {

    [Header("Datos del Vaso")]
    public ItemData vasoData; // El ScriptableObject que configuraste arriba
    public Sprite vasoLimpioSprite;

    private bool yaSeInspecciono = false;

    private void OnMouseDown() {
        if (!yaSeInspecciono) {
            // 1. Llamamos al Manager persistente y le pasamos los datos del vaso
            InspeccionManager.Instance.InspectItem(vasoData);

            // 2. Intentamos revelar el secreto inmediatamente (el pelo)
            // Esto añadirá el pelo al inventario automáticamente gracias a tu código
            InspeccionManager.Instance.AttemptRevealSecret();

            // 3. Cambiamos el aspecto en el mundo
            if (vasoLimpioSprite != null) {
                GetComponent<SpriteRenderer>().sprite = vasoLimpioSprite;
            }

            yaSeInspecciono = true;
            Debug.Log("Vaso inspeccionado: Pelo enviado al inventario.");
        }
        else {
            // Si ya se inspeccionó, solo mostramos el panel sin intentar dar el pelo otra vez
            InspeccionManager.Instance.InspectItem(vasoData);
        }
    }
}
