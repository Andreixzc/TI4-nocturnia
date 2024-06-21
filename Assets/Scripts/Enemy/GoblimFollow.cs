using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblimFollow : MonoBehaviour
{
    public Transform player; // Refer�ncia para o transform do player
    public float followDistance = 15.0f; // Dist�ncia a partir da qual o goblim come�a a seguir
    public float speed = 3.0f; // Velocidade com a qual o goblim segue

    void Update()
    {
        // Calcula a dist�ncia at� o player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verifica se o player est� dentro da dist�ncia para seguir
        if (distanceToPlayer < followDistance)
        {
            // Calcula a dire��o em dire��o ao player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move o goblim em dire��o ao player
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
