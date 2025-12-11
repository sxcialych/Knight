using System;
using System.Windows;
using System.Windows.Media;

namespace Knight
{
    public partial class MainWindow : Window
    {
        // Характеристики тёмного принца
        private string knightName = "Артур";
        private string castleName = "Камелот";
        private int deedsCount = 0;
        private int armorIntegrity = 100;
        private int health = 100;

        // Начальные значения
        private const string DEFAULT_NAME = "Артур";
        private const string DEFAULT_CASTLE = "Камелот";
        private const int DEFAULT_DEEDS = 0;
        private const int DEFAULT_ARMOR = 100;
        private const int DEFAULT_HEALTH = 100;

        public MainWindow()
        {
            InitializeComponent();
            UpdateKnightInfo();
        }

        private void UpdateKnightInfo()
        {
            tbKnightInfo.Text = $"ИНФОРМАЦИЯ О РЫЦАРЕ\n\n" +
                               $"Имя: {knightName}\n\n" +
                               $"Замок: {castleName}\n\n" +
                               $"Количество подвигов: {deedsCount}\n\n" +
                               $"Целостность доспехов: {armorIntegrity}%\n\n" +
                               $"Здоровье: {health}%\n\n" +
                               $"==========================";

            // Меняем цвет рамки в зависимости от состояния здоровья
            UpdateKnightVisualState();
        }

        private void UpdateKnightVisualState()
        {
            // Меняем оттенок изображения в зависимости от здоровья
            if (health <= 30)
            {
                // Красный оттенок при низком здоровье
                KnightImage.Opacity = 0.7;
            }
            else if (health <= 50)
            {
                // Желтый оттенок при среднем здоровье
                KnightImage.Opacity = 0.8;
            }
            else
            {
                // Нормальное состояние
                KnightImage.Opacity = 1.0;
            }

            // Меняем размер изображения в зависимости от целостности доспехов
            if (armorIntegrity <= 30)
            {
                KnightImage.Height = 180; // Уменьшаем при поврежденных доспехах
            }
            else
            {
                KnightImage.Height = 200; // Нормальный размер
            }
        }

        private void BtnCurrentState_Click(object sender, RoutedEventArgs e)
        {
            UpdateKnightInfo();
        }

        private void BtnDeed_Click(object sender, RoutedEventArgs e)
        {
            // Совершаем 1 подвиг
            if (CanPerformAction())
            {
                deedsCount++;
                health = Math.Max(0, health - 10);
                armorIntegrity = Math.Max(0, armorIntegrity - 5);
                UpdateKnightInfo();
            }
            else
            {
                MessageBox.Show("Рыцарь не может совершать подвиги! Проверьте здоровье и доспехи.",
                              "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnRepair_Click(object sender, RoutedEventArgs e)
        {
            // Восстанавливаем доспехи на 25%
            armorIntegrity = Math.Min(100, armorIntegrity + 25);
            UpdateKnightInfo();
        }

        private void BtnRest_Click(object sender, RoutedEventArgs e)
        {
            health = Math.Min(100, health + 20);
            UpdateKnightInfo();
        }

        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            knightName = DEFAULT_NAME;
            castleName = DEFAULT_CASTLE;
            deedsCount = DEFAULT_DEEDS;
            armorIntegrity = DEFAULT_ARMOR;
            health = DEFAULT_HEALTH;
            UpdateKnightInfo();
        }

        private void BtnDuel_Click(object sender, RoutedEventArgs e)
        {
            if (CanPerformAction())
            {
                deedsCount++;
                health = Math.Max(0, health - 15);
                armorIntegrity = Math.Max(0, armorIntegrity - 10);
                UpdateKnightInfo();
            }
            else
            {
                MessageBox.Show("Рыцарь не может участвовать в дуэли! Проверьте здоровье и доспехи.",
                              "Дуэль", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Проверка возможности выполнения действия
        /// </summary>
        private bool CanPerformAction()
        {
            return health > 0 && armorIntegrity > 0;
        }
    }
}