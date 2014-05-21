using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Evasion.Affichage;
using Microsoft.Xna.Framework.Graphics;
using Evasion.Affichage._3D;

namespace Evasion.Decors
{
    class Murs : ObjetDecors
    {
        private Evasion.Affichage._3D.Mur image;

        public Murs(ContentManager Content, Vector3 position, Vector2 taille, Matrix view, float aspectRatio, TypeMur type, GraphicsDeviceManager graphics)
            : base(Content, position, taille, view, aspectRatio, graphics)
        {
            image = new Evasion.Affichage._3D.Mur(Content, position, view, aspectRatio, type, graphics);
        }

        public void Update()
        {

        }

        override public void Draw(Camera camera)
        {
            image.draw(camera);
        }

    }
}