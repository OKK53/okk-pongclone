using System.Collections;
using UnityEngine;

public class MoveRacket : MonoBehaviour {

    public float speed = 30;
    public string axis = "";
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v) * speed;
    }

    public void Reset()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        transform.position = startPosition;
    }
}
