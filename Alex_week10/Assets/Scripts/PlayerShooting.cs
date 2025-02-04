using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] int gunDamage = 1;
    [SerializeField] float fireRate = 0.25f;
    [SerializeField] float range = 25;
    [SerializeField] float hitForce = 20;
    [SerializeField] float shotDuration = 0.5f;
    float nextFire;

    [Header("Camera and Positioning")]
    Camera playerCam;
    [SerializeField] Transform gunEnd;

    AudioSource _as;
    public AudioClip shootSFX, emptySFX;
    LineRenderer _lr;

    [Header("Ammo")]
    public int ammo = 6;
    public TMP_Text ammoText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _as = GetComponent<AudioSource>();
        _lr = GetComponent<LineRenderer>();
        playerCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ammoText.text = ammo.ToString();
        if (ammo > 6)
        {
            ammo = 6;
        }
        #region Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo > 0 && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                ammo--;
                StartCoroutine(ShootingEffect());

                RaycastHit hit;
                _lr.SetPosition(0, gunEnd.transform.position);

                if (Physics.Raycast(gunEnd.transform.position, gunEnd.transform.forward, out hit, range)) //In order: where the ray starts, where it's travelling, the ray's output and the distance the ray travels
                {
                    _lr.SetPosition(1, hit.point);

                    //Deals with hitting an enemy
                    ShootableBox targetBox = hit.transform.GetComponent<ShootableBox>();
                    if (targetBox != null)
                    {
                        targetBox.Damage(gunDamage);
                    }

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce, ForceMode.Impulse);
                    }
                }
                else
                {
                    _lr.SetPosition(1, gunEnd.transform.forward * 100000);
                }
            }
            else if (ammo <= 0)
            {
                _as.PlayOneShot(emptySFX);
            }
        }
    }
    IEnumerator ShootingEffect()
    {
        _as.PlayOneShot(shootSFX);
        _lr.enabled = true;
        yield return new WaitForSeconds(shotDuration);
        _lr.enabled = false;
    }
    #endregion
}
