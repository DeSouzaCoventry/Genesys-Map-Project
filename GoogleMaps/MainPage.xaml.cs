using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace GoogleMaps
{
    public partial class MainPage : ContentPage
    {
        MapPageViewModel mapPageViewModel;


        public MainPage()
        {
            InitializeComponent();

            BindingContext = mapPageViewModel = new MapPageViewModel();


            //map.MoveToRegion(MapSpan.FromCenterAndRadius(pinTokyo.Position, Distance.FromMeters(5000)));


        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var contents = await mapPageViewModel.LoadVehicles();

            if (contents != null)
            {
                foreach (var item in contents)
                {
                    Pin VehiclePins = new Pin()
                    {
                        Label = "Cars",
                        Type = PinType.Place,

                        Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                        Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),

                    };
                    map.Pins.Add(VehiclePins);
                }
            }

            var positions = new Position(52.405911, -1.501261);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

        }
        void Button_Clicked_1(System.Object sender, System.EventArgs e)
        {
            var positions = new Position(52.405911, -1.501261);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));

            Device.StartTimer(TimeSpan.FromSeconds(5), () => TimerStarted());
        }
        private bool TimerStarted()
        {
            Device.BeginInvokeOnMainThread(async () =>
             {
                 Compass.Start(SensorSpeed.UI, applyLowPassFilter: true);
                 Compass.ReadingChanged += Compass_ReadingChanged;
                 map.Pins.Clear();
                 map.Polylines.Clear();
                 var contents = await mapPageViewModel.LoadVehicles();
                 if (contents != null)
                 {
                     foreach (var item in contents)
                     {
                         Pin VehiclePins = new Pin()
                         {
                             Label = "Cars",
                             Type = PinType.Place,

                             Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("CarPins.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "CarPins.png", WidthRequest = 30, HeightRequest = 30 }),
                             Position = new Position(Convert.ToDouble(item.Latitude), Convert.ToDouble(item.Longitude)),
                             Rotation = ToRotationPoints(headernorthvalue)
                         };
                         map.Pins.Add(VehiclePins);
                     }
                 }
             }

);
            Compass.Stop();
            return true;
        }

        private float ToRotationPoints(double headernorthvalue)
        {
            return (float)headernorthvalue;
        }

        double headernorthvalue;
        private void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            headernorthvalue = data.HeadingMagneticNorth;
        }
        void map_PinDragStart(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {

        }
        async void map_PinDragEnd(System.Object sender, Xamarin.Forms.GoogleMaps.PinDragEventArgs e)
        {
            var positions = new Position(e.Pin.Position.Latitude, e.Pin.Position.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
            await App.Current.MainPage.DisplayAlert("Alert", "Pick up location : Latitude :" + e.Pin.Position.Latitude + " Longitude  :" + e.Pin.Position.Longitude, "OK");

        }
        void PickupButton_Clicked(System.Object sender, System.EventArgs e)
        {
            Pin VehiclePins = new Pin()
            {
               Label = "Me",
               Type = PinType.Place,
                Icon = (Device.RuntimePlatform == Device.Android) ? BitmapDescriptorFactory.FromBundle("GooglePin.png") : BitmapDescriptorFactory.FromView(new Image() { Source = "GooglePin.png", WidthRequest = 30, HeightRequest = 30 }),
                Position = new Position(Convert.ToDouble(52.405911), Convert.ToDouble(-1.501261)),
               IsDraggable = true

            };
            map.Pins.Add(VehiclePins);
            var positions = new Position(52.405911, -1.501261);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(positions, Distance.FromMeters(500)));
        }
    }
}



