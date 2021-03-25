using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {
        private SpriteFont font;

        Texture2D ballTexture;
        Texture2D p1PaddleTexture;
        Texture2D p2PaddleTexture;
        Texture2D middleLineTexture;

        Vector2 ballPosition;
        Vector2 ballSpeed;
        Vector2 p1PaddlePosition;
        Vector2 p2PaddlePosition;
        Vector2 middleLinePosition;

        //float ballSpeed;
        float p1PaddleSpeed;
        float p2PaddleSpeed;

        int p1Score = 0;
        int p2Score = 0;
        int gameStart = 0;

        Random r = new Random();


        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static Texture2D rect;
        private void DrawRectangle(Rectangle coords, Color color)
        {
            if (rect == null)
            {
                rect = new Texture2D(_graphics.GraphicsDevice, 1, 1);
                rect.SetData(new[] { Color.White });
            }
            _spriteBatch.Draw(rect, coords, color);
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //ballSpeed = 100f;

            //ballSpeed = new Vector2(100, 100);
            ballSpeed = new Vector2(-150, 150);

            //ballAngle = r.Next(4, 5);

            //System.Diagnostics.Trace.Write("Ball Angle: ");
            //System.Diagnostics.Trace.Write(ballAngle * 45);
            //System.Diagnostics.Trace.WriteLine("");

            p1PaddleSpeed = 350f;
            p2PaddleSpeed = 350f;

            //ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, 0);
            ballPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            p1PaddlePosition = new Vector2(16, _graphics.PreferredBackBufferHeight / 2);
            p2PaddlePosition = new Vector2(_graphics.PreferredBackBufferWidth-16, _graphics.PreferredBackBufferHeight / 2);
            middleLinePosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Score");

            // TODO: use this.Content to load your game content here
            //ballTexture = Content.Load<Texture2D>("ball");
            ballTexture = Content.Load<Texture2D>("pongBall");
            p1PaddleTexture = Content.Load<Texture2D>("paddle");
            p2PaddleTexture = Content.Load<Texture2D>("paddle");
            middleLineTexture = Content.Load<Texture2D>("middle");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            int maxX = _graphics.PreferredBackBufferWidth;
            int maxY = _graphics.PreferredBackBufferHeight - ballTexture.Height/2;
            ballPosition.X += ballSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;
            ballPosition.Y += ballSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Check for Collision
            if (ballPosition.X < 0 || ballPosition.X > maxX)
                ballSpeed.X *= -1;
            // Floor & Ceiling
            if (ballPosition.Y < 0 || ballPosition.Y > maxY)
                ballSpeed.Y *= -1;

            // P2 Score
            if (ballPosition.X < 0)
            {
                p2Score++;
                ballPosition.X = _graphics.PreferredBackBufferWidth / 2;
                ballPosition.Y = _graphics.PreferredBackBufferHeight / 2;
                ballSpeed.X = -175;
                ballSpeed.Y = 0;

            }

            // P1 Score
            if (ballPosition.X > maxX)
            {
                p1Score++;
                ballPosition.X = _graphics.PreferredBackBufferWidth / 2;
                ballPosition.Y = _graphics.PreferredBackBufferHeight / 2;
                ballSpeed.X = 175;
                ballSpeed.Y = 0;
            }

            Rectangle ballRect =
                new Rectangle((int)ballPosition.X, (int)ballPosition.Y,
                ballTexture.Width, ballTexture.Height);

            Rectangle p1PaddleRect =
                new Rectangle((int)p1PaddlePosition.X-(p1PaddleTexture.Width/2), (int)p1PaddlePosition.Y - (p1PaddleTexture.Height / 2),
                    p1PaddleTexture.Width, p1PaddleTexture.Height+8);

            Rectangle p2PaddleRect =
                new Rectangle((int)p2PaddlePosition.X-(p2PaddleTexture.Width/2)+16, (int)p2PaddlePosition.Y - (p2PaddleTexture.Height / 2),
                    p2PaddleTexture.Width, p2PaddleTexture.Height+8);

            if (ballRect.Intersects(p1PaddleRect) && ballSpeed.X < 0)
            {
                if (ballSpeed.Y < 0)
                    ballSpeed.Y -= 50;
                else
                    ballSpeed.Y += 50;

                if (ballSpeed.X < 0)
                    ballSpeed.X -= 50;
                else
                    ballSpeed.X += 50;
                ballSpeed.X *= -1;
            }

            if (ballRect.Intersects(p2PaddleRect) && ballSpeed.X > 0)
            {
                if (ballSpeed.Y < 0)
                    ballSpeed.Y -= 50;
                else
                    ballSpeed.Y += 50;

                if (ballSpeed.X < 0)
                    ballSpeed.X -= 50;
                else
                    ballSpeed.X += 50;
                ballSpeed.X *= -1;
            }

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();

            /*
            if (kstate.IsKeyDown(Keys.Up))
                ballPosition.Y -= ballSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                ballPosition.Y += ballSpeed.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Left))
                ballPosition.X -= ballSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                ballPosition.X += ballSpeed.X * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (ballPosition.Y > _graphics.PreferredBackBufferHeight - ballTexture.Height)
                ballPosition.Y = _graphics.PreferredBackBufferHeight - ballTexture.Height;
            else if (ballPosition.Y < 0)
                ballPosition.Y = 1;
            */
            
            // P1 Paddle Controls
            if (kstate.IsKeyDown(Keys.W))
                p1PaddlePosition.Y -= p1PaddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.S))
                p1PaddlePosition.Y += p1PaddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Paddle Boundary
            if (p1PaddlePosition.Y > _graphics.PreferredBackBufferHeight - p1PaddleTexture.Height / 2)
                p1PaddlePosition.Y = _graphics.PreferredBackBufferHeight - p1PaddleTexture.Height / 2;
            else if (p1PaddlePosition.Y < p1PaddleTexture.Height / 2)
                p1PaddlePosition.Y = p1PaddleTexture.Height / 2;


            // P2 Paddle Controls
            if (kstate.IsKeyDown(Keys.Up))
                p2PaddlePosition.Y -= p2PaddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Down))
                p2PaddlePosition.Y += p2PaddleSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Paddle Boundary
            if (p2PaddlePosition.Y > _graphics.PreferredBackBufferHeight - p2PaddleTexture.Height / 2)
                p2PaddlePosition.Y = _graphics.PreferredBackBufferHeight - p2PaddleTexture.Height / 2;
            else if (p2PaddlePosition.Y < p2PaddleTexture.Height / 2)
                p2PaddlePosition.Y = p2PaddleTexture.Height / 2;
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.DrawString(font, p1Score.ToString(), new Vector2(100, 50), Color.White);
            _spriteBatch.DrawString(font, p2Score.ToString(), new Vector2(700, 50), Color.White);
            //_spriteBatch.DrawString(font, ballPosition.X.ToString(), new Vector2(200, 0), Color.White);

            for (int i = 0; i <= _graphics.PreferredBackBufferHeight / middleLineTexture.Height; ++i)
                _spriteBatch.Draw(
                    middleLineTexture,
                    middleLinePosition,
                    null,
                    Color.White,
                    0f,
                    new Vector2(middleLineTexture.Width / 2, middleLineTexture.Height * i),
                    Vector2.One,
                    SpriteEffects.None,
                    0f
                );

            _spriteBatch.Draw(
                ballTexture,
                ballPosition,
                null,
                Color.White,
                0f,
                new Vector2(ballTexture.Width / 2, ballTexture.Height/2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );
            
            _spriteBatch.Draw(
                p1PaddleTexture,
                p1PaddlePosition,
                null,
                Color.White,
                0f,
                new Vector2(p1PaddleTexture.Width/2, p1PaddleTexture.Height/2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            _spriteBatch.Draw(
                p2PaddleTexture,
                p2PaddlePosition,
                null,
                Color.White,
                0f,
                new Vector2(p2PaddleTexture.Width/2, p2PaddleTexture.Height/2),
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            //DrawRectangle(new Rectangle((int)p1PaddlePosition.X, (int)p1PaddlePosition.Y, p1PaddleTexture.Width/2, p1PaddleTexture.Height/2), Color.BlueViolet);
            //DrawRectangle(new Rectangle((int)p2PaddlePosition.X, (int)p2PaddlePosition.Y, p2PaddleTexture.Width, p2PaddleTexture.Height), Color.BlueViolet);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
