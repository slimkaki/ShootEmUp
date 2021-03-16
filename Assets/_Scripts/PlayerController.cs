using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SteerableBehaviour, IShooter, IDamageable {

    Animator animator;
    public GameObject bullet;
    public GameObject backwardsBullet;
    public float shootDelay = 0.0f;
    private float _lastShootTimestamp = 0.0f;
    public AudioClip shootSFX;
    public GameManager gm;
    public bool olhandoParaDireita = true;

    public void Start() {
        animator = GetComponent<Animator>();
        gm = GameManager.GetInstance();
    }
  
    void FixedUpdate() {
        if (gm.gameState != GameManager.GameState.GAME) return;
        if (Input.GetKeyDown(KeyCode.Escape) && gm.gameState == GameManager.GameState.GAME) {
            gm.ChangeState(GameManager.GameState.PAUSE);
        }
        float yInput = Input.GetAxis("Vertical");
        float xInput = Input.GetAxis("Horizontal");

        if (Input.GetAxisRaw("Jump") != 0) {
            Shoot();
        }

        if ((transform.position.y > 4.5f && yInput > 0) || (transform.position.y < -4.9f &&yInput < 0)) {
            yInput = 0;
        }

        if (xInput < 0 && olhandoParaDireita) {
            Vira();
        } else if (xInput > 0 && !olhandoParaDireita) {
            Vira();
        }

        Thrust(xInput, yInput);
        if (yInput != 0 || xInput != 0) {
            animator.SetFloat("Velocity", 1.0f);
        } else {
            animator.SetFloat("Velocity", 0.0f);
        }        
    }

    void Vira() {
        olhandoParaDireita = !olhandoParaDireita;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    
    public void Shoot() {
        if (Time.time - _lastShootTimestamp < shootDelay) return;
        AudioManager.PlaySFX(shootSFX);
        _lastShootTimestamp = Time.time;
        if (olhandoParaDireita) {
            Instantiate(bullet, transform.position + new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
        } else {
            Instantiate(backwardsBullet, transform.position - new Vector3(1.0f, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    public void TakeDamage() {
        gm.vidas--;
        if (gm.vidas <= 0) Die();
    }

    public void Die() {
        gm.ChangeState(GameManager.GameState.ENDGAME);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Inimigos") || collision.CompareTag("Asteroide") ) {
            Destroy(collision.gameObject);
            TakeDamage();
        }
    }    
}
