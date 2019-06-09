using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToe_goes_big
{
    // Класс ячейки, который отображается в виде кнопки на поле
    public class BoardCell : INotifyPropertyChanged
    {
        // Символ - крестик или нолик
        private char m_sign;
        
        public char Sign
        {
            get { return m_sign; }
            // Если по кнопке кликнули
            set
            {
                // То проставляем указанный символ
                m_sign = value;
                // Запрещаем повторный клик
                CanSelect = false;
                // Уведомляем о том, что состояние кнопки изменилось
                OnPropertyChanged();
            }
        }

        public bool CanSelect { get; set; } = true;

        // Немного магии, для того, чтобы предотвратить повторный клик по кнопке
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    // Класс доски, она хранит все ячейки с крестиками и ноликами
    public class Board
    {
       
        // Собственно список с ячейками
        private ObservableCollection<BoardCell> m_cells;
        public Board () {
            m_cells = new ObservableCollection<BoardCell>(Enumerable.Range(0, 20 * 20).Select(i => new BoardCell()));
        }
        // Количество строк и столбцов
        public int Rows { get; set; }
        public int Columns { get; set; }

        public ObservableCollection<BoardCell> Cells
        {
            get { return m_cells; }
        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private bool m_firstPlayer = true;

        // Обработчик клика по кнопке
        private void CellClick(object sender, RoutedEventArgs e)
        {
            // Получили ссылку на ячейку по которой кликнули
            var cell = (sender as Button).DataContext as BoardCell;
            cell.Sign = m_firstPlayer ? 'X' : 'O';
            m_firstPlayer = !m_firstPlayer;
            // Проверить на выигрыш 
        }


    }
}
