#define DEBUG_BB

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

        private string infoDeb;


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
        Evasion.Affichage._3D.Mur Tmur;

        

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
            infoDeb = "";
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            this.textFont = Content.Load<SpriteFont>("MyFont");
            Vie = new Affichage.Informations.BarreVie(100, 100, 200, Content, spriteBatch);

            bellick = new Affichage._3D.PNJ(Content, Vector3.Zero, Vector3.Zero, viewMatrix, aspectRatio, Affichage.TypePerso.bellick);
            michael = new Affichage._3D.Perso_Model(Content, new Vector3(20, 0, 20), viewMatrix, aspectRatio, graphics);
            murchangeant = new Affichage._3D.Mur(Content, new Vector3(0, 0, 0), viewMatrix, aspectRatio, Affichage.TypeMur.beton);
            solChangeant = new Affichage._3D.Sol(Content, Vector3.Zero, viewMatrix, aspectRatio, TypeSol.prison);
            Tmur = new Affichage._3D.Mur(Content, new Vector3(1, 0, 0), viewMatrix, aspectRatio, TypeMur.brique);
        }

        protected override void UnloadContent()
        {

        }

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

            infoDeb = "";

            

            //BoundingBox b1 = new BoundingBox(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            //BoundingBox b2 = new BoundingBox(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.5f, 1.5f, 1.5f));

            //if (b1.Intersects(b2))
            //    infoDeb += "COLLISION\n";
            //else
            //    infoDeb += "FALSE\n";

            infoDeb += (1000.0 / gameTime.ElapsedGameTime.TotalMilliseconds).ToString()+'\n';
            infoDeb += michael.informations + '\n';
            infoDeb +='\n' + murchangeant.informations +
                '\n' + Tmur.informations;

            Vector3 pos = michael.getPosition();
            Vector3 rot = michael.getRotation();

            michael.UpdatePosition(gameTime);

            if (michael.boundingBoxes.Intersects(murchangeant.boundingBoxes)
                || michael.boundingBoxes.Intersects(Tmur.boundingBoxes))
            {
                michael.persoPosition = pos;
                michael.Rotation = rot;
            }

            
                
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here
            if (fenetre.ok)
            {
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;
                
                bellick.draw();
                michael.draw();
                murchangeant.draw();
                solChangeant.draw();
                Tmur.draw();

                spriteBatch.Begin(); spriteBatch.DrawString(this.textFont, infoDeb, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                //Vie.Draw();
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                fenetre.Display(spriteBatch);
#if DEBUG_BB
                spriteBatch.DrawString(this.textFont, "Menuqqqqqq", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#else
                spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#endif
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