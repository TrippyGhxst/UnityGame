using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jump = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    private float Vector3jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);
    }
    void Update()
    {

    }
    void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    
   

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        {
            if (Input.GetKeyDown("space") && rb.transform.position.y <= 0.4f)
            {
                Vector3 jump = new Vector3(movementX, 200.0f, movementY);

                rb.AddForce(jump);
            }
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if(count >= 35)
        {
            winTextObject.SetActive(true);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

    }
    
      public void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "LevelWall" && count >= 12)
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "LevelWall2" && count >= 34)
        {
            Destroy(col.gameObject);
        }
        if (col.gameObject.name == "SpeedBoost")
        {
                speed = 25f;   
        }
        if (col.gameObject.name == "SpeedDown")
        {
            speed = 10f;
        }
    }

    
}
