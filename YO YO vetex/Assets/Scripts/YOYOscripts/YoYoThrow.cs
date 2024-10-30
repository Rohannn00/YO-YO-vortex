using UnityEngine;

public class YoYoThrow : MonoBehaviour
{
    [Header("Yo-Yo Settings")]
    public GameObject yoYoPrefab; // Prefab of the yo-yo to be instantiated
    public float throwForce = 10f; // Force applied to throw the yo-yo
    public float returnDelay = 1.5f; // Time before the yo-yo returns
    public Transform yoYoSpawnPoint; // Point from where the yo-yo is thrown (e.g., player's hand)

    private GameObject activeYoYo; // Reference to the active yo-yo instance

    void Update()
    {
        // Check if the player presses the throw button (e.g., Left Mouse Button)
        if (Input.GetMouseButtonDown(0) && activeYoYo == null)
        {
            ThrowYoYo();
        }
    }

    void ThrowYoYo()
    {
        // Instantiate the yo-yo prefab at the spawn point's position and rotation
        activeYoYo = Instantiate(yoYoPrefab, yoYoSpawnPoint.position, yoYoSpawnPoint.rotation);

        // Calculate the direction from the player to the mouse position (where to throw)
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 throwDirection = (mousePosition - (Vector2)yoYoSpawnPoint.position).normalized;

        // Apply a force to the yo-yo in the calculated direction
        Rigidbody2D yoYoRb = activeYoYo.GetComponent<Rigidbody2D>();
        yoYoRb.velocity = throwDirection * throwForce;

        // Start the coroutine to return the yo-yo after a delay
        StartCoroutine(ReturnYoYoAfterDelay());
    }

    System.Collections.IEnumerator ReturnYoYoAfterDelay()
    {
        // Wait for the specified return delay
       yield return new WaitForSeconds(0.2f);
        // Code to return yo-yo instantly without delay
       
        // Return the yo-yo to the player
        if (activeYoYo != null)
        {
            ReturnYoYo();
        }
    }

    void ReturnYoYo()
    {
        // Return logic: move the yo-yo back towards the player
        Rigidbody2D yoYoRb = activeYoYo.GetComponent<Rigidbody2D>();

        // Calculate the direction back to the player and apply force
        Vector2 returnDirection = ((Vector2)yoYoSpawnPoint.position - (Vector2)activeYoYo.transform.position).normalized;
        yoYoRb.velocity = returnDirection * throwForce;

        // Optional: Destroy the yo-yo when it reaches close to the player
        Destroy(activeYoYo, 0.2f); // Delay to ensure the yo-yo reaches the player first
    }
}
