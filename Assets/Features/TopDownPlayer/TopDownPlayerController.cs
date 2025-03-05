using UnityEngine;

namespace TopDownPlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownPlayerController : MonoBehaviour
    {
        private static readonly int X = Animator.StringToHash("MoveX");
        private static readonly int Y = Animator.StringToHash("MoveY");
        private static readonly int Moving = Animator.StringToHash("Moving");

        private Vector2 _movement;
        private Rigidbody2D _rb;
        private Animator _animator;

        private readonly TopDownPlayerModel _model = new();

        private MainGameInputAction _inputAction;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            InitInputAction();
            UpdateAnim();
        }

        private void InitInputAction()
        {
            _inputAction = new MainGameInputAction();
            _inputAction.Enable();
            _inputAction.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            _inputAction.Player.Move.canceled += _ => Move(Vector2.zero);
        }

        private void Move(Vector2 moveDir)
        {
            _movement = moveDir;
            UpdateAnim();
        }

        private void OnDisable()
        {
            _inputAction?.Disable();
        }


        private void UpdateAnim()
        {
            if (_animator == null) return;
            _animator.SetFloat(X, _movement.x);
            _animator.SetFloat(Y, _movement.y);
            _animator.SetBool(Moving, _movement != Vector2.zero);
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = _movement.normalized * _model.MoveSpeed;
        }
    }
}