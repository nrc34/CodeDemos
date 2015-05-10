using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AppDemoAndroid
{
    [Activity(Label = "AppDemoAndroid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);


            var txtUser = FindViewById<EditText>(Resource.Id.editTextUser);
            var btnSearch = FindViewById<Button>(Resource.Id.btnSearch);
            var lvw = FindViewById<ListView>(Resource.Id.listView1);


            btnSearch.Click += async (object sender, EventArgs e) =>
            {
                var github = new GitHub();
                var repositories = await github.GetAsync(txtUser.Text);
                lvw.Adapter =
                    new ArrayAdapter(this,
                                     Android.Resource.Layout.SimpleListItemSingleChoice,
                                     repositories);


            };
        }
    }
}

