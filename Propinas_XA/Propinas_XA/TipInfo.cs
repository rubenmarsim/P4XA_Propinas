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

namespace Propinas_XA
{
    class TipInfo
    {

        /// <summary>
        /// Creamos la propiedad Total y en el set 
        /// le decimos que si total es diferente de
        /// value que ejecute el metodo OnTipValueChanged
        /// </summary>
        decimal _Total;
        public decimal Total
        {
            get { return _Total; }
            set
            {
                if (_Total != value)
                {
                    _Total = value;
                    OnTipValueChanged();
                }
            }
        }
        /// <summary>
        /// Creamos la propiedad Subtotal y en el set 
        /// le decimos que si total es diferente de
        /// value que ejecute el metodo OnTipValueChanged
        /// </summary>
        decimal _Subtotal;
        public decimal Subtotal
        {
            get { return _Subtotal; }
            set
            {
                if (_Subtotal != value)
                {
                    _Subtotal = value;
                    OnTipValueChanged();
                }
            }
        }

        /// <summary>
        /// Creamos la propiedad TipPercent y en el set 
        /// le decimos que si total es diferente de
        /// value que ejecute el metodo OnTipValueChanged
        /// </summary>
        decimal _TipPercent;
        public decimal TipPercent
        {
            get { return _TipPercent; }
            set
            {
                if (_TipPercent != value)
                {
                    _TipPercent = value;
                    OnTipValueChanged();
                }
            }
        }
        /// <summary>
        /// Creamos el metodo OnTipValueChanged
        /// que vacia el EventArgs
        /// </summary>
        private void OnTipValueChanged()
        {
            var h = TipValueChanged;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }
        /// <summary>
        /// Creamos la propiedad Tax, y en
        /// el get le decimos que devuelva el
        /// total menos el subtotal
        /// </summary>
        public decimal Tax
        {
            get { return Total - Subtotal; }
        }

        /// <summary>
        /// Creamos la propiedad TipValue, y en el get
        /// le decimos que si total, subtotal o tippercent es 0
        /// devuelva 0, luego hacemos el calculo del porcentaje
        /// y para obtener el tipvalue le decimos que nos haga
        /// el total menos el calculo que hemos hecho
        /// </summary>
        public decimal TipValue
        {
            get
            {
                if(Total == 0m || Subtotal == 0m || TipPercent == 0m)
                {
                    return 0m;
                }

                var percent = TipPercent;
                percent /= 100;
                decimal value = (Subtotal * (1 + percent)) + (Total - Subtotal);
                decimal fract = value - Math.Truncate(value);
                int f = (int)(fract * 100);
                while ((f % 25) != 0)
                {
                    ++f;
                }
                fract = f;
                fract /= 100;
                value = Math.Truncate(value) + fract;
                return value - Total;
            }
        }
        /// <summary>
        /// creamos el evento TipValueChanged
        /// </summary>
        public event EventHandler TipValueChanged;
    }
}