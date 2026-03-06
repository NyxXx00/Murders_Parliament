using System;
using UnityEngine;

public class RelojHUD : MonoBehaviour
{
    public RectTransform agujaHoras;

    [Header("Configuración del Tiempo")]
    public float horaInicial = 18f;
    public float velocidadDelTiempo = 10f;

    private float tiempoEnSegundos;
    const float gradosPorHora = 30f;

    void Start() {
        // Convertimos la hora de inicio a segundos totales
        tiempoEnSegundos = horaInicial * 3600f;
    }

    void Update() {
        // El tiempo avanza
        tiempoEnSegundos += Time.deltaTime * velocidadDelTiempo;

        // Calculamos la hora actual para la rotación
        float horasTotales = (tiempoEnSegundos / 3600f) % 12f;

        // Aplicamos la rotación
        if (agujaHoras != null) {
            agujaHoras.localRotation = Quaternion.Euler(0, 0, -horasTotales * gradosPorHora);
        }
    }

    // Función extra para que otros scripts sepan qué hora es
    public float GetHoraActual() {
        return (tiempoEnSegundos / 3600f) % 24f;
    }
}
