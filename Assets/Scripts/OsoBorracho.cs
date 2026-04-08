using UnityEngine;
using System.Collections;

public class OsoBorracho : MonoBehaviour {
    [Header("Referencias")]
    public Transform posicionMesaAmigo;
    public PuzzleVaso scriptVaso;
    public Dialogue pensamientoProtagonista; // El diálogo de ella hablando sola

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
        // 1. EL OSO CAMINA...
        while (Vector2.Distance(transform.position, posicionMesaAmigo.position) > 0.1f) {
            transform.position = Vector2.MoveTowards(transform.position, posicionMesaAmigo.position, velocidad * Time.deltaTime);
            yield return null;
        }

        // 2. EL OSO LLEGA Y LIBERA EL VASO
        scriptVaso.PermitirInspeccion();

        // 3. PEQUEÑA PAUSA (Para que no sea instantáneo)
        yield return new WaitForSeconds(0.5f);

        // 4. DISPARAR EL PENSAMIENTO
        if (pensamientoLuegoDeQueSeVa != null) {
            DialogueManager.Instance.StartDialogue(pensamientoLuegoDeQueSeVa);
        }
    }
}