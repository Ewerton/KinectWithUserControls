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
using Microsoft.Samples.Kinect.WpfViewers;
using Microsoft.Kinect;

namespace KinectWithUserControls
{
    /// <summary>
    /// Interaction logic for KinectUserControl.xaml
    /// </summary>
    public partial class KinectUserControl : UserControl
    {
        // the user control need to expose a KinectSensor property to be binded, right?
        // i preffer to expose a KinectSensor than KinectSensorManager
        public static readonly DependencyProperty KinectSensorProperty =
            DependencyProperty.Register(
                "KinectSensor",
                typeof(KinectSensor),
                typeof(KinectUserControl),
                new PropertyMetadata(null));

        public KinectSensor KinectSensor
        {
            get { return (KinectSensor)GetValue(KinectSensorProperty); }
            set { SetValue(KinectSensorProperty, value); }
        }



        // The color viewer needs a KinectSensorManager bind, how to do this?

        public KinectUserControl()
        {           
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // this type of attribuition is possible?
            //ColorViewer.KinectSensorManager = someKinectSensorManager;

            this.KinectSensor.ColorFrameReady += new EventHandler<ColorImageFrameReadyEventArgs>(KinectSensor_ColorFrameReady);
            this.KinectSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(KinectSensor_AllFramesReady);
        }

        void KinectSensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        void KinectSensor_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            // just to test is this breakpoint will be reached
            //throw new NotImplementedException();
        }
    }
}
