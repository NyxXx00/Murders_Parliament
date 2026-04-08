using UnityEngine;
using System.Collections;

public class ActivarSirvienta : MonoBehaviour {

    public GameObject sirvientaObjeto;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && GameManager.Instance.tieneAceite) {
            sirvientaObjeto.SetActive(true);
            Destroy(gameObject);
        }
    }

}
