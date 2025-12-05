using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour {

    public static Inventario Instance { get; private set; }

    private ItemData ultimoObjetoRecogido = null;

    public RanuraInventario RanuraSeleccionada { get; private set; }
    public ItemData DatosItemSeleccionado => RanuraSeleccionada?.itemData;

    public delegate void AlCambiarSeleccion(ItemData itemData);
    public event AlCambiarSeleccion alCambiarSeleccion;

    // Referencias de la UI
    public RectTransform panelInventario;
    public Transform contenedorItems;
    public GameObject prefabRanuraCircular;

    [Header("UI de Crafteo")]
    public GameObject panelCrafteo;

    public int contadorRanuras = 6;

    private bool estaAbierto = false;
    public List<RanuraInventario> ranuras = new List<RanuraInventario>();

    private float ultimoTiempoAlternancia = -1f;
    private float enfriamientoAlternancia = 0.2f;

    public bool estaAbiertoUI => estaAbierto;

    private List<string> idsItemsInspeccionados = new List<string>();

    public void MarkItemInspected(string itemID) {
        if (!idsItemsInspeccionados.Contains(itemID)) {
            idsItemsInspeccionados.Add(itemID);
        }
        UpdateAllSlotUI();
    }

    public bool HasItemBeenInspected(string itemID) {
        return idsItemsInspeccionados.Contains(itemID);
    }

    private Canvas lienzoPersistente;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        lienzoPersistente = GetComponentInParent<Canvas>();
    }

    void OnEnable() {
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    private void AlCargarEscena(Scene scene, LoadSceneMode mode) {
        if (lienzoPersistente != null) {
            lienzoPersistente.gameObject.SetActive(false);
            lienzoPersistente.gameObject.SetActive(true);
        }

        estaAbierto = false;
        panelInventario.gameObject.SetActive(false);
        contenedorItems.gameObject.SetActive(false);
        if (panelCrafteo != null) {
            panelCrafteo.SetActive(false);
        }
        ultimoObjetoRecogido = null;
    }

    public void Start() {
        if (panelInventario == null || contenedorItems == null || prefabRanuraCircular == null) {
            Debug.LogError("Alguna referencia en Inventario no está asignada.");
            return;
        }

        for (int i = 0; i < contadorRanuras; i++) {
            GameObject objetoRanura = Instantiate(prefabRanuraCircular, contenedorItems);

            Image icono = objetoRanura.transform.Find("ItemIcon").GetComponent<Image>();
            Image resalteSecreto = objetoRanura.transform.Find("Highlight")?.GetComponent<Image>();
            Image resalteSeleccion = objetoRanura.transform.Find("SelectionHighlight")?.GetComponent<Image>();


            ranuras.Add(new RanuraInventario(null, 0, icono, resalteSecreto, resalteSeleccion));

            SlotHandler handler = objetoRanura.GetComponent<SlotHandler>();
            if (handler != null) {
                int indiceRanura = i;
                handler.SetInventory(this, indiceRanura);
            }
        }
    }

    public void Update() {
    }

    public void Toggle() {
        if (Time.time - ultimoTiempoAlternancia < enfriamientoAlternancia) return;
        estaAbierto = !estaAbierto;
        ultimoTiempoAlternancia = Time.time;

        panelInventario.gameObject.SetActive(estaAbierto);
        contenedorItems.gameObject.SetActive(estaAbierto);

        if (panelCrafteo != null) {
            panelCrafteo.SetActive(estaAbierto);
        }

        if (estaAbierto) {
            UpdateAllSlotUI();
        }
        else {
            DeselectItem();
            ultimoObjetoRecogido = null;
        }

        Debug.Log("Función Toggle llamada. Inventario Estado: " + (estaAbierto ? "ABIERTO" : "CERRADO"));
    }

    public void UpdateAllSlotUI() {
        foreach (var ranura in ranuras) {
            bool esRecienRecogido = (ranura.itemData != null && ranura.itemData == ultimoObjetoRecogido);
            ranura.ActualizarUI(esRecienRecogido);
        }
    }

    public bool AddItem(ItemData datosItem, int cantidad = 1) {
        foreach (var ranura in ranuras) {
            if (ranura.itemData == null) {
                ranura.itemData = datosItem;
                ranura.cantidad = 1;

                ultimoObjetoRecogido = datosItem;

                ranura.ActualizarUI(true);

                return true;
            }
        }
        return false;
    }

    public void HandleSlotInteraction(int indice, int contadorClic) {
        if (indice < 0 || indice >= ranuras.Count || ranuras[indice].itemData == null) {
            DeselectItem();
            return;
        }

        if (contadorClic == 2) {
            AttemptInspect(indice);
        }
        else if (contadorClic == 1) {
            OnSlotClicked(indice);
        }
    }

    public void AttemptInspect(int indice) {
        ItemData itemAInspeccionar = ranuras[indice].itemData;

        if (itemAInspeccionar != null && InspeccionManager.Instance != null) {
            DeselectItem();

            MarkItemInspected(itemAInspeccionar.ItemID);

            panelInventario.gameObject.SetActive(false);
            contenedorItems.gameObject.SetActive(false);

            if (panelCrafteo != null) {
                panelCrafteo.SetActive(false);
            }

            InspeccionManager.Instance.InspectItem(itemAInspeccionar);
        }
    }

    public void OnSlotClicked(int indice) {
        if (indice < 0 || indice >= ranuras.Count) return;

        RanuraInventario ranuraClicada = ranuras[indice];

        if (ranuraClicada.itemData == null) {
            DeselectItem();
            return;
        }

        if (RanuraSeleccionada == ranuraClicada) {
            DeselectItem();
        }
        else {
            if (RanuraSeleccionada != null) {
                RanuraSeleccionada.UpdateSelection(false);
            }

            RanuraSeleccionada = ranuraClicada;
            RanuraSeleccionada.UpdateSelection(true);

            if (InventarioCursor.Instance != null) {
                InventarioCursor.Instance.SeleccionarItem(DatosItemSeleccionado);
            }

            alCambiarSeleccion?.Invoke(DatosItemSeleccionado);

            Debug.Log("Objeto seleccionado: " + DatosItemSeleccionado.ItemName);
        }
    }

    public void DeselectItem() {
        if (RanuraSeleccionada != null) {
            RanuraSeleccionada.UpdateSelection(false);
        }

        RanuraSeleccionada = null;

        if (InventarioCursor.Instance != null) {
            InventarioCursor.Instance.VolverACursorBase();
        }

        alCambiarSeleccion?.Invoke(null);

        Debug.Log("Objeto deseleccionado.");
    }

    public bool HasItem(string itemID) {
        return ranuras.Exists(ranura => ranura.itemData != null && ranura.itemData.ItemID == itemID);
    }

    public bool RemoveItem(string itemID) {
        RanuraInventario ranuraAEliminar = ranuras.Find(ranura => ranura.itemData != null && ranura.itemData.ItemID == itemID);
        if (ranuraAEliminar != null) {
            ranuraAEliminar.itemData = null;
            ranuraAEliminar.cantidad = 0;
            ranuraAEliminar.ActualizarUI(false);

            if (RanuraSeleccionada == ranuraAEliminar) {
                DeselectItem();
            }
            return true;
        }
        return false;
    }

    [System.Serializable]
    public class RanuraInventario {

        public ItemData itemData;
        public int cantidad;

        public Image icono;
        public Image resalteSecreto;
        public Image resalteSeleccion;

        public RanuraInventario(ItemData i, int c, Image img, Image hSecreto, Image hSeleccion) {
            itemData = i;
            cantidad = c;
            icono = img;
            resalteSecreto = hSecreto;
            resalteSeleccion = hSeleccion;

            ActualizarUI(false);
            UpdateSelection(false);
        }

        public void ActualizarUI(bool esRecienRecogido) {
            bool tieneItem = itemData != null;

            if (icono != null) {
                icono.enabled = tieneItem;
                icono.sprite = itemData?.icon;
            }

            bool mostrarResalteSecreto = false;

            if (tieneItem && itemData.tieneSecreto) {
                if (Inventario.Instance != null &&
                    !Inventario.Instance.HasItemBeenInspected(itemData.ItemID)) {
                    mostrarResalteSecreto = true;
                }
            }

            if (resalteSeleccion != null) {
                if (resalteSeleccion.enabled) {
                    if (resalteSecreto != null) resalteSecreto.enabled = false;
                }
                else {
                    if (resalteSecreto != null) resalteSecreto.enabled = mostrarResalteSecreto;
                }
            }
            else if (resalteSecreto != null) {
                resalteSecreto.enabled = mostrarResalteSecreto;
            }
        }

        public void UpdateSelection(bool estaSeleccionado) {
            if (resalteSeleccion != null) {
                resalteSeleccion.enabled = estaSeleccionado;
            }
            if (resalteSecreto != null && estaSeleccionado) {
                resalteSecreto.enabled = false;
            }
        }
    }
}