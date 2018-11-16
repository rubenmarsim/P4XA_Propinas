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
        /// <summary>
        /// instanciamos la clase TipInfo
        /// y le damos 15 de valor a TipPercent
        /// </summary>
        TipInfo info = new TipInfo()
        {
            TipPercent = 15,
        };
        /// <summary>
        /// Creamos las variables publicas,
        /// en este caso de tipo TextView
        /// </summary>
        public TextView TipPercent;
        TextView Total;
        TextView TipValue;

        /// <summary>
        /// Dentro del MainActivity le decimos que
        /// El valor de los textviews TipValue y Total
        /// van a ser igual a la operacion que hacemos
        /// con las propiedades de TipInfo, y luego
        /// pasamos todo a string
        /// </summary>
        public MainActivity()
        {
            info.TipValueChanged += (sender, e) => {
                TipValue.Text = info.TipValue.ToString();
                Total.Text = (info.TipValue + info.Total).ToString();
            };
        }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Buscamos los EditText del layout
            TipValue = FindViewById<TextView>(Resource.Id.edtxtTipValue);
            Total = FindViewById<TextView>(Resource.Id.edtxtTotal);
            TipPercent = FindViewById<TextView>(Resource.Id.edtxtTipPercent);

            //Vinculamos los EditText con las propiedades 
            //de la clase TipInfo
            TipPercent.AfterTextChanged += (sender, e) =>
            {
                info.TipPercent = Parse(TipPercent);
            };
            //Buscamos el SeekBar del layout
            FindViewById<SeekBar>(Resource.Id.slider).SetOnSeekBarChangeListener(new SeekBarChangeListener(this));
            //Buscamos los EditText del layout y los
            //vinculamos los EditText con las propiedades 
            //de la clase TipInfo
            var subtotal = FindViewById<TextView>(Resource.Id.edtxtSubtotal);
            subtotal.AfterTextChanged+=(sender, e) => {
                info.Subtotal = Parse(subtotal);
            };
            var total = FindViewById<TextView>(Resource.Id.edtxtTotalPostTax);
            total.AfterTextChanged += (sender, e) =>
              {
                  info.Total = Parse(total);
              };
        }
        /// <summary>
        /// Creamos un metodo y le pasamos el campo del TextView
        /// y le decimos que si este campo esta vacio que devuelva 0
        /// luego hacemos un try catch en el cual le decimos que 
        /// intente pasarlo a decimal y si no puede que muestre un
        /// mensaje de error
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
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
    /// <summary>
    /// Configuramos la SeekBar para que cuando
    /// la modifiquemos nos cambie los numeros en
    /// los TextView
    /// </summary>
    class SeekBarChangeListener:Java.Lang.Object, SeekBar.IOnSeekBarChangeListener
    {
        MainActivity context;

        public SeekBarChangeListener(MainActivity context)
        {
            this.context = context;
        }
        public void OnProgressChanged(SeekBar seekBar, int progress, bool fromUser)
        {
            context.TipPercent.Text = progress.ToString();
        }
        public void OnStartTrackingTouch(SeekBar seekBar)
        {

        }
        public void OnStopTrackingTouch(SeekBar seekBar)
        {

        }
    }
}