using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;

namespace Propinas_XA
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        TipInfo info = new TipInfo()
        {
            TipPercent = 15,
        };

        TextView TipPercent;
        TextView Total;
        TextView TipValue;

        public MainActivity()
        {
            info.TipValueChanged += (sender, e) =>
            {
                TipValue.Text = info.TipValue.ToString();
                Total.Text = (info.TipValue + info.Total).ToString();
            };
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TipValue = FindViewById<TextView>(Resource.Id.txtVwTipValue);
            Total = FindViewById<TextView>(Resource.Id.txtVwTotal);
            TipPercent = FindViewById<TextView>(Resource.Id.txtVwTipPercent);

            TipPercent.AfterTextChanged += (sender, e) =>
            {
                info.TipPercent = Parse(TipPercent);
            };

            FindViewById<SeekBar>(Resource.Id.slider).SetOnSeekBarChangeListener(new SeekBarChangeListener(this));

            var subtotal = FindViewById<TextView>(Resource.Id.txtVwSubtotal);
            subtotal.AfterTextChanged+=(sender, e) =>
            {
                info.Subtotal = Parse(Total);
            };
            var total = FindViewById<TextView>(Resource.Id.txtVwTotalPostTax);
            total.AfterTextChanged += (sender, e) =>
              {
                  info.Total = Parse(total);
              };
        }

        static decimal Parse(TextView field)
        {
            if (field.Text == "")
            {
                return 0m;
            }
            try
            {
                return Convert.ToDecimal(field.Text);
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

                field.Text = string.Empty;
                return 0m;
            }
        }
    }

    class SeekBarChangeListener:Java.Lang.Object, SeekBar.IOnSeekBarChangeListener
    {
        MainActivity context;

        public SeekBarChangeListener(MainActivity context)
        {
            this.context = context;
        }
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            context.TipPercent.text = progress.ToString();
        }
        public void OnStartTrackingTouch(SeekBar seekBar)
        {

        }
        public void OnStopTrackingTouch(SeekBar seekBar)
        {

        }
    }
}