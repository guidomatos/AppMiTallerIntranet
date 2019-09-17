using System;
using System.Collections.Generic;
using System.Text;

namespace AppMiTaller.Intranet.BL
{
    public class BaseBL
    {
        private string _Error;

        public string Error
        {
            get { return _Error; }
            set { _Error = value; }
        }
    }

}

