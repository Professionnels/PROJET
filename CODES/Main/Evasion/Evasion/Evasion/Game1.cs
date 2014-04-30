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
using Evasion.Affichage;

namespace Evasion
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        // Personnage perso = new Personnage();
        Fenetre fenetre;

        Evasion.Affichage._3D.priso   bellick;
        Evasion.Affichage._3D.Perso_Model michael;

        SpriteFont textFont;
        string informations;

        //PERSONNAGE

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

        //SOL
        private Model sol;
        private Vector3 solPosition;
        private Vector3 solRotation;

        public Game1()
        {
            fenetre = new Fenetre(this);
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = Evasion.Affichage.Constantes.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = Evasion.Affichage.Constantes.SCREEN_HEIGHT;
            this.Window.Title = "Evasion";
            this.graphics.ApplyChanges();
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
            this.IsMouseVisible = true;
            InitPhysique();
            base.Initialize();
            bellick = new Affichage._3D.priso(Content, Vector3.Zero, Vector3.Zero, viewMatrix, aspectRatio);
            michael = new Affichage._3D.Perso_Model(Content, new Vector3(20,0,20), viewMatrix, aspectRatio);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            LoadModel();
            this.textFont = Content.Load<SpriteFont>("MyFont");
            informations = "";
            
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
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {

                graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                if (this.graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = Evasion.Affichage.Constantes.SCREEN_WIDTH;
                    graphics.PreferredBackBufferHeight = Evasion.Affichage.Constantes.SCREEN_HEIGHT;
                }
                this.graphics.IsFullScreen = !(this.graphics.IsFullScreen);
                this.graphics.ApplyChanges();
            }
            fenetre.Update(Keyboard.GetState(), Mouse.GetState());
            // TODO: Add your update logic here

            //UpdatePosition(gameTime);
            bellick.UpdatePosition(gameTime);
            michael.UpdatePosition(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            if (fenetre.ok)
            {
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                
                bellick.draw();
                michael.draw();
                DrawMeshes();
                spriteBatch.Begin();
                spriteBatch.DrawString(this.textFont, bellick.informations, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                fenetre.Display(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void InitPhysique()
        {
            murPosition = new Vector3(1f, 0f, -1.37f);
            TmurPosition = new Vector3(-1f, 0f, -1.2f);
            solPosition = new Vector3(0f, 0f, -0.28f);

            cameraPosition = new Vector3(200.0f, 100f, 100f);

            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);

            murRotation = new Vector3(90.0f, 0f, 180f);
            TmurRotation = new Vector3(90.0f, 0f, 180f);
            solRotation = new Vector3(90.0f, 0f, 180f);

            viewMatrix = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
        }

        private void LoadModel()
        {
            mur = Content.Load<Model>("Models/mur");
            Tmur = Content.Load<Model>("Models/murtableau");
            sol = Content.Load<Model>("Models/sol");
        }

        private void DrawMeshes()
        {
            Matrix[] transforms = new Matrix[michael.persoModel.Bones.Count];
            michael.persoModel.CopyAbsoluteBoneTransformsTo(transforms);

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

            foreach (ModelMesh mesh in sol.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(solPosition) *
                        Matrix.CreateScale(100) * Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(TmurRotation.X)) *
                        Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(TmurRotation.Y)) * Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(TmurRotation.Z));

                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                }
                mesh.Draw();
            }
        }
    }
}