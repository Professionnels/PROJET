using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Evasion.Affichage;
using Evasion.Affichage._3D;

namespace Evasion.Decors
{
    class Sols : ObjetDecors
    {        
        private Evasion.Affichage._3D.Sol image;

        public Sols(ContentManager Content, Vector3 position, Matrix view, float aspectRatio, TypeSol type, GraphicsDeviceManager graphics)
            : base(Content, position,  new Vector2(0,0), view, aspectRatio, graphics)
        {
            image = new Evasion.Affichage._3D.Sol(Content, position, view, aspectRatio, type);
        }

        public void Update()
        {

        }

        public override void Draw(Camera camera)
        {
            image.draw(camera);
        }
    }
}
