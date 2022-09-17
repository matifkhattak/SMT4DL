using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExcelProcesser
{
    public partial class CyberSecurityDataPreProcessing : Form
    {
        public CyberSecurityDataPreProcessing()
        {
            InitializeComponent();
        }



        private void btnStartPreProcessing_Click(object sender, EventArgs e)
        {
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\faqeerrehman\MSU\CIDDS_week1Updated.csv"))
            {
                
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");


                var csv = new StringBuilder();
                // Unprocessed File Columns
                string dateFirstSeen = "";
                decimal duration ;
                string proto = "";
                string srcIPAddr = "";
                decimal srcPt;
                string dstIPAddr = "";
                decimal dstPt;
                decimal packets;
                decimal bytes;
                string flows = "";
                string flags = "";
                string tos = "";
                string classLabel = "";
                string newLine = "";

                // Processed File Columns against 'dateFirstSeen'
                string isMonday = "";
                string isTuesday = "";
                string isWednesday = "";
                string isThursday = "";
                string isFriday = "";
                string isSaturday = "";
                string isSunday = "";
                double timeOfDay ;

                // Processed File Columns against transport protocol 'Proto', In referenced paper they have only 3 (TCP,UCP,ICMP) but we have found four
                string isTCP = "";
                string isUDP = "";
                string isIGMP = "";
                string isICMP = "";

                // Processed File Columns against Source IP Address 'Src IP Addr'
                decimal srcIpAddressPart1;
                decimal srcIpAddressPart2;
                decimal srcIpAddressPart3;
                decimal srcIpAddressPart4;

                // Processed File Columns against Destination IP Address 'Dst IP Addr'
                decimal dstIpAddressPart1;
                decimal dstIpAddressPart2;
                decimal dstIpAddressPart3;
                decimal dstIpAddressPart4;

                // Processed File Columns against TCP Flags 'Flags'
                int isURG;
                int isACK;
                int isPSH;
                int isRES;
                int isSYN;
                int isFIN;


                decimal minDuration = 0.0M;
                decimal maxDuration = 224.412M;
                int minBytes = 42;
                int maxBytes = 516200000;
                int minPackets = 1;
                int maxPackets = 208768;

                int rowNo = 0;
                int index = 0;
                string[] ipAddressArray;
                int charIndexInFlags = 0;
                while (!parser.EndOfData)
                {
                    rowNo = rowNo + 1;
                    // Reset Fields
                    isMonday = "0";
                    isTuesday = "0";
                    isWednesday = "0";
                    isThursday = "0";
                    isFriday = "0";
                    isSaturday = "0";
                    isSunday = "0";

                    isTCP = "0";
                    isUDP = "0";
                    isIGMP = "0";
                    isICMP = "0";                    

                    isURG = 0;
                    isACK = 0;
                    isPSH = 0;
                    isRES = 0;
                    isSYN = 0;
                    isFIN = 0;

                    charIndexInFlags = 0;

                    //Extract columns information from each row
                    string[] fields = parser.ReadFields();
                    dateFirstSeen = fields[0].ToString();
                    duration = Convert.ToDecimal(fields[1]);
                    proto = fields[2].ToString();
                    srcIPAddr = fields[3].ToString();
                    srcPt = Convert.ToDecimal(fields[4]);
                    dstIPAddr = fields[5].ToString();
                    dstPt = Convert.ToDecimal(fields[6]);
                    packets = Convert.ToDecimal(fields[7]);
                    bytes = Convert.ToDecimal(fields[8]);
                    flows = fields[9].ToString();
                    flags = fields[10].ToString();
                    tos = fields[11].ToString();
                    classLabel = fields[12].ToString();
                                        
                    // 1
                    switch ((Convert.ToDateTime(dateFirstSeen).ToString("ddd")))
                    {
                        case "Mon":
                            isMonday = "1";
                            break;
                        case "Tue":
                            isTuesday = "1";
                            break;
                        case "Wed":
                            isWednesday = "1";
                            break;
                        case "Thu":
                            isThursday = "1";
                            break;
                        case "Fri":
                            isFriday = "1";
                            break;
                        case "Sat":
                            isSaturday = "1";
                            break;
                        case "Sun":
                            isSunday = "1";
                            break;
                    }
                    // 2
                    index = dateFirstSeen.IndexOf(".");
                    if (dateFirstSeen.IndexOf(".") > 0)
                        dateFirstSeen = dateFirstSeen.Substring(0, index);

                    timeOfDay = Math.Round((Convert.ToDateTime(dateFirstSeen).TimeOfDay.TotalSeconds)/86400,4); // We have 86400 total seconds in a day(24 hours)

                    //3
                    duration = Math.Round(((duration - minDuration)/(maxDuration-minDuration)),4);

                    //4
                    switch (proto)
                    {
                        case "TCP":
                            isTCP = "1";
                            break;
                        case "UDP":
                            isUDP = "1";
                            break;
                        case "IGMP":
                            isIGMP = "1";
                            break;
                        case "ICMP":
                            isICMP = "1";
                            break;
                    }
                    
                    //5
                    ipAddressArray = srcIPAddr.Split('.');
                    if (ipAddressArray.Count() != 4)
                    {
                        continue;
                    }
                    srcIpAddressPart1 = Decimal.Round((Convert.ToInt16(ipAddressArray[0])/255.0M),4);
                    srcIpAddressPart2 = Decimal.Round((Convert.ToInt16(ipAddressArray[1]) / 255.0M), 4);
                    srcIpAddressPart3 = Decimal.Round((Convert.ToInt16(ipAddressArray[2]) / 255.0M), 4);
                    srcIpAddressPart4 = Decimal.Round((Convert.ToInt16(ipAddressArray[3]) / 255.0M), 4);

                    //6
                    srcPt = Math.Round((srcPt / 65535.0M),4); //max ports 65535
                    
                    //7
                    ipAddressArray = dstIPAddr.Split('.');
                    if (ipAddressArray.Count() != 4)
                    {
                        continue;
                    }
                    dstIpAddressPart1 = Decimal.Round((Convert.ToInt16(ipAddressArray[0]) / 255.0M), 4);
                    dstIpAddressPart2 = Decimal.Round((Convert.ToInt16(ipAddressArray[1]) / 255.0M), 4);
                    dstIpAddressPart3 = Decimal.Round((Convert.ToInt16(ipAddressArray[2]) / 255.0M), 4);
                    dstIpAddressPart4 = Decimal.Round((Convert.ToInt16(ipAddressArray[3]) / 255.0M), 4);

                    //8
                    dstPt = Math.Round((dstPt / 65535.0M),4); //max ports 65535
                    
                    //9
                    packets = Math.Round(((packets - minPackets) / (maxPackets - minPackets)),4);

                    //10
                    bytes = Math.Round(((bytes - minBytes) / (maxBytes - minBytes)),4);

                    //11-flows
                    
                    //12- flags
                    foreach (char c in flags)
                    {

                        if (charIndexInFlags == 0)
                        {
                            if (c != '.')
                            {
                                isURG = 1;
                            }
                        }
                        else if (charIndexInFlags == 1)
                        {
                            if (c != '.')
                            {
                                isACK = 1;
                            }
                        }
                        else if (charIndexInFlags == 2)
                        {
                            if (c != '.')
                            {
                                isPSH = 1;
                            }
                        }
                        else if (charIndexInFlags == 3)
                        {
                            if (c != '.')
                            {
                                isRES = 1;
                            }
                        }
                        else if (charIndexInFlags == 4)
                        {
                            if (c != '.')
                            {
                                isSYN = 1;
                            }
                        }
                        else if (charIndexInFlags == 5)
                        {
                            if (c != '.')
                            {
                                isFIN = 1;
                            }
                        }
                        
                        charIndexInFlags = charIndexInFlags + 1;
                    }

                    //13 tos

                    //14 classLabel

                    newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33}", isMonday, isTuesday, isWednesday, isThursday, isFriday, isSaturday, isSunday, timeOfDay, duration, isTCP, isUDP, isICMP, isIGMP
                        ,srcIpAddressPart1, srcIpAddressPart2, srcIpAddressPart3, srcIpAddressPart4, srcPt, dstIpAddressPart1, dstIpAddressPart2, dstIpAddressPart3, dstIpAddressPart4, dstPt,
                        packets, bytes, flows, isURG, isACK, isPSH, isRES, isSYN, isFIN,tos,classLabel);
                    File.AppendAllText(@"C:\Users\faqeerrehman\MSU\CIDDS_week1Processed.csv", newLine + "\n");
                    //newLine = string.Format("{0},{1}", dateFirstSeen, duration);
                    //csv.AppendLine(newLine);
                    //File.WriteAllText(@"C:\Users\faqeerrehman\MSU\testfile1.csv", csv.ToString());
                }
            }
        }

        private void btnProcessByteAttribute_Click(object sender, EventArgs e)
        {
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\faqeerrehman\MSU\CIDDS_week1.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                var csv = new StringBuilder();
                // Unprocessed File Columns
                string dateFirstSeen = "";
                string duration = "";
                string proto = "";
                string srcIPAddr = "";
                string srcPt = "";
                string dstIPAddr = "";
                string dstPt = "";
                string packets = "";
                string bytes = "";
                string flows = "";
                string flags = "";
                string tos = "";
                string classLabel = "";
                string newLine = "";
                double processedBytes = 0;
                string[] fields = null;
                int rowCount = 0;
                while (!parser.EndOfData)
                {
                    rowCount = rowCount + 1;

                    fields = null;
                    //Extract columns information from each row
                    fields = parser.ReadFields();

                    dateFirstSeen = fields[0].ToString();
                    duration = fields[1].ToString();
                    proto = fields[2].ToString();
                    srcIPAddr = fields[3].ToString();
                    srcPt = fields[4].ToString();
                    dstIPAddr = fields[5].ToString();
                    dstPt = fields[6].ToString();
                    packets = fields[7].ToString();
                    bytes = fields[8].ToString();
                    flows = fields[9].ToString();
                    flags = fields[10].ToString();
                    tos = fields[11].ToString();
                    classLabel = fields[12].ToString();

                    if (bytes.Contains("M"))
                    {
                        string[] splitString = bytes.Split('M');
                        processedBytes = Convert.ToDouble(splitString[0]) * 1000000;
                    }
                    else
                    {
                        processedBytes = Convert.ToInt32(bytes);
                    }
                    newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}", dateFirstSeen, duration, proto, srcIPAddr, srcPt, dstIPAddr, dstPt, packets, processedBytes.ToString(), flows, flags, tos, classLabel);
                    File.AppendAllText(@"C:\Users\faqeerrehman\MSU\CIDDS_week1Updated.csv", newLine + "\n");

                    //csv.AppendLine(newLine);

                }


            }
        }
    }
}
