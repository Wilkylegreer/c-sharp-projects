using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Windows.Forms;
using System.Media;

namespace FakeVirsu
{
    class Program
    {

        public static Random _random = new Random();

        public static int startupDelaySeconds = 1;
        public static int totatlDurationSeconds = 100;

        static void Main(string[] args)
        {
            Console.WriteLine("Fake Virus by: Kyle Greer");

            if(args.Length >= 2)
            {
                startupDelaySeconds = Convert.ToInt32(args[0]);
                totatlDurationSeconds = Convert.ToInt32(args[1]);
            }

            Thread mouseThread = new Thread(new ThreadStart(MouseThread));
            Thread keyboardThread = new Thread(new ThreadStart(KeyboardThread));
            Thread soundThread = new Thread(new ThreadStart(SoundThread));
            Thread popupThread = new Thread(new ThreadStart(PopupThread));

            DateTime future = DateTime.Now.AddSeconds(startupDelaySeconds);
            Console.WriteLine("Waiting " + startupDelaySeconds + " seconds before starting threads");
            while(future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            mouseThread.Start();
            keyboardThread.Start();
            soundThread.Start();
            popupThread.Start();

            future = DateTime.Now.AddSeconds(totatlDurationSeconds);
            while(future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Terminating all threads.");

            mouseThread.Abort();
            keyboardThread.Abort();
            soundThread.Abort();
            popupThread.Abort();
        }

        public static void MouseThread()
        {

            int moveX = 0;
            int moveY = 0;

            while (true)
            {

                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);

                Thread.Sleep(_random.Next(500));
            }
        }

        public static void KeyboardThread()
        {
            while (true)
            {
                char key = (char)(_random.Next(25) + 65);

                if(_random.Next(2) == 0)
                {
                    key = Char.ToLower(key);
                }
                SendKeys.SendWait(key.ToString());

                Thread.Sleep(1000);
            }
        }

        public static void SoundThread()
        {
            while (true)
            {
                if(_random.Next(100) > 80)
                {
                    switch (_random.Next(4))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                    }
                }

                Thread.Sleep(500);
            }
        }

        public static void PopupThread()
        {
            while (true)
            {
                if(_random.Next(100) > 90)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show(
                                "Chrome has stopped working",
                                "Google Chrome",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show(
                                "Your computer is running low on resources",
                                "Microsoft Windows",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            break;
                    }
                }

                Thread.Sleep(1000);
            }
        }
    }
}
