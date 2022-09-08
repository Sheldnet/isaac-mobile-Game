using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Text collectedText;
    public static int collectedAmount = 0;
    private Rigidbody2D rb;
    
    private float lastFire;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float fireDelay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVer = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVer != 0) && Time.time > fireDelay + lastFire)
        {
            Shoot(shootHor, shootVer);
            lastFire = Time.time;
        }
        
        rb.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

        collectedText.text = "Items Collected " + collectedAmount;

    }

    private void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
            );
    }
}
