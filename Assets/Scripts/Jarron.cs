using System.Collections;
using UnityEngine;

public class Jarron : MonoBehaviour {
    [Header("Sprites")]
    public Sprite jarronEntero;  // El jarrón sano
    public Sprite jarronRoto;    // La imagen con todos los trozos juntos

    [Header("Configuración")]
    public SpriteRenderer render;
    public GameObject objetoGuardiaAActivar;
    public float segundosDeRetraso = 2.0f;
    public int taskID;

    [Header("Sonido")]
    public AudioClip sonidoRotura;
    private AudioSource altavoz;

    public void Romper() {

        if (altavoz != null && sonidoRotura != null) {
            altavoz.PlayOneShot(sonidoRotura);
        }

        if (render != null && jarronRoto != null) {
            render.sprite = jarronRoto;
        }

        if (GameManager.Instance != null) GameManager.Instance.jarronRoto = true;

        if (TareasManager.Instance != null && taskID != 0) {
            TareasManager.Instance.CompleteTask(taskID);
        }

        StartCoroutine(EsperarYActivarGuardia());
        Debug.Log("Jarrón sustituido por imagen rota. Tarea " + taskID + " completada.");
    }

    IEnumerator EsperarYActivarGuardia() {
        yield return new WaitForSeconds(segundosDeRetraso);
        if (objetoGuardiaAActivar != null) {
            objetoGuardiaAActivar.SetActive(true);
            Guardia g = objetoGuardiaAActivar.GetComponent<Guardia>();
            if (g != null) {
                g.EmpezarAMoverse();
            }
        }
    }
}