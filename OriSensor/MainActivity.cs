using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Xamarin.Essentials;

namespace OriSensor
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            new OrientationSensorTest();
        }
        public class OrientationSensorTest
        {

            SensorSpeed speed = SensorSpeed.UI;

            public OrientationSensorTest()
            {

                OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
                ToggleOrientationSensor();
            }

            void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
            {
                var data = e.Reading;
                Console.WriteLine($"Reading: X: {data.Orientation.X}, Y: {data.Orientation.Y}, Z: {data.Orientation.Z}, W: {data.Orientation.W}");


            }

            public void ToggleOrientationSensor()
            {
                try
                {
                    if (OrientationSensor.IsMonitoring)
                        OrientationSensor.Stop();
                    else
                        OrientationSensor.Start(speed);
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    Log.Debug("features is not support", fnsEx.Message);
                }
                catch (Exception ex)
                {
                    Log.Debug("feature is not support", ex.Message);
                }
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}