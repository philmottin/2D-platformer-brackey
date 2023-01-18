using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0f;
    public float damage = 10f;
    public LayerMask whatToHit;
    float timeToFire = 0f;
    Transform firePoint;

    private void Awake() {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null) {
            Debug.LogError("No FirePoint object found.");
        }
    }

    // Update is called once per frame
    void Update() {
        if (fireRate == 0) {
            if (Input.GetButtonDown("Fire1")) {
                Shoot();
            }
        } else {
            if (Input.GetButton("Fire1") && Time.time > timeToFire) {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }

        }

        void Shoot() {
            //Get the coordinates from mousePositon converted to world position
            Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);

            //raycast parameters are origin and direction. 
            // mousePosition-firePointPosition give us the direction
            RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition-firePointPosition, 100f, whatToHit);
            Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) *100, Color.cyan);
            //Debug.DrawLine(firePointPosition, mousePosition);
            if (hit.collider != null) {
                Debug.DrawLine(firePointPosition, hit.point, Color.red);
                Debug.Log("we hit " + hit.collider.name + "and did " + damage + " damage");
            }

        }
    }
}