#define DEBUG_BB
#define MULTI
//#define RES

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

        Viewport defaultview;
        Viewport leftview;
        Viewport rightview;

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

        //Evasion.Affichage._3D.PNJ bellick;
        Evasion.Affichage._3D.Perso_Model michael;
        Evasion.Affichage._3D.Perso_Model bellick;
        Evasion.Affichage._3D.Mur murchangeant;
        Evasion.Affichage._3D.Sol solChangeant;
        Evasion.Affichage._3D.Mur Tmur;

        Evasion.Affichage._3D.Camera camera;

#if MULTI
        Evasion.Affichage._3D.Camera cameratwo;
#endif

        //SOL
        private Model sol;
        private Vector3 solPosition;
        private Vector3 solRotation;

        //Barre de Vie
        private Evasion.Affichage.Informations.BarreVie Vie;
        private Evasion.Affichage.Informations.BarreVie Vie2;

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
            infoDeb = "";
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
#if MULTI
            defaultview = GraphicsDevice.Viewport;
            leftview = defaultview;
            rightview = defaultview;
            leftview.Width = leftview.Width / 2;
            rightview.Width = rightview.Width / 2 - 9;
            rightview.X = leftview.Width + 9;
#endif
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            Vie = new Affichage.Informations.BarreVie(100, 100, 200, Content, spriteBatch);
            Vie2 = new Affichage.Informations.BarreVie(100, 100, 200, Content, spriteBatch);
            this.textFont = Content.Load<SpriteFont>("MyFont");

            michael = new Affichage._3D.Perso_Model(Content, new Vector3(20, 0, 20), viewMatrix, aspectRatio, graphics, 1); 

#if MULTI
            bellick = new Affichage._3D.Perso_Model(Content, new Vector3(40, 0, -20), viewMatrix, aspectRatio, graphics, 2); 
#endif

            murchangeant = new Affichage._3D.Mur(Content, new Vector3(0, 0, 0), viewMatrix, aspectRatio, Affichage.TypeMur.beton, graphics);
            solChangeant = new Affichage._3D.Sol(Content, Vector3.Zero, viewMatrix, aspectRatio, TypeSol.prison);
            Tmur = new Affichage._3D.Mur(Content, new Vector3(1, 0, 0), viewMatrix, aspectRatio, TypeMur.brique, graphics);

            camera = new Affichage._3D.Camera(michael.persoPosition, aspectRatio, fenetre.multi);

#if MULTI
            cameratwo = new Affichage._3D.Camera(bellick.persoPosition, aspectRatio, fenetre.multi);
#endif
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
                int ratioX = graphics.PreferredBackBufferWidth / Evasion.Affichage.Constantes.SCREEN_WIDTH;
                int ratioY = graphics.PreferredBackBufferHeight / Evasion.Affichage.Constantes.SCREEN_HEIGHT;
                if (this.graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = Evasion.Affichage.Constantes.SCREEN_WIDTH;
                    graphics.PreferredBackBufferHeight = Evasion.Affichage.Constantes.SCREEN_HEIGHT;
#if MULTI
                    leftview.Width /= ratioX;
                    rightview.Width /= ratioX;
                    leftview.Height /= ratioY;
                    rightview.Height /= ratioY;
#endif
                }
                else
                {
#if MULTI
                    leftview.Width *= ratioX;
                    rightview.Width *= ratioX;
                    leftview.Height *= ratioY;
                    rightview.Height *= ratioY;
#endif
                }
#if MULTI
                leftview.Y = (graphics.PreferredBackBufferHeight / 2)-(leftview.Height/2);
                rightview.Y = (graphics.PreferredBackBufferHeight / 2) - (rightview.Height / 2);
                leftview.X = (graphics.PreferredBackBufferWidth / 2) - leftview.Width;
                rightview.X = leftview.X+leftview.Width + 9;
#endif
                this.graphics.IsFullScreen = !(this.graphics.IsFullScreen);
                this.graphics.ApplyChanges();
            }
            fenetre.Update(Keyboard.GetState(), Mouse.GetState());

            infoDeb = "";

            michael.UpdatePosition(gameTime);
            camera.initialize(michael.persoPosition, michael.Rotation, this.graphics);

#if MULTI
            bellick.UpdatePosition(gameTime);
            cameratwo.initialize(bellick.persoPosition, bellick.Rotation, this.graphics);
#endif

            //BoundingBox b1 = new BoundingBox(new Vector3(0, 0, 0), new Vector3(1, 1, 1));
            //BoundingBox b2 = new BoundingBox(new Vector3(0.5f, 0.5f, 0.5f), new Vector3(1.5f, 1.5f, 1.5f));

            //if (b1.Intersects(b2))
            //    infoDeb += "COLLISION\n";
            //else
            //    infoDeb += "FALSE\n";

            infoDeb += (1000.0 / gameTime.ElapsedGameTime.TotalMilliseconds).ToString() + '\n';

            Vector3 pos = michael.getPosition();
            Vector3 rot = michael.getRotation();
            
#if RES
            Evasion.Jeu.Client_L client = new Jeu.Client_L();
            Evasion.Jeu.Server_L serveur = new Jeu.Server_L();

#endif
            // michael.UpdatePosition(gameTime);

            // camera.initialize(michael.persoPosition, michael.Rotation, this.graphics);

            foreach (BoundingBox box in michael.boundingBoxes)
            {
                foreach (BoundingBox mBox in murchangeant.boundingBoxes)
                {
                    if (box.Intersects(mBox))
                    {
                        michael.persoPosition = pos;
                        michael.Rotation = rot;
                        infoDeb += "\nCollision\n";
                        infoDeb += box.Min.ToString() + " " + box.Max.ToString() + "\n";
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (fenetre.ok)
            {
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                GraphicsDevice.Clear(Color.Gray);

                

#if MULTI
                GraphicsDevice.Viewport = leftview;
                    bellick.draw(camera);
#endif
                    murchangeant.draw(camera);
                    solChangeant.draw(camera);
                    Tmur.draw(camera);
                    michael.draw(camera);

#if MULTI
                GraphicsDevice.Viewport = rightview;
                    bellick.draw(cameratwo); 
                    murchangeant.draw(cameratwo);
                    solChangeant.draw(cameratwo);
                    Tmur.draw(cameratwo);
                    michael.draw(cameratwo);

                GraphicsDevice.Viewport = defaultview;

                spriteBatch.Begin();
                spriteBatch.Draw(Content.Load<Texture2D>("Separation"), new Vector2(graphics.PreferredBackBufferWidth/2, 0), Color.White);
                spriteBatch.End();
#endif
#if MULTI
                GraphicsDevice.Viewport = leftview;
#endif
                spriteBatch.Begin();
                spriteBatch.DrawString(this.textFont, infoDeb + michael.informations, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                Vie.Draw();
                spriteBatch.End();

#if MULTI
                GraphicsDevice.Viewport = rightview;
                spriteBatch.Begin();
                Vie2.Draw();
                spriteBatch.End();
#endif
            }
            else
            {
                spriteBatch.Begin();
                fenetre.Display(spriteBatch, GraphicsDevice, Content);
#if DEBUG_BB
                spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#else
                spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#endif
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }

        

        //private void InitPhysique()
        //{
        //    solPosition = new Vector3(0f, 0f, 0f);
        //    cameraPosition = new Vector3(200.0f, 100f, 100f);
        //    aspectRatio = graphics.GraphicsDevice.Viewport.AspectRatio;
        //    projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
        //    solRotation = new Vector3(90.0f, 0f, 180f);
        //    viewMatrix = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
        //}


    }
}