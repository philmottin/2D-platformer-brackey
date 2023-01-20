using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float fireRate = 0f;
    float timeToFire = 0f;
    
    float bulletTrailRate = 10f;
    float timeToBulletTrail = 0f;

    public int damage = 10;

    public LayerMask whatToHit;
    Transform firePoint;
    public Transform bulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    public Transform HitEffectPrefab;

    public float camShakeAmount = 0.1f;
    //CinemachineShake camShake;
    CinemachineShake_coroutine camShake;

    private void Awake() {
        firePoint = transform.Find("FirePoint");
        if (firePoint == null) {
            Debug.LogError("No FirePoint object found.");
        }
    }

    private void Start() {
        //camShake = GameMaster.gm.GetComponent<CinemachineShake>();
        camShake = GameMaster.gm.GetComponent<CinemachineShake_coroutine>();
        if (camShake == null) {
            Debug.Log("No CinemachineShake script found on GM object");
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

            
            
            //Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) *100, Color.cyan);
            //Debug.DrawLine(firePointPosition, mousePosition);

            if (hit.collider != null) {
                //Debug.DrawLine(firePointPosition, hit.point, Color.red);
                Debug.Log("we hit " + hit.collider.name + "and did " + damage + " damage");
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.DamageEnemy(damage);
            }
            
            // limit the bulletTrail effect because at rate of 10/second is good enough to look like a machine gun
            // to have more will cause to computing.
            if (Time.time > timeToBulletTrail) {
                Vector3 hitPos;
                Vector3 hitNormal;
                if (hit.collider == null) {
                    hitPos = (mousePosition - firePointPosition) * 10;
                    hitNormal = new Vector3(9999, 9999, 9999);
                } else {
                    hitPos = hit.point;
                    hitNormal = hit.normal;

                }
                Effect(hitPos, hitNormal);
                timeToBulletTrail = Time.time + 1 / bulletTrailRate;
            }
        }

        void Effect(Vector3 hitPosition, Vector3 hitNormal) {
            Transform bulletTrail = Instantiate(bulletTrailPrefab, firePoint.position, firePoint.rotation);
            LineRenderer lr = bulletTrail.GetComponent<LineRenderer>();
            if (lr != null) {
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, hitPosition);
            }
            Destroy(bulletTrail.gameObject, 0.04f);
            if (hitNormal != new Vector3(9999, 9999, 9999))
            {
                //deleted on the particle system
                Instantiate(HitEffectPrefab, hitPosition, Quaternion.FromToRotation(Vector3.forward,hitNormal));
            }

            Transform muzzleFlashClone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation);
            muzzleFlashClone.parent = firePoint;
            float size = Random.Range(0.6f, 0.9f);
            muzzleFlashClone.localScale = new Vector3(size, size, 0);

            // a way to pause for 1 frame and destroy. needs coroutine.
            // yield return 0;
            // Destroy(muzzleFlashClone.gameObject);

            // or destroy after 0.02f time
            Destroy(muzzleFlashClone.gameObject, 0.02f);

            camShake.Shake(0.2f, 0.8f, 1f);
        }
    }
}
