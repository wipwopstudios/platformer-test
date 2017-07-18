using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 365f;   // Force at which player is moving
	public float maxSpeed = 5f;      // Max speed to keep player from accelerating too much
	public float jumpForce = 1000f;  // Force at which player jumps
	public Transform groundCheck;    // Line cast down to tell if player is standing on solid object.

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;	
		}
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		anim.SetFloat("Speed", Mathf.Abs(h));

		// Increase speed if possible
		if (h * rb2d.velocity.x < maxSpeed)
		{
			rb2d.AddForce(Vector2.right * h * moveForce);
		}

		// Cap movement speed
		if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
		{
			rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}

		// Ensure hero is facing the direction he is moving
		if (h > 0 && !facingRight)
		{
			Flip();
		}
		else if (h < 0 && facingRight)
		{
			Flip();
		}

		// Perform the jump action
		if (jump)
		{
			anim.SetTrigger("Jump");
			rb2d.AddForce(new Vector2(0f, jumpForce));
			jump = false;
		}
	}

	void Flip()
	{
		facingRight = !facingRight;

		// Flip by scaling x component by -1
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
