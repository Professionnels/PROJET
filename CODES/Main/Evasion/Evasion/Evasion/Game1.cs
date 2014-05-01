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
        SpriteFont textFont;

        //VARIABLES NECESSAIRES POUR LE JEU
        //NE PAS LES MODIFIER OU LES SUPPRIMER
        private Vector3 cameraPosition;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix orientation = Matrix.Identity;
        private float aspectRatio;

        Evasion.Affichage._3D.PNJ bellick;
        Evasion.Affichage._3D.Perso_Model michael;
        Evasion.Affichage._3D.Mur murchangeant;
        Evasion.Affichage._3D.Sol solChangeant;

        Evasion.Affichage._3D.Camera camera;

        //SOL
        private Model sol;
        private Vector3 solPosition;
        private Vector3 solRotation;

        //Barre de Vie
        private Evasion.Affichage.Informations.BarreVie Vie;

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
        
        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            InitPhysique();
            camera = new Evasion.Affichage._3D.Camera(Vector3.Zero, aspectRatio);
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            this.textFont = Content.Load<SpriteFont>("MyFont");

            bellick = new Affichage._3D.PNJ(Content, Vector3.Zero, Vector3.Zero, viewMatrix, aspectRatio, Affichage.TypePerso.bellick);
            michael = new Affichage._3D.Perso_Model(Content, new Vector3(20, 0, 20), aspectRatio, this.camera, graphics);
            murchangeant = new Affichage._3D.Mur(Content, new Vector3(0, 0, 0), viewMatrix, aspectRatio, camera, Affichage.TypeMur.beton);
            solChangeant = new Affichage._3D.Sol(Content, Vector3.Zero, viewMatrix, aspectRatio, TypeSol.prison);         
        }

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

            if (Keyboard.GetState().IsKeyDown(Keys.T))
            {
                murchangeant.indexMur = murchangeant.indexMur ^ 1;
            }
            
            
            //viewMatrix = Matrix.CreateLookAt(, Vector3.Zero, Vector3.Up);
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
                
                //bellick.draw();
                michael.draw();
                murchangeant.draw();
                //solChangeant.draw();

                spriteBatch.Begin();
                spriteBatch.DrawString(this.textFont, michael.informations + '\n' + camera.informations, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                //Vie.Draw();
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                fenetre.Display(spriteBatch);
                spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        private void InitPhysique()
        {
            solPosition = new Vector3(0f, 0f, 0f);
            cameraPosition = new Vector3(200.0f, 100f, 100f);
            aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            solRotation = new Vector3(90.0f, 0f, 180f);
            viewMatrix = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
        }
    }
}