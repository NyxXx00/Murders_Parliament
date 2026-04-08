    using UnityEngine;

    public class DoctoraItem : MonoBehaviour {

    private static DoctoraItem Instance;    

    public ItemData itemToGive;
        public int quantity = 1;
        public string taskID;

        [Header("Mecánica de Estrés")]
        public float tiempoLimite = 30f;
        private float cronometro;
        private bool retoActivo = false;

        private Inventario inventario;

        void Start() {
            inventario = Inventario.Instance;
        }

        void Update() {
            // Si el reto está activo y no has abierto el maletín, el tiempo corre
            if (retoActivo && !Maletin.Instance.maletinAbierto) {
                cronometro -= Time.deltaTime;
                if (cronometro <= 0) {
                    Debug.Log("Doctora: ¡Eres muy lento! Me voy a mi laboratorio.");
                    retoActivo = false;
                    // Opcional: gameObject.SetActive(false); para que desaparezca
                }
            }
        }

        void OnMouseDown() {
            // 1. ¿Ya abriste el maletín?
            if (Maletin.Instance != null && Maletin.Instance.maletinAbierto) {
                EntregarRecompensa();
                return;
            }

            // 2. Si no, te doy la pista y empieza el estrés
            if (!retoActivo) {
                Debug.Log("Doctora: ¡Rápido! El código es el año de Basil al revés. ¡TENGO PRISA!");
                retoActivo = true;
                cronometro = tiempoLimite;
            }
            else {
                Debug.Log("Doctora: ¡¿A qué esperas?! ¡Faltan " + (int)cronometro + " segundos!");
            }
        }

        public void EntregarRecompensa() {
            if (inventario.AddItem(itemToGive, quantity)) {
                Debug.Log("Doctora: ¡Al fin! Gracias. Toma la píldora.");

                if (TareasManager.Instance != null && taskID != "") {
                    TareasManager.Instance.CompleteTask(taskID);
                }
                Destroy(this); // Solo te lo da una vez
            }
            else {
                Debug.Log("Inventario lleno.");
            }
        }
    }