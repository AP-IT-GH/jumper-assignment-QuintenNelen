using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float moveSpeed;
    public static bool hit;
    public static float minMovespeed = 1f;
    public static float maxMovespeed = 5f;
    public static float random;
    private void Update()
    {
        this.transform.Translate(Vector3.back * (moveSpeed * random) * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") == true)
        {
            hit = true;
        }
    }
    public static void GenRandom()
    {
        random = Random.Range(minMovespeed, maxMovespeed);
    }
}
