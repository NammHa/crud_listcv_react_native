using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLCV_API.Models
{
    public class TheoDoiCV_View
    {
        public TBL_THEO_DOI_CV getTDCV { get; set; }
        public TBL_HOP_DONG getHD { get; set; }
        public TBL_HE_THONG getHT { get; set; }
        public TBL_NGUOI_DUNG getUser { get; set; }
        public TBL_NGUOI_DUNG_PHONG_BAN getLeader { get; set; }

    }
}