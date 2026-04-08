using System.Collections.Generic;
using UnityEngine;

// Mantenemos tus clases exactamente igual
[System.Serializable]
public class DialogueCharacter {
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine {
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue {
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour {
    [Header("Configuración de Diálogos")]
    public Dialogue dialogoInicial;     // Lo que dice la primera vez
    public Dialogue dialogoProgreso;    // Lo que dice si no ha cumplido el objetivo
    public Dialogue dialogoFinalizado;  // Lo que dice cuando ya terminó su tarea

    [Header("Estado de la Misión")]
    public bool objetivoCumplido = false; // Esta casilla la cambias por código o a mano

    private bool hasTriggered = false;

    public void TriggerDialogue() {
        if (DialogueManager.Instance == null) return;

        // 1. Si ya se cumplió el objetivo (ej: maletín abierto, llave entregada...)
        if (objetivoCumplido) {
            DialogueManager.Instance.StartDialogue(dialogoFinalizado);
        }
        // 2. Si es la primera vez que hablamos
        else if (!hasTriggered) {
            DialogueManager.Instance.StartDialogue(dialogoInicial);
            hasTriggered = true;
        }
        // 3. Si ya hablamos pero aún no cumple el objetivo
        else {
            DialogueManager.Instance.StartDialogue(dialogoProgreso);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            TriggerDialogue();
        }
    }
}