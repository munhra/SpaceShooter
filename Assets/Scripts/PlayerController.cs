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

    void Start()
    {
        Debug.Log("Player started");
		calibrateAccelerometer ();
    }
	
    void FixedUpdate()
    {
     
		float moveHorizontal = CrossPlatformInputManager.GetAxis ("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis ("Vertical");

		Vector3 accelerationRaw = Input.acceleration;
		Vector3 acceleration = fixAcceleration (accelerationRaw);

        Rigidbody rg = GetComponent<Rigidbody>();
        rg.velocity = new Vector3(moveHorizontal * speed , 0.0f, moveVertical * speed);

        rg.position = new Vector3(
            Mathf.Clamp(rg.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rg.position.z, boundary.zMin, boundary.zMax)
        );
    }

    void Update()
    {
        if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			Debug.Log ("Fire");
			Rigidbody rg = GetComponent<Rigidbody> ();
			nextFire = Time.time + fireRate;
			Instantiate (shoot, rg.position, Quaternion.identity);
			AudioSource audio = GetComponent<AudioSource> ();
			audio.Play ();

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
