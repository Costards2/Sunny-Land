using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavização do movimento
    public float offsetX = 0f; // Offset no eixo X

    private Vector3 initialPosition; // Posição inicial da câmera

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player não atribuído à câmera. Por favor, arraste o jogador no inspector.");
        }

        // Salva a posição inicial da câmera
        initialPosition = transform.position;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Calcula a nova posição somente no eixo X
            Vector3 targetPosition = new Vector3(player.position.x + offsetX, initialPosition.y, initialPosition.z);

            // Suaviza a transição
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
