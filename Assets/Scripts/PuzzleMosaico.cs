using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMosaico : MonoBehaviour {

    [Header("Control de Visibilidad")]
    public GameObject panelVisual; // ARRASTRA AQUÍ EL 'PUZZLEGIRATORIO'

    [Header("Configuración de Sonido")]
    public AudioSource fuenteAudio;
    public float inicioBoton = 0.1f;
    public float duracionBoton = 1.0f;
    public float inicioMosaico = 0.3f;
    public float duracionMosaico = 1.0f;

    [Header("Sprites de las Piezas")]
    public Sprite[] spritesGrande;
    public Sprite[] spritesMediana;
    public Sprite[] spritesCentro;

    [Header("Referencias UI (Imágenes hijas)")]
    public Image displayGrande;
    public Image displayMediana;
    public Image displayCentro;

    [Header("Combinación Ganadora")]
    public int solGrande = 0;
    public int solMediana = 1;
    public int solCentro = 1;

    private int estadoGrande = 0;
    private int estadoMediana = 0;
    private int estadoCentro = 0;

    [Header("Recompensas")]
    public GameObject objetoPiedra;
    public GameObject grupoSirvientas;
    public GameObject flechaGuia;

    private static bool completadoEnSesion = false;

    void Awake() {
        // Si ya se ganó, que no aparezca nada al recargar escena
        if (completadoEnSesion) {
            SolucionInmediata();
        }
        else {
            if (panelVisual != null) panelVisual.SetActive(false);
        }
    }

    // FUNCIÓN PARA LAS ESTATUAS
    public void AbrirPuzzle() {
        if (completadoEnSesion) return;

        if (panelVisual != null) {
            panelVisual.SetActive(true);
            // Forzamos posición al centro por si acaso
            RectTransform rt = panelVisual.GetComponent<RectTransform>();
            if (rt != null) rt.anchoredPosition = Vector2.zero;
        }
    }

    public void ClickBoton(string pieza) {
        if (completadoEnSesion) return;

        // Sonido de giro
        StopAllCoroutines();
        StartCoroutine(ReproducirSegmento(inicioBoton, duracionBoton));
        Invoke("ReproducirGiroMosaico", 0.15f);

        if (pieza == "grande") {
            estadoGrande = (estadoGrande + 1) % 4;
            displayGrande.sprite = spritesGrande[estadoGrande];
        }
        else if (pieza == "mediana") {
            estadoMediana = (estadoMediana + 1) % 4;
            displayMediana.sprite = spritesMediana[estadoMediana];
        }
        else if (pieza == "centro") {
            estadoCentro = (estadoCentro + 1) % 4;
            displayCentro.sprite = spritesCentro[estadoCentro];
        }

        ComprobarVictoria();
    }

    void ReproducirGiroMosaico() {
        StartCoroutine(ReproducirSegmento(inicioMosaico, duracionMosaico));
    }

    IEnumerator ReproducirSegmento(float startTime, float duration) {
        if (fuenteAudio != null && fuenteAudio.clip != null) {
            fuenteAudio.time = startTime;
            fuenteAudio.Play();
            yield return new WaitForSeconds(duration);
            fuenteAudio.Stop();
        }
    }

    void ComprobarVictoria() {
        if (estadoGrande == solGrande && estadoMediana == solMediana && estadoCentro == solCentro) {
            Ganar();
        }
    }

    void Ganar() {
        completadoEnSesion = true;
        if (objetoPiedra != null) objetoPiedra.SetActive(true);
        if (grupoSirvientas != null) grupoSirvientas.SetActive(true);

        // Bloquear interacción
        CanvasGroup cg = panelVisual.GetComponent<CanvasGroup>();
        if (cg != null) cg.interactable = false;

        Invoke("CerrarPanelGanador", 0.5f);

        if (objetoPiedra != null) objetoPiedra.SetActive(true);
        if (grupoSirvientas != null) {
            grupoSirvientas.SetActive(true);
            StartCoroutine(ActivarSirvientasEscalonado());
        }
        System.Collections.IEnumerator ActivarSirvientasEscalonado() {
            foreach (Transform hija in grupoSirvientas.transform) {
                hija.gameObject.SetActive(true);
                Debug.Log("Sirvienta activada: " + hija.name);

                // Espera 2 segundos antes de activar a la siguiente
                yield return new WaitForSeconds(2.0f);
            }
            if (flechaGuia != null) {
                flechaGuia.SetActive(true);
                Debug.Log("Flecha de lanzamiento activada.");
            }
        }
    }

    void CerrarPanelGanador() {
        if (panelVisual != null) {
            panelVisual.SetActive(false);
        }
    }

    public void RecogerPiedra() {
        if (panelVisual != null) panelVisual.SetActive(false);
        if (flechaGuia != null) flechaGuia.SetActive(true);
    }

    void SolucionInmediata() {
        if (objetoPiedra != null) objetoPiedra.SetActive(true);
        if (grupoSirvientas != null) grupoSirvientas.SetActive(true);
        if (panelVisual != null) panelVisual.SetActive(false);
    }
}