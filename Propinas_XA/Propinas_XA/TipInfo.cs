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

        private void OnTipValueChanged()
        {
            var h = TipValueChanged;
            if (h != null)
            {
                h(this, EventArgs.Empty);
            }
        }

        public decimal Tax
        {
            get { return Total - Subtotal; }
        }

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

        public event EventHandler TipValueChanged;
    }
}