using TMPro;
using UnityEngine;

public class Maletin : MonoBehaviour {
    public static Maletin Instance;

    [Header("Configuración")]
    public string codigoCorrecto = "3891";
    public bool maletinAbierto = false;

    [Header("Referencias UI")]
    public GameObject panelTeclado;
    public TMP_InputField inputCodigo;
    public DoctoraItem doctoraScript;

    void Awake() {
        if (Instance == null) Instance = this;
    }

    void OnMouseDown() {
        // 1. SEGURO: Si la PLACA está abierta, no abras el maletín
        if (Placa.Instance != null && Placa.Instance.panelPlaca.activeSelf) {
            return;
        }

        // 2. Si el propio panel del maletín ya está abierto, no hagas nada
        if (panelTeclado.activeSelf) return;

        if (maletinAbierto) return;

        AbrirPanel();
    }

    public void ValidarCodigo() {
        if (inputCodigo.text == codigoCorrecto) {
            maletinAbierto = true;
            panelTeclado.SetActive(false);

            if (doctoraScript != null) {
                DialogueTrigger trigger = doctoraScript.GetComponent<DialogueTrigger>();
                if (trigger != null) trigger.objetivoCumplido = true;
                doctoraScript.EntregarRecompensa();
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

    void AbrirPanel() {
        panelTeclado.SetActive(true);
        if (inputCodigo != null) {
            inputCodigo.text = "";
            inputCodigo.ActivateInputField();
        }
        Debug.Log("Maletín abierto. Ahora el FondoBloqueador protegerá la escena.");
    }
}