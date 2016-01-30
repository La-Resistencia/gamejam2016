using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private float prev_hor;
    private float prev_ver;
    private float rotationSpeed;
    private float rotation;

    private float speedToClamp;

    private Queue<Vector2> commandQueueSend;
    private Queue<Vector2> commandQueueRecv;
	// Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rotation = rb.rotation;
        maxSpeedTurbo = 20;
        commandQueueSend = new Queue<Vector2>();
        commandQueueRecv = new Queue<Vector2>();

    }

    void Update()
    {

    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical") *20;
        if (ver < 0) ver = 0;
        

        if (prev_hor != hor || prev_ver != ver)
        {
            prev_hor = hor;
            prev_ver = ver;

            commandQueueSend.Enqueue(new Vector2(prev_hor, prev_ver));
        }

        if (commandQueueRecv.Count > 0)
        {
            Vector2 command = commandQueueRecv.Dequeue();

            rotation = (command.x * 200);
            Vector3 eulerAngleVelocity = new Vector3(0.0f, rotation, 0.0f);
            Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
            //rb.MoveRotation(-(rotation));
            //Debug.Log("rotation "+rb.rotation+" ver: "+ver);
            //rb.velocity += Vector2.ClampMagnitude(new Vector2(ver * Mathf.Cos(ToRadians(rotation)), -ver * Mathf.Sin(ToRadians(rotation))), maxSpeed);
            speedToClamp = Input.GetKey(KeyCode.Space) ? maxSpeedTurbo : maxSpeed;
            
            rb.velocity = Vector3.ClampMagnitude(
                 new Vector3(
                         command.y * Mathf.Cos(ToRadians(rb.rotation.eulerAngles.y)),
                         0.0f,
                         command.y * -Mathf.Sin(ToRadians(rb.rotation.eulerAngles.y))
                     ),
                 speedToClamp); ;

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

        
	}
    public Vector2 sendCommand()
    {
        if (commandQueueSend.Count > 0) return commandQueueSend.Dequeue();
        return new Vector2(prev_hor, prev_ver);
    }

    public void receiveCommand(Vector3 _command)
    {
        commandQueueRecv.Enqueue(new Vector2(_command.x, _command.y));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            Debug.Log("collision" + collision);
            rotation = -rotation;
            ver = 10;
            //rb.AddForce(-rb.velocity);
        }
            //rb.velocity = Vector3.Reflect(rb.velocity, rb.velocity.normalized);
    }

    public float ToRadians(float angle)
    {
        return (Mathf.PI / 180) * angle;
    }

    public Vector3 Velocity
    { 
        get{
            return Vector3.ClampMagnitude(
                 new Vector3(
                         ver * Mathf.Cos(ToRadians(rb.rotation.eulerAngles.y)),
                         0.0f,
                         ver * -Mathf.Sin(ToRadians(rb.rotation.eulerAngles.y))
                     ),
                 speedToClamp);
       }
    }

}
