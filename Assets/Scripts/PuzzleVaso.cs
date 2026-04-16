using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleVaso : MonoBehaviour {

    [Header("Datos del Item")]
    public ItemData vasoData; // El ItemData que tiene el "Manojo de Pelo" como RevealedItemData
    public Sprite vasoLimpioSprite;
    public int taskID;

    [Header("Estado del Puzzle")]
    public bool osoSeHaIdo = false; // Cambia esto a TRUE desde el sistema de diálogos
    private bool peloRecogido = false;

    private void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Caso 1: El Oso sigue ahí
        if (!osoSeHaIdo) {
            Debug.Log("No puedes inspeccionar el vaso mientras el Oso está mirando.");
            // Aquí podrías disparar una frase de texto: "Mejor espero a que se distraiga."
            return;
        }

        // Caso 2: El Oso se fue, pero aún no tenemos el pelo
        if (!peloRecogido) {
            EjecutarInspeccionYRecogida();
        }
        // Caso 3: Ya lo inspeccionamos, solo abrimos la vista de nuevo
        else {
            InspeccionManager.Instance.InspectItem(vasoData);
        }
    }

    void EjecutarInspeccionYRecogida() {
        // 1. Abrimos el panel de inspección
        InspeccionManager.Instance.InspectItem(vasoData);

        // 2. Revelamos el secreto (Añade el Pelo al inventario automáticamente)
        InspeccionManager.Instance.AttemptRevealSecret();

        if (TareasManager.Instance != null && taskID != 0) {
            TareasManager.Instance.CompleteTask(taskID);
        }

        // 3. Feedback visual en el escenario (Vaso ahora se ve limpio)
        if (vasoLimpioSprite != null) {
            GetComponent<SpriteRenderer>().sprite = vasoLimpioSprite;
        }

        peloRecogido = true;
        Debug.Log("¡Pelo obtenido! Ahora está en el inventario.");
    }

    // Esta función la llamas desde el final del diálogo del Oso
    public void PermitirInspeccion() {
        osoSeHaIdo = true;
    }
}