using System.Collections.Generic;
using UnityEngine;

public class TareasManager : MonoBehaviour {
    public static TareasManager Instance;

    [Header("Prefab y contenedor")]
    public GameObject taskPrefab;
    public Transform taskContainer;

    [Header("Lista de Objetivos")]
    public Tarea[] tareasPredefinidas;   // Pon las 15 tareas aquí en orden

    private int proximaTareaParaMostrar = 0; // El índice de la siguiente tarea que aún no ha salido
    private Dictionary<int, ItemTareas> tasksInDisplay = new Dictionary<int, ItemTareas>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        // Al empezar, intentamos mostrar las 3 primeras tareas
        for (int i = 0; i < 3; i++) {
            ActivarSiguienteTareaDeLaLista();
        }
    }

    // Función que saca la siguiente tarea disponible del array
    private void ActivarSiguienteTareaDeLaLista() {
        if (proximaTareaParaMostrar < tareasPredefinidas.Length) {
            Tarea t = tareasPredefinidas[proximaTareaParaMostrar];

            if (int.TryParse(t.id, out int idNumerico)) {
                AddTask(idNumerico, t.descripcion);
                proximaTareaParaMostrar++;
            }
        }
    }

    public void AddTask(int id, string descripcion) {
        if (tasksInDisplay.ContainsKey(id)) return;

        GameObject obj = Instantiate(taskPrefab, taskContainer);
        ItemTareas item = obj.GetComponent<ItemTareas>();

        if (item != null) {
            item.Initialize(id.ToString(), descripcion);
            tasksInDisplay.Add(id, item);
        }
    }

    public void CompleteTask(int id) {
        if (!tasksInDisplay.ContainsKey(id)) return;

        // 1. Borramos la tarea que el jugador acaba de hacer
        ItemTareas itemAEliminar = tasksInDisplay[id];
        tasksInDisplay.Remove(id);
        Destroy(itemAEliminar.gameObject);

        // 2. Metemos la siguiente tarea de la cola para volver a tener 3
        // Usamos un pequeño delay para que visualmente se vea primero cómo suben las otras
        Invoke("ActivarSiguienteTareaDeLaLista", 0.3f);

        Debug.Log("Tarea " + id + " completada. Quedan " + (tareasPredefinidas.Length - proximaTareaParaMostrar + tasksInDisplay.Count) + " tareas en total.");
    }
}