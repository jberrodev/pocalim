using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;
using Android.Gms.Common;
using Android.Util;

namespace PocAlim.Droid
{
    [Activity(Theme = "@style/MyTheme.NoTitle")]
    public class MapActivity : Activity, IOnMapReadyCallback
    {
        public static readonly int InstallGooglePlayServicesId = 1000;
        public static readonly string Tag = "CheckGooglePlayService";
        private bool _isGooglePlayServicesInstalled;

        private GoogleMap gMap;        

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //On vérifie si les Google Play Services sont dispo
            _isGooglePlayServicesInstalled = TestIfGooglePlayServicesIsInstalled();
            // Si OUI, on charge le Layout de manière classique
            if (_isGooglePlayServicesInstalled)
            {
                SetContentView(Resource.Layout.Map);
                SetUpMap();
            }
            //Si NON, on cache les éléments du layout
            //pour que l'utiliseur ne voit que la proposition
            //de MAJ de Google Play Services
            else
            {
                SetContentView(Resource.Layout.Map);
                SetUpMap();
                FrameLayout myLayout = (FrameLayout)FindViewById(Resource.Id.myLayout);
                myLayout.Visibility = ViewStates.Invisible;
            }
        }

        private void SetUpMap()
        {
            if (gMap == null)
            {
                FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            gMap = googleMap;
        }
       

        private bool TestIfGooglePlayServicesIsInstalled()
        {
            int queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == ConnectionResult.Success)
            {
                Log.Info(Tag, "Google Play Services is installed on this device.");
                return true;
            }
            return false;
        }
    }
}