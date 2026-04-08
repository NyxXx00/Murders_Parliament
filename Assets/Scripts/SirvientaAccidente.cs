using UnityEngine;

public class SirvientaAccidente : MonoBehaviour
{
    public GameObject jarron;

    private void OnTriggerEnter2D(Collider2D other) {
        // Si pisa el trigger del suelo que tiene el tag "ZonaAceite"
        if (other.CompareTag("ZonaAceite") && GameManager.Instance.pasilloSaboteado) {
            Resbalar();
        }
    }

    void Resbalar() {
        if (GetComponent<Patrulla>() != null) GetComponent<Patrulla>().enabled = false;
        if (jarron != null) jarron.GetComponent<Jarron>().Romper();
        Debug.Log("¡La sirvienta se ha escurrido!");
    }
}
