using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public Boundary boundary;
    public float tilt;
    public GameObject shoot;
    public Transform shootSpanw;
    public float fireRate;
    private float nextFire;
	private Quaternion calibrationQuaternion;
	public SimpleTouchPad simpleTouchPad;

    void Start()
    {
        Debug.Log("Player started");
		calibrateAccelerometer ();
    }

    void FixedUpdate()
    {
     


		//float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");


		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");



		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = fixAcceleration (accelerationRaw);


		Vector2 direction = simpleTouchPad.GetDirection ();

        Rigidbody rg = GetComponent<Rigidbody>();
        rg.velocity = new Vector3(moveHorizontal * speed , 0.0f, moveVertical * speed);
		//rg.velocity = new Vector3(acceleration.x * speed , 0.0f, acceleration.y * speed);
		//rg.velocity = new Vector3(direction.x * speed , 0.0f, direction.y * speed);


        rg.position = new Vector3(
            Mathf.Clamp(rg.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rg.position.z, boundary.zMin, boundary.zMax)


        );

        rg.rotation = Quaternion.Euler(0.0f, 0.0f, rg.velocity.x * -tilt);

    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.Log("Fire");
            nextFire = Time.time + fireRate;
            Instantiate(shoot, shootSpanw.position, shootSpanw.rotation);
			AudioSource audio = GetComponent<AudioSource>();
			audio.Play();

        }

            
    }

	void calibrateAccelerometer(){
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}
	
	Vector3 fixAcceleration(Vector3 acceleration) {
	
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	
	}


	void OnTriggerEnter(Collider other)
	{
		Debug.Log ("OnTriggerEnter Player Controller");
	}


}
