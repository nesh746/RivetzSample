using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Com.Rivetz.Bridge;
using Com.Rivetz.Lib;

namespace RivetzSample
{
    [Activity(Label = "RivetzSample", MainLauncher = true)]
    public class MainActivity : ActionBarActivity
    {
        Rivet rivet;
        string keyName = "MyKey";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            //Toolbar will now take on default actionbar characteristics 
            SetSupportActionBar(toolbar);

            SupportActionBar.Title = "Hello from Appcompat Toolbar";

            rivet = new Rivet(this, Rivet.DeveloperSpid);

            Button createBtn = FindViewById<Button>(Resource.Id.createkey);
            Button pairBtn = FindViewById<Button>(Resource.Id.pair);
            Button signBtn = FindViewById<Button>(Resource.Id.sign);
            Button deleteBtn = FindViewById<Button>(Resource.Id.delete);
            createBtn.Click += CreateBtn_Click;
            pairBtn.Click += PairBtn_Click;
            signBtn.Click += SignBtn_Click;
            deleteBtn.Click += deleteBtn_Click;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            rivet.DeleteKey(keyName);
        }

        private void SignBtn_Click(object sender, System.EventArgs e)
        {
            byte[] signature = rivet.Sign(keyName, "this is a string");
            Toast.MakeText(this, Utilities.BytesToHex(signature), ToastLength.Long).Show();
        }

        private void PairBtn_Click(object sender, System.EventArgs e)
        {
            rivet.PairDevice(this);
            Toast.MakeText(Android.App.Application.Context, "Return from doPair", ToastLength.Long).Show();
        }

        private void CreateBtn_Click(object sender, System.EventArgs e)
        {
            KeyRecord key = rivet.CreateKey(Rivet.KeyType.EcdsaDflt, keyName);
            if (key != null)
            {
                //           Toast.makeText(this, key.name + " has been created", ToastLength.Long).Show();
                Toast.MakeText(Android.App.Application.Context, key.Name + " has been created", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Error creating key: " + rivet.Status, ToastLength.Long).Show();
            }
        }

        public void doPair(View v)
        {
            rivet.PairDevice(this);
            Toast.MakeText(Android.App.Application.Context, "Return from doPair", ToastLength.Long).Show();
        }

        public void doCreateKey(View v)
        {
            KeyRecord key = rivet.CreateKey(Rivet.KeyType.EcdsaDflt, keyName);
            if (key != null)
            {
                //           Toast.makeText(this, key.name + " has been created", ToastLength.Long).Show();
                Toast.MakeText(Android.App.Application.Context, key.Name + " has been created", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(Android.App.Application.Context, "Error creating key: " + rivet.Status, ToastLength.Long).Show();
            }
        }

        public void doSign(View v)
        {
            byte[] signature = rivet.Sign(keyName, "this is a string");
            Toast.MakeText(this, Utilities.BytesToHex(signature), ToastLength.Long).Show();
        }

        public void doDelete(View v)
        {
            rivet.DeleteKey(keyName);

        }
    }
}

