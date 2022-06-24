using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Moving_PC_V2 : IMoving
    {
        [Header("Config MovingComponet for player")]
        [Header("Config speeds")]
        [SerializeField] private float speed;
        [SerializeField] private float speedRoll;

        [Header("Add components")]
        [SerializeField] private ChangeAnimation animationManager;


        // ====== Properties for moving ======//

        private int horizontalDirection = 0;
        private int verticalDirection = 0;
        [SerializeField] private Vector2 direction;
        [SerializeField] private Vector2 oldDirection;
        [SerializeField] private bool _left;
        [SerializeField] private bool _right;
        [SerializeField] private bool _up;
        [SerializeField] private bool _down;

        [SerializeField]private int status = 0;
        private const int INDLE = 0;
        private const int RUN = 1;
        private const int ROLL = 2;
        private const int NONE = -1;

        #region Setter and getter
        public int Status
        {
            get => status;
            set
            {
                status = value;
                setAnimation();
            }
        }
        public Vector2 OldDirection
        {
            get => oldDirection; set
            {
                if (value != Vector2.zero) oldDirection = value;
            }
        }
        #endregion

        /// Detect direction when user press button or press joy-stick

        #region Get direction
        private void ditectDirection()
        {
           
            if (Input.GetKeyDown(KeyCode.A))
                _left = true;
            else if (Input.GetKeyUp(KeyCode.A))
                _left = false;


            if (Input.GetKeyDown(KeyCode.D))
                _right = true;
            else if (Input.GetKeyUp(KeyCode.D))
                _right = false;


            if (Input.GetKeyDown(KeyCode.W))
                _up = true;

            else if (Input.GetKeyUp(KeyCode.W))
                _up = false;


            if (Input.GetKeyDown(KeyCode.S))
                _down = true;
            else if (Input.GetKeyUp(KeyCode.S))
                _down = false;

            horizontalDirection = _left || _right ? ( _left ? -1 : 1) : 0;
            verticalDirection = _up || _down ? (_down ? -1: 1) : 0;

        }
        private void changeHorizaltalDirection(int hoz)
        {
            horizontalDirection += hoz;
        }
        private void changeVerticalDirection(int ver)
        {
            verticalDirection += ver;
        }
        #endregion

        #region set animation
        void setAnimation()
        {
            //const string INDLE_UP = "Player_body_top_indle";
            //const string INDLE_DOWN = "Player_body_down_indle";
            //const string INDLE_LEFT = "Player_body_left_indle";
            //const string INDLE_RIGHT = "Player_body_right_indle";
            //const string INDLE_UP_RIGHT = "Player_body_topright_indle";
            //const string INDLE_UP_LEFT = "Player_body_topleft_indle";
            //const string INDLE_DOWN_RIGHT = "Player_body_downright_indle";
            //const string INDLE_DOWN_LEFT = "Player_body_downleft_indle";


            //const string RUN_UP = "Player_body_top_run";
            //const string RUN_DOWN = "Player_body_down_run";
            //const string RUN_LEFT = "Player_body_left_run";
            //const string RUN_RIGHT = "Player_body_right_run";
            //const string RUN_UP_RIGHT = "Player_body_topright_run";
            //const string RUN_UP_LEFT = "Player_body_topleft_run";
            //const string RUN_DOWN_RIGHT = "Player_body_downright_run";
            //const string RUN_DOWN_LEFT = "Player_body_downleft_run";

            //const string ROLL_UP = "Player_body_top_roll";
            //const string ROLL_DOWN = "Player_body_down_roll";
            //const string ROLL_LEFT = "Player_body_left_roll";
            //const string ROLL_RIGHT = "Player_body_right_roll";
            //const string ROLL_UP_RIGHT = "Player_body_topright_roll";
            //const string ROLL_UP_LEFT = "Player_body_topleft_roll";
            //const string ROLL_DOWN_RIGHT = "Player_body_downright_roll";
            //const string ROLL_DOWN_LEFT = "Player_body_downleft_roll";

            string animation_name = "";
            bool right = direction.x > 0;
            bool left = direction.x < 0;
            bool up = direction.y > 0;
            bool down = direction.y < 0;


            if (Status == INDLE)
            {
                right = OldDirection.x > 0;
                left = OldDirection.x < 0;
                up = OldDirection.y > 0;
                down = OldDirection.y < 0;
                string animation_direction = "";
                animation_direction += up ? "top" : "";
                animation_direction += down ? "down" : "";
                animation_direction += left ? "left" : "";
                animation_direction += right ? "right" : "";

                if (animation_direction != "")
                    animation_name = "Player_body_" + animation_direction + "_indle";
            }
            else if (Status == RUN)
            {
                string animation_direction = "";
                animation_direction += up ? "top" : "";
                animation_direction += down ? "down" : "";
                animation_direction += left ? "left" : "";
                animation_direction += right ? "right" : "";

                if (animation_direction != "")
                    animation_name = "Player_body_" + animation_direction + "_run";
            }
            else if (Status == ROLL)
            {
                string animation_direction = "";
                animation_direction += up ? "top" : "";
                animation_direction += down ? "down" : "";
                animation_direction += left ? "left" : "";
                animation_direction += right ? "right" : "";

                if (animation_direction != "")
                    animation_name = "Player_body_" + animation_direction + "_roll";
            }
            if (animation_name != "")
            {
                animationManager.play(animation_name);
            }
        }
        #endregion

        private void Update()
        {
            ditectDirection();
            if (Status != ROLL)
            {
                setDirection();   
            }
            ditectRoll();
            if (Status != ROLL)
            {
                Status = horizontalDirection == 0 && verticalDirection == 0 ? INDLE : RUN;
            }

            if (Status == RUN)
            {
                move(speed);
            }
            else if (Status == ROLL)
            {
                move(speedRoll);
            }


        }
        private void endRoll()
        {
            direction = Vector2.zero;
            horizontalDirection = 0;
            verticalDirection = 0;
            Status = INDLE;
        }
        private void setDirection()
        {
            OldDirection = direction;
            direction = new Vector2(horizontalDirection, verticalDirection);
            if(horizontalDirection != 0 && verticalDirection != 0 )
            {
                direction = new Vector2(
                   (horizontalDirection > 0 ? 1 : -1) * Mathf.Sqrt(2) / 2,
                   (verticalDirection > 0 ? 1 : -1) * Mathf.Sqrt(2) / 2
                   );
            }
        }


        private void move(float _speed)
        {
            spirit.Target = (Vector2)transform.position + direction;
            spirit.State = SpiritV2.FOLLOW_PLAYER;
            transform.Translate(new Vector2(direction.x, direction.y) * _speed * Time.deltaTime);
        }
        private void ditectRoll()
        {
            if (Status != ROLL && Input.GetKeyDown(KeyCode.Space))
            {

                if (direction == Vector2.zero || Status == INDLE)
                {
                    direction = OldDirection;
                }
    
                Status = ROLL;
            }
        }

        private void Start()
        {
            Status = INDLE;
            animationManager.play("Player_body_top_indle");
            OldDirection = Vector2.up;
        }

        public override void forceStop()
        {
            direction = Vector2.zero;
            Status = INDLE;
        }

        public override void configSpeed(float sp)
        {
            this.speed = sp;
        }

        public override float getSpeed()
        {
            return speed;
        }

    }
}
