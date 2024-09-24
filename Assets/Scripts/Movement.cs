using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Bullet bulletPrefab;

    public float movspeed = 1.0f;
    public float bulletOffset = 0.25f;

    private Rigidbody2D _rigidbody;

    private bool _Forward;
    private bool _Backward;

    public int Buffed = 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        _Forward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        _Backward = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        
        if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0)){
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (_Forward)
        {
            _rigidbody.AddForce(this.transform.up * this.movspeed);
        }
        if (_Backward)
        {
            _rigidbody.AddForce(-this.transform.up * this.movspeed);
        }

        RotateTowardsMouse();
    }

    private void RotateTowardsMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector2 direction = (mousePos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        _rigidbody.MoveRotation(angle - 90);
    }

    private void Shoot(){
        if(Buffed == 0){
            Bullet bullet =  Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            bullet.Project(this.transform.up);
        }
        
        if(Buffed == 1){
            
            Bullet leftBullet = Instantiate(this.bulletPrefab, this.transform.position + this.transform.right * -bulletOffset, this.transform.rotation);
            leftBullet.Project(this.transform.up);
            
            Bullet rightBullet = Instantiate(this.bulletPrefab, this.transform.position + this.transform.right * bulletOffset, this.transform.rotation);
            rightBullet.Project(this.transform.up);
        }
        if (Buffed == 2) {
            float angleOffset = 10f;
            Bullet centerBullet = Instantiate(this.bulletPrefab, this.transform.position, this.transform.rotation);
            centerBullet.Project(this.transform.up);
            
            Bullet leftBullet = Instantiate(this.bulletPrefab, this.transform.position + this.transform.right * -bulletOffset, this.transform.rotation);
            leftBullet.Project(Quaternion.Euler(0, 0, angleOffset) * this.transform.up);
            
            Bullet rightBullet = Instantiate(this.bulletPrefab, this.transform.position + this.transform.right * bulletOffset, this.transform.rotation);
            rightBullet.Project(Quaternion.Euler(0, 0, -angleOffset) * this.transform.up);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Asteroid"){
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = 0.0f;

            this.gameObject.SetActive(false);
            FindObjectOfType<GameManager>().PlayerDied();
        }
    }
}

