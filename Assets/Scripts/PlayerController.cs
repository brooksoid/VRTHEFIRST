using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    public float speed; //shows up in Inspector as public property
    public Text countText;
    public Text winText;
	public AudioClip pickupSound;

    private Rigidbody rb;
    private int count;
	private AudioSource source;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        setCountText();
        winText.text = "";
    }

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("HorizontalDPAD");
        float moveVertical = Input.GetAxis("VerticalDPAD");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            setCountText();
			source.PlayOneShot(pickupSound, 1.0f);
        }
    }

    void setCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}