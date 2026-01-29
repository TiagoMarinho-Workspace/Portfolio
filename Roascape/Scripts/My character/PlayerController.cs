using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.5f;
    public float rotation = 10f;
    public float jumpForce = 5.5f;
    private float moveHorizontal, moveVertical;

    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Pega o Animator automaticamente
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        // Movimento e rotação
        if (movement.magnitude > 0.1f)
        {
            Quaternion newRotation = Quaternion.LookRotation(movement.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotation * Time.deltaTime);
            transform.position += movement.normalized * speed * Time.deltaTime;
        }

        // Atualiza a animação de movimento
        animator.SetFloat("isRuning", movement.magnitude);

        //  Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("IsJumping", true);
            isGrounded = false;
        }
    }

    //  Detecta se está tocando o chão
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
        animator.SetBool("IsJumping", false);
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }
}
