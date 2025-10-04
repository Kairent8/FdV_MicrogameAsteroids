using System.IO.Compression;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] GameObject asteroidPrefab;
    [SerializeField] float spawnRatePerMinute = 30f;
    [SerializeField] float spawnRateIncrement = 1f;
    [SerializeField] float xBorderLimit = 10, yBorderLimit = 8;
    private float spawnNext = 0;

    public float maxTimeLife = 4f;

    // Update is called once per frame
    void Update()
    {
        // instanciamos enemigos sólo si ha pasado tiempo suficiente desde el último.
        if (Time.time > spawnNext)
        {
            // indicamos cuándo podremos volver a instanciar otro enemigo
            spawnNext = Time.time + 60 / spawnRatePerMinute;
            // con cada spawn hay mas asteroides por minuto para incrementar la dificultad
            spawnRatePerMinute += spawnRateIncrement;
            // guardamos un punto aleatorio entre las esquinas superiores de la pantalla
            var rand = Random.Range(-xBorderLimit, xBorderLimit);
            var spawnPosition = new Vector2(rand, yBorderLimit);

            // instanciamos el asteroide en el punto y con el ángulo aleatorios
            GameObject meteor = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
            Destroy(meteor, maxTimeLife);
        }
    }
}
