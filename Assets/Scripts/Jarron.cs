using UnityEngine;
using System.Collections; // Importante para las Corrutinas

public class Jarron : MonoBehaviour {
    public Sprite jarronRotoSprite; // Arrastra el sprite de los cristales rotos aquí
    public GameObject objetoGuardiaAActivar; // Arrastra al Guardia aquí
    public float segundosDeRetraso = 2.0f; // Tiempo de espera

    public void Romper() {
        // 1. Efecto visual inmediato
        if (jarronRotoSprite != null) {
            GetComponent<SpriteRenderer>().sprite = jarronRotoSprite;
        }

        // 2. Avisamos al GameManager para otros eventos
        if (GameManager.Instance != null) {
            GameManager.Instance.jarronRoto = true;
        }

        // 3. Empezamos la cuenta atrás para el guardia
        StartCoroutine(EsperarYActivarGuardia());
        Debug.Log("Jarrón roto. Esperando a que el guardia aparezca.");
    }

    IEnumerator EsperarYActivarGuardia() {
        yield return new WaitForSeconds(segundosDeRetraso);

        // ¡Aparece el guardia!
        if (objetoGuardiaAActivar != null) {
            objetoGuardiaAActivar.SetActive(true);

            // IMPORTANTE: Le damos la orden directa de moverse
            Guardia scriptGuardia = objetoGuardiaAActivar.GetComponent<Guardia>();
            if (scriptGuardia != null) {
                scriptGuardia.EmpezarAMoverse();
            }
        }
    }
}