//#define MULTI

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

namespace Evasion.Affichage._3D
{
    class Camera
    {
        public Vector3 position;
        public Matrix viewMatrix;
        public Matrix projectionMatrix;
        public Vector3 cameraReference;
        public Matrix rotationMatrix;
        public Vector3 transformedReference;
        public Vector3 cameraLookat;
        public Vector3 thirdPersonReference;
        public string informations;
        public float aspectRatio;
        private bool multi;
        private float offset;
        public MouseState previousMouse;

        private Dictionary<string, Keys> controles1;
        private Dictionary<string, Keys> controles2;
        private Dictionary<string, Keys> touches;

        public Camera(Vector3 position, float aspectRatio, bool multi, int nbCam)
        {
            this.thirdPersonReference = new Vector3(0, 40, -40);
            this.position = position;
            this.viewMatrix = Matrix.CreateLookAt(this.position, new Vector3(0, 35, 0), Vector3.Up);
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            this.cameraReference = new Vector3(0, 0, 1);
            this.transformedReference = Vector3.Zero;
            this.aspectRatio = aspectRatio;
            this.multi = multi;

            controles1 = new Dictionary<string, Keys>();
            controles1.Add("haut", Keys.NumPad8);
            controles1.Add("bas", Keys.NumPad2);
            controles1.Add("gauche", Keys.NumPad4);
            controles1.Add("droite", Keys.NumPad6);

            controles2 = new Dictionary<string, Keys>();
            controles2.Add("haut", Keys.R);
            controles2.Add("bas", Keys.F);
            controles2.Add("gauche", Keys.Q);
            controles2.Add("droite", Keys.E);

            if (nbCam == 1)
                touches = controles1;
            else
                touches = controles2;
        }

        public void initialize(Vector3 persoPos, Vector3 persoRot, GraphicsDeviceManager graphics, MouseState ms)
        {
            int z = (ms.Y -previousMouse.Y)/2;
            thirdPersonReference.Y += z;

            if (thirdPersonReference.Y > 55)
                thirdPersonReference.Y = 55;
            else if (thirdPersonReference.Y < -5)
                thirdPersonReference.Y = -5;

            rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(-persoRot.Y + offset));
            transformedReference = Vector3.Transform(thirdPersonReference, rotationMatrix);
            position = transformedReference + persoPos;
            cameraLookat = position + transformedReference;
            viewMatrix = Matrix.CreateLookAt(position, Vector3.Add(persoPos, new Vector3(0, 35, 0)), new Vector3(0f, 1f, 0f));
            informations = "";


            Viewport viewport = graphics.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
            if (multi)
                aspectRatio /= 2;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1.0f, aspectRatio,
                    1.0f, 10000.0f);
            previousMouse = ms;
            //informations += this.position.ToString();
        }

        public void updatePosition(Vector3 persoPos)
        {
        }
    }
}
