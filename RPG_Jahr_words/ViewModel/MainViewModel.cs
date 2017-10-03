using GalaSoft.MvvmLight;

namespace RPG_Jahr_words.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        RPGEntities15 _bdd;
        NameGen _gen =new NameGen();
        private int test = 2015;

        public MainViewModel()
        {
        //    Gen = new NameGen();
        //    Bdd = new RPGEntities15();
        }

        public NameGen Gen { get => _gen; set { _gen = value; RaisePropertyChanged(); } }
        public RPGEntities15 Bdd { get => _bdd; set { _bdd = value; RaisePropertyChanged(); } }

        public int Test { get => test; set { test = value; RaisePropertyChanged(); } }
    }
}