using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CharacterController2D : MonoBehaviour
{
	public int BaseLife = 3;
	public int currentLife;
	public Vector3 respawnPosition;
	public TextMeshProUGUI LifeText;

	[SerializeField] public float m_RunSpeed = 400f;
	[SerializeField] public float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] public float m_DoubleJumpForce = 250f;                          // Amount of force added when the player double jumps.
																				//[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
																				//[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private BoxCollider2D HitboxCollider;

	[SerializeField] private Animator animator;


	[SerializeField] private PlayerFXSource audioPlayer;
	[SerializeField] private SpriteRenderer shieldRenderer;
	const float k_GroundedWidth = 46f; // Radius of the overlap circle to determine if grounded
	const float k_GroundedHeight = 1f; // Radius of the overlap circle to determine if grounded
	[SerializeField] private bool m_Grounded;            // Whether or not the player is grounded.
	//const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public HitboxInfo currentHitboxInfo;

	[SerializeField] private float DashDuration = .5f;
	[SerializeField] private float DashSpeed = 100f;

	
	public float currentShieldTimer;
	[SerializeField] private bool isShielded = false;

	private bool isDashing;
	private float currentDashTimer = 0f;

	// Inputs
	private bool jumpRequested;
	private bool doubleJumpRequested;
	private bool dashRequested;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	//public BoolEvent OnCrouchEvent;
	//private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		audioPlayer = GetComponent<PlayerFXSource>();

		currentLife = BaseLife;

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

	//	if (OnCrouchEvent == null)
	//		OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapBoxAll(m_GroundCheck.position, new Vector2(k_GroundedWidth, k_GroundedHeight), 0f, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}


		animator.SetBool("isGrounded", m_Grounded);
		animator.SetFloat("yVelocity", m_Rigidbody2D.velocity.y);
		animator.SetBool("isDashing", isDashing);
	}


	public void Move(float move)
	{


		if (dashRequested && !isDashing)
		{
			audioPlayer.PlaySound("Dash");
			isDashing = true;
			currentDashTimer = DashDuration;
		}

		if(isDashing)
		{
			currentDashTimer -= Time.deltaTime;
			if(currentDashTimer <= 0 )
			{
				isDashing = false;
			}
            else
            {
				Vector3 targetVelocity = new Vector2(DashSpeed * (m_FacingRight ? 10f : -10f), 0);
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
		}

        //only control the player if grounded or airControl is turned on
        if ((m_Grounded || m_AirControl) && !isDashing)
		{

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f * m_RunSpeed, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			//TODO add dash canceling logic


			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jumpRequested)
		{
			audioPlayer.PlaySound("Jump");
			// Add a vertical force to the player.
			m_Grounded = false;
			//Dash Cancel
			isDashing = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}

		if(doubleJumpRequested)
		{
			audioPlayer.PlaySound("Jump");
			animator.SetTrigger("DoubleJump");
			//Dash Cancel
			m_Grounded = false;
			isDashing = false;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_DoubleJumpForce));
		}

		if(isShielded)
		{
			currentShieldTimer -= Time.deltaTime;
			if(currentShieldTimer <= 0)
			{
				isShielded = false;
			}
		} 

		shieldRenderer.enabled = isShielded;
		LifeText.text = "Life : " + currentLife;

		jumpRequested = false;
		dashRequested = false;
		doubleJumpRequested = false;
	}

	public void Jump()
	{
		jumpRequested = true;
	}

	public void DoubleJump()
	{
		doubleJumpRequested = true;
	}

	public void Dash()
	{
		dashRequested = true;
	}

	public void Shield(float duration)
	{
		currentShieldTimer = duration;
		isShielded = true;
	}
	public void Attack(HitboxInfo info)
	{
		audioPlayer.PlaySound("Slash");
		ApplyHitboxInfo(info);
		animator.SetTrigger("BasicAttack");
	}
	public void InkAttack(HitboxInfo info)
	{
		audioPlayer.PlaySound("Steal");
		ApplyHitboxInfo(info);
		animator.SetTrigger("InkAttack");
	}
	public void SuperAttack(HitboxInfo info)
	{
		audioPlayer.PlaySound("Slash");
		ApplyHitboxInfo(info);
		animator.SetTrigger("SuperAttack");
	}

	public void Ink(HitboxInfo info)
	{
		audioPlayer.PlaySound("Steal");
		ApplyHitboxInfo(info);
		animator.SetTrigger("Ink");
	}

	private void ApplyHitboxInfo(HitboxInfo info)
	{
		HitboxCollider.size = new Vector2(info.X, info.Y);
		HitboxCollider.offset = new Vector2(info.OffsetX, info.OffsetY);
		currentHitboxInfo = info;
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	private void Respawn()
	{
		GetComponent<PlayerMovement>().ResetAbilities();
		currentLife = BaseLife;
		transform.position = respawnPosition;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
		if (collision.tag == "PlayerAttack" && collision.attachedRigidbody != m_Rigidbody2D)
		{
			if (isShielded)
			{
				isShielded = false;
			}
			else
			{
				audioPlayer.PlaySound("Bump2");
				var hitboxInfo = collision.GetComponentInParent<CharacterController2D>().currentHitboxInfo;
				Vector2 damageBump = hitboxInfo.BumpDirection.normalized * hitboxInfo.BumpPower;
				if (m_FacingRight) damageBump.x = -damageBump.x;
				m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, damageBump, ref m_Velocity, m_MovementSmoothing);

				currentLife -= hitboxInfo.Damage;
				if (currentLife <= 0) Respawn();
			}
		}
	}

}
