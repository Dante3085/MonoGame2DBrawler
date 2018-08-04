using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame2DBrawler.General;
using MonoGame2DBrawler.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame2DBrawler.Sprites
{
    /// <summary>
    /// Describes a static Sprite image.
    /// </summary>
    public class Sprite : GameObject, ICollidable, IMDrawable, IInputable
    {
        #region MemberVariables
        // Stores this Sprite's texture.
        protected Texture2D _texture;

        // Stores this Sprite's Position in the 2D-World.
        public Vector2 _position;

        // Stores this Sprite's velocity as a Vector2.
        private Vector2 _velocity;
        // Constant that will be applied to _velocity for movement.
        private int _speed = 300; // ???? Warum entsteht hierdurch eine flüssige Bewegung.

        // Stores this Sprite's KeyboardInput instance.
        public KeyboardInput _keyboardInput;
        public GamePadInput _gamePadInput;

        #region BoundingBox
        // Stores this Sprite's BoundingBox for collision detection.
        protected Rectangle _boundingBox;

        // Rectangles used to draw the BoundingBox.
        protected Rectangle _boundingBoxLeftLine;
        protected Rectangle _boundingBoxTopLine;
        protected Rectangle _boundingBoxRightLine;
        protected Rectangle _boundingBoxBottomLine;

        protected Texture2D _boundingBoxTexture;

        protected bool _drawBoundingBox = true;
        #endregion

        protected string _name;

        protected PlayerIndex _playerIndex;

        protected GamePadState _gamePadState;
        protected KeyboardState _keyboardState;

        /// <summary>
        /// Variable to remember if collision happened with this Sprite.
        /// </summary>
        protected bool _collisionDetected = false;
        #endregion

        #region Properties
        public string Name { get { return _name; } }

        /// <summary>
        /// Grants access to this Sprite's BoundingBox.
        /// BoundingBox is used for collision detection.
        /// </summary>
        public Rectangle BoundingBox { get { return _boundingBox; } }

        public bool PDrawBoundingBox
        {
            get { return _drawBoundingBox; }
            set { _drawBoundingBox = value; }
        }

        #endregion

        public Sprite(string name, Vector2 position, PlayerIndex playerIndex, GraphicsDevice graphicsDevice, Texture2D texture = null, KeyboardInput input = null, GamePadInput gamePadInput = null)
        {
            // Assign Parameter Values
            _name = name;
            _position = position;
            _playerIndex = playerIndex;
            _texture = texture;
            _keyboardInput = input;
            _gamePadInput = gamePadInput;

            // BoundingBox
            _boundingBox = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);

            _boundingBoxLeftLine = new Rectangle(_boundingBox.X, _boundingBox.Y, 2, _texture.Height);
            _boundingBoxTopLine = new Rectangle(_boundingBox.X, _boundingBox.Y, _texture.Width, 2);
            _boundingBoxRightLine = new Rectangle(_boundingBox.X + _texture.Width - 2, _boundingBox.Y, 2, _texture.Height);
            _boundingBoxBottomLine = new Rectangle(_boundingBox.X, _boundingBox.Y + _texture.Height - 2, _texture.Width, 2);

            _boundingBoxTexture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            _boundingBoxTexture.SetData(new[] { Color.White });

            // HandleConstructorDefaults();
        }

        ///// <summary>
        ///// Assigns Default Values to constructor arguments that have not been passed (null has been passed).
        ///// </summary>
        //private void HandleConstructorDefaults()
        //{
        //    // Assign Default KeyboardInput if none was passed.
        //    if (_keyboardInput == null)
        //        _keyboardInput = new KeyboardInput()
        //        {
        //            Left = Keys.A,
        //            Up = Keys.W,
        //            Right = Keys.D,
        //            Down = Keys.S,
        //            Attack = Keys.Space
        //        };

        //    // Assign Default GamePadInput if none was passed.
        //    if (_gamePadInput == null)
        //        _gamePadInput = new GamePadInput()
        //        {
        //            Left = Buttons.LeftThumbstickLeft,
        //            Up = Buttons.LeftThumbstickUp,
        //            Right = Buttons.LeftThumbstickRight,
        //            Down = Buttons.LeftThumbstickDown,
        //            Attack = Buttons.A
        //        };
        //}

        #region VirtualMethods
        /// <summary>
        /// Draw this Sprite with passed SpriteBatch.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            DrawBoundingBox(spriteBatch, Color.Blue);
        }

        /// <summary>
        /// Central Methods for HandleKeyboardInput and HandleGamePadInput.
        /// This way, both methods don't have to have their own treatment for the same input actions.
        /// If something is changed here, both Keyboard and GamePad can immediately execute that change.
        /// </summary>
        #region InputHelperMethods
        protected virtual void GoLeft()
        {
            _velocity.X = -_speed;
        }

        protected virtual void GoUp()
        {
            _velocity.Y = -_speed;
        }

        protected virtual void GoRight()
        {
            _velocity.X = _speed;
        }

        protected virtual void GoDown()
        {
            _velocity.Y = _speed;
        }

        #endregion

        #region HandleInput
        /// <summary>
        /// Handles basic KeyboardInput for this Sprite.
        /// </summary>
        public virtual void HandleKeyboardInput(KeyboardState keyboardState)
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
        }

        
        /// <summary>
        /// Handles basic GamePadInput for this Sprite.
        /// </summary>
        public virtual void HandleGamePadInput(GamePadState gamePadState)
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
        }
        #endregion

        /// <summary>
        /// Checks whether or not this Sprite collides with partner.
        /// For this check each Sprite's BoundingBoxes are used.
        /// Returns true if both BoundingBoxes intersected.
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public virtual bool CollidesWith(ICollidable partner)
        {
            // This can't collide with itself => Return false.
            if (this.Equals(partner))
                return false;

            // Return whether or not this collides with partner.
            return this._boundingBox.Intersects(partner.BoundingBox);
        }

        protected virtual void DrawBoundingBox(SpriteBatch spriteBatch, Color borderColor)
        {
            if (!_drawBoundingBox)
                return;

            // Update Left Line values
            _boundingBoxLeftLine.X = _boundingBox.X;
            _boundingBoxLeftLine.Y = _boundingBox.Y;

            // Update Top Line values
            _boundingBoxTopLine.X = _boundingBox.X;
            _boundingBoxTopLine.Y = _boundingBox.Y;

            // Update Right Line values
            _boundingBoxRightLine.X = _boundingBox.X + _texture.Width - 2;
            _boundingBoxRightLine.Y = _boundingBox.Y;

            // Update Bottom Line values
            _boundingBoxBottomLine.X = _boundingBox.X;
            _boundingBoxBottomLine.Y = _boundingBox.Y + _texture.Height - 2;

            // Draw Left Line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxLeftLine, borderColor);

            // Draw Top Line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxTopLine, borderColor);

            // Draw Right Line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxRightLine, borderColor);

            // Draw Bottom Line
            spriteBatch.Draw(_boundingBoxTexture, _boundingBoxBottomLine, borderColor);
        }

        /// <summary>
        /// Updates Sprite each frame.
        /// Checks input.
        /// Updates position using velocity.
        /// Resets velocity.
        /// // TODO: 
        /// </summary>
        public virtual void Update(GameTime gameTime)
        {
            // Apply Velocity to Position.
            _position.X += (float)((double)_velocity.X * gameTime.ElapsedGameTime.TotalSeconds);
            _position.Y += (float)((double)_velocity.Y * gameTime.ElapsedGameTime.TotalSeconds);

            // Set BoundingBox Position to new Position of this Sprite.
            _boundingBox.X = (int)_position.X;
            _boundingBox.Y = (int)_position.Y;

            // If GamePad is connected, handle it's input. Else, handle Keyboard's input.
            _gamePadState = GamePad.GetState(_playerIndex);
            if (_gamePadState.IsConnected)
                HandleGamePadInput(_gamePadState);
            else
                HandleKeyboardInput(Keyboard.GetState());

            // Reset Velocity. Prevents Sprite from moving without there being actual input.
            _velocity = Vector2.Zero;
        }

        public override string ToString()
        {
            return _name + "[" + _position.X + "|" + _position.Y + "]";
        }
        #endregion
    }
}
