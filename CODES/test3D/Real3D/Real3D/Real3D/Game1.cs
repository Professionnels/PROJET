using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Real3D
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //PERSONNAGE
        private Model persoModel;
        private Vector3 persoPosition;

        private Vector3 Rotation;
        private Vector3 Position;

        KeyboardState currentKeyboardState = new KeyboardState();

        private Vector3 cameraPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix orientation = Matrix.Identity;

        private float aspectRatio;
        private float scale = 10f;

        //MUR
        private Model mur;
        private Vector3 murPosition;
        private Vector3 murRotation;

        //MUR2
        private Model Tmur;
        private Vector3 TmurPosition;
        private Vector3 TmurRotation;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            persoPosition = Vector3.Zero;
            murPosition = new Vector3(1f, 0f, -1.2f);
            TmurPosition = new Vector3(-1f, 0f, -1.2f);

            cameraPosition = new Vector3(100.0f, 50f, 200f);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);

            Position = Vector3.Zero;
            Rotation = new Vector3(90.0f,0f,180f);

            murRotation = new Vector3(90.0f, 0f, 180f);
            TmurRotation = new Vector3(90.0f, 0f, 180f);

            viewMatrix = Matrix.CreateLookAt(cameraPosition, persoPosition, Vector3.Up);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            persoModel = Content.Load<Model>("Models/perso");
            mur = Content.Load<Model>("Models/mur");
            Tmur = Content.Load<Model>("Models/murtableau");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            UpdatePosition(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            Matrix[] transforms = new Matrix[persoModel.Bones.Count];
            persoModel.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in persoModel.Meshes)
            {
                foreach(BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(persoPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z));
                    
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
                
            }

            foreach (ModelMesh mesh in mur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(murPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(murRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(murRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(murRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }

            foreach (ModelMesh mesh in Tmur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(TmurPosition) *
                        Matrix.CreateScale(scale) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(TmurRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(TmurRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(TmurRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }

            base.Draw(gameTime);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            currentKeyboardState = Keyboard.GetState();

            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            float vitesse = 0.01f;


            if (currentKeyboardState.IsKeyDown(Keys.Right))
                Rotation.Y -= time * vitesse*10;
            if (currentKeyboardState.IsKeyDown(Keys.Left))
                Rotation.Y += time * vitesse*10 ;


            if (currentKeyboardState.IsKeyDown(Keys.Down))
                persoPosition.Y += time * vitesse;

            if (currentKeyboardState.IsKeyDown(Keys.Up))
                persoPosition.Y -= time * vitesse;
            

        }
    }
}
