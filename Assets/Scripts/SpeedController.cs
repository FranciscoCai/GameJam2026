using UnityEngine;
using UnityEngine.VFX;

public class SpeedController : MonoBehaviour
{
    public VisualEffect vfxGraph; // Arrastra aquí tu GameObject del VFX
    public Transform playerCamera; // Arrastra aquí tu Main Camera

    public float orbDistanceBehind = 10f; // Qué tan atrás estará el orbe
    public float spawnDistanceInFront = 5f; // (Opcional) Si necesitas ajustar el spawn

    private string orbProperty = "AtractorPOS";


    // Update is called once per frame
    void Update()
    {
        if (vfxGraph == null || playerCamera == null) return;

        // CALCULAR POSICIÓN DEL ORBE (DETRÁS)
        // Fórmula: Posición Cámara - (Vector Adelante * Distancia)
        Vector3 targetPosition = playerCamera.position - (playerCamera.forward * orbDistanceBehind);

        // Enviar el dato al VFX Graph
        vfxGraph.SetVector3(orbProperty, targetPosition);

        // OPCIONAL: MANTENER EL VFX PEGADO AL FRENTE
        // Esto mueve todo el sistema de partículas frente a la cámara
        transform.position = playerCamera.position + (playerCamera.forward * spawnDistanceInFront);

        // Alineamos la rotación para que coincida con la cámara
        transform.rotation = playerCamera.rotation * Quaternion.Euler(0, 90, 0);
        //transform.rotation = playerCamera.rotation;

    }
}
