﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using USP.Models.Entity;

namespace USP.Bll
{
    public interface ISysCorpFeeTypeBll
    {
        List<SysCorpFeeType> GetCorpFeeTypeDropDownList();
    }
}
