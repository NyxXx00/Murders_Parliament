using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngranajesManager : MonoBehaviour {
    public static PuzzleEngranajesManager Instance;

    public List<PiezaEngranaje> piezas;
    public float margenDistancia = 350f;

    [Header("Referencias de Victoria")]
    public GameObject panelPuzzle;
    public GameObject imagenPistaPuzzle;
    public DialogueTrigger dialogoVictoria;
    public int taskID;

    [Header("Limpieza de Escena")]
    public DisparadorPuzzle disparador;

    [Header("Sonido de Victoria")]
    public AudioClip sonidoEngranajes;
    public float duracionSonido = 2.0f;
    private AudioSource altavoz;

    private bool victoriaFinalizada = false;

    // Esta variable guarda el estado mientras el juego esté ejecutándose
    // Al cerrar y abrir el juego, volverá a ser false automáticamente.
    private static bool yaCompletadoEstaSesion = false;

    void Awake() {
        Instance = this;
        altavoz = GetComponent<AudioSource>();
        if (altavoz == null) altavoz = gameObject.AddComponent<AudioSource>();
    }

    void Start() {
        // Si ya se completó desde que abriste el juego, limpiamos la escena
        if (yaCompletadoEstaSesion) {
            OcultarElementosCompletados();
        }
    }

    public void IntentarMover(PiezaEngranaje piezaClicada) {
        if (victoriaFinalizada || yaCompletadoEstaSesion) return;

        PiezaEngranaje hueco = piezas.Find(p => p.esHuecoVacio);
        if (hueco == null) return;

        float distancia = Vector2.Distance(piezaClicada.GetComponent<RectTransform>().anchoredPosition, hueco.GetComponent<RectTransform>().anchoredPosition);

        if (distancia < margenDistancia) {
            Vector2 posTemp = hueco.GetComponent<RectTransform>().anchoredPosition;
            hueco.MoverA(piezaClicada.GetComponent<RectTransform>().anchoredPosition);
            piezaClicada.MoverA(posTemp);
            Invoke("ComprobarVictoria", 0.4f);
        }
    }

    public void ComprobarVictoria() {
        if (victoriaFinalizada || yaCompletadoEstaSesion) return;

        int correctas = 0;
        foreach (PiezaEngranaje p in piezas) {
            if (p.EstaEnSuSitio()) correctas++;
        }

        if (correctas == piezas.Count) {
            victoriaFinalizada = true;
            yaCompletadoEstaSesion = true;
            StartCoroutine(SecuenciaVictoria());
        }
    }

    IEnumerator SecuenciaVictoria() {
        if (altavoz != null && sonidoEngranajes != null) {
            altavoz.clip = sonidoEngranajes;
            altavoz.Play();
            Invoke("DetenerAudio", duracionSonido);
        }

        if (TareasManager.Instance != null) TareasManager.Instance.CompleteTask(taskID);
        if (imagenPistaPuzzle != null) imagenPistaPuzzle.SetActive(false);

        Armario scriptArmario = Object.FindFirstObjectByType<Armario>();
        if (scriptArmario != null) scriptArmario.puzzleResuelto = true;

        yield return new WaitForSeconds(1.0f);

        if (panelPuzzle != null) panelPuzzle.SetActive(false);
        if (dialogoVictoria != null) dialogoVictoria.TriggerDialogue();
        if (disparador != null) disparador.FinalizarPuzzleYLimpiarEscena();
    }

    private void DetenerAudio() {
        if (altavoz != null && altavoz.isPlaying) altavoz.Stop();
    }

    private void OcultarElementosCompletados() {
        if (imagenPistaPuzzle != null) imagenPistaPuzzle.SetActive(false);
        if (panelPuzzle != null) panelPuzzle.SetActive(false);

        Armario scriptArmario = Object.FindFirstObjectByType<Armario>();
        if (scriptArmario != null) scriptArmario.puzzleResuelto = true;

        this.enabled = false;
    }
}