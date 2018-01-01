using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alphabet = System.Collections.Generic.Dictionary<string, int>;

namespace RPG_Jahr_words
{
    public class StuffItem
    {
        private Items _stuff;
        private int _nombre;
        private bool enchanted;
        private Enchantements ench1, ench2, ench3;

        public int Nombre { get => _nombre; set => _nombre = value; }
        public Items Stuff { get => _stuff; set => _stuff = value; }
        public bool Enchanted { get => enchanted; set => enchanted = value; }
        public Enchantements Ench1 { get => ench1; set => ench1 = value; }
        public Enchantements Ench2 { get => ench2; set => ench2 = value; }
        public Enchantements Ench3 { get => ench3; set => ench3 = value; }

        public override string ToString()
        {
            string ret = Stuff.nom + "|" + Stuff.Id + "~" + Nombre;
            if (Enchanted)
            {
                ret += "[";
                if (Ench1 != null)
                    ret += Ench1.Id + "|" + Ench1.nom;
                {
                    if (Ench2 != null)
                    {
                        ret += ";" + Ench2.Id + "|" + Ench2.nom;
                        if (Ench3 != null)
                            ret += ";" + Ench3.Id + "|" + Ench3;
                    }
                }
                ret += "]";
            }
            return ret;
        }
    }
    public class RecipeItem
    {
        private int n_recette;
        private Items _component;
        private double _quantite;

        public double Quantite { get => _quantite; set => _quantite = value; }
        public int N_recette { get => n_recette; set => n_recette = value; }
        public Items Component { get => _component; set => _component = value; }

        override public string ToString() { return Component.Id + "|" + Component.nom + ":" + Quantite; }
    }

    public class LootItem
    {
        private int _chance;
        private Items _loot;
        private double _quantite;
        private Condition _condition;
        private bool enchanted;
        private Enchantements ench1, ench2, ench3;

        public double Quantite { get => _quantite; set => _quantite = value; }
        public Items Loot { get => _loot; set => _loot = value; }
        public int Chance { get => _chance; set => _chance = value; }
        public Condition Condition { get => _condition; set => _condition = value; }
        public bool Enchanted { get => enchanted; set => enchanted = value; }
        public Enchantements Ench1 { get => ench1; set => ench1 = value; }
        public Enchantements Ench2 { get => ench2; set => ench2 = value; }
        public Enchantements Ench3 { get => ench3; set => ench3 = value; }
        public override string ToString()
        {
            string ret = Loot.nom + "|" + Loot.Id + "~" + Chance + "%~" + Condition.facon + "=>" + Quantite; if (Enchanted)
            {
                ret += "[";
                if (Ench1 != null)
                    ret += Ench1.Id + "|" + Ench1.nom;
                {
                    if (Ench2 != null)
                    {
                        ret += ";" + Ench2.Id + "|" + Ench2.nom;
                        if (Ench3 != null)
                            ret += ";" + Ench3.Id + "|" + Ench3;
                    }
                }
                ret += "]";
            }
            return ret;
        }
    }

    public class RecipeResult
    {
        private int _nombre;
        private int _idRecipe;
        private Procede _process;

        public int Nombre { get => _nombre; set => _nombre = value > 0 ? value : 1; }
        public int IdRecipe { get => _idRecipe; set => _idRecipe = value; }
        public Procede Process { get => _process; set => _process = value; }

        public override string ToString() { return "" + Nombre + "(" + Process.process + ")"; }
    }

    public class NameGen
    {
        private String signes = "-' ";
        private String ret;
        private Alphabet voyelles;
        private Alphabet consonnes;
        private Alphabet RPG_cons;
        private Alphabet RPG_voy;

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
            RPG_voy.Add("a", 0);
            RPG_voy.Add("e", 1);
            RPG_voy.Add("i", 2);
            RPG_voy.Add("o", 3);
            RPG_voy.Add("u", 4);
            RPG_voy.Add("Q", 5);//ou
            RPG_voy.Add("ê", 6);//en
            RPG_voy.Add("é", 7);
            RPG_voy.Add("è", 8);
            RPG_voy.Add("ë", 9);//eu
            RPG_voy.Add("ö", 10);//on
            RPG_voy.Add("î", 11);//in
            RPG_voy.Add("ò", 12);//ò
            RPG_voy.Add("ù", 13);//ouo

            RPG_cons.Add("b", 0);
            RPG_cons.Add("c", 1);
            RPG_cons.Add("d", 2);
            RPG_cons.Add("f", 3);
            RPG_cons.Add("g", 4);
            RPG_cons.Add("h", 5);
            RPG_cons.Add("j", 6);
            RPG_cons.Add("l", 7);
            RPG_cons.Add("m", 8);
            RPG_cons.Add("n", 9);
            RPG_cons.Add("p", 10);
            RPG_cons.Add("r", 11);
            RPG_cons.Add("q", 12);//r roulé
            RPG_cons.Add("s", 13);
            RPG_cons.Add("t", 14);
            RPG_cons.Add("v", 15);
            RPG_cons.Add("w", 16);
            RPG_cons.Add("y", 17);
            RPG_cons.Add("z", 18);
            RPG_cons.Add("x", 19);//ch
            RPG_cons.Add("K", 20);//hr
            RPG_cons.Add("ç", 21);//th
            RPG_cons.Add("k", 22);//r anglais

        }

        private void C_Alpha()
        {
            voyelles.Add("a", 0);
            voyelles.Add("e", 1);
            voyelles.Add("i", 2);
            voyelles.Add("o", 3);
            voyelles.Add("u", 4);
            voyelles.Add("[ou]", 5);
            voyelles.Add("[an]", 6);
            voyelles.Add("é", 7);
            voyelles.Add("è", 8);
            voyelles.Add("[eu]", 9);
            voyelles.Add("[on]", 10);
            voyelles.Add("[in]", 11);
            voyelles.Add("ò", 12);
            voyelles.Add("[ouo]", 13);

            consonnes.Add("b", 0);
            consonnes.Add("k", 1);
            consonnes.Add("d", 2);
            consonnes.Add("f", 3);
            consonnes.Add("g", 4);
            consonnes.Add("h", 5);
            consonnes.Add("j", 6);
            consonnes.Add("l", 7);
            consonnes.Add("m", 8);
            consonnes.Add("n", 9);
            consonnes.Add("p", 10);
            consonnes.Add("r", 11);
            consonnes.Add("[rr]", 12);
            consonnes.Add("s", 13);
            consonnes.Add("t", 14);
            consonnes.Add("v", 15);
            consonnes.Add("w", 16);
            consonnes.Add("y", 17);
            consonnes.Add("z", 18);
            consonnes.Add("[ch]", 19);
            consonnes.Add("[hr]", 20);
            consonnes.Add("[th]", 21);
            consonnes.Add("[rw]", 22);
            consonnes.Add("[gn]", 23);
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
                    int ch = k.Next(voyelles.Count);
                    ret += voyelles.First(v => v.Value == ch).Key;
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
                    int ch = k.Next(consonnes.Count - 1);
                    ret += consonnes.First(c => c.Value == ch).Key;
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
                    int ch = k.Next(voyelles.Count);
                    ret += voyelles.First(v => v.Value == ch).Key;
                    //ret += voyelles.First(v => v.Value == ch);
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
                    int ch = k.Next(consonnes.Count);
                    ret += consonnes.First(c => c.Value == ch).Key;
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

                    if (voyfnd = voyelles.ContainsKey(rec))
                        ret += RPG_voy.First(v => v.Value == voyelles[rec]).Key;
                    if (confnd = consonnes.ContainsKey(rec))
                        ret += RPG_cons.First(v => v.Value == consonnes[rec]).Key;
                    if (signes.Contains(word[i]))
                        ret += word[i];
                }
            return ret;
        }
    }
}
