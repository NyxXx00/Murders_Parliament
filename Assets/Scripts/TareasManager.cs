using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TareasManager : MonoBehaviour
{
    public static TareasManager Instance;

    [Header("Prefab y contenedor")]
    public GameObject taskPrefab;        // ItemTareas
    public Transform taskContainer;      // TareasCont

    [Header("Tareas")]
    public Tarea[] tareas;   // Lista de tareas que quieres que aparezcan al iniciar

    private Dictionary<string, ItemTareas> tasks = new Dictionary<string, ItemTareas>();

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() {
        // Crear automáticamente todas las tareas predefinidas
        foreach (Tarea t in tareas) {
            AddTask(t.id, t.descripcion);
        }
    }

    // Crear tarea y añadirla al diccionario
    public void AddTask(string id, string descripcion) {
        if (tasks.ContainsKey(id)) return;

        GameObject obj = Instantiate(taskPrefab, taskContainer);
        ItemTareas item = obj.GetComponent<ItemTareas>();

        if (item != null) {
            item.Initialize(id, descripcion);
            tasks.Add(id, item);
        }
    }

    // Marcar tarea como completada
    public void CompleteTask(string id) {
        if (!tasks.ContainsKey(id)) return;

        tasks[id].Complete();
    }
}
