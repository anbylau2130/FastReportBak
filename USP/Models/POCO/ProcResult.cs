using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP.Models.POCO
{
    public class ProcResult
    {
        private bool _isSuccess = false;
        private string _procMsg = string.Empty;
        public bool IsSuccess
        {
            get { return _isSuccess; }
            set { _isSuccess = value; }
        }

        public string ProcMsg
        {
            get { return _procMsg; }
            set { _procMsg = value; }
        }


    }
}
