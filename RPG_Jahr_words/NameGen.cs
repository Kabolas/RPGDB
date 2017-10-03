using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alphabet = System.Collections.Generic.Dictionary<int, string>;

namespace RPG_Jahr_words
{
    public class StuffItem
    {
        private Items _stuff;
        private int _nombre;

        public int Nombre { get => _nombre; set => _nombre = value; }
        public Items Stuff { get => _stuff; set => _stuff = value; }
    }
    public class RecipeItem
    {
        private int n_recette;
        private int _id;
        private double _quantite;
        private string _origine, _nom;

        public string Origine { get => _origine; set => _origine = value; }
        public string Nom { get => _nom; set => _nom = value; }
        public double Quantite { get => _quantite; set => _quantite = value; }
        public int Id { get => _id; set => _id = value; }
        public int N_recette { get => n_recette; set => n_recette = value; }


        override public string ToString() { return Id + "|" + Nom + ":" + Quantite; }
    }

    public class LootItem
    {
        private int _chance;
        private Items _loot;
        private double _quantite;

        public double Quantite { get => _quantite; set => _quantite = value; }
        public Items Loot { get => _loot; set => _loot = value; }
        public int Chance { get => _chance; set => _chance = value; }
    }

    public class RecipeResult
    {
        private int _nombre;
        private int _idRecipe;

        public int Nombre { get => _nombre; set => _nombre = value > 0 ? value : 1; }
        public int IdRecipe { get => _idRecipe; set => _idRecipe = value; }

        public override string ToString() { return "" + Nombre; }
    }

    public class NameGen
    {
        //private String alphabet_cons = "bcdfghjklmnpqrstvwxz";
        //private String alphabet_voy = "aeiouy";
        private String signes = "- ";
        private String ret;
        private Alphabet voyelles;
        private Alphabet consonnes;
        private Alphabet RPG_cons;
        private Alphabet RPG_voy;

        private Dictionary<string, int> Alpha;

        public bool triphtongue_flag = false;
        public bool symbol = false;
        public bool sign_flag = false;
        public static int max_voy = 2;
        private static int max_cons = 2;

        public static bool adjectif = false;
        public static bool monde = false;
        public static bool mots = true;
        public static bool objets = false;
        public static bool verbes = true;
        public static bool RPG = true;
        //public static bool son_cons = false;
        //public static bool sou_cons = false;
        //public static bool oc_cons = false;
        //public static bool fr_cons = false;
        //public static bool alv_cons = false;
        //public static bool before_word = true;
        //public static string word_in_word = "";

        public NameGen()
        {
            voyelles = new Alphabet(14);

            consonnes = new Alphabet(24);

            C_Alpha();

            RPG_voy = new Alphabet(14);
            RPG_cons = new Alphabet(23);
            RPGen();
        }

        private void RPGen()
        {
            RPG_voy.Add(00, "a");
            RPG_voy.Add(01, "e");
            RPG_voy.Add(02, "i");
            RPG_voy.Add(03, "o");
            RPG_voy.Add(04, "u");
            RPG_voy.Add(05, "Q");//ou
            RPG_voy.Add(06, "ê");//en
            RPG_voy.Add(07, "é");
            RPG_voy.Add(08, "è");
            RPG_voy.Add(09, "ë");//eu
            RPG_voy.Add(10, "ö");//on
            RPG_voy.Add(11, "î");//in
            RPG_voy.Add(12, "ò");//ò
            RPG_voy.Add(13, "ù");//ouo

            RPG_cons.Add(00, "b");
            RPG_cons.Add(01, "c");
            RPG_cons.Add(02, "d");
            RPG_cons.Add(03, "f");
            RPG_cons.Add(04, "g");
            RPG_cons.Add(05, "h");
            RPG_cons.Add(06, "j");
            RPG_cons.Add(07, "l");
            RPG_cons.Add(08, "m");
            RPG_cons.Add(09, "n");
            RPG_cons.Add(10, "p");
            RPG_cons.Add(11, "r");
            RPG_cons.Add(12, "q");//r roulé
            RPG_cons.Add(13, "s");
            RPG_cons.Add(14, "t");
            RPG_cons.Add(15, "v");
            RPG_cons.Add(16, "w");
            RPG_cons.Add(17, "y");
            RPG_cons.Add(18, "z");
            RPG_cons.Add(19, "x");//ch
            RPG_cons.Add(20, "K");//hr
            RPG_cons.Add(21, "ç");//th
            RPG_cons.Add(22, "k");//r anglais
        }

        private void C_Alpha()
        {
            voyelles.Add(0, "a");
            voyelles.Add(1, "e");
            voyelles.Add(2, "i");
            voyelles.Add(3, "o");
            voyelles.Add(04, "u");
            voyelles.Add(05, "[ou]");
            voyelles.Add(06, "[an]");
            voyelles.Add(07, "é");
            voyelles.Add(08, "è");
            voyelles.Add(09, "[eu]");
            voyelles.Add(10, "[on]");
            voyelles.Add(11, "[in]");
            voyelles.Add(12, "ò");
            voyelles.Add(13, "[ouo]");

            consonnes.Add(0, "b");
            consonnes.Add(1, "k");
            consonnes.Add(2, "d");
            consonnes.Add(3, "f");
            consonnes.Add(4, "g");
            consonnes.Add(5, "h");
            consonnes.Add(6, "j");
            consonnes.Add(7, "l");
            consonnes.Add(8, "m");
            consonnes.Add(9, "n");
            consonnes.Add(10, "p");
            consonnes.Add(11, "r");
            consonnes.Add(12, "[rr]");
            consonnes.Add(13, "s");
            consonnes.Add(14, "t");
            consonnes.Add(15, "v");
            consonnes.Add(16, "w");
            consonnes.Add(17, "y");
            consonnes.Add(18, "z");
            consonnes.Add(19, "[ch]");
            consonnes.Add(20, "[hr]");
            consonnes.Add(21, "[th]");
            consonnes.Add(22, "[rw]");
            consonnes.Add(23, "[gn]");
        }

        public string default_Generation_Sons(int taille, string wantedword = "", bool before = true, bool tripht = false, bool symb = false)
        {
            Random k = new Random();
            ret = before ? wantedword : "";
            int comp_voy = 0, comp_cons = 0;
            bool sign_flag = false;
            for (int i = 0; i < taille; i++)
            {
                int choix;
                if (comp_cons == max_cons) choix = 0;
                else if (comp_voy == max_voy) choix = 1;
                else choix = k.Next(2);
                if (choix == 0)
                {
                    ret += voyelles[k.Next(voyelles.Count)];
                    comp_voy++;
                    comp_cons = 0;
                    if (symbol && !sign_flag && k.Next(2) == 0 && i >= 1)
                    {
                        ret += signes[k.Next(3)];
                        sign_flag = true;
                    }
                }
                else if (choix == 1)
                {
                    comp_voy = 0;
                    ret += consonnes[k.Next(consonnes.Count - 1)];
                    comp_cons++;
                }
            }
            return before ? ret : ret + wantedword;
        }
        public string Generation_gn_Sons(int taille, string wantedword = "", bool before = true, bool tripht = false, bool symb = false)
        {
            Random k = new Random();
            ret = before ? wantedword : "";
            int comp_voy = 0, comp_cons = 0;
            bool sign_flag = false;
            for (int i = 0; i < taille; i++)
            {
                int choix;
                if (comp_cons == max_cons) choix = 0;
                else if (comp_voy == max_voy) choix = 1;
                else choix = k.Next(2);
                if (choix == 0)
                {
                    ret += voyelles[k.Next(voyelles.Count)];
                    comp_voy++;
                    comp_cons = 0;
                    if (symbol && !sign_flag && k.Next(2) == 0 && i >= 1)
                    {
                        ret += signes[k.Next(3)];
                        sign_flag = true;
                    }
                }
                else if (choix == 1)
                {
                    comp_voy = 0;
                    ret += consonnes[k.Next(consonnes.Count)];
                    comp_cons++;
                }

            }
            return before ? ret : ret + wantedword;
        }

        public string RPG_Convert(string word)
        {
            string ret = "", rec = "";
            bool confnd = false;
            bool voyfnd = false;
            int flag = 0;
            if (word != null)
                for (int i = 0; i < word.Length; i++)
                {
                    if (word[i] == '[')
                    {
                        flag = 1;
                        rec = "[";
                    }
                    else if (word[i] == ']')
                    {
                        rec += word[i];
                        flag = 0;
                    }
                    else if (flag == 1)
                    {
                        rec += word[i];
                        continue;
                    }
                    else
                        rec = "" + word[i];
                    confnd = false;
                    voyfnd = false;

                    if (voyfnd = voyelles.ContainsValue(rec))
                        ret += RPG_voy[voyelles.First(v=>v.Value==rec).Key];
                    if (confnd = consonnes.ContainsValue(rec))
                        ret += RPG_cons[consonnes.First(v => v.Value == rec).Key];
                    if (signes.Contains(word[i]))
                        ret += word[i];
                }
            return ret;
        }
    }
}
