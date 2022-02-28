using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D _playerBody;
    private SpriteRenderer _playerSprite;
    private Animator _playerAnimator;
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Grounded = Animator.StringToHash("grounded");
    private bool _grounded;
    private static readonly int Jump = Animator.StringToHash("jump");


    // Start is called before the first frame update
    private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //Este metodo con horizontal nos devuelve un valor entre -1 y 1 cuando se presione la tecla a y <- nos dara
        //-1 y 1 cuando se presione d y -> con vertical seria basicamente lo mismo pero con distintas teclas
        var horizontalAxisValue = Input.GetAxis("Horizontal");

        //para agregarle movimiento al player mediante fisicas debemos de aumentarle la velocidad mediante un vector
        //y aumentar el componente X ya que queremos moverlo izq y derecha, si fuera de arriba abajo seria el componente Y
        //Y luego le dejo la misma velocidad al componente que no deseo alterar
        _playerBody.velocity = new Vector2(horizontalAxisValue * speed, _playerBody.velocity.y);
        
        //La animacion de correr sera true cuando el horizontalValue sea diferente de 0, asi me ahorro utilizar un if
        _playerAnimator.SetBool(Run, horizontalAxisValue != 0);
        
        FlipTheCharacter(horizontalAxisValue);

        PlayerJump();
    }
    
    private void FlipTheCharacter(float horizontalAxisValue)
    {
        if (horizontalAxisValue < -0.01f)
        {
            _playerSprite.flipX = true;
        }

        else if (horizontalAxisValue > 0.01f)
        {
            _playerSprite.flipX = false;
        }
    }
    
    
    //La animacion de jump no se esta ejecutando revisar luego
    private void PlayerJump()
    {
        _playerAnimator.SetBool(Grounded, _grounded);

        //Nuestro jugador solo podra saltar cuando se presiones space y grounded sea true
        if (Input.GetKey(KeyCode.Space) && _grounded)
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, speed);
            
            _playerAnimator.SetTrigger(Jump);
            
            _grounded = false;
        }
    }

    //Este metodo se ejecuta cada vez que el collider de nuestro player entra en contacto con otro collider
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _grounded = true;
        }
    }
}