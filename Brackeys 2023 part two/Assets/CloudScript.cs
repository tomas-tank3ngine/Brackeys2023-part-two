using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool movingRight;
    [SerializeField] private float spawnPosX;
    [SerializeField] private float spawnPosY;

    // Start is called before the first frame update
    void Start()
    {
        spawnPosX = transform.position.x;
        spawnPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.CompareTag("Killzone"))
        {
            transform.position = new Vector3(spawnPosX, spawnPosY, 0f);
            Debug.Log("killzone found");
        }
    }
}
