using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If encountering a hitbox
        if (collision.tag == "HitBox")
        {
            // Get the hitbox component
            HitBox hitBox = collision.GetComponent<HitBox>();
            // Apply the hit to the player
            playerController.ApplyKnockback(hitBox.knockback);
            playerController.ApplyDamage(hitBox.damage);
            playerController.ApplyHitstun(hitBox.hitstun);
            playerController.CheckForDeath(collision.transform.root.name);

            AudioManager.Instance.PlaySound(AudioManager.Instance.hitSound);
            Instantiate(UIManager.Instance.bloodEffect, transform.position, transform.rotation);
        }
    }

}
