	using UnityEngine;
	using System.Collections;
	
	[RequireComponent (typeof(CharacterController))]
	
	public class FirstPersonController : MonoBehaviour
	{
	    public float movementSpeed = 5;
		public float mouseSensitivity = 2;
		public float upDownRange = 60;
	    public float jumpSpeed = 5;
	    public AudioClip[] Sound;
	
	    private float verticalRotation = 0;
	    private float verticalVelocity = 0;
	    private float horizontalRotation = 0;
	    private CharacterController cc;
	    private float delaySoundPas = 0;
	  
		void PlaySound(int clip)
		{
			GetComponent<AudioSource>().clip = Sound[clip];
			GetComponent<AudioSource>().Play();
		}
	
	  // Use this for initialization
	  void Start ()
	  {
			Screen.lockCursor = true;
		    cc = GetComponent<CharacterController> ();
	  }
		
	  // Update is called once per frame
	  void Update ()
	  {
			float rotX = Input.GetAxis("Mouse X") + horizontalRotation, rotY = Input.GetAxis("Mouse Y");
			horizontalRotation = 0;
	        transform.Rotate (0, rotX * mouseSensitivity, 0);
	
	        verticalRotation -= rotY * mouseSensitivity;
	        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
			Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
	
	        // Déplacement
	
	        verticalVelocity += Physics.gravity.y * Time.deltaTime;
	        if (cc.isGrounded && Input.GetButtonDown("Jump"))
	        {
	          verticalVelocity = jumpSpeed;
	          PlaySound(1);
	        }
			float forwardSpeed = Input.GetAxis ("Vertical"), sideSpeed = Input.GetAxis("Horizontal");
			Vector3 speed = new Vector3 (sideSpeed * movementSpeed, verticalVelocity, forwardSpeed * movementSpeed);
	
			speed = transform.rotation * speed;
	
			cc.Move (speed * Time.deltaTime);
	
		if ((forwardSpeed > 0.75f || sideSpeed > 0.75f || forwardSpeed < -0.75f || sideSpeed < -0.75f) && cc.isGrounded)
	        {
	          if (delaySoundPas < 0)
              {
                PlaySound(0);
                delaySoundPas = Sound[0].length * 1.5f;
              }
              else
                delaySoundPas -= Time.deltaTime;
	        }
	  }
	
	  public void recul(float x, float y)
	  {
	    horizontalRotation += x;
	    verticalRotation += y;
	  }
	
		
		private Vector3 coords(Vector3 position)
		{
			float x, y = 2, z;
			x = (-position.z + 41f) * (122.5f / 82f) + 2.5f;
			y = 2;
			z = (-(position.x + 3.5f) + 13.5f) * (42f / 27f) + 2f;
			
			return new Vector3(x, y, z);
		}
	}