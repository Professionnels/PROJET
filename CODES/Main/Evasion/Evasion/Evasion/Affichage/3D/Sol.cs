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

    class Sol
    {
        public Model sol;
        private Vector3 solPosition;

        private Matrix viewMatrix;
        private Matrix projectionMatrix;
        private Vector3 Rotation;

        public int indexSol;

        private Matrix orientation;
        private float scale = 15f;

        public TypeSol type;

        private Texture2D[] textures = new Texture2D[2];

        public Sol(ContentManager Content, Vector3 position, Matrix view, float aspectRatio, TypeSol type)
        {
            this.sol = Content.Load<Model>("Models\\sol");
            this.solPosition = position;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(40.0f), aspectRatio, 100.0f, 10000.0f);
            viewMatrix = view;
            orientation = Matrix.Identity;
            textures[0] = Content.Load<Texture2D>("Models\\evasion");
            textures[1] = Content.Load<Texture2D>("Models\\sol_prison");
            this.type = type;
            this.initPhyMur();
            this.initMur();
            indexSol = 0;
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

            Matrix[] transforms = new Matrix[sol.Bones.Count];
            sol.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in sol.Meshes)
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
                                    Matrix.CreateTranslation(solPosition);
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;

                }
                mesh.Draw();
            }
        }


    }
}
