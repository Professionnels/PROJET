using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Evasion.Affichage
{
    enum Content_t
    {
        Menu,
        Options,
        Quit,
        NewGame,
        LoadGame,
        Save
    };

    class Fenetre
    {
        public bool ok;
        public bool multi;
        private Jeu.Jeu jeu;
        private Content_t content;
        private Microsoft.Xna.Framework.Game screen;
        private Menu.Menu menu;

        public Fenetre(Microsoft.Xna.Framework.Game screen)
        {
            content = Content_t.Menu;
            this.screen=screen;
            menu = new Menu.Menu("Main menu", this);
        }

        public void LoadContent(Content_t content)
        {
            this.content = content;
            MediaPlayer.Stop();
            switch (content)
            {
                case Content_t.Menu:
                    ok = false;
                    menu.LoadContent(screen);
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
                    jeu = new Jeu.Jeu(this, 1);
                    multi = true;
                    break;
                case Content_t.LoadGame:
                    menu = new Menu.Menu("Pause", this);
                    jeu = new Jeu.Jeu(this, 1); // temporaire


                    break;
                default:
                    Console.WriteLine("Erreur: ce contenu de la fenetre n'est pas pris en compte par l'application.");
                    Console.Read();
                    break;
            }
        }

        public void Update(KeyboardState keyboardState, MouseState mouseState)
        {
            switch (content)
            {
                case Content_t.Menu:
                    menu.Update(keyboardState, mouseState);
                    break;
                case Content_t.NewGame:
                case Content_t.LoadGame:
                    jeu.Update(keyboardState, mouseState);
                    break;
                case Content_t.Save:
                    jeu.DisplaySaveProgressing();
                    break;
                default:
                    break;
            }
        }

        public void Display(SpriteBatch sb)
        {
            switch (content)
            {
                case Content_t.Menu:
                    menu.Display(screen, sb);
                    break;
                case Content_t.NewGame:
                case Content_t.LoadGame:
                    jeu.Display(screen, sb);
                    break;
                default:
                    break;
            }
        }
    }
}
