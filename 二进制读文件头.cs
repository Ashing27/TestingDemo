using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace ConsoleApplication1
{
    //[StructLayout(LayoutKind.Auto)]
    public struct SequenceHeaderInfo
    {
        public int a1;
        public int a2;
        public int a3;
        public int a4;
        public int a5;
        public int a6;
        public int a7;
    };

    //[StructLayout(LayoutKind.Auto)]
    struct GantryAndTable
    {
        public int Tabledata1;
        public int Tabledata2;
        public double d1;
        public double d2;
        public double d3;
        public double d4;
        public double d5;
        public double d6;
        public double d7;
        public double d8;
        public double d9;
        public double d10;
        public double d11;
    };

    class Program
    {
        static void Main(string[] args)
        {
            //Init read.
            string filePath = "F:\\";
            string fileName = "testFile.txt";
            //string fileName = "863A2489-5028-4D9E-965A-4D40C2B189F5.raw";
            //863F1008-BBEC-4D1A-B5E8-B30E5C6FCDAC
            
            //string fileName = "863F1008-BBEC-4D1A-B5E8-B30E5C6FCDAC.raw";
            if (System.IO.Directory.Exists(filePath))
            {
                Console.WriteLine("Got path.");
            }
            else
            {
                Console.WriteLine("Path missing");
            }
            if (System.IO.File.Exists(filePath + fileName))
            {
                Console.WriteLine("Got file.");
            }
            else
            {
                Console.WriteLine("File missing");
            }

            #region Adam solution

            //read raw file.
            SequenceHeaderInfo currentSequenceHeaderInfo = new SequenceHeaderInfo();
            GantryAndTable currentGantryAndTable = new GantryAndTable();

            FileStream fs = new FileStream(filePath + fileName, FileMode.Open);
            byte[] seqHeaderBuffer = new byte[Marshal.SizeOf(currentSequenceHeaderInfo)];
            byte[] frameInfoBuffer = new byte[Marshal.SizeOf(currentGantryAndTable)];

            //Read seqsInfo
            fs.Read(seqHeaderBuffer, 0, Marshal.SizeOf(currentSequenceHeaderInfo));
            IntPtr structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(currentSequenceHeaderInfo));
            Marshal.Copy(seqHeaderBuffer, 0, structPtr, Marshal.SizeOf(currentSequenceHeaderInfo));
            object obj = Marshal.PtrToStructure(structPtr, typeof(SequenceHeaderInfo));
            Marshal.FreeHGlobal(structPtr);
            SequenceHeaderInfo localSequenceHeaderInfo = (SequenceHeaderInfo)obj;

            Console.WriteLine(  " " + localSequenceHeaderInfo.a1 +
                                " " + localSequenceHeaderInfo.a2 +
                                " " + localSequenceHeaderInfo.a3 +
                                " " + localSequenceHeaderInfo.a4 +
                                " " + localSequenceHeaderInfo.a5 +
                                " " + localSequenceHeaderInfo.a6 +
                                " " + localSequenceHeaderInfo.a7   );

                

            //Read Gantry and table info
            for (int i = 0; i < 8000; i++)
            {
                //Read frameID
                byte[] frameIDbuffer = new byte[sizeof(int)];
                fs.Read(frameIDbuffer, 0, sizeof(int));
                int frameID = System.BitConverter.ToInt32(frameIDbuffer, 0);
                Console.WriteLine(frameID);

                fs.Read(frameInfoBuffer, 0, Marshal.SizeOf(currentGantryAndTable));
                structPtr = Marshal.AllocHGlobal(Marshal.SizeOf(currentGantryAndTable));
                Marshal.Copy(frameInfoBuffer, 0, structPtr, Marshal.SizeOf(currentGantryAndTable));
                obj = Marshal.PtrToStructure(structPtr, typeof(GantryAndTable));
                Marshal.FreeHGlobal(structPtr);
                GantryAndTable localGantryAndTable = (GantryAndTable)obj;

                Console.WriteLine(localGantryAndTable.Tabledata1 +
                                    " " + localGantryAndTable.Tabledata2 +
                                    " " + localGantryAndTable.d1 +
                                    " " + localGantryAndTable.d2 +
                                    " " + localGantryAndTable.d3 +
                                    " " + localGantryAndTable.d4 +
                                    " " + localGantryAndTable.d5 +
                                    " " + localGantryAndTable.d6 +
                                    " " + localGantryAndTable.d7 +
                                    " " + localGantryAndTable.d8 +
                                    " " + localGantryAndTable.d9 +
                                    " " + localGantryAndTable.d10 +
                                    " " + localGantryAndTable.d11
                    );
                Console.ReadLine();
            }
                
            Console.WriteLine("Adam code end.");
            #endregion
                // ==============================================
                #region
                //start read.
            BinaryReader fileBinaryReader = new BinaryReader(new FileStream(filePath + fileName, FileMode.Open));
            int flag = fileBinaryReader.ReadInt32();
            int seqHeadSize = fileBinaryReader.ReadInt32();
            int frameInfoSize = fileBinaryReader.ReadInt32();
            int imageWidth = fileBinaryReader.ReadInt32();
            int imageHeight = fileBinaryReader.ReadInt32();
            int frameCount = fileBinaryReader.ReadInt32();
            int detectorCount = fileBinaryReader.ReadInt32();

            //Seq info test lines:
            Console.WriteLine("Seq Info: " + flag + " " + " " + seqHeadSize + " " + frameInfoSize + " " + imageWidth + " " + imageHeight + " " + frameCount + " " + detectorCount );
            Console.ReadLine();

            //Read all Frame info

            for (int i = 0; i <= 10; i++)
            {
                int frameid = fileBinaryReader.ReadInt32();
                int acqID = fileBinaryReader.ReadInt32();
                int detectorFrameID = fileBinaryReader.ReadInt32();

                int l1Angle = fileBinaryReader.ReadInt32();
                int l2Angle = fileBinaryReader.ReadInt32();
                int pAngle = fileBinaryReader.ReadInt32();
                int cAngle = fileBinaryReader.ReadInt32();
                int SID = fileBinaryReader.ReadInt32();
                int DR = fileBinaryReader.ReadInt32();
                int CR = fileBinaryReader.ReadInt32();
                int TX = fileBinaryReader.ReadInt32();
                int TY = fileBinaryReader.ReadInt32();
                int TZ = fileBinaryReader.ReadInt32();
                int TR = fileBinaryReader.ReadInt32();

                Console.WriteLine("Frame info: " + frameid + " " + acqID + " " + detectorFrameID
                    + " "
                    + l1Angle + " " + l2Angle + " " + pAngle + " " + cAngle + " " + SID + " "
                    + DR + " " + CR + " " + TX + " " + TY + " " + TZ + " " + TR);
                Console.WriteLine("Test Frame: " + i);
                Console.ReadLine();
            }
                #endregion
        }
    }
}
