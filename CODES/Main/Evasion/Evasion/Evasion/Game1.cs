#define DEBUG_BB
//#define MULTI
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
using System.IO;

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

        FileStream file;
        StreamReader str;

        bool multijoueurCharge;
        private string infoDeb;


        //VARIABLES NECESSAIRES POUR LE JEU
        //NE PAS LES MODIFIER OU LES SUPPRIMER
        private Vector3 cameraPosition;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix orientation = Matrix.Identity;
        private float aspectRatio;

        //Evasion.Affichage._3D.PNJ bellick;
        private List<Evasion.Affichage._3D.Mur> murs;
        private List<Evasion.Affichage._3D.PNJ> pnjs;
        private List<Evasion.Affichage._3D.Sol> sols;
        private List<Evasion.Affichage._3D.Sol> plafonds;
        Evasion.Affichage._3D.Perso_Model michael;
        Evasion.Affichage._3D.Perso_Model bellick;
        Evasion.Affichage._3D.PNJ prisonnier;
      //  Evasion.Affichage._3D.Mur Tmur;

        Evasion.Affichage._3D.Camera camera;

        Evasion.Affichage._3D.Camera cameratwo;
        public int scale;
        Random rnd;
        //SOL
        private Model sol;
        private Vector3 solPosition;
        private Vector3 solRotation;
        private List<Vector3> positionSpawn;

        //Barre de Vie
        private Evasion.Affichage.Informations.BarreVie Vie;
        private Evasion.Affichage.Informations.BarreVie Vie2;

        public Game1()
        {
            rnd = new Random();
            positionSpawn = new List<Vector3>();
            scale = 20;
            murs = new List<Affichage._3D.Mur>();
            pnjs = new List<Evasion.Affichage._3D.PNJ>();
            sols = new List<Affichage._3D.Sol>();
            plafonds = new List<Affichage._3D.Sol>();
            multijoueurCharge = false;
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
            Son.ChargerSon.Init(Content);
            ChargerImages.InitMenu(Content);
            fenetre.LoadContent(Content_t.Menu);
            Vie = new Affichage.Informations.BarreVie(100, 100, 200, Content, spriteBatch);
            Vie2 = new Affichage.Informations.BarreVie(100, 100, 200, Content, spriteBatch);
            this.textFont = Content.Load<SpriteFont>("MyFont");

            michael = new Affichage._3D.Perso_Model(Content, new Vector3(20, 0, 20), viewMatrix, aspectRatio, graphics, 1);
            prisonnier = new Affichage._3D.PNJ(Content, new Vector3(-20, 0, -20), aspectRatio, TypePerso.prisonnier); 
            //murchangeant = new Affichage._3D.Mur(Content, new Vector3(0, 0, 0), viewMatrix, aspectRatio, Affichage.TypeMur.beton, graphics);
            //Tmur = new Affichage._3D.Mur(Content, new Vector3(1, 0, 0), viewMatrix, aspectRatio, TypeMur.brique, graphics);

            camera = new Affichage._3D.Camera(michael.persoPosition, aspectRatio, fenetre.multi,1);

#if MULTI
            cameratwo = new Affichage._3D.Camera(bellick.persoPosition, aspectRatio, fenetre.multi,2);
#endif
            load(Directory.GetCurrentDirectory() + @"\..\..\..\..\EvasionContent\Maps\prison_2.txt");
        }

        public void load(string fileName)
        {
            file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read);
            str = new StreamReader(file);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    int a = (int)str.Read()-48;
                    if (j%5==0)
                        sols.Add(new Evasion.Affichage._3D.Sol(Content, new Vector3(scale * j, 0, scale * i), viewMatrix, aspectRatio, TypeSol.prison));
                    switch (a)
                    {
                        case 0:
                            break;
                        case 1:
                            murs.Add(new Evasion.Affichage._3D.Mur(Content, new Vector3(scale * j, 0, scale * i), viewMatrix, aspectRatio, TypeMur.brique, graphics));
                            break;
                        case 2:
                            pnjs.Add(new Evasion.Affichage._3D.PNJ(Content, new Vector3(scale * j, 0, scale * i), aspectRatio, TypePerso.bellick));
                            break;
                        case 3:
                            pnjs.Add(new Evasion.Affichage._3D.PNJ(Content, new Vector3(scale * j, 0, scale * i), aspectRatio, TypePerso.gardien));
                            break;
                        case 4:
                            pnjs.Add(new Evasion.Affichage._3D.PNJ(Content, new Vector3(scale * j, 0, scale * i), aspectRatio, TypePerso.prisonnier));
                            break;
                        case 5:
                            pnjs.Add(new Evasion.Affichage._3D.PNJ(Content, new Vector3(20 * j, 0, 20 * i), aspectRatio, TypePerso.perso));
                            break;
                        case 6:
                            positionSpawn.Add(new Vector3(scale * j, 0, scale * i));
                            break;
                        case 7:
                            break;
                        default:
                            break;

                    }
                }
                str.ReadLine();
            }
            file.Close();
            michael.persoPosition = positionSpawn[rnd.Next(positionSpawn.Count)];
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
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                michael.persoPosition = positionSpawn[rnd.Next(positionSpawn.Count)];
                if (fenetre.multi)
                    bellick.persoPosition = positionSpawn[rnd.Next(positionSpawn.Count)];
            }
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

                    if (fenetre.multi)
                    {
                        leftview.Height /= ratioY;
                        rightview.Height /= ratioY;
                        leftview.Width /= ratioX;
                        rightview.Width /= ratioX;
                    }

                }
                else
                {

                    if (fenetre.multi)
                    {
                        leftview.Height *= ratioY;
                        rightview.Height *= ratioY;
                        leftview.Width *= ratioX;
                        rightview.Width *= ratioX;
                    }

                }

                if (fenetre.multi)
                {
                    leftview.Y = (graphics.PreferredBackBufferHeight / 2) - (leftview.Height / 2);
                    rightview.Y = (graphics.PreferredBackBufferHeight / 2) - (rightview.Height / 2);
                    leftview.X = (graphics.PreferredBackBufferWidth / 2) - leftview.Width;
                    rightview.X = leftview.X + leftview.Width + 9;
                }

                this.graphics.IsFullScreen = !(this.graphics.IsFullScreen);
                this.graphics.ApplyChanges();
            }
            fenetre.Update(Keyboard.GetState(), Mouse.GetState());
            if (fenetre.multi && !multijoueurCharge)
            {
                multijoueurCharge = true;
                defaultview = GraphicsDevice.Viewport;
                leftview = defaultview;
                rightview = defaultview;
                leftview.Width = leftview.Width / 2;
                rightview.Width = rightview.Width / 2 - 9;
                rightview.X = leftview.Width + 9;
                bellick = new Affichage._3D.Perso_Model(Content, new Vector3(40, 0, -20), viewMatrix, aspectRatio, graphics, 2);
                bellick.persoPosition = positionSpawn[rnd.Next(positionSpawn.Count)];
                cameratwo = new Affichage._3D.Camera(bellick.persoPosition, aspectRatio, fenetre.multi,2);
            }

            infoDeb = "";

            michael.UpdatePosition(gameTime);
            camera.initialize(michael.persoPosition, michael.Rotation, this.graphics);

            if (fenetre.multi)
            {
                bellick.UpdatePosition(gameTime);
                cameratwo.initialize(bellick.persoPosition, bellick.Rotation, this.graphics);
            }

            prisonnier.UpDate(false, gameTime);

            infoDeb += (1000.0 / gameTime.ElapsedGameTime.TotalMilliseconds).ToString() + '\n';

#if RES
            Evasion.Jeu.Client_L client = new Jeu.Client_L();
            Evasion.Jeu.Server_L serveur = new Jeu.Server_L();

#endif

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (fenetre.ok)
            {
                GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                GraphicsDevice.Clear(Color.Gray);


                if (fenetre.multi)
                {
                    GraphicsDevice.Viewport = leftview;
                    bellick.draw(camera);
                }
                 //   murchangeant.draw(camera);
                 //   solChangeant.draw(camera);
                //    Tmur.draw(camera);
                    int i = 0;
                    for (i = 0; i < murs.Count(); i++)
                        murs[i].draw(camera);
                    int j = 0;
                    for (j= 0; j < pnjs.Count(); j++)
                        pnjs[j].draw(camera);
                    for (j = 0; j < sols.Count(); j++)
                        sols[j].draw(camera);
                    michael.draw(camera);

                    if (fenetre.multi)
                    {
                        GraphicsDevice.Viewport = rightview;
                        bellick.draw(cameratwo);
                        for (i = 0; i < murs.Count(); i++)
                            murs[i].draw(cameratwo);
                        for (j = 0; j < pnjs.Count(); j++)
                            pnjs[j].draw(cameratwo);
                        for (j = 0; j < sols.Count(); j++)
                            sols[j].draw(cameratwo);
                        michael.draw(cameratwo);
                    }
             //   murchangeant.draw(camera);
             //   solChangeant.draw(camera);
             //   Tmur.draw(camera);
              //  michael.draw(camera);
           //     prisonnier.draw(camera);

                if (fenetre.multi)
                {
                    GraphicsDevice.Viewport = rightview;
                  //  bellick.draw(cameratwo);
              //      murchangeant.draw(cameratwo);
               //     solChangeant.draw(cameratwo);
             //       Tmur.draw(cameratwo);
                //    michael.draw(cameratwo);

                    GraphicsDevice.Viewport = defaultview;

                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("Separation"), new Vector2(graphics.PreferredBackBufferWidth / 2, 0), null, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, new Vector2(1, graphics.PreferredBackBufferWidth/600), SpriteEffects.None, 0);
                    spriteBatch.Draw(Content.Load<Texture2D>("Separation"), new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight/2-300), Color.White);
                    spriteBatch.End();
                    GraphicsDevice.Viewport = leftview;
                }

                spriteBatch.Begin();
                //spriteBatch.DrawString(this.textFont, infoDeb + michael.informations, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
                Vie.Draw();
                spriteBatch.End();

                if (fenetre.multi)
                {
                    GraphicsDevice.Viewport = rightview;
                    spriteBatch.Begin();
                    Vie2.Draw();
                    spriteBatch.End();
                }
            }
            else
            {
                spriteBatch.Begin();

                if (fenetre.multi)
                    GraphicsDevice.Viewport = defaultview;

                fenetre.Display(spriteBatch, GraphicsDevice, Content);
#if DEBUG_BB
                //spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
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