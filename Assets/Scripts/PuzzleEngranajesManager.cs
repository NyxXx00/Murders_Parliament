using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngranajesManager : MonoBehaviour {
    public static PuzzleEngranajesManager Instance;

    public List<PiezaEngranaje> piezas;
    [Tooltip("Sube esto si las piezas no se mueven (ej: 500)")]
    public float margenDistancia = 350f;

    [Header("Referencias de Victoria")]
    public GameObject panelPuzzle;
    public DialogueTrigger dialogoVictoria;
    public int taskID;

    [Header("Limpieza de Escena")]
    public DisparadorPuzzle disparador;

    [Header("Sonido de Victoria")]
    public AudioClip sonidoEngranajes;
    private AudioSource altavoz;

    void Awake() {
        Instance = this;
        altavoz = GetComponent<AudioSource>();

        if (piezas == null || piezas.Count == 0) {
            piezas = new List<PiezaEngranaje>(GetComponentsInChildren<PiezaEngranaje>());
        }
    }

    public void IntentarMover(PiezaEngranaje piezaClicada) {
        PiezaEngranaje hueco = piezas.Find(p => p.esHuecoVacio);

        if (hueco == null) {
            Debug.LogError("ERROR: ¡No hay ninguna pieza marcada como 'Es Hueco Vacio'!");
            return;
        }

        float distancia = Vector2.Distance(piezaClicada.GetComponent<RectTransform>().anchoredPosition, hueco.GetComponent<RectTransform>().anchoredPosition);

        if (distancia < margenDistancia) {
            Vector2 posTemp = hueco.GetComponent<RectTransform>().anchoredPosition;
            hueco.MoverA(piezaClicada.GetComponent<RectTransform>().anchoredPosition);
            piezaClicada.MoverA(posTemp);
            Invoke("ComprobarVictoria", 0.4f);
        }
    }

    public void ComprobarVictoria() {
        int correctas = 0;
        foreach (PiezaEngranaje p in piezas) {
            if (p.EstaEnSuSitio()) correctas++;
        }

        // Si todas están bien, lanzamos la CORRUTINA de victoria
        if (correctas == piezas.Count) {
            StartCoroutine(SecuenciaVictoria());
        }
    }

    IEnumerator SecuenciaVictoria() {
        Debug.Log("¡VICTORIA DETECTADA!");

        if (disparador != null) {
            disparador.FinalizarPuzzleYLimpiarEscena();
        }

        // 1. Sonido y Tarea
        if (altavoz != null && sonidoEngranajes != null) {
            altavoz.PlayOneShot(sonidoEngranajes);
        }

        if (TareasManager.Instance != null && taskID != 0) {
            TareasManager.Instance.CompleteTask(taskID);
        }

        // 2. Desbloquear armario
        Armario scriptArmario = Object.FindFirstObjectByType<Armario>();
        if (scriptArmario != null) scriptArmario.puzzleResuelto = true;

        // 3. CERRAR EL PANEL PRIMERO (para que no estorbe al diálogo)
        if (panelPuzzle != null) panelPuzzle.SetActive(false);

        // 4. ESPERA un momento con el panel ya cerrado
        yield return new WaitForSeconds(1.0f);

        // 5. DISPARAR DIÁLOGO
        if (dialogoVictoria != null) {
            dialogoVictoria.TriggerDialogue();
        }

        // Desactivar el manager
        this.gameObject.SetActive(false);
    }
}