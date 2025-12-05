using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    private Inventario inventario;

    private void Start() {
        inventario = Inventario.Instance;
        if (inventario == null) {
            Debug.LogError("GameManager: No se encontró la instancia del Inventario.");
        }
    }

    void Update() {

        //comprobacion de tecla para abrir inventario
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Tecla 'E' detectada. Toggle Inventario.");

            if (inventario != null) {
                inventario.Toggle(); // Llama a la función de apertura/cierre del Inventario.
            }
        }
    }

    public void OptionMenu() {
        SceneManager.LoadScene("OptionsPanel");
    }

    public void QuitGame() {
        SceneManager.LoadScene("MainMenu");
    }

    // Almacena el id del punto de la última puerta que cruzó el jugador
    private string nextSpawnPointID;

    private void Awake() {
        //Singleton
        if (Instance != null && Instance != this) {
            // Si ya existe una instancia, destruye este nuevo objeto
            Destroy(gameObject);
            return;
        }

        //Asigna esta instancia como la única instancia activa.
        Instance = this;

        //Evita que el objeto se destruya al cargar una nueva escena.
        DontDestroyOnLoad(gameObject);
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