using System.Collections;
using UnityEngine;

public class OsoBorracho : MonoBehaviour {
    [Header("Referencias")]
    public Transform posicionMesaAmigo;
    public PuzzleVaso scriptVaso;
    public Dialogue pensamientoProtagonista;
    public int taskID;

    [Header("Configuración")]
    public float velocidad = 1.5f;
    private bool yaHableConEl = false;

    [Header("Pensamiento de la Protagonista")]
    public Dialogue pensamientoLuegoDeQueSeVa;

    // Suscribirse al evento del Manager
    private void OnEnable() {
        DialogueManager.Instance.OnDialogueEnd += AlTerminarDialogo;
    }

    private void OnDisable() {
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.OnDialogueEnd -= AlTerminarDialogo;
    }

    void AlTerminarDialogo() {
        // Solo si es la primera vez que hablamos con él y estamos cerca
        if (!yaHableConEl) {
            yaHableConEl = true;
            StartCoroutine(SecuenciaOsoYProtagonista());
        }
    }

    IEnumerator SecuenciaOsoYProtagonista() {

        if (TareasManager.Instance != null && taskID != 0) {
            TareasManager.Instance.CompleteTask(taskID);
        }
        while (Vector2.Distance(transform.position, posicionMesaAmigo.position) > 0.1f) {
            transform.position = Vector2.MoveTowards(transform.position, posicionMesaAmigo.position, velocidad * Time.deltaTime);
            yield return null;
        }

        scriptVaso.PermitirInspeccion();

        yield return new WaitForSeconds(0.5f);

        if (pensamientoLuegoDeQueSeVa != null) {
            DialogueManager.Instance.StartDialogue(pensamientoLuegoDeQueSeVa);
        }
    }
}