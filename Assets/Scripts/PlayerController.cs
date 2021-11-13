using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    //sachen mehr counts geben als anderen
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winGameObject;
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    float timeLeft = 35.0f;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        setCountText();
        winGameObject.SetActive(false);
    }

    private void OnMove(InputValue movementValue)
    {
            Vector2 movementVector = movementValue.Get<Vector2>();

            movementX = movementVector.x;
            movementY = movementVector.y;
    }

    private void Update()
    {
        if (count <= 10)
            timeLeft -= Time.deltaTime;
        text.text = "Time Left: " + Mathf.Round(timeLeft);
        if (timeLeft < 0)
        {
            text.fontSize = 50;
            text.fontStyle= FontStyle.Bold;
            //text.transform.position = new Vector3(-200, -30, 0);
            text.color = Color.red;
            text.text = " Lost";
            this.gameObject.SetActive(false);
        }
        
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            setCountText();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Restart"))
        {
            Vector3 start = new Vector3(0, 0, 0);
            this.transform.position = start;

        }

    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 10)
        {
            winGameObject.SetActive(true);
            countText.color = Color.green;
            text.gameObject.SetActive(false);
        
        }
    }
}
