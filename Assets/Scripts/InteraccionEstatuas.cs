using UnityEngine;

public class InteraccionEstatuas : MonoBehaviour {
    [Header("Referencia al Puzzle")]
    public GameObject controladorMosaico;

    private void OnMouseDown() {
        if (controladorMosaico != null) {
            // Activamos el panel
            controladorMosaico.GetComponent<PuzzleMosaico>().AbrirPuzzle();
            // Forzamos que se vea (por si el Alpha del CanvasGroup estaba en 0)
            CanvasGroup cg = controladorMosaico.GetComponent<CanvasGroup>();
            if (cg != null) {
                cg.alpha = 1;
                cg.interactable = true;
                cg.blocksRaycasts = true;
            }

            Debug.Log("Has pinchado en la estatua: " + gameObject.name);
        }
    }
}