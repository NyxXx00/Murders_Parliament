using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleMosaico : MonoBehaviour {

    [Header("Configuración de Sonido")]
    public AudioSource fuenteAudio;

    [Header("Tiempos del Clip (en segundos)")]
    public float inicioBoton = 0.0f;
    public float duracionBoton = 0.3f;
    public float inicioMosaico = 0.5f;
    public float duracionMosaico = 1.0f;

    [Header("Sprites de las Piezas (4 por cada una)")]
    public Sprite[] spritesGrande;
    public Sprite[] spritesMediana;
    public Sprite[] spritesCentro;

    [Header("Referencias de la UI (Images)")]
    public Image displayGrande;
    public Image displayMediana;
    public Image displayCentro;

    [Header("Combinación Ganadora (0 a 3)")]
    public int solGrande = 0;
    public int solMediana = 0;
    public int solCentro = 0;

    // Estados actuales
    private int estadoGrande = 0;
    private int estadoMediana = 0;
    private int estadoCentro = 0;

    [Header("Recompensa e Infiltración")]
    public GameObject objetoPiedra;     // La piedra que aparece en el suelo
    public GameObject grupoSirvientas;  // El objeto PADRE que tiene a todas las sirvientas
    public GameObject flechaGuia;       // La flecha de la cocina

    public void ClickBoton(string pieza) {
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
        if (fuenteAudio != null) {
            fuenteAudio.time = startTime;
            fuenteAudio.Play();
            yield return new WaitForSeconds(duration);
            fuenteAudio.Stop();
        }
    }

    void ComprobarVictoria() {
        if (estadoGrande == solGrande && estadoMediana == solMediana && estadoCentro == solCentro) {
            GanarPuzzle();
        }
    }

    void GanarPuzzle() {
        Debug.Log("¡Mosaico Completado!");
        // 1. Aparece la piedra en el suelo
        if (objetoPiedra != null) objetoPiedra.SetActive(true);

        // 2. Aparecen las sirvientas en el jardín
        if (grupoSirvientas != null) grupoSirvientas.SetActive(true);

        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg != null) cg.interactable = false;
    }

    // Esta función se llama desde el script de la piedra al recogerla
    public void RecogerPiedra() {
        // Desactivar el panel del puzzle (UI)
        gameObject.SetActive(false);

        // Activar la flecha indicadora en la cocina
        if (flechaGuia != null) flechaGuia.SetActive(true);

        Debug.Log("Piedra recogida. Sirvientas activas. Flecha activada.");
    }
}