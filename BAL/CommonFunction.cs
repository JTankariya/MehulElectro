using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL
{
    public class CommonFunction
    {
        public static Font font10Normal = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.NORMAL);
        public static Font font10 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10, Font.BOLD);
        public static Font fontTitle17 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 17, Font.BOLD);
        public static Font fontTitle13 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 13, Font.BOLD);
        public static Font font8 = FontFactory.GetFont(FontFactory.TIMES_ROMAN, 8);
        public static string CurrencyFormat = "$#,##0.00;($#,##0.00)";
        public static string QtyFormat = "0.000;(0.000)";
    }
}
