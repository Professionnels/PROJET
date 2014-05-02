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
        public string informations;

        public Camera(Vector3 position, float aspectRatio)
        {
            this.position = position;
            this.viewMatrix = Matrix.CreateLookAt(this.position, Vector3.Zero, Vector3.Up);
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            this.cameraReference = new Vector3(0, 0, 1);
            this.transformedReference = Vector3.Zero;
        }

        public void initialize(Vector3 persoPos, Vector3 persoRot)
        {
            this.position = persoPos;
            this.position.Y += 150;
            this.position.X += 50;
            this.position.Z += 50;
            rotationMatrix = Matrix.CreateRotationY(persoRot.Y);
            transformedReference = Vector3.Transform(cameraReference, rotationMatrix);
            cameraLookat = position + transformedReference;
            viewMatrix = Matrix.CreateLookAt(position, cameraLookat, new Vector3(0f, 1f, 0f));
            informations = "";
            //informations += this.position.ToString();
        }

        public void updatePosition(Vector3 persoPos)
        {
        }
    }
}
