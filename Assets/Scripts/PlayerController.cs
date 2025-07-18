using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private GameObject inventory;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) inventory.SetActive(!inventory.activeInHierarchy);
        moveSpeed = inventory.activeInHierarchy ? 0f : 5f;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed;
        float vertical = Input.GetAxis("Vertical") * moveSpeed;
        rb.linearVelocity = new Vector2(horizontal, vertical);
    }
}
