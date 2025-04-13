using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ParalaxMovement : MonoBehaviour
{
    [Tooltip("Array de Transform de los objetos background en la escena.")]
    public Transform[] backgrounds;
    [Tooltip("Array de escala de parallax para cada background (a mayor valor, mayor desplazamiento relativo).")]
    public float[] parallaxScales;
    [Tooltip("Factor de suavizado para el movimiento (recomendado > 0).")]
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // Si no se asigna manualmente la escala, se calculará en base a la posición Z del background
        if (parallaxScales == null || parallaxScales.Length != backgrounds.Length)
        {
            parallaxScales = new float[backgrounds.Length];
            for (int i = 0; i < backgrounds.Length; i++)
            {
                // Cuanto mayor la diferencia en Z respecto a la cámara, mayor es el efecto parallax
                parallaxScales[i] = backgrounds[i].position.z - cam.position.z;
            }
        }
    }

    void LateUpdate()
    {
        // Calcula el movimiento de la cámara desde el último frame
        Vector3 deltaMovement = cam.position - previousCamPos;

        // Actualiza la posición de cada background en función del desplazamiento de la cámara
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaxX = deltaMovement.x * parallaxScales[i] * smoothing;

            Vector3 targetPosition = backgrounds[i].position;
            targetPosition.x += parallaxX;

            // Aplicamos la interpolación para un movimiento suave (puedes quitar Lerp si deseas un movimiento inmediato)
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, targetPosition, smoothing * Time.deltaTime);
        }

        // Guarda la posición actual de la cámara para el siguiente cálculo
        previousCamPos = cam.position;
    }
}
