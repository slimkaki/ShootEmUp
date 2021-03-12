﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable {

    Animator animator;
    public GameObject bullet;
    public float shootDelay = 0.0f;
    private float _lastShootTimestamp = 0.0f;
    public AudioClip shootSFX;
    public GameManager gm;

    public void Start() {
        animator = GetComponent<Animator>();
        gm = GameManager.GetInstance();
    }
  
    void FixedUpdate() {
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        if (Input.GetAxisRaw("Jump") != 0) {
            Shoot();
        }

        if ((transform.position.y > 4.5f && yInput > 0) || (transform.position.y < -4.9f &&yInput < 0)) {
            yInput = 0;
        }

        Thrust(xInput, yInput);
        if (yInput != 0 || xInput != 0) {
            animator.SetFloat("Velocity", 1.0f);
        } else {
            animator.SetFloat("Velocity", 0.0f);
        }        
    }    
    
    public void Shoot() {
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);
        _lastShootTimestamp = Time.time;
        Instantiate(bullet, transform.position + new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
        // Instantiate(bullet, transform.position + new Vector3(1.0f, 0.05f, 0.0f), Quaternion.identity);
    }

    public void TakeDamage() {
        gm.vidas--;
        if (gm.vidas <= 0) Die();
    }

    public void Die() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inimigos")) {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }    
}
