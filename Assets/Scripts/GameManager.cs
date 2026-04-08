using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private Inventario inventario;

    // Almacena el id del punto de la última puerta que cruzó el jugador
    private string nextSpawnPointID;

    public bool cupcakeEnvenenado = false;
    public bool tieneAceite = false;
    public bool pasilloSaboteado = false;
    public bool jarronRoto = false;

    // Guardamos los IDs de los objetos ya recogidos
    private HashSet<string> pickedUpItems = new HashSet<string>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Metodo para registrar que un objeto ha sido recogido
    public void RegisterPickup(string id) {
        if (!string.IsNullOrEmpty(id) && !pickedUpItems.Contains(id)) {
            pickedUpItems.Add(id);
        }
    }

    // Metodo para consultar si un objeto ya fue recogido antes
    public bool IsItemPickedUp(string id) {
        return pickedUpItems.Contains(id);
    }

    private void Start() {
        // Buscamos el inventario al iniciar
        inventario = Inventario.Instance;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (inventario != null) {
                inventario.Toggle();
            }
            else {
                // Re-intentamos buscarlo si por alguna razón se perdió la referencia
                inventario = Inventario.Instance;
            }
        }
    }

    public void OptionMenu() {
        SceneManager.LoadScene("OptionsPanel");
    }

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }


    // Llamada por el script de la puerta de salida para guardar la ID.
    public void SetNextSpawnPoint(string spawnID) {
        nextSpawnPointID = spawnID;
    }

    // Llamada por el script en la escena de destino para saber dónde posicionar al jugador.
    public string GetNextSpawnPoint() {
        return nextSpawnPointID;
    }

    // Limpia la id después de usarla.
    public void ClearSpawnPoint() {
        nextSpawnPointID = string.Empty;
    }
}