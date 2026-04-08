using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance;

    public event Action OnDialogueEnd;

    [Header("UI Elements")]
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    [Header("Settings")]
    public float typingSpeed = 0.05f;
    public Animator animator;

    private bool isTyping = false;
    private string currentSentence; // Guardamos la frase actual completa

    private Queue<DialogueLine> lines;
    private DialogueLine currentLine;

    private bool isDialogueActive = false;
    private bool isPanelVisible = false;
    private bool isHiding = false;

    private Coroutine typingCoroutine;

    private void Awake() {
        if (Instance == null) Instance = this;
        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialogue dialogue) {
        if (dialogue == null || dialogue.dialogueLines.Count == 0) return;
        if (isDialogueActive || isPanelVisible || isHiding) return;

        isDialogueActive = true;
        lines.Clear();
        foreach (DialogueLine line in dialogue.dialogueLines)
            lines.Enqueue(line);

        ShowPanel();
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine() {
        // Si ya estamos cerrando, no hagas nada
        if (!isDialogueActive || isHiding) return;

        if (lines.Count == 0) {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();

        dialogueArea.text = "";

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;
        currentSentence = currentLine.line;

        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    public void EndDialogue() {
        if (!isDialogueActive || isHiding) return;

        isDialogueActive = false;

        dialogueArea.text = "";
        characterName.text = "";
        lines.Clear();

        if (typingCoroutine != null) {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }

        HidePanel();
        OnDialogueEnd?.Invoke();
    }

    private IEnumerator TypeSentence(string sentence) {
        dialogueArea.text = "";
        isTyping = true;

        foreach (char letter in sentence.ToCharArray()) {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null; // Limpiamos la referencia al terminar
    }
    public void SkipOrAdvance() {
        if (!isDialogueActive || isHiding) return;

        if (isTyping) {
            if (typingCoroutine != null) StopCoroutine(typingCoroutine);
            dialogueArea.text = currentSentence;
            isTyping = false;
            typingCoroutine = null;
        }
        else {
            DisplayNextDialogueLine();
        }
    }

    private void ShowPanel() {
        if (isPanelVisible) return;

        CanvasGroup cg = animator.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 1; // Hacerlo visible

        animator.gameObject.SetActive(true);
        animator.Play("show");
        isPanelVisible = true;
    }

    private IEnumerator HidePanelCoroutine() {
        isHiding = true;

        CanvasGroup cg = animator.GetComponent<CanvasGroup>();
        if (cg != null) cg.alpha = 0;

        animator.Play("hide");

        yield return new WaitForSeconds(0.5f);

        animator.gameObject.SetActive(false);
        isPanelVisible = false;
        isHiding = false;
    }
    private void HidePanel() {
        if (!isPanelVisible || isHiding) return;
        StartCoroutine(HidePanelCoroutine());
    }
}