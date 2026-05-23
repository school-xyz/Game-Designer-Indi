using UnityEngine;

public class PlayerRunner : MonoBehaviour
{
    [Tooltip("Сила прыжка")]
    public float jumpStrenght = 5.0f;

    //Физическое тело игрока
    private Rigidbody2D _body = null; 

    private void Start()
    {
        //Получаем компонент RigidBody2D из текущего объекта
        if(_body == null)
            _body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //Проверяем нажатие игроком на левую кнопку мыши
        if (Input.GetMouseButtonDown(0)) 
            Jump();
    }

    private void Jump()
    {
        if (_body == null)
            return;
        //Даем импульс на тело игрока в напревлении вверх и умножаем на силу прыжка
        _body.AddForce(Vector2.up * jumpStrenght, ForceMode2D.Impulse); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Проверяем наличие компонента CoinPickup у коллайдера
        if (collision.TryGetComponent(out CoinPickup coin)) 
        {
            coin.PickUp();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем наличие компонетна ObstacleHit у объекта коллизии 
        if (collision.collider.TryGetComponent(out ObstacleHit obstacle)) 
        {
            obstacle.MakeHit();
        }
    }
}
