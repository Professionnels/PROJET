using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evasion.Affichage._3D;
using Evasion.Decors;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.IO;
using Evasion.Affichage;
using Microsoft.Xna.Framework.Input;
using Evasion.Personnages;

namespace Evasion.Jeu
{
    class Niveau
    {
        public List<Personnage> pnjs;
        private List<ObjetDecors> decors;
        private List<Sols> sols;
       //private List<Sols> plafonds;
        string fichier;
        private FileStream file;
        private StreamReader str;
        private List<Vector3> positionSpawn;

        public Niveau(int id, ContentManager Content, GraphicsDeviceManager graphics)
        {
            fichier = Directory.GetCurrentDirectory() + @"\..\..\..\..\EvasionContent\Maps\prison_"+id+".txt";
            sols=new List<Sols>();
            //plafonds = new List<Sols>();
            pnjs = new List<Personnage>();
            positionSpawn = new List<Vector3>();
            decors = new List<ObjetDecors>();
            
        }

        public void Load(ContentManager Content, GraphicsDeviceManager graphics, Matrix viewMatrix, float aspectRatio)
        {
            file = new FileStream(fichier, FileMode.OpenOrCreate, FileAccess.Read);
            str = new StreamReader(file);
            for (int i = 0; i < 40; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    int a = (int)str.Read() - 48;
                    if (j % 5 == 0)
                    {
                        sols.Add(new Sols(Content, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), viewMatrix, aspectRatio, TypeSol.prison, graphics));
                        //plafonds.Add(new Sols(Content, new Vector3(Constantes.scale * j, 100, Constantes.scale * i), viewMatrix, aspectRatio, TypeSol.prison, graphics));
                    }
                    switch (a)
                    {
                        case 0:
                            break;
                        case 1:
                            decors.Add(new Murs(Content, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), new Vector2(10,10), viewMatrix, aspectRatio, TypeMur.brique, graphics));
                            break;
                        case 2:
                            pnjs.Add(new Ennemi("Ennemi", 100, deplacement_t.marche, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), 1f, Content, graphics, viewMatrix, aspectRatio, this, 75, 7000, 500, false, TypePerso.bellick));
                            break;
                        case 3:
                            pnjs.Add(new Ennemi("Ennemi", 75, deplacement_t.accroupi, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), 1f, Content, graphics, viewMatrix, aspectRatio, this, 50, 5000, 300, false, TypePerso.gardien));
                            break;
                        case 4:
                            pnjs.Add(new Ennemi("Ennemi", 50, deplacement_t.accroupi, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), 0.7f, Content, graphics, viewMatrix, aspectRatio, this, 0, 1000, 150, false, TypePerso.prisonnier));
                            break;
                        case 5:
                            pnjs.Add(new Ennemi("Ennemi", 75, deplacement_t.courir, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), 1.5f, Content, graphics, viewMatrix, aspectRatio, this, 100, 10000, 3000, false, TypePerso.perso));
                            break;
                        case 6:
                            positionSpawn.Add(new Vector3(Constantes.scale * j, 0, Constantes.scale * i));
                            break;
                        case 7:
                            break;
                        case 8:
                            decors.Add(new Murs(Content, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), new Vector2(10, 10), viewMatrix, aspectRatio, TypeMur.barreaux, graphics));
                            break;
                        case 9:
                            decors.Add(new Murs(Content, new Vector3(Constantes.scale * j, 0, Constantes.scale * i), new Vector2(10, 10), viewMatrix, aspectRatio, TypeMur.beton, graphics));
                            break;
                        default:
                            break;

                    }
                }
                str.ReadLine();
            }
            file.Close();
        }

        public Vector3 GetPositionPerso()
        {
            Random rnd = new Random();
            int x = rnd.Next(positionSpawn.Count);
            Vector3 res = positionSpawn[x];
            positionSpawn.RemoveAt(x);
            return res;
        }

        public position_t Collision(Vector3 position)
        {
            Rectangle rect = new Rectangle((int)position.X - Personnage.tailleX / 2, (int)position.Z - Personnage.tailleZ/2, Personnage.tailleX, Personnage.tailleZ);
            Rectangle rect2 = new Rectangle(0, 0, 0, 0);
            for (int i = 0; i < decors.Count; i++)
            {
                rect2.X = (int)decors[i].Position.X - (int)(decors[i].Taille.X/2);
                rect2.Y = (int)decors[i].Position.Z - (int)(decors[i].Taille.Y / 2);
                rect2.Width = (int)decors[i].Taille.X;
                rect2.Height = (int)decors[i].Taille.Y;
                if (rect.Intersects(rect2))
                    return position_t.decors;
            }
            rect2.Width = Personnage.tailleX;
            rect2.Height = Personnage.tailleZ;
            for (int i = 0; i < pnjs.Count; i++)
            {
                rect2.X = (int)pnjs[i].Position.X - Personnage.tailleX/2;
                rect2.Y = (int)pnjs[i].Position.Z - Personnage.tailleZ/2;
                if (rect.Intersects(rect2))
                    return position_t.joueur;
            } 
            return position_t.vide;
        }

        public void Update(Joueur j, KeyboardState keyboardState, MouseState mouseState, GameTime gametime)
        {
            int i = 0;
            for (i = 0; i < pnjs.Count(); i++)
                pnjs[i].Update(j, keyboardState, mouseState, gametime);
        }

        public void Draw(Camera camera)
        {
            int i = 0;
                for (i = 0; i < decors.Count(); i++)
                    decors[i].Draw(camera);
                for (i = 0; i < pnjs.Count(); i++)
                    pnjs[i].Display(camera);
                for (i = 0; i < sols.Count(); i++)
                    sols[i].Draw(camera);
                //for (i = 0; i < plafonds.Count(); i++)
                //    plafonds[i].Draw(camera);
        }

    }
}
