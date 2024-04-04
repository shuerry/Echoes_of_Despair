using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootPipe : MonoBehaviour
{
    public GameObject projectilePrefab;
   
    public float projectileSpeed = 100;
    public Image reticleImage;
   
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            GameObject projectile = Instantiate(projectilePrefab,
                transform.position + transform.forward, transform.rotation) as GameObject;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

            projectile.transform.SetParent(GameObject.FindGameObjectWithTag("PipeParent").transform);

       
        }
    }

    private void FixedUpdate() {
        ReticleEffect();
    }

    void ReticleEffect() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
            if (hit.collider.CompareTag("Enemy")) {
                reticleImage.color = Color.Lerp
                    (reticleImage.color, Color.red, Time.deltaTime * 2);
            
                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, new Vector3(0.7f, 0.7f, 1), Time.deltaTime * 2);

            } else {                
                reticleImage.color = Color.Lerp
                    (reticleImage.color, Color.white, Time.deltaTime * 2);
            
                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
            }
        } else {
                reticleImage.color = Color.Lerp
                    (reticleImage.color, Color.white, Time.deltaTime * 2);
            
                reticleImage.transform.localScale = Vector3.Lerp
                    (reticleImage.transform.localScale, Vector3.one, Time.deltaTime * 2);
        }
    }

}
