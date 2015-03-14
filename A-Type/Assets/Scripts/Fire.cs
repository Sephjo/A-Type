using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public GameObject bullet;
    public float cooldown = 0.3f;
    public float range = 200;
    public float damage = 30;
    public AudioClip[] Sound;
    public int nbBalle = 6;

    private float cooldownRemaning = 0;
    private float recul = 0;
    private float hitMarker = 0;
    
    // Cette fonction joue le son du tir, elle est appelée quand le joueur tire.
    void PlaySound(int clip)
    {
        audio.clip = Sound[clip];
        audio.Play();
    }

    void Update()
    {
        Camera cam = Camera.main;
        float instantReculY = 0, instantReculX = 0;
      
        cooldownRemaning -= Time.deltaTime;

        // On met à jour l'affichage du hit marker
        if (hitMarker > 0)
        {
            hitMarker -= Time.deltaTime;
            if (hitMarker < 0)
            {
                hitMarker = 0;
                GameObject.Find("hitMarker").GetComponent<hitMarker>().off();
            }
        }

        // Si le joueur appuie sur la touche de tir, et que l'arme est prete à tirer
        if (Input.GetButton("Fire1") && cooldownRemaning <= 0 && !gameObject.GetComponent<PauseMenu>().isPaused() && nbBalle > 0)
        {
            PlaySound(0);
      
            // On crée une balle avec de la force
            GameObject obj = (GameObject)Instantiate(bullet, cam.transform.position, Quaternion.Euler(cam.transform.rotation.eulerAngles.x + 90, cam.transform.rotation.eulerAngles.y, cam.transform.rotation.eulerAngles.z));
            obj.rigidbody.AddForce(cam.transform.forward * 1000);

            // On crée un rayon pour tester si le joueur touche un ennemi
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, range))
            {
                GameObject go = hitInfo.collider.gameObject;
                Health h = go.GetComponent<Health>();

                // Si l'objet touché a de la vie, c'est que c'est un ennemi
                if (h != null)
                {
                    // On envoie des dégats à l'ennemi
                    h.reciveDamage(damage);
                    // On affiche le hit marker
                    GameObject.Find("hitMarker").GetComponent<hitMarker>().on();
                    hitMarker = 0.25f;
                }
            }
      
            // Le joueur vient de tirer, on fait légèrement bouger son arme vers le haut pour simuler le recul
            instantReculY = Random.Range(-2, -1);
            instantReculX = Random.Range(-0.25f, 0.25f);
		
            recul -= instantReculY / 1.5f;
            cooldownRemaning = cooldown;

            nbBalle = nbBalle - 1;
        }

        if (Input.GetButton("Reload"))
        {
            nbBalle = 6;
            PlaySound(1);
        }

        // On met à jour le recul
        FirstPersonController fpc = gameObject.GetComponent<FirstPersonController>();
        if (recul > 0)
        {
            fpc.recul(0f, 10 * Time.deltaTime);
            recul -= 10 * Time.deltaTime;
            if (recul < 0)
                recul = 0;
        }
        fpc.recul(instantReculX, instantReculY);
    }
}