using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame2DBrawler.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Sprites
{
    /// <summary>
    /// An AnimatedSprite is a Sprite that can make use of a Spritesheet to create Animation.
    /// </summary>
    public class AnimatedSprite : Sprite
    {
        #region MemberVariables

        // Variables for handling the Animation.

        private Dictionary<string, Rectangle[]> _frames = new Dictionary<string, Rectangle[]>();
        private Dictionary<string, Vector2> _offsets = new Dictionary<string, Vector2>();

        private double _timeElapsed;
        private double _timeToUpdate;

        private int _frameIndex;
        private string _currentFrame;

        private enum Direction { none, left, up, right, down };
        private Direction _currentDirection = Direction.none;

        private bool _isAttacking = false;

        private bool _playingAnimation = false;
        #endregion

        #region Properties
        public int FPS
        {
            set { _timeToUpdate = 1f / value; }
        }

        public bool PlayingAnimation { get => _playingAnimation; set => _playingAnimation = value; }
        #endregion

        public AnimatedSprite(string name, Vector2 position, PlayerIndex playerIndex, GraphicsDevice graphicsDevice, Texture2D texture = null, KeyboardInput keyboardInput = null, GamePadInput gamePadInput = null) : base(name, position, playerIndex, graphicsDevice, texture, keyboardInput, gamePadInput)
        {
            FPS = 20;

            AddAnimation(12, 0, 0, "Down", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 0, 0, "IdleDown", 50, 50, new Vector2(0, 0));
            AddAnimation(12, 50, 0, "Up", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 50, 0, "IdleUp", 50, 50, new Vector2(0, 0));
            AddAnimation(8, 100, 0, "Left", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 100, 0, "IdleLeft", 50, 50, new Vector2(0, 0));
            AddAnimation(8, 100, 8, "Right", 50, 50, new Vector2(0, 0));
            AddAnimation(1, 100, 8, "IdleRight", 50, 50, new Vector2(0, 0));
            AddAnimation(9, 150, 0, "AttackDown", 70, 80, new Vector2(0, 0));
            AddAnimation(9, 230, 0, "AttackUp", 70, 80, new Vector2(-13, -27));
            AddAnimation(9, 310, 0, "AttackLeft", 70, 70, new Vector2(-30, -5));
            AddAnimation(9, 380, 0, "AttackRight", 70, 70, new Vector2(+15, -5));

            PlayAnimation("IdleDown");

            // Set initial size of BoundingBox. In AnimatedSprite initial size of BoundingBox depends on size of first active frame.
            // This BoundingBox Update also has to happen when frames change, since frames can have different sizes.
            Rectangle temp = _frames[_currentFrame][_frameIndex];
            _boundingBox.Width = temp.Width;
            _boundingBox.Height = temp.Height;

            // Setting constant values for drawable BoundingBox lines.
            // constant = These values do not change depending on things that happen during runtime.
            _boundingBoxLeftLine.Width = 2;
            _boundingBoxTopLine.Height = 2;
            _boundingBoxRightLine.Width = 2;
            _boundingBoxBottomLine.Height = 2;

        }

        private void AddAnimation(int numFrames, int yPos, int xStartFrame, string name, int width, int height, Vector2 offset)
        {
            // Creates an array of rectangles which will be used when playing an animation
            Rectangle[] Rectangles = new Rectangle[numFrames];

            // Fills up the array of rectangles
            for (int i = 0; i < numFrames; i++)
            {
                Rectangles[i] = new Rectangle((i + xStartFrame) * width, yPos, width, height);
            }

            // Store frames and offset in two different dictionaries. But both with same key (name.)
            _frames.Add(name, Rectangles);
            _offsets.Add(name, offset);
        }

        /// <summary>
        /// Central Methods for HandleKeyboardInput and HandleGamePadInput.
        /// This way, both methods don't have to have their own treatment for the same input actions.
        /// If something is changed here, both Keyboard and GamePad can immediately execute that change.
        /// </summary>
        #region InputHelperMethods
        protected override void GoLeft()
        {
            base.GoLeft();
            PlayAnimation("Left");
            _currentDirection = Direction.left;
        }

        protected override void GoUp()
        {
            base.GoUp();
            PlayAnimation("Up");
            _currentDirection = Direction.up;
        }

        protected override void GoRight()
        {
            base.GoRight();
            PlayAnimation("Right");
            _currentDirection = Direction.right;
        }

        protected override void GoDown()
        {
            base.GoDown();
            PlayAnimation("Down");
            _currentDirection = Direction.down;
        }

        protected void Idle()
        {
            if (_currentFrame.Contains("Left"))
                PlayAnimation("IdleLeft");

            if (_currentFrame.Contains("Right"))
                PlayAnimation("IdleRight");

            if (_currentFrame.Contains("Up"))
                PlayAnimation("IdleUp");

            if (_currentFrame.Contains("Down"))
                PlayAnimation("IdleDown");
        }

        protected void Attack()
        {
            _isAttacking = true;
            // LEFT ATTACK
            if (_currentFrame.Contains("Left"))
            {
                PlayAnimation("AttackLeft");
                _currentDirection = Direction.left;

                _boundingBox.Y -= 5;
                _boundingBox.X -= 20;
            }

            // UP ATTACK
            if (_currentFrame.Contains("Up"))
            {
                PlayAnimation("AttackUp");
                _currentDirection = Direction.up;

                _boundingBox.Y -= 20;
                _boundingBox.X -= 5;
            }

            // RIGHT ATTACK
            if (_currentFrame.Contains("Right"))
            {
                PlayAnimation("AttackRight");
                _currentDirection = Direction.right;

                _boundingBox.Y -= 5;
                _boundingBox.X += 5;
            }

            // DOWN ATTACK
            if (_currentFrame.Contains("Down"))
            {
                PlayAnimation("AttackDown");
                _currentDirection = Direction.down;

                _boundingBox.X -= 5;
            }
        }

        #endregion 

        public override void HandleKeyboardInput(KeyboardState keyboardState)
        {
            // Directional Movement or Idle
            if (!_isAttacking)
            {
                // LEFT
                if (keyboardState.IsKeyDown(_keyboardInput.Left))
                    GoLeft();

                // UP
                if (keyboardState.IsKeyDown(_keyboardInput.Up))
                    GoUp();

                // RIGHT
                if (keyboardState.IsKeyDown(_keyboardInput.Right))
                    GoRight();

                // DOWN
                if (keyboardState.IsKeyDown(_keyboardInput.Down))
                    GoDown();

                // No Movement => Idle Frame for respective Direction.
                else
                    Idle();
            }

            // ATTACKING
            if (keyboardState.IsKeyDown(_keyboardInput.Attack))
                Attack();

            _currentDirection = Direction.none;
        }

        public override void HandleGamePadInput(GamePadState gamePadState)
        {
            // Directional Movement or Idle
            if (!_isAttacking)
            {
                // LEFT
                if (gamePadState.IsButtonDown(_gamePadInput.Left))
                    GoLeft();

                // UP
                if (gamePadState.IsButtonDown(_gamePadInput.Up))
                    GoUp();

                // RIGHT
                if (gamePadState.IsButtonDown(_gamePadInput.Right))
                    GoRight();

                // DOWN
                if (gamePadState.IsButtonDown(_gamePadInput.Down))
                    GoDown();

                // No Movement => Idle Frame for respective Direction.
                else
                    Idle();
            }

            // ATTACKING
            if (gamePadState.IsButtonDown(_gamePadInput.Attack))
                Attack();

            _currentDirection = Direction.none;
        }

        /// <summary>
        /// Handles frame Update.
        /// </summary>
        /// <param name="GameTime">GameTime</param>
        public override void Update(GameTime gameTime)
        {
            // HandleInput, Update Position, Update BoundingBox Position.
            base.Update(gameTime);

            // If GamePad is connected, handle it's input. Else, handle Keyboard's input.
            // Da das Sprite eigentlich schon weiß, ob Input per Keyboard oder GamePad kommt,
            // könnte dies irgendwie an das AnimatedSprite weitergeleitet werden.
            _gamePadState = GamePad.GetState(_playerIndex);
            if (_gamePadState.IsConnected)
                HandleGamePadInput(_gamePadState);
            else
                HandleKeyboardInput(Keyboard.GetState());

            // Adds time that has elapsed since our last draw
            _timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;

            // We need to change our image if the timeElapsed is greater than our timeToUpdate(calculated by our framerate)
            if (_timeElapsed > _timeToUpdate)
            {
                // Resets the timer in a way, so that we keep our desired FPS
                _timeElapsed -= _timeToUpdate;

                // Increment frameIndex
                if (_frameIndex < _frames[_currentFrame].Length - 1)
                    _frameIndex++;

                // Restarts the animation
                else
                {
                    AnimationDone(_currentFrame);
                    _frameIndex = 0;
                }
            }

            // Update BoundingBox size to fit current frame size.
            _boundingBox.Width = _frames[_currentFrame][_frameIndex].Width;
            _boundingBox.Height = _frames[_currentFrame][_frameIndex].Height;
        }

        /// <summary>
        /// Draws the sprite on the screen
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch</param>
        // TODO: Collide-Logic shouldn't be in here!
        public void Draw(SpriteBatch spriteBatch, List<AnimatedSprite> animSprites = null)
        {
            spriteBatch.Draw(_texture, _position + _offsets[_currentFrame], _frames[_currentFrame][_frameIndex], Color.White);

            // Set flag to true if collision happened.
            if (animSprites != null)
                foreach (AnimatedSprite a in animSprites)
                    if (this.CollidesWith(a))
                        _collisionDetected = true;

            if (_collisionDetected)
                DrawBoundingBox(spriteBatch, Color.Red);
            else
                DrawBoundingBox(spriteBatch, Color.Blue);

            // Reset flag for collision detection.
            _collisionDetected = false;
        }

        /// <summary>
        /// Draws this AnimateSprite's BoundingBox. // TODO: Should be Sprite's task, since BoundingBox stems from it.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="graphicsDevice"></param>
        protected override void DrawBoundingBox(SpriteBatch spriteBatch, Color borderColor)
        {
            // TODO: An DrawBoundingBox aus Sprite anpassen. new Operationen weg usw.
            if (!_drawBoundingBox)
                return;

            // Update Left Line values
            _boundingBoxLeftLine.X = _boundingBox.X;
            _boundingBoxLeftLine.Y = _boundingBox.Y;
            _boundingBoxLeftLine.Height = _frames[_currentFrame][_frameIndex].Height;

            // Update Top Line values
            _boundingBoxTopLine.X = _boundingBox.X;
            _boundingBoxTopLine.Y = _boundingBox.Y;
            _boundingBoxTopLine.Width = _frames[_currentFrame][_frameIndex].Width;

            // Update Right Line values 
            _boundingBoxRightLine.X = _boundingBox.X + _frames[_currentFrame][_frameIndex].Width - 2;
            _boundingBoxRightLine.Y = _boundingBox.Y;
            _boundingBoxRightLine.Height = _frames[_currentFrame][_frameIndex].Height;

            // Update Bottom Line values 
            _boundingBoxBottomLine.X = _boundingBox.X;
            _boundingBoxBottomLine.Y = _boundingBox.Y + _frames[_currentFrame][_frameIndex].Height - 2;
            _boundingBoxBottomLine.Width = _frames[_currentFrame][_frameIndex].Width;


            // Draw left line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxLeftLine, borderColor);

            // Draw top line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxTopLine, borderColor);

            // Draw right line 
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxRightLine, borderColor);
            
            // Draw bottom line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxBottomLine, borderColor);
            
        }

        /// <summary>
        /// Plays the animation specified by it's name.
        /// </summary>
        /// <param name="name">Animation to play</param>
        private void PlayAnimation(string name)
        {
            // Makes sure we won't start a new annimation unless it differs from our current animation.
            if (_currentFrame != name && _currentDirection.Equals(Direction.none)) 
            {
                _playingAnimation = true;

                _currentFrame = name;
                _frameIndex = 0;
            }
        }

        /// <summary>
        /// Called everytime a frame finishes.
        /// </summary>
        /// <param name="animation"></param>
        private void AnimationDone(string animation)
        {
            if (animation.Contains("Attack"))
                _isAttacking = false;

            _playingAnimation = false;
        }
    }
}
