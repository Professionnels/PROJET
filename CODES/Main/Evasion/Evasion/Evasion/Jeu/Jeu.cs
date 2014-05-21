using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Evasion.Affichage;
using Microsoft.Xna.Framework.Media;
using Evasion.Personnages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.IO;
using Evasion.Decors;
using Evasion.Affichage._3D;

namespace Evasion.Jeu
{
    class Jeu
    {
        private Son.Son SonAmbiance1;
        private Son.Son SonAmbiance2;
        private Son.Son current;
        private Son.Son pause;
        private Fenetre Fen;
        private Niveau niveau;
        public bool son;
        public bool appuieSon;
        private bool multi;

        Viewport defaultview;
        Viewport leftview;
        Viewport rightview;

        SpriteFont textFont;
        AffichageHUD hud;
        private string infoDeb;


        //VARIABLES NECESSAIRES POUR LE JEU
        //NE PAS LES MODIFIER OU LES SUPPRIMER
        private Vector3 cameraPosition;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Matrix orientation = Matrix.Identity;
        private float aspectRatio;
        Personnage michael;
        Personnage bellick;

        Camera camera;

        Camera cameratwo;
        public int scale;
        Random rnd;
        ////SOL
        //private Model sol;
        //private Vector3 solPosition;
        //private Vector3 solRotation;

        public Jeu(Fenetre fen, bool multi, ContentManager Content, GraphicsDeviceManager graphics, GraphicsDevice gd, SpriteBatch sb)
        {
            appuieSon = false;
            son = true;
            niveau = new Niveau(3, Content, graphics);
            SonAmbiance1 = new Son.Son(Son.ChargerSon.theme1);
            SonAmbiance2 = new Son.Son(Son.ChargerSon.theme2);
            pause = new Son.Son(Son.ChargerSon.son_pause);
            current = SonAmbiance1;
            Fen = fen;
            this.multi = multi;
            hud = new AffichageHUD(Content, sb, multi);
            rnd = new Random();
            niveau.Load(Content, graphics, viewMatrix, aspectRatio);
            michael = new Joueur("Michael", 100, deplacement_t.marche, Vector3.Zero, 1f, null, Content, graphics, viewMatrix, aspectRatio, niveau);
            michael.Position = niveau.GetPositionPerso();
            camera = new Affichage._3D.Camera(michael.Position, aspectRatio, multi, 1);
            if (multi)
            {
                bellick = new Joueur("Bellick", 100, deplacement_t.marche, Vector3.Zero, 1f, null, Content, graphics, viewMatrix, aspectRatio, niveau);
                bellick.Position = niveau.GetPositionPerso();
                cameratwo = new Affichage._3D.Camera(bellick.Position, aspectRatio, multi, 2);
                defaultview = gd.Viewport;
                leftview = defaultview;
                rightview = defaultview;
                leftview.Width = leftview.Width / 2;
                rightview.Width = rightview.Width / 2 - 9;
                rightview.X = leftview.Width + 9;
            }
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState, ContentManager Content, GraphicsDeviceManager graphics, GameTime gametime, GraphicsDevice graph, SpriteBatch sb)
        {
            // Musique

            if (Keyboard.GetState().IsKeyUp(Keys.P))
                appuieSon=false;
            if (Keyboard.GetState().IsKeyDown(Keys.P) && !appuieSon)
            {
                if (son)
                {
                    current.Stop();
                    son = false;
                }
                else
                {
                    current = current == SonAmbiance1 ? SonAmbiance2 : SonAmbiance1;
                    current.Play();
                    son = true;
                }
                appuieSon = true;
            }

            if (MediaPlayer.State == MediaState.Stopped && son)
            {
                current.Play();
                current = current == SonAmbiance1 ? SonAmbiance2 : SonAmbiance1;
            }

            // Pause
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                pause.Play();
                Fen.LoadContent(Content_t.Menu, Content, graphics, graph, sb);
            }

            // Actions
            if (Keyboard.GetState().IsKeyDown(Keys.V))
                michael.ModifierVie(-2);
            if (Keyboard.GetState().IsKeyDown(Keys.P))
                michael.ModifierVie(2);
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {

                graphics.PreferredBackBufferWidth = graph.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = graph.DisplayMode.Height;
                int ratioX = graphics.PreferredBackBufferWidth / Evasion.Affichage.Constantes.SCREEN_WIDTH;
                int ratioY = graphics.PreferredBackBufferHeight / Evasion.Affichage.Constantes.SCREEN_HEIGHT;
                if (graphics.IsFullScreen)
                {
                    graphics.PreferredBackBufferWidth = Evasion.Affichage.Constantes.SCREEN_WIDTH;
                    graphics.PreferredBackBufferHeight = Evasion.Affichage.Constantes.SCREEN_HEIGHT;

                    if (multi)
                    {
                        leftview.Height /= ratioY;
                        rightview.Height /= ratioY;
                        leftview.Width /= ratioX;
                        rightview.Width /= ratioX;
                    }

                }
                else
                {

                    if (multi)
                    {
                        leftview.Height *= ratioY;
                        rightview.Height *= ratioY;
                        leftview.Width *= ratioX;
                        rightview.Width *= ratioX;
                    }

                }

                if (multi)
                {
                    leftview.Y = (graphics.PreferredBackBufferHeight / 2) - (leftview.Height / 2);
                    rightview.Y = (graphics.PreferredBackBufferHeight / 2) - (rightview.Height / 2);
                    leftview.X = (graphics.PreferredBackBufferWidth / 2) - leftview.Width;
                    rightview.X = leftview.X + leftview.Width + 9;
                }

                graphics.IsFullScreen = !(graphics.IsFullScreen);
                graphics.ApplyChanges();
            }

            infoDeb = "";

            michael.Update(keyboardState, mouseState, gametime);
            camera.initialize(michael.Position, michael.Rotation, graphics);
            if (multi)
            {
                bellick.Update(keyboardState, mouseState, gametime);
                cameratwo.initialize(bellick.Position, bellick.Rotation, graphics);
            }

            infoDeb += (1000.0 / gametime.ElapsedGameTime.TotalMilliseconds).ToString() + '\n';

//#if RES
//            Evasion.Jeu.Client_L client = new Jeu.Client_L();
//            Evasion.Jeu.Server_L serveur = new Jeu.Server_L();

//#endif

        }

        public void Save()
        { }

        public void DisplaySaveProgressing()
        { }

        public void Display(Microsoft.Xna.Framework.Game screen, SpriteBatch sb, GraphicsDevice gd, ContentManager cm, GraphicsDeviceManager gdm)
        {
            // Camera
            if (Fen.ok)
            {
                gd.DepthStencilState = DepthStencilState.Default;

                gd.Clear(Color.Gray);

                niveau.Draw(camera);
                michael.Display(camera);

                if (multi)
                {
                    gd.Viewport = leftview;
                    bellick.Display(camera);
                    gd.Viewport = rightview;
                    niveau.Draw(cameratwo);
                    bellick.Display(cameratwo);
                    michael.Display(cameratwo);
                }

                if (multi)
                {
                    gd.Viewport = defaultview;

                    sb.Draw(cm.Load<Texture2D>("Separation"), new Vector2(gdm.PreferredBackBufferWidth / 2, 0), null, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, new Vector2(1, gdm.PreferredBackBufferWidth / 600), SpriteEffects.None, 0);
                }

                //spriteBatch.DrawString(this.textFont,Tmur.boundingBoxes.ToString()+"\n"+michael.boundingBoxes.ToString()+"\n"+this.infoDeb, Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
            }
            else
            {

                if (multi)
                    gd.Viewport = defaultview;

#if DEBUG_BB
                //spriteBatch.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#else
                sb.DrawString(this.textFont, "Menu", Vector2.Zero, Color.White, 0.0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0);
#endif
            }

            // HUD
            hud.Display(gd, multi, defaultview, leftview, rightview);
        }
    }
}
