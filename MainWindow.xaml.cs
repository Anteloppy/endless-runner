using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Threading;

namespace endless_runer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer game_timer = new DispatcherTimer();

        Rect player_hitbox;
        Rect ground_hitbox;
        Rect obstacle_hitbox;

        bool jumping;

        int force = 20, speed = 5;

        Random rnd = new Random();

        bool gameover;

        double sprite_index = 0;

        ImageBrush player_sprite = new ImageBrush();
        ImageBrush bg_sprite = new ImageBrush();
        ImageBrush obstacle_sprite = new ImageBrush();

        int[] obstacle_position = { 320, 310, 300, 305, 315 };
        int score = 0;




        public MainWindow()
        {
            InitializeComponent();

            canvas.Focus();
            game_timer.Tick += game_engine;
            game_timer.Interval = TimeSpan.FromMilliseconds(20);

            bg_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/background.gif"));

            bg1.Fill = bg_sprite;
            bg2.Fill = bg_sprite;

            start_game();
        }

        private void game_engine(object sender, EventArgs e)
        {
            Canvas.SetLeft(bg1, Canvas.GetLeft(bg1) - 2);
            Canvas.SetLeft(bg2, Canvas.GetLeft(bg2) - 2);

            if (Canvas.GetLeft(bg1) < - 1000)
            {
                Canvas.SetLeft(bg1, Canvas.GetLeft(bg2) + bg2.Width);
            }
            
            if (Canvas.GetLeft(bg2) < - 1000)
            {
                Canvas.SetLeft(bg2, Canvas.GetLeft(bg1) + bg1.Width);
            }

            Canvas.SetTop(player, Canvas.GetTop(player) + speed);
            Canvas.SetLeft(obstcle, Canvas.GetLeft(obstcle) - 12);

            scoretext.Content = "Score: " + score;

            player_hitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width - 15, player.Height);
            obstacle_hitbox = new Rect(Canvas.GetLeft(obstcle), Canvas.GetTop(obstcle), obstcle.Width, obstcle.Height);
            ground_hitbox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width, ground.Height);

            if (player_hitbox.IntersectsWith(ground_hitbox))
            {
                speed = 0;

                Canvas.SetTop(player, Canvas.GetTop(ground) - player.Height);

                jumping = false;

                sprite_index += .5;

                if (sprite_index > 8)
                {
                    sprite_index = 1;
                }

                run_sprite(sprite_index);
            }

            if (jumping == true)
            {
                speed = -9;

                force -= 1;
            }
            else
            {
                speed = 10;
            }

            if (force < 0)
            {
                jumping = false;
            }

            if (Canvas.GetLeft(obstcle) < -50)
            {
                Canvas.SetLeft(obstcle, 950);

                Canvas.SetTop(obstcle, obstacle_position[rnd.Next(0, obstacle_position.Length)]);

                score += 1;
            }

            if (player_hitbox.IntersectsWith(obstacle_hitbox))
            {
                gameover = true;

                game_timer.Stop();
            }

            if (gameover == true)
            {
                scoretext.Content = "Score: " + score + "\nPress Enter to play again";
            }

        }

        private void canvas1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && gameover == true)
            {
                start_game();
            }
        }

        private void canvas1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && jumping == false && Canvas.GetTop(player) > 260)
            {
                jumping = true;
                force = 15;
                speed = -12;

                player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_02.gif"));
            }
        }

        private void start_game()
        {
            Canvas.SetLeft(bg1, 0);
            Canvas.SetLeft(bg2, 1000);

            Canvas.SetLeft(player, 50);
            Canvas.SetTop(player, 150);

            Canvas.SetLeft(obstcle, 950);
            Canvas.SetTop(obstcle, 310);

            run_sprite(1);

            obstacle_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/obstacle.png"));
            obstcle.Fill = obstacle_sprite;

            jumping = false;
            gameover = false;
            score = 0;

            scoretext.Content = "Score: " + score;

            game_timer.Start();
            
        }

        private void run_sprite(double i)
        {
            switch (i)
            {
                case 1: player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_01.gif"));
                    break;
                case 2:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_02.gif"));
                    break;
                case 3:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_03.gif"));
                    break;
                case 4:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_04.gif"));
                    break;
                case 5:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_05.gif"));
                    break;
                case 6:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_06.gif"));
                    break;
                case 7:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_07.gif"));
                    break;
                case 8:
                    player_sprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/sprites/newRunner_08.gif"));
                    break;
            }

            player.Fill = player_sprite;

        }

    }
}
