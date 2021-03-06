using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failed1 : MonoBehaviour
{
    private GameObject gun;
    private Vector3 gunPos;
    private Quaternion gunRot;
    public GameObject prefab;
    bool isSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        gun = GameObject.FindGameObjectWithTag("1stgun");
        gunPos = gameObject.transform.position;
        gunRot = gameObject.transform.rotation;

        if (gun == null && isSpawn == false)
        {
            isSpawn = true;
            StartCoroutine(RespawnItem());
        }
        

    }
    IEnumerator RespawnItem()
    {
        if (isSpawn)
        {
            int respawnTime = 2;
            yield return new WaitForSeconds(respawnTime);
            GameObject pb = Instantiate(prefab, gunPos, gunRot);
            pb.tag = "1stgun";

        }
        isSpawn = false;
    }
}
