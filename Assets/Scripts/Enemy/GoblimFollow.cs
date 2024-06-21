using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblimFollow : MonoBehaviour
{
    public Transform player; // Referência para o transform do player
    public float followDistance = 15.0f; // Distância a partir da qual o goblim começa a seguir
    public float speed = 3.0f; // Velocidade com a qual o goblim segue

    void Update()
    {
        // Calcula a distância até o player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verifica se o player está dentro da distância para seguir
        if (distanceToPlayer < followDistance)
        {
            // Calcula a direção em direção ao player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move o goblim em direção ao player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
