using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuInicialManager : MonoBehaviour {

    public string escena; // Nombre de la escena jugable

    public GameObject JugarBot;
    public GameObject SalirBot;
    public GameObject ControlBot;


    public GameObject ControlesPanel;
    public GameObject JugarPanel;
    public GameObject SalirPanel;

    public void MostrarTabs() {

        JugarPanel.SetActive(false);
        ControlesPanel.SetActive(false);
        SalirPanel.SetActive(false);

        JugarBot.GetComponent<Button>().image.color = new Color32(197, 175, 132, 255);
        ControlBot.GetComponent<Button>().image.color = new Color32(197, 175, 132, 255);
        SalirBot.GetComponent<Button>().image.color = new Color32(197, 175, 132, 255);
    }
    public void MostrarJugar() {

        MostrarTabs();
        JugarPanel.SetActive(true);
        JugarBot.GetComponent<Button>().image.color = new Color32(159, 124, 77, 255);
    }
    public void MostrarControles() {

        MostrarTabs();
        ControlesPanel.SetActive(true);
        ControlBot.GetComponent<Button>().image.color = new Color32(159, 124, 77, 255);
    }
    public void MostrarSalir() {

        MostrarTabs();
        SalirPanel.SetActive(true);
        SalirBot.GetComponent<Button>().image.color = new Color32(159, 124, 77, 255);
    }

    public void Jugar() {
        SceneManager.LoadScene(escena); // Carga la escena jugable
    }

    public void Salir() {
        Application.Quit(); // Sale del juego
    }
}
