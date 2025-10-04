using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] float thrustForce = 5f;
    [SerializeField] float rotationSpeed = 120f;
    Vector2 thrustDirection;
    Rigidbody _rigidbody;

    public static int SCORE = 0;
    public static float xBorderLimit, yBorderLimit;
    [SerializeField] GameObject gun, bulletPrefab, miniMeteorPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody>();

        xBorderLimit = Camera.main.orthographicSize + 3;
        yBorderLimit = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    void FixedUpdate()
    {
        // obtenemos las pulsaciones de teclado
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce;
        // la dirección de empuje por defecto es .right (el eje X positivo)
        thrustDirection = transform.right;
        // rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation);
        // añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrust * thrustDirection);
    }

    // Update is called once per frame
    void Update()
    {
        correctPos();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();

            bullet.transform.position = gun.transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Bullet balaScript = bullet.GetComponent<Bullet>();
            balaScript.targetVector = transform.right;
            balaScript.miniMeteorPrefab = miniMeteorPrefab;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "MiniEnemy")
        {
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He colisionado con otra cosa...");
        }
    }

    private void correctPos()
    { 
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit + 1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.y = -yBorderLimit + 1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;
    }
}
