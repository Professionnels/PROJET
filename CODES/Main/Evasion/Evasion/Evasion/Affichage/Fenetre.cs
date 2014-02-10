using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            switch (content)
            {
                case Content_t.Menu:
                    menu.LoadContent(screen);
                    break;
                case Content_t.Quit:
                    screen.Exit();
                    break;
                default:
                    Console.WriteLine("Un nouveau contenu a été chargé !");
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
                default:
                    Console.WriteLine("Un nouveau contenu a été chargé !");
                    break;
            }
        }

        public void Display(SpriteBatch sb)
        {
            menu.Display(screen, sb);
        }
    }
}
