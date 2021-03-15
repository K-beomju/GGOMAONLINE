using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walls : MonoBehaviour
{
    public Rigidbody2D rig;
    public Collider2D col;
    private SpriteRenderer dd;


    // Start is called before the first frame update
    void Start()
    {
      rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        dd = GetComponent<SpriteRenderer>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            StartCoroutine("Fell");
            dd.color = new Color(255f, 200f, 0f, 225f); // Set to opaque black
        }
    }

  
    IEnumerator Fell()
    {
        yield return new WaitForSeconds(1.5f);
      rig.bodyType = RigidbodyType2D.Dynamic;
       // rig.constraints = RigidbodyConstraints2D.FreezePositionX;
       // rig.bodyType = RigidbodySleepMode2D.NeverSleep;
    }
}
