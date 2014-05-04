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
        private float offset;

        public Camera(Vector3 position, float aspectRatio)
        {
            this.thirdPersonReference = new Vector3(0, 75, -120);
            this.position = position;
            this.viewMatrix = Matrix.CreateLookAt(this.position, new Vector3(0, 35, 0), Vector3.Up);
            this.projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            this.cameraReference = new Vector3(0, 0, 1);
            this.transformedReference = Vector3.Zero;
            this.aspectRatio = aspectRatio;
        }

        public void initialize(Vector3 persoPos, Vector3 persoRot, GraphicsDeviceManager graphics)
        {

            if (Keyboard.GetState().IsKeyDown(Keys.NumPad8))
                thirdPersonReference.Y += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad2))
                thirdPersonReference.Y -= 5;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad6))
                offset += 5;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                offset -= 5;

            rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(-persoRot.Y + offset));
            transformedReference = Vector3.Transform(thirdPersonReference, rotationMatrix);
            position = transformedReference + persoPos;
            cameraLookat = position + transformedReference;
            viewMatrix = Matrix.CreateLookAt(position, Vector3.Add(persoPos, new Vector3(0, 35, 0)), new Vector3(0f, 1f, 0f));
            informations = "";


            Viewport viewport = graphics.GraphicsDevice.Viewport;
            float aspectRatio = (float)viewport.Width / (float)viewport.Height;
#if MULTI
            aspectRatio /= 2;
#endif
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(1.0f, aspectRatio,
                    1.0f, 10000.0f);
            //informations += this.position.ToString();
        }

        public void updatePosition(Vector3 persoPos)
        {
        }
    }
}
