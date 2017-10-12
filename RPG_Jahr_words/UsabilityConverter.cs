using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Reflection;
using System.Collections;
using System.Data.Entity;

namespace RPG_Jahr_words
{
    public class CheckToVisible : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectedToVisible : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class UncheckedToTrue : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? false : true;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MagoToBool : IValueConverter
    {
        private RPGEntities15 bdd;//= new RPGEntities15();
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                if (value is string && bdd.Monde_w.ToList().Find(m => m.nom == (string)value) != null)
                    return (string)value != "Technocosme";
                else return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MagoToVisible : IValueConverter
    {
        private RPGEntities15 bdd;//= new RPGEntities15();
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                if (value is string && bdd.Monde_w.ToList().Find(m => m.nom == (string)value) != null)
                    return (string)value != "Technocosme" ? Visibility.Visible : Visibility.Hidden;
                else return Visibility.Hidden;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TechnoToBool : IValueConverter
    {
        private RPGEntities15 bdd;//= new RPGEntities15();
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                if (value is string && bdd.Monde_w.ToList().Find(m => m.nom == (string)value) != null)
                    return (string)value != "Magocosme";
                else return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class TechnoToVisible : IValueConverter
    {
        private RPGEntities15 bdd;//= new RPGEntities15();
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                if (value is string && bdd.Monde_w.ToList().Find(m => m.nom == (string)value) != null)
                    return (string)value != "Magocosme" ? Visibility.Visible : Visibility.Hidden;
                else return Visibility.Hidden;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AllToBool : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return (!((Mag_element)value).element.Contains("Tous"));
            return true;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WeapLinkConvert : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)values[0] ? values[1] : values[2];
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class SchoolToList : IValueConverter
    {
        private RPGEntities15 bdd;
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                return ((Magie_type)value).ecole == "Tous" ? bdd.Sorts.ToList() : bdd.Sorts.ToList().FindAll(s => s.Magie_type == ((Magie_type)value));
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringToJewel : IValueConverter
    {
        private RPGEntities15 bdd;

        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            using (bdd = new RPGEntities15())
                switch ((string)value)
                {
                    case "Armes": return bdd.Items.Where(a => a.Weaponry != null).ToList();
                    case "Armures": return bdd.Items.Where(a => a.Armory != null).ToList();
                    case "Véhicule":
                    case "Vehicule": return bdd.Items.Where(v => v.Vehicule != null).ToList();
                    case "Munition":
                    case "Munitions": return bdd.Items.Where(m => m.Munition != null).ToList();
                    case "Alliages": return bdd.Items.Where(a => a.Alliage != null).ToList();
                    case "Bijoux": return bdd.Items.Where(b => b.Bijoux != null).ToList();
                    case "Consommable": return bdd.Items.Where(c => c.Consommables != null).ToList();
                    case "Loot": return bdd.Items.Where(l => l.Loot != null).ToList();
                    case "Métaux": return bdd.Items.Where(m => m.Mineraux != null && m.Mineraux.Minerai_type.type == "Métal").ToList();
                    case "Parchemins": return bdd.Items.Where(p => p.Parchemins != null).ToList();
                    case "Pierres":
                    case "Pierre": return bdd.Items.Where(m => m.Mineraux != null && m.Mineraux.Minerai_type.type == "Pierre").ToList();
                    case "Végétaux": return bdd.Items.Where(v => v.Mineraux != null && v.Mineraux.Minerai_type.type == "Végétal").ToList();
                    case "Conteneur": return bdd.Items.Where(c => c.Conteneurs != null).ToList();
                    case "Commun":
                    case "Communs":
                        return bdd.Items.Where(i => i.Armory == null && i.Mineraux == null && i.Bijoux == null &&
        i.Weaponry == null && i.Vehicule == null && i.Loot == null && i.Mineraux == null && i.Munition == null && i.Conteneurs == null && i.Consommables
        == null && i.Parchemins == null).ToList();
                    default:
                        return null;
                }
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CatToPiece : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Exosquelette";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Not : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class Or : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = values[0] is bool ? (bool)values[0] : false;
            for (int i = 1; i < values.Length; i++)
                if (values[i] is bool)
                    ret = ret || (bool)values[i];
            return ret;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class And : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            bool ret = values[0] is bool ? (bool)values[0] : false;
            for (int i = 1; i < values.Length; i++)
                if (values[i] is bool)
                    ret = ret && (bool)values[i];
            return ret;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class SelectedToItemType : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return ((ViewModel.ItemType)value).ToString();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            try { return Enum.Parse(typeof(ViewModel.ItemType), value as string); } catch { return ViewModel.ItemType.None; }
        }
    }

    public class SameTypeToFalse : IValueConverter
    {
        private RPGEntities15 bdd;
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && ((Items)value).Id != 0)
            {
                using (bdd = new RPGEntities15())
                {
                    Items temp = bdd.Items.ToList().Find(i => i.Id == (value as Items).Id);
                    return temp.GetType().GetProperty(parameter as string).GetValue(temp) == null;
                }
            }
            return true;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullToTrue : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class NullToFalse : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class IntEquals : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value as string, out int val))
                return val == int.Parse((string)parameter);
            return false;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringNotEquals : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value as string != (string)parameter;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class StringEquals : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value as string == parameter as string;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class FoundToNull : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is int)
                if ((int)parameter == 0) return null;
                else return value;
            else throw new NotImplementedException();
        }
    }
    public class IntToBool : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value != 0;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ComboOrSpell : DependencyObject, IMultiValueConverter
    {

        public static bool isCombo;
        private bool _oneToSource = false;
        private bool ignore = false;
        public bool OneToSource { get => _oneToSource; set => _oneToSource = value; }

        private object defvalue1, defvalue2;

        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            defvalue1 = values[0];
            defvalue2 = values[1];
            if (ignore)
            {
                ignore = false;
                if (isCombo)
                { return "" + values[0]; }
                else { return "" + values[1]; }
            }
            else if (!isCombo)
            { return "" + values[0]; }
            else { return "" + values[1]; }
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            ignore = true;
            if (isCombo)
            {
                if (OneToSource)
                    defvalue1 = value;
                if (targetTypes[0] == typeof(string))
                    return new object[] { value, defvalue2 };
                else if (targetTypes[0] == typeof(double) || targetTypes[0] == typeof(double?))
                    return new object[] { (string)value == "" ? 0 : double.Parse((value as string).Replace('.', ',')), defvalue2 };
                else if (targetTypes[0] == typeof(int) || targetTypes[0] == typeof(int?))
                    return new object[] { (string)value == "" ? 0 : int.Parse(value as string), defvalue2 };
                else
                    return new object[] { value, defvalue2 };
            }
            else
            {
                if (OneToSource)
                    defvalue2 = value;
                if (targetTypes[0] == typeof(string))
                    return new object[] { defvalue1, value };
                else if (targetTypes[0] == typeof(double) || targetTypes[0] == typeof(double?))
                    return new object[] { defvalue1, (string)value == "" ? 0 : double.Parse((value as string).Replace('.', ',')) };
                else if (targetTypes[0] == typeof(int) || targetTypes[0] == typeof(int?))
                    return new object[] { defvalue1, (string)value == "" ? 0 : int.Parse(value as string) };
                else
                    return new object[] { defvalue1, value };
            }
        }
    }

    public class IsElem : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Magie_type).ecole == "Elémentaire";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ComboToStats : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            try { return (bool)values[2] ? values[0] : values[1]; }
            catch { return null; }
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class DisplayFromBool : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < values.Length; i++)
                if (values[i] is bool && (bool)values[i])
                    return (parameter as string).Split(',')[i];
            return null;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class OrToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            return new CheckToVisible().Convert(new Or().Convert(values, targetType, parameter, culture), targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class OneFromMany : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (values.Length == 2)
                    if ((bool)values[1]) return values[0];
                    else return null;
                else
                {

                    for (int i = values.Length / 2; i < values.Length; i++)
                        if ((bool)values[i])
                            return values[i - (values.Length / 2)];
                    return null;
                }
            }
            catch { return null; }
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class AndToVisibility : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            return new CheckToVisible().Convert(new And().Convert(values, targetType, parameter, culture), targetType, parameter, culture);
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ToWorldEnum : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return ((ViewModel.WorldType)value).ToString();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            try { return Enum.Parse(typeof(ViewModel.WorldType), value as string); } catch { return ViewModel.WorldType.None; }
        }
    }
    public class WorldElemWrite : IMultiValueConverter
    {
        public static ViewModel.WorldWrap which;
        private object[] vals = new object[7];
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            for (int i = 0; i < values.Length; i++)
                vals[i] = values[i];
            if (which != null)
                switch (which.Type)
                {
                    case ViewModel.WorldType.None:
                        return null;
                    case ViewModel.WorldType.Continent:
                        return targetType == typeof(string) ? "" + vals[0] : vals[0];
                    case ViewModel.WorldType.Ville:
                        return targetType == typeof(string) ? "" + vals[1] : vals[1];
                    case ViewModel.WorldType.Region:
                        return targetType == typeof(string) ? "" + vals[2] : vals[2];
                    case ViewModel.WorldType.Mineraux:
                        object k1 = targetType == typeof(string) ? "" + vals[3] : vals[3];
                        return k1;
                    case ViewModel.WorldType.Alliage:
                        object k2 = targetType == typeof(string) ? "" + vals[4] : vals[4];
                        return k2;
                    case ViewModel.WorldType.Phenomene:
                        return targetType == typeof(string) ? "" + vals[5] : vals[5];
                    default:
                        return null;
                }
            return null;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            switch (which.Type)
            {
                case ViewModel.WorldType.None:
                    break;
                case ViewModel.WorldType.Continent:
                    vals[0] = targetTypes[0] == typeof(string) ? "" + value : (targetTypes[0] == typeof(int) || targetTypes[0] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[0] == typeof(double) || targetTypes[0] == typeof(double?)) ? double.Parse(value as string) : value;
                    break;
                case ViewModel.WorldType.Ville:
                    vals[1] = targetTypes[1] == typeof(string) ? "" + value : (targetTypes[1] == typeof(int) || targetTypes[1] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[1] == typeof(double) || targetTypes[1] == typeof(double?)) ? double.Parse(value as string) : value;
                    break;
                case ViewModel.WorldType.Region:
                    vals[2] = targetTypes[2] == typeof(string) ? "" + value : (targetTypes[2] == typeof(int) || targetTypes[2] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[2] == typeof(double) || targetTypes[2] == typeof(double?)) ? double.Parse(value as string) : value;
                    break;
                case ViewModel.WorldType.Mineraux:
                    vals[3] = targetTypes[3] == typeof(string) ? "" + value : (targetTypes[3] == typeof(int) || targetTypes[3] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[3] == typeof(double) || targetTypes[3] == typeof(double?)) ? double.Parse(value as string) : (targetTypes[3] == typeof(decimal) || targetTypes[3] == typeof(decimal?)) ? decimal.Parse(value as string) : value;
                    break;
                case ViewModel.WorldType.Alliage:
                    vals[4] = targetTypes[4] == typeof(string) ? "" + value : (targetTypes[4] == typeof(int) || targetTypes[4] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[4] == typeof(double) || targetTypes[4] == typeof(double?)) ? double.Parse(value as string) : (targetTypes[4] == typeof(decimal) || targetTypes[4] == typeof(decimal?)) ? decimal.Parse(value as string) : value;
                    break;
                case ViewModel.WorldType.Phenomene:
                    vals[5] = targetTypes[5] == typeof(string) ? "" + value : (targetTypes[5] == typeof(int) || targetTypes[5] == typeof(int?)) ? int.Parse(value as string) : (targetTypes[5] == typeof(double) || targetTypes[5] == typeof(double?)) ? double.Parse(value as string) : value;
                    break;
                default:
                    break;
            }
            return vals;
        }
    }
    public class NullReturn : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    public class TrueToSingle : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? SelectionMode.Single : SelectionMode.Multiple;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (SelectionMode)value == SelectionMode.Single;
        }
    }

    public class ToList : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class CommonToAlph : IValueConverter
    {
        public static NameGen DicoConvert;
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (DicoConvert != null && value != null && value != DependencyProperty.UnsetValue && value as string != "")
                switch (parameter as string)
                {
                    case "Jahr":
                        return DicoConvert.RPG_Convert(value as string);
                    default:
                        return null;
                }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class WorldOrWord : DependencyObject, IMultiValueConverter
    {

        public static bool isWorld;
        private bool _oneToSource = false;
        private bool ignore = false;
        public bool OneToSource { get => _oneToSource; set => _oneToSource = value; }

        private object defvalue1, defvalue2;

        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            defvalue1 = values[0];
            defvalue2 = values[1];
            if (values[0] == DependencyProperty.UnsetValue && values[1] == DependencyProperty.UnsetValue) return null;
            if (ignore)
            {
                ignore = false;
                if (isWorld)
                { return "" + values[0]; }
                else { return "" + values[1]; }
            }
            else if (targetType == typeof(string))
                if (!isWorld)
                { return "" + values[0]; }
                else { return "" + values[1]; }
            else if (!isWorld)
            { return values[0]; }
            else { return values[1]; }
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            ignore = true;
            if (isWorld)
            {
                if (OneToSource)
                    defvalue1 = value;
                if (targetTypes[0] == typeof(string))
                    return new object[] { value, defvalue2 };
                else if (targetTypes[0] == typeof(double) || targetTypes[0] == typeof(double?))
                    return new object[] { (string)value == "" ? 0 : double.Parse((value as string).Replace('.', ',')), defvalue2 };
                else if (targetTypes[0] == typeof(int) || targetTypes[0] == typeof(int?))
                    return new object[] { (string)value == "" ? 0 : int.Parse(value as string), defvalue2 };
                else
                    return new object[] { value, defvalue2 };
            }
            else
            {
                if (OneToSource)
                    defvalue2 = value;
                if (targetTypes[0] == typeof(string))
                    return new object[] { defvalue1, value };
                else if (targetTypes[0] == typeof(double) || targetTypes[0] == typeof(double?))
                    return new object[] { defvalue1, (string)value == "" ? 0 : double.Parse((value as string).Replace('.', ',')) };
                else if (targetTypes[0] == typeof(int) || targetTypes[0] == typeof(int?))
                    return new object[] { defvalue1, (string)value == "" ? 0 : int.Parse(value as string) };
                else
                    return new object[] { defvalue1, value };
            }
        }
    }

    public class WriteInBoth : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            return values[0];
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            object[] retab = new object[targetTypes.Length];
            if (value as string != "")
                for (int i = 0; i < retab.Length; i++)
                {
                    if (targetTypes[i] == null)
                        retab[i] = null;
                    else if (targetTypes[i] == typeof(string))
                        retab[i] = value as string;
                    else if (targetTypes[i] == typeof(int) || targetTypes[i] == typeof(int?))
                        retab[i] = int.Parse(value as string);
                    else if (targetTypes[i] == typeof(double) || targetTypes[i] == typeof(double?))
                        retab[i] = double.Parse(value as string);
                    else
                        retab[i] = value;
                }
            return retab;
        }
    }

    public class MultiplyBy : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            return (targetType == typeof(int) ? (int)value / (int)parameter : (double)value / (double)parameter);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType == null || value == null)
                return null;
            return (targetType == typeof(int) ? int.Parse(value as string) * int.Parse(parameter as string) : double.Parse(value as string) * double.Parse(parameter as string));
        }
    }
}
