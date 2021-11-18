//Authors: Gowridevi Akasapu, Anju Thomas, Apurva Jain, Aditya Gupta, Emmanuel Kevin, Ravali Chilucoti, Amritpal Singh
//Copy Rights:      Conestoga College
//Group Number:     4

using System.Windows;

namespace BeeBreeding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constants
        public const int MAX_CELL_VALUE = 10000;
        #endregion

        private readonly VM vm = VM.Instance;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        // This method calculates the distance between the maggot cells A and B.
        private void CalcDistance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maggotCellAVal = int.Parse(MaggotCellA.Text);
                int maggotCellBVal = int.Parse(MaggotCellB.Text);
                if (maggotCellAVal > 0 && maggotCellBVal > 0 && maggotCellAVal <= MAX_CELL_VALUE && maggotCellBVal <= MAX_CELL_VALUE)
                    vm.CalcDistance();
            }
            catch
            {
                vm.Error();
                return;
            }
        }

        // Resets the fields to initial value.
        private void Reset_Click(object sender, RoutedEventArgs e) => vm.Reset();
    }
}
