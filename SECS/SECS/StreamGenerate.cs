using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace SECS
{
    public class StreamGenerate
    {
        public string StreamFormatGenerate(string hsmsName, object stream)
        {
            string _rtn = "";
            switch (hsmsName)
            {
                case "S6F103":
                    _rtn = S6F103(stream);
                    break;
            }
            return _rtn;
        }
        private string S6F103(object stream)
        {
            string _rtn = "";
            string space4 = new string(' ', 4);
            string space8 = new string(' ', 8);
            string space16 = new string(' ', 16);
            string space24 = new string(' ', 24);
            string space30 = new string(' ', 30);
            string space32 = new string(' ', 32);

            S6F103 data = (S6F103)stream;
            _rtn += "Discrete Variable Data Send:'S6F103' W    \n";
            _rtn += "<L [5]\n";
            _rtn += space8 + string.Format("<A [1]{0}>\n", data.CEID);    //CEID
            _rtn += space8 + string.Format("<A [2]{0}>\n", data.SUBCD);    //SUBCD
            _rtn += space8 + string.Format("<A [8]{0}>\n", PadSpace(data.INLINEID, 8));    //INLINEID
            _rtn += space8 + string.Format("<A [8]{0}>\n", PadSpace(data.EQUIPMENTID, 8));    //EQUIPMENTID
            _rtn += space8 + "<L [20]\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [6]UNITID>\n";
            _rtn += space24 + string.Format("<A [8]{0}>\n", PadSpace(data.UNITID, 8));    //UNITID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [7]GLASSID>\n";
            _rtn += space24 + string.Format("<A [12]{0}>\n", PadSpace(data.GLASSID, 12));    //GLASSID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [5]LOTID>\n";
            _rtn += space24 + string.Format("<A [12]{0}>\n", PadSpace(data.LOTID, 12));    //LOTID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [9]PRODUCTID>\n";
            _rtn += space24 + string.Format("<A [16]{0}>\n", PadSpace(data.PRODUCTID, 16));    //PRODUCTID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RECIPEID>\n";
            _rtn += space24 + string.Format("<A [16]{0}>\n", PadSpace(data.RECIPEID, 16));    //RECIPEID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [7]ROUTEID>\n";
            _rtn += space24 + string.Format("<A [8]{0}>\n", PadSpace(data.ROUTEID, 8));    //ROUTEID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [7]OWNERID>\n";
            _rtn += space24 + string.Format("<A [8]{0}>\n", PadSpace(data.OWNERID, 8));    //OWNERID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [11]OPERATIONID>\n";
            _rtn += space24 + string.Format("<A [4]{0}>\n", PadSpace(data.OPERATIONID, 4));    //OPERATIONID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [10]CASSETTEID>\n";
            _rtn += space24 + string.Format("<A [8]{0}>\n", PadSpace(data.CASSETTEID, 8));    //CASSETTEID
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]OPERATOR>\n";
            _rtn += space24 + string.Format("<A [16]{0}>\n", PadSpace(data.OPERATOR, 16));    //OPERATOR
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [7]CLMDATE>\n";
            _rtn += space24 + string.Format("<A [10]{0}>\n", PadSpace(data.CLMDATE, 10));    //CLMDATE
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [7]CLMTIME>\n";
            _rtn += space24 + string.Format("<A [8]{0}>\n", PadSpace(data.CLMTIME, 8));    //CLMTIME
            _rtn += space16 + ">\n";

            //Below RESERVE
            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE1>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE1
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE2>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE2
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE3>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE3
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE4>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE4
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE5>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE5
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE6>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE6
            _rtn += space16 + ">\n";

            _rtn += space16 + "<L [2]\n";
            _rtn += space24 + "<A [8]RESERVE7>\n";
            _rtn += space24 + string.Format("<A [30]{0}>\n", space30);    //RESERVE7
            _rtn += space16 + ">\n";

            //BELOW ITEM
            _rtn += space16 + "<L [1]\n";
            _rtn += space24 + "<L [4]\n";
            _rtn += space32 + string.Format("<A [16]{0}>\n", PadSpace(data.DVNAME, 16));  //DVNAME1
            _rtn += space32 + string.Format("<A [4]{0}>\n", PadSpace(data.DVTYPE, 4));    //DVTYPE1
            _rtn += space32 + string.Format("<A [12]{0}>\n", PadSpace(data.DVVAL, 12));    //DVVAL1
            _rtn += space32 + string.Format("<A [1]{0}>\n", "Y");    //SPC_FLAG
            //END
            _rtn += space24 + ">\n" + space16 + ">\n" + space8 + ">\n" + ">\n" + ".";

            return _rtn;
        }

        private string PadSpace(string input, int length)
        {
            return input += new string(' ', length - input.Length);
        }
    }

    public class S6F103
    {
        public int listCount { get; set; }
        public string CEID { get; set; }
        public string SUBCD { get; set; }
        public string LOTID { get; set; }
        public string CASETID { get; set; }
        public string GLASSID { get; set; }
        public string SLOTID { get; set; }
        public string PORTID { get; set; }
        public string CHAMBERID { get; set; }
        public string LOTSTARTTIME { get; set; }
        public string LOTENDTIME { get; set; }
        public string LOTPROCESSTIME { get; set; }
        public string LOTPROCST { get; set; }
        public string GLASS_RECIPEID { get; set; }
        public string GPRSTARTTIME { get; set; }
        public string GPRENTIME { get; set; }
        public string DVNAME { get; set; }
        public string DVTYPE { get; set; }
        public string DVVAL { get; set; }
        public string MES_LINK_KEY { get; set; }
        public string ULFLAG { get; set; }
        public string LOTCATEGORY { get; set; }
        public string PRODCATEGORY { get; set; }
        public string REWORK_COUNT { get; set; }
        public string EQID { get; set; }
        public string INLINEID { get; set; }
        public string EQUIPMENTID { get; set; }
        public string UNITID { get; set; }
        public string PRODUCTID { get; set; }
        public string RECIPEID { get; set; }
        public string ROUTEID { get; set; }
        public string OWNERID { get; set; }
        public string OPERATIONID { get; set; }
        public string CASSETTEID { get; set; }
        public string OPERATOR { get; set; }
        public string CLMDATE { get; set; }
        public string CLMTIME { get; set; }
        public string PROCESS_TIME { get; set; }
        public string SPEC_FLAG { get; set; }
    }

    public class StreamFunction
    {


        
 
        

    }
}
