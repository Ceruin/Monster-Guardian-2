using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour, IDamageable
{
    public int health = 100; // You can adjust the initial health value as needed
    public GameObject cubeminPrefab;
    public Transform cubeminThrowPoint;
    public PlayerCursor cursor;

    public void Slap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 1f))
            {
                IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(1);
                }
            }
        }
    }

    public void Whistle(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            cursor.WhistleEffect();
        }
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GameObject cubemin = Instantiate(cubeminPrefab, cubeminThrowPoint.position, Quaternion.identity);
            cubemin.GetComponent<Rigidbody>().AddForce(transform.forward * 10, ForceMode.Impulse);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Handle player death, e.g., respawn or end game
            health = 0; // Ensure health doesn't go negative
            Debug.Log("Player has been defeated!");
            // You may want to add code here to trigger a death animation, reload the scene, etc.
        }
        else
        {
            Debug.Log("Player took damage! Remaining health: " + health);
            // You may want to add code here to trigger a damage animation, play a sound, etc.
        }
    }
}
