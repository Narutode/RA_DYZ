using System.Collections;
using FishNet.Object;
using UnityEngine;

public class CameraShoot : NetworkBehaviour
{
    public GameObject bulletPrefab; // Prefab de la balle
    public float bulletSpeed = 10f; // Vitesse de la balle
    public float targetThreshold = 0.5f; // Marge pour déterminer si un NPC est centré
    public float cooldownTime = 2f; // Temps de recharge entre chaque tir
    private float lastShootTime; // Temps du dernier tir
    public GameManager gameManager;
    
   
    void Update()
    {
       
            if(LocalConnection.ClientId == 0 && !gameManager.gainpistolP1)
            {
                return;
            }
            if(LocalConnection.ClientId == 1 && !gameManager.gainpistolP2)
            {
                return;
            }

            if (Time.time - lastShootTime > cooldownTime)
            {
                // Trouver tous les objets portant le tag "NPC"
                GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

                foreach (GameObject npc in npcs)
                {
                    // Convertir la position du NPC dans l'espace écran
                    Vector3 npcScreenPosition = Camera.main.WorldToViewportPoint(npc.transform.position);

                    // Vérifier si le NPC est centré dans la vue de la caméra
                    if (Mathf.Abs(npcScreenPosition.x - 0.5f) < targetThreshold &&
                        Mathf.Abs(npcScreenPosition.y - 0.5f) < targetThreshold)
                    {
                        // Tirer la balle vers le NPC
                        ShootBullet(npc.transform);
                        lastShootTime = Time.time;
                    }
                }
            }
    }

    void ShootBullet(Transform npcTransform)
    {
        // Instancier la balle à partir du prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        if(LocalConnection.ClientId == 0)
        {
            gameManager.playerHand1.GetComponent<AudioSource>().Play();
        }
        else if(LocalConnection.ClientId == 1)
        {
            gameManager.playerHand2.GetComponent<AudioSource>().Play();
        }

        // Calculer la direction vers le NPC
        Vector3 direction = (npcTransform.position - transform.position).normalized;

        // Appliquer une force à la balle dans la direction du NPC
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;

        // Détruire la balle après un certain délai
        Destroy(bullet, 1f);
        DespawnCoroutine(npcTransform.gameObject, 1f);
        
        if(gameManager.NPCList.Contains(npcTransform.gameObject))
        {
            gameManager.NPCList.Remove(npcTransform.gameObject);
            if(gameManager.NPCList.Count == 0)
            {
                if (LocalConnection.ClientId == 0)
                {
                    Spawn(gameManager.scissors);
                    Spawn( gameManager.code2);

                }
                else if (LocalConnection.ClientId == 1)
                {
                    
                }
            }
        }

        
    }
    [ObserversRpc(RunLocally = true)]
    public void DespawnCoroutine(GameObject npc, float delay)
    {
        StartCoroutine(DespawnNPC(npc, delay));
    }
    public IEnumerator DespawnNPC(GameObject npc, float delay)
    {
        yield return new WaitForSeconds(delay);
        Despawn(npc);
    }
}
