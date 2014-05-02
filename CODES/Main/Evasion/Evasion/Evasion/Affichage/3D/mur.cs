#define DEBUG_BB

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
        public string informations;

        public TypeMur type;

        private Texture2D[] textures = new Texture2D[2];

#if DEBUG_BB
        public BoundingBox boundingBoxes = new BoundingBox();
#endif

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

            this.informations = "";

            Vector3 meshMax = new Vector3(float.MinValue);
            Vector3 meshMin = new Vector3(float.MaxValue);

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

#if DEBUG_BB
                    BuildBoundingBox(mesh, effect.World, ref meshMin, ref meshMax);
#endif
                    
                }
                mesh.Draw();


            }

#if DEBUG_BB
            boundingBoxes = new BoundingBox(Vector3.Min(meshMin, meshMax),
                Vector3.Max(meshMin, meshMax));
            this.informations = "Mur : " + "Min : " + boundingBoxes.Min.ToString() + '\n' + "Max : " + boundingBoxes.Max.ToString();
#endif
        }

        private void BuildBoundingBox(ModelMesh mesh, Matrix meshTransform, ref Vector3 meshMin, ref Vector3 meshMax)
        {

            foreach (ModelMeshPart part in mesh.MeshParts)
            {
                int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

                VertexPositionNormalTexture[] vertexData = new VertexPositionNormalTexture[part.NumVertices];
                part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, stride);

                Vector3 vertPosition = new Vector3();

                for (int i = 0; i < vertexData.Length; i++)
                {
                    vertPosition = vertexData[i].Position;

                    meshMin = Vector3.Min(meshMin, vertPosition);
                    meshMax = Vector3.Max(meshMax, vertPosition);
                }
            }

            meshMin = Vector3.Transform(meshMin, meshTransform);
            meshMax = Vector3.Transform(meshMax, meshTransform);

        }


    }
}
