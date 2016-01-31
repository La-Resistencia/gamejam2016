using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

    Vector3 location;
    Vector3 velocity;
    Vector3 acceleration;
    Vector3 steering;
    
    public float r;
    public float maxForce;

    public float maxSpeed;
    private float maxSpeedTurbo;

    public float maxRotSpeed;

    private Rigidbody rb;
    private float hor;
    private float ver;
    private float rotationSpeed;
    private float rotation;

    private float speedToClamp;
    
	// Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rotation = rb.rotation;
        maxSpeedTurbo = 20;
    }

    void Update()
    {

    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        hor = Input.GetAxis("Horizontal");
        ver += Input.GetAxis("Vertical");
        if (ver < 0) ver = 0;
        rotation = (hor * 200);

        Vector3 eulerAngleVelocity = new Vector3(0.0f, rotation, 0.0f);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        //rb.MoveRotation(-(rotation));
        //Debug.Log(rb.rotation);
        //rb.velocity += Vector2.ClampMagnitude(new Vector2(ver * Mathf.Cos(ToRadians(rotation)), -ver * Mathf.Sin(ToRadians(rotation))), maxSpeed);
        speedToClamp = Input.GetKey(KeyCode.Space) ? maxSpeedTurbo : maxSpeed;
        rb.velocity = Vector3.ClampMagnitude(
            new Vector3(
                    ver * Mathf.Cos(ToRadians(rb.rotation.eulerAngles.y)),
                    0.0f,
                    ver * -Mathf.Sin(ToRadians(rb.rotation.eulerAngles.y))
                ),
            speedToClamp);

        //hor = Input.GetAxis("Horizontal");
        //ver = Input.GetAxis("Vertical");
        //rotationSpeed += hor;
        //if (Mathf.Abs(rotationSpeed) > maxRotSpeed)
        //{
        //    rotationSpeed = rotationSpeed > 0? maxRotSpeed: -maxSpeed;
        //}

        //Vector3 eulerAngleVelocity = new Vector3(0.0f, rotationSpeed, 0.0f);
        //Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
        //rb.MoveRotation(rb.rotation * deltaRotation);

        //acceleration = new Vector3(
        //    ver * Mathf.Cos(ToRadians(rb.rotation.eulerAngles.y)), 
        //    0.0f,
        //    ver * -Mathf.Sin(ToRadians(rb.rotation.eulerAngles.y)));
        //Debug.Log(rb.rotation.eulerAngles);
        //steering += acceleration;
        //velocity += steering;
        //rb.velocity = Vector3.ClampMagnitude(velocity, maxSpeed); 
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("collision" + collision);
            rb.AddForce(-rb.velocity);
        }
            //rb.velocity = Vector3.Reflect(rb.velocity, rb.velocity.normalized);
    }

    public float ToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }

}
