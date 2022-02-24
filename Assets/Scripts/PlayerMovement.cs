using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D _playerBody;
    private SpriteRenderer _playerSprite;

    // Start is called before the first frame update
    private void Awake()
    {
        _playerBody = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
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
        
        FlipTheCharacter(horizontalAxisValue);
        
        if (Input.GetKey(KeyCode.Space))
        {
            _playerBody.velocity = new Vector2(_playerBody.velocity.x, speed);
        }
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
}
