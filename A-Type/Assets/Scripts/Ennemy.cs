using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour
{
		public float movementSpeed = 5;
		public float upDownRange = 60;
		public float jumpSpeed = 5;
		
		private float verticalRotation = 0;
		private float verticalVelocity = 0;
		private float horizontalRotation = 0;
		private CharacterController cc;
    private float vitesseX = -1;
    private float vitesseZ = 0;
    private int[,,] map = new int[3, 128, 45];
		
		public Animator anim = null;
		
		public enum CharacterState
        {
          Idle,
          Running,
          Attacking,
          Walking

        }
        private CharacterState _characterState = CharacterState.Walking;
	
		// Use this for initialization
		void Start ()
		{
		  cc = GetComponent<CharacterController>();
            
            // 2e Etage
          for (int i = 0 ; i < 128 ; i ++)
			map[2, i, 0] = 1;
	 	
		  for (int i = 0 ; i < 128 ; i ++)
			map[2, i, 18] = 1;
         
          for (int i = 0 ; i < 128 ; i ++)
			map[2, i, 26] = 1;
		
		  for (int i = 2 ; i <= 5 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 20 ; i <= 23 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 26 ; i <= 29 ; i ++)
			map[2, i, 18] = 2;
		
	      for (int i = 54 ; i <= 57 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 61 ; i <= 64 ; i ++)
			map[2, i, 18] = 2;
		
          for (int i = 76 ; i <= 79 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 82 ; i <= 85 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 88 ; i <= 91 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 102 ; i <= 105 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 107 ; i <= 110 ; i ++)
			map[2, i, 18] = 2;
		
		  for (int i = 0 ; i <= 44 ; i ++)
			map[2, 1, i] = 1;
		
		  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 3, i] = 1;
		
	  	  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 24, i] = 1;
		
		  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 58, i] = 1;
		
		  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 72, i] = 1;
		
		  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 82, i] = 1;
		
		  for (int i = 0 ; i <= 18 ; i ++)
			map[2, 87, i] = 1;
		
		  for (int i = 0 ; i <= 44 ; i ++)
			map[2, 126, i] = 1;

           Debug.Log (map);
		}
		
		// Update is called once per frame
		void Update ()
		{
		  float rotX = 0, rotY = 0, forwardSpeed = 0, sideSpeed = 0;
		
		if (vitesseX == 1)
		{
			if (map[2, (int) coords(gameObject.transform.position).x + 1, (int) coords(gameObject.transform.position).z] != 1)
				forwardSpeed = 1;
			else
			{
              vitesseX = 0;
				if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z + 1] != 1)
                {
                    vitesseZ = 1;
					rotX = 90;
                }
				else if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z - 1] != 1)
                {
                    vitesseZ = -1;
					rotX = -90;
                }
				else
                {
                    vitesseX = -1;
					rotX = 180;
                }
			}
		}
		if (vitesseX == -1)
		{
			if (map[2, (int) coords(gameObject.transform.position).x - 1, (int) coords(gameObject.transform.position).z] != 1)
				forwardSpeed = 1;
			else
			{
				vitesseX = 0;
				if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z - 1] != 1)
                {
                  vitesseZ = -1;
				  rotX = 90;
                }
				else if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z + 1] != 1)
				{
				  rotX = -90;
                  vitesseZ = 1;
				}
                else
                {
				  rotX = 180;
                  vitesseX = 1;
                }
			}
		}
        if (vitesseZ == 1)
		{
			if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z + 1] != 1)
				forwardSpeed = 1;
			else
			{
				vitesseZ = 0;
				if (map[2, (int) coords(gameObject.transform.position).x + 1, (int) coords(gameObject.transform.position).z] != 1)
                {
                    vitesseX = -1;
					rotX = 90;
                }
				else if (map[2, (int) coords(gameObject.transform.position).x - 1, (int) coords(gameObject.transform.position).z] != 1)
				{
                  vitesseX = 1;
         	      rotX = -90;
                }
				else
                {
                    vitesseZ = -1;
					rotX = 180;
                }
			}
		}
		if (vitesseZ == -1)
		{
			if (map[2, (int) coords(gameObject.transform.position).x, (int) coords(gameObject.transform.position).z - 1] != 1)
				forwardSpeed = 1;
			else
			{
				vitesseZ = 0;
				if (map[2, (int) coords(gameObject.transform.position).x + 1, (int) coords(gameObject.transform.position).z] != 1)
                {
                    vitesseX = 1;
					rotX = 90;
                }
				else if (map[2, (int) coords(gameObject.transform.position).x - 1, (int) coords(gameObject.transform.position).z] != 1)
                {
                    vitesseX = -1;
					rotX = -90;
                }
				else
                {
                    vitesseZ = 1;
					rotX = 180;
                }
			}
		}

		  transform.Rotate (0, rotX, 0);
			
		//	verticalRotation -= rotY;
		//	verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
		//	Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
			
		    // Déplacement
			
			verticalVelocity += Physics.gravity.y * Time.deltaTime;
			if (cc.isGrounded && false)
			{
				verticalVelocity = jumpSpeed;
			}
			Vector3 speed = new Vector3 (sideSpeed * movementSpeed, verticalVelocity, forwardSpeed * movementSpeed);
			
			speed = transform.rotation * speed;
			
			cc.Move (speed * Time.deltaTime);
			
			if (movementSpeed < 0.1)
            {
                anim.SetBool("isWalking", false); 
            }
            else
            {
                if (_characterState == CharacterState.Running)
                {
                        //anim.SetBool("isRunning", true);
                }
                else if (_characterState == CharacterState.Walking)
                {
                        anim.SetBool("isWalking", true);                    
                }
            
            }
            
            //Debug.Log(coords (gameObject.transform.position));
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
