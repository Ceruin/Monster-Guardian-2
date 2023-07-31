using System.Collections;
using UnityEngine;

public class CubeminActions : MonoBehaviour, IDamageable
{
    public int health = 30; // You can adjust the initial health value as needed

    public int attackPower = 1;
    public GameObject enemy;

    public void AttackEnemy()
    {
        transform.SetParent(enemy.transform);
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        while (true)
        {
            enemy.GetComponent<IDamageable>().TakeDamage(attackPower);
            yield return new WaitForSeconds(1);
        }
    }

    public void StopAttacking()
    {
        StopAllCoroutines();
        transform.SetParent(null);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            // Handle Cubemin death, e.g., play animation and remove from the game
            health = 0; // Ensure health doesn't go negative
            Debug.Log("Cubemin has been defeated!");
            Destroy(gameObject); // Removes the Cubemin from the game
                                 // You may want to add additional code to handle specific effects or logic when a Cubemin is defeated.
        }
        else
        {
            Debug.Log("Cubemin took damage! Remaining health: " + health);
            // You may want to add code here to trigger a damage animation, play a sound, etc.
        }
    }

}
