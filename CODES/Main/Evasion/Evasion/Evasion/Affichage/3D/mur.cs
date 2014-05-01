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
    
    class Mur
    {
        public Model mur;
        private Vector3 murPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 Rotation;

        public int indexMur;

        private Matrix orientation;
        private float scale = 10f;

        public TypeMur type;

        private Texture2D[] textures = new Texture2D[2];

        public Vector3 getPosition()
        {
            return murPosition;
        }

        public Vector3 getRotation()
        {
            return Rotation;
        }

        public Mur(ContentManager Content, Vector3 position, Matrix view, float aspectRatio, TypeMur type)
        {
            this.mur = Content.Load<Model>("Models\\mur");
            this.murPosition = position;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            orientation = Matrix.Identity;
            textures[0] = Content.Load<Texture2D>("Models\\briques");
            textures[1] = Content.Load<Texture2D>("Models\\mur_prison");
            this.type = type;
            this.initPhyMur();
            this.initMur();
            indexMur = 0;
        }

        public void initPhyMur()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        private void initMur()
        {
            Rotation = new Vector3(90.0f, 0f, 180f);
        }

        public void draw()
        {

            Matrix[] transforms = new Matrix[mur.Bones.Count];
            mur.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in mur.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.TextureEnabled = true;

                    effect.Texture = textures[(int)(type)];
                    effect.World = transforms[mesh.ParentBone.Index] *
                                    Matrix.CreateScale(scale) *
                                    Matrix.CreateFromAxisAngle(orientation.Right, (float)MathHelper.ToRadians(Rotation.X)) *
                                    Matrix.CreateFromAxisAngle(orientation.Up, (float)MathHelper.ToRadians(Rotation.Y)) *
                                    Matrix.CreateFromAxisAngle(orientation.Forward, (float)MathHelper.ToRadians(Rotation.Z)) *
                                    Matrix.CreateTranslation(murPosition);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;
                    
                }
                mesh.Draw();
            }
        }


    }
}
