using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 1.5f;
    public float speed = 50.0f;
    public float duration = 30.0f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
        {
            Debug.LogError("Animator component missing on Asteroid!");
            return;
        }
    }

    private void Start()
    {
        _animator.SetInteger("New Int",Random.Range(0,4));
        

        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
        this.transform.localScale = Vector3.one * this.size;

        _rigidbody.mass = this.size;
    }

    public void SetTrajectory(Vector2 direction)
    {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.duration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this.size * 0.5f) >= this.minSize && this.gameObject.tag == "Asteroid")
            {
                CreateSplit();
            }

            if (gameObject.tag == "SpecialAsteroid")
            {
                FindObjectOfType<Movement>().Buffed += 1;
            }
            else if (gameObject.tag == "totallySpecialAsteroid")
            {
                FindObjectOfType<Movement>().Buffed += 1;
            }

            GameManager.Instance.AsteroidDestroyed(this);
            Destroy(this.gameObject);
        }
    }

    private void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);

        half.size = this.size * 0.5f;
        half.transform.localScale = Vector3.one * half.size;
        half._rigidbody.mass = half.size;
        half.SetTrajectory(Random.insideUnitCircle.normalized);
        half.duration = this.duration * 0.1f;

        half.gameObject.SetActive(true);
        half._rigidbody.simulated = true;
    }
}
