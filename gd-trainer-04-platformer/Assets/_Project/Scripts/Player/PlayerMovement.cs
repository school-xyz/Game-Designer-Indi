using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Физическое тело персонажа
    private Rigidbody2D _body;
    // Спрайт для отражения направления движения
    private SpriteRenderer _spriteRenderer;

    // Базовая скорость и сила прыжка (задаются из PlayerController)
    private float _moveSpeed;
    private float _jumpForce;

    // Текущий ввод и модификаторы от окружения
    private float _conveyorPush;
    private float _speedMultiplier = 1f;

    [SerializeField] private InputActionReference _moveReference;
    [SerializeField] private InputActionReference _jumpReference;

    [Tooltip("Плавность разгона и остановки. 0 = без плавности")]
    [SerializeField] private float _inputSmoothTime = 0.05f;

    [Tooltip("Сколько секунд после схода с платформы ещё можно прыгнуть")]
    [SerializeField] private float _coyoteTime = 0.1f;

    [Tooltip("Сколько секунд игра помнит раннее нажатие прыжка")]
    [SerializeField] private float _jumpBuffer = 0.1f;

    [Tooltip("Насколько быстрее персонаж падает вниз")]
    [SerializeField] private float _fallMultiplier = 2.5f;

    [Tooltip("Насколько укорачивается прыжок при отпускании кнопки")]
    [SerializeField] private float _jumpCutMultiplier = 0.5f;

    private Vector2 _moveDirection;
    private float _smoothMoveInput;
    private float _smoothMoveVelocity;
    private float _coyoteTimeCounter;
    private float _jumpBufferCounter;

    // Счётчик пересечений с объектами Ground
    private int _groundContacts;

    public bool isGround => _groundContacts > 0;
    public bool isMoving => _moveDirection.x != 0;
    public bool isIdle => _moveDirection.x == 0;
    public bool isJump => !isGround;


    // Вызов: передаёт в движение скорость и силу прыжка из PlayerController
    public void Init(float moveSpeed, float jumpForce)
    {
        _jumpReference.action.performed += OnJumpPressed;
        _jumpReference.action.canceled += OnJumpCanceled;
        _body = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _moveSpeed = moveSpeed;
        _jumpForce = jumpForce;
    }

    private void OnDestroy()
    {
        _jumpReference.action.performed -= OnJumpPressed;
        _jumpReference.action.canceled -= OnJumpCanceled;
    }

    private void Update()
    {
        _moveDirection = _moveReference.action.ReadValue<Vector2>();
        // Разворачиваем спрайт только при движении
        if (_moveDirection.x != 0)
            _spriteRenderer.flipX = _moveDirection.x < 0;

        // Если стоим на земле, обновляем запас времени для прыжка
        if (isGround)
            _coyoteTimeCounter = _coyoteTime;
        else
            _coyoteTimeCounter -= Time.deltaTime;

        _jumpBufferCounter -= Time.deltaTime;

        // Прыжок срабатывает, если нажали чуть заранее или чуть позже края
        if (_jumpBufferCounter > 0 && _coyoteTimeCounter > 0)
            Jump();
    }

    private void FixedUpdate()
    {
        float horizontalInput = _moveDirection.x;

        if (_inputSmoothTime > 0)
        {
            horizontalInput = Mathf.SmoothDamp(_smoothMoveInput,_moveDirection.x,ref _smoothMoveVelocity,_inputSmoothTime);

            _smoothMoveInput = horizontalInput;
        }

        // Считаем итоговую скорость с учётом слайма
        float playerSpeed = horizontalInput * _moveSpeed * _speedMultiplier;

        // Применяем горизонтальную скорость и толчок конвейера
        _body.linearVelocity = new Vector2( playerSpeed + _conveyorPush,_body.linearVelocity.y);

        // При падении добавляем тяжести, чтобы прыжок не был слишком ватным
        if (_body.linearVelocity.y < 0)
        {
            _body.linearVelocity += Vector2.up * (Physics2D.gravity.y * (_fallMultiplier - 1) * Time.fixedDeltaTime);
        }
    }

    private void OnJumpPressed(InputAction.CallbackContext ctx)
    {
        // Запоминаем нажатие прыжка на короткое время
        _jumpBufferCounter = _jumpBuffer;
    }

    private void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        // Если отпустили кнопку во время подъёма, делаем прыжок короче
        if (_body.linearVelocity.y > 0)
            _body.linearVelocity = new Vector2(_body.linearVelocity.x, _body.linearVelocity.y * _jumpCutMultiplier);
    }

    private void Jump()
    {
        // Задаём вертикальную скорость напрямую
        _body.linearVelocity = new Vector2(_body.linearVelocity.x,_jumpForce * _speedMultiplier);

        // Сбрасываем землю, чтобы нельзя было прыгнуть повторно в воздухе
        _groundContacts = 0;
        _coyoteTimeCounter = 0;
        _jumpBufferCounter = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Ground>() != null)
            _groundContacts++;

        if (other.TryGetComponent(out Conveyor conveyor))
            _conveyorPush = conveyor.PushSpeed;

        if (other.TryGetComponent(out Slime slime))
            _speedMultiplier = slime.SpeedMultiplier;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Ground>() != null)
            _groundContacts = Mathf.Max(0, _groundContacts - 1);

        if (other.TryGetComponent(out Conveyor conveyor))
        {
            if (_conveyorPush == conveyor.PushSpeed)
                _conveyorPush = 0f;
        }

        if (other.GetComponent<Slime>() != null)
            _speedMultiplier = 1f;
    }
}
