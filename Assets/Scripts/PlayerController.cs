using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    // Use this for initialization
    public float speed;

    private Rigidbody rb;

    public Text countText;
    public Text winText;
    
    public AudioClip correctObject;
    public float volumeCorrect;
    public AudioClip wrongObject;
    public float volumeWrong;
    public AudioClip plusBallButton;
    public float volumePlusBall;

    public float jumpHeight = 20.0f;

    private int count;
    private AudioSource source;

	void Start ()
    {

        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        transform.localScale.Set(3.0f, 5.0f, 3.0f);

    }

	// Update is called once per frame
	void FixedUpdate () 
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float jump = 0.0f;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = jumpHeight;
        }
        Vector3 movement = new Vector3(moveHorizontal, jump, moveVertical);
        if (Input.GetKeyDown(KeyCode.I)){
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);  
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.localScale = transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
        }
        if (Input.GetKeyDown(KeyCode.Plus))
        {
            GetComponent<AudioSource>().volume = volumePlusBall;
            GetComponent<AudioSource>().PlayOneShot(plusBallButton);
            transform.localScale = transform.localScale + new Vector3(7.0f, 7.0f, 7.0f);
        }
        if (Input.GetKeyDown(KeyCode.Minus))
        {
            transform.localScale = transform.localScale - new Vector3(7.0f, 7.0f, 7.0f);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
            transform.position = new Vector3(0.0f, 4.0f, 0.0f);
            rb.velocity = Vector3.zero;
        }

        rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Pick Up"))
        {
            GetComponent<AudioSource>().volume = volumeCorrect;
            GetComponent<AudioSource>().PlayOneShot(correctObject);
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
           
        }
        if(other.gameObject.CompareTag("Wrong Object"))
        {
            // AudioSource.PlayClipAtPoint(other.gameObject.GetComponent<AudioSource>().clip, transform.position);
            GetComponent<AudioSource>().volume = volumeWrong;
            GetComponent<AudioSource>().PlayOneShot(wrongObject);
            //AudioSource.PlayClipAtPoint(wrongClip, other.transform.position);
            other.gameObject.SetActive(false);
            count = count - 1;
            SetCountText();

        }
	}

    void SetCountText()
    {
        countText.text = "Your Score : " + count.ToString();

        if(count >=10)
        {
            winText.text = "Good Job !";
        }

    }
}