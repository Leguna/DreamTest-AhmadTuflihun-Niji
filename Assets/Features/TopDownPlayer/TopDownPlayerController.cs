using Facing;
using PauseSystem;
using UnityEngine;

namespace TopDownPlayer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TopDownPlayerController : MonoBehaviour, IPauseAble
    {
        [SerializeField] private TopDownPlayerCombat combat;
        
        private static readonly int X = Animator.StringToHash("MoveX");
        private static readonly int Y = Animator.StringToHash("MoveY");
        private static readonly int Moving = Animator.StringToHash("Moving");
        
        private Vector2 _movement;
        private Rigidbody2D _rb;
        private Animator _animator;

        private readonly TopDownPlayerModel _model = new();

        private MainGameInputAction _inputAction;

        private FacingDirection _facingDirection;

        private PlayerState _playerState;

        public void Init(GameState gameState)
        {
            if (combat == null) TryGetComponent(out combat);
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            combat.Init(gameState);

            InitInputAction();
            UpdateAnim();
        }

        private void InitInputAction()
        {
            _inputAction = new MainGameInputAction();
            _inputAction.Enable();
            _inputAction.Player.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
            _inputAction.Player.Move.canceled += _ => Move(Vector2.zero);
            _inputAction.Player.Interact.performed += _ => combat.Attack(_facingDirection);
        }

        private void Move(Vector2 moveDir)
        {
            _movement = moveDir;
            if (_movement != Vector2.zero)
            {
                _facingDirection = _movement.ToFacingDirection();
                combat.UpdateFacingDirection(_facingDirection);
            }

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

        public void Pause()
        {
            gameObject.SetActive(false);
        }

        public void Resume()
        {
            gameObject.SetActive(true);
        }
    }

    internal enum PlayerState
    {
        Idle,
        Moving,
        Attacking
    }
}