using UnityEngine;
using TMPro;

public class Maletin : MonoBehaviour {
    public static Maletin Instance;

    [Header("Configuración")]
    public string codigoCorrecto = "3891";
    public bool maletinAbierto = false;

    [Header("Referencias UI")]
    public GameObject panelTeclado;
    public TMP_InputField inputCodigo;
    public DoctoraItem doctoraScript; // Aquí tienes arrastrada a la Doctora

    void Awake() {
        if (Instance == null) Instance = this;
    }

    void OnMouseDown() {
        if (!maletinAbierto) {
            panelTeclado.SetActive(true);
            inputCodigo.text = "";
            inputCodigo.ActivateInputField();
        }
        else {
            Debug.Log("El maletín ya está vacío.");
        }
    }

    public void ValidarCodigo() {
        if (inputCodigo == null) {
            Debug.LogError("¡Oye! Se te olvidó arrastrar el InputField.");
            return;
        }

        if (inputCodigo.text == codigoCorrecto) {
            maletinAbierto = true;
            panelTeclado.SetActive(false);

            if (doctoraScript != null) {

                // Buscamos el script de diálogo que está en el mismo objeto que la doctora
                DialogueTrigger trigger = doctoraScript.GetComponent<DialogueTrigger>();
                if (trigger != null) {
                    trigger.objetivoCumplido = true; // <--- AQUÍ se activa el cambio de frase
                }
                // ------------------------

                doctoraScript.EntregarRecompensa();
            }
            else {
                Debug.LogWarning("Código correcto, pero falta asignar doctoraScript.");
            }
        }
        else {
            Debug.Log("Código erróneo");
            inputCodigo.text = "";
        }
    }

    public void CerrarPanel() {
        panelTeclado.SetActive(false);
    }
}