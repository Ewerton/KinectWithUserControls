using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;
using Microsoft.Kinect;

namespace KinectWithUserControls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declare a SensorChooser from KinectToolkit and  and a SensorManager from Samples.Kinect.WpfViewers
        // Is possíble to use only one ?
        KinectSensorChooser SensorChooser = new KinectSensorChooser();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.SensorChooser.KinectChanged += new EventHandler<KinectChangedEventArgs>(SensorChooser_KinectChanged);

            // Starts the Kinect discovering process.
            this.SensorChooser.Start();
        }

        void SensorChooser_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            KinectSensor oldSensor = (KinectSensor)e.OldSensor;
            StopKinect(oldSensor);

            KinectSensor newSensor = (KinectSensor)e.NewSensor;

            if (newSensor == null)
            {
                // No sensor detected
                return;
            }

            try
            {
               StartKinect(newSensor);
            }
            catch (System.IO.IOException)
            {
                this.SensorChooser.TryResolveConflict();
            }
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // i know, some validations is needed.
            StartKinect(this.SensorChooser.Kinect);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopKinect(this.SensorChooser.Kinect);
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    sensor.Stop();
                   
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }

                    btnStart.IsEnabled = true;
                    btnStop.IsEnabled = false;
                    txtKinectStatus.Text = "Kinect Stopped";
                }
            }
        }

        private void StartKinect(KinectSensor sensor)
        {
            sensor.Start();

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;
            txtKinectStatus.Text = "Kinect Started";

            //Where i need to write the code that sends the sensor information (color stream, sekeleton stream, etc) to the 
            // user control? Here?
            kinectUserControl1.KinectSensor = this.SensorChooser.Kinect;

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //this.SensorChooser.Stop();
        }



    }
}
