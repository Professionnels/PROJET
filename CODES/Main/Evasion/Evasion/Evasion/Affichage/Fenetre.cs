using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Evasion.Affichage
{
    enum Content_t
    {
        Menu,
        Options,
        Quit,
        NewGame,
        NewGameMulti,
        LoadGame,
        Reprendre,
        Save
    };

    class Fenetre
    {
        public bool multi;
        public bool ok;
        public Jeu.Jeu jeu;
        private Content_t content;
        private Microsoft.Xna.Framework.Game screen;
        private Menu.Menu menu;

        public Fenetre(Microsoft.Xna.Framework.Game screen)
        {
            multi = false;
            content = Content_t.Menu;
            this.screen=screen;
            menu = new Menu.Menu("Main menu", this);
        }

        public void LoadContent(Content_t content, ContentManager Content, GraphicsDeviceManager graphics, GraphicsDevice gd, SpriteBatch sb)
        {
            this.content = content;
            MediaPlayer.Stop();
            switch (content)
            {
                case Content_t.Menu:
                    ok = false;
                    menu.LoadContent(screen, Content, graphics, gd, sb);
                    break;
                case Content_t.Quit:
                    screen.Exit();
                    break;
                case Content_t.Save:
                    jeu.Save();
                    break;
                case Content_t.NewGame:
                    ok = true;
                    menu = new Menu.Menu("Pause", this);
                    jeu = new Jeu.Jeu(this, false, Content, graphics, gd, sb);
                    multi = false;
                    break;
                case Content_t.NewGameMulti:
                    ok = true;
                    menu = new Menu.Menu("Pause", this);
                    jeu = new Jeu.Jeu(this, true, Content, graphics, gd, sb);
                    multi = true;
                    break;
                case Content_t.LoadGame:
                    menu = new Menu.Menu("Pause", this);
                    jeu = new Jeu.Jeu(this, false, Content, graphics, gd, sb); // temporaire
                    break;
                case Content_t.Reprendre:
                    break;
                default:
                    Console.WriteLine("Erreur: ce contenu de la fenetre n'est pas pris en compte par l'application.");
                    Console.Read();
                    break;
            }
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState, ContentManager Content, GraphicsDeviceManager graphics, GameTime gametime, GraphicsDevice graph, SpriteBatch sb)
        {
            switch (content)
            {
                case Content_t.Menu:
                    menu.Update(keyboardState, mouseState, Content, graphics, graph, sb);
                    break;
                case Content_t.NewGame:
                case Content_t.LoadGame:
                case Content_t.NewGameMulti:
                case Content_t.Reprendre:
                    jeu.Update(keyboardState, mouseState, Content, graphics, gametime, graph, sb);
                    break;
                case Content_t.Save:
                    jeu.DisplaySaveProgressing();
                    break;
                default:
                    break;
            }
        }

        public void Display(SpriteBatch sb, GraphicsDevice gd, ContentManager cm, GraphicsDeviceManager gdm)
        {
            switch (content)
            {
                case Content_t.Menu:
                    menu.Display(screen, sb);
                    break;
                case Content_t.NewGame:
                case Content_t.NewGameMulti:
                case Content_t.LoadGame:
                case Content_t.Reprendre:
                    jeu.Display(screen, sb, gd, cm,gdm);
                    break;
                default:
                    break;
            }
        }
    }
}
