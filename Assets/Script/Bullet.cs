using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{

    [SerializeField] int speed = 10;
    [SerializeField] float maxLifeTime = 3;
    public Vector3 targetVector;

    public GameObject miniMeteorPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DisableAfterTime(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        // la bala se mueve en la dirección del jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            IncreaseScore();

            gameObject.SetActive(false);

            SplitAsteroid(other.transform.position, other.transform.rotation);
            
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("MiniEnemy"))
        {
            IncreaseScore();

            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }

    private void IncreaseScore()
    {
        Player.SCORE++;
        Debug.Log(Player.SCORE);
        UpgradeScoreText();
    }

    private void UpgradeScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Puntos: " + Player.SCORE;
    }

    private void SplitAsteroid(Vector3 position, Quaternion rotation)
    {
        Vector2 bisectorDirection = Quaternion.Euler(0, 0, 45) * targetVector.normalized;

        Vector2 miniMeteorDirection1 = Quaternion.Euler(0, 0, 22.5f) * bisectorDirection;
        Vector2 miniMeteorDirection2 = Quaternion.Euler(0, 0, -22.5f) * bisectorDirection;

        GameObject miniMeteor1 = Instantiate(miniMeteorPrefab, position, rotation);
        GameObject miniMeteor2 = Instantiate(miniMeteorPrefab, position, rotation);

        miniMeteor1.GetComponent<MiniMeteorController>().moveDirection = miniMeteorDirection1;
        miniMeteor2.GetComponent<MiniMeteorController>().moveDirection = miniMeteorDirection2;
    }
    
    private IEnumerator DisableAfterTime(GameObject obj)
    {
        yield return new WaitForSeconds(maxLifeTime);

        if (obj != null && obj.activeSelf)
        {
            obj.SetActive(false);
        }
    }
}
