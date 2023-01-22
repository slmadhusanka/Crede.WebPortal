using System;

namespace Portal.Model
{
    public class TempObsData
    {
        public Int32 TempObsId
        { get; set; }

        public Int32 Id
        { get; set; }

        public Int32 FK_TempHCPId
        { get; set; }

        public string Moment1
        { get; set; }

        public string Moment2
        { get; set; }

        public string Moment3
        { get; set; }

        public string Moment4
        { get; set; }

        public string Moment5
        { get; set; }

        public string Result1
        { get; set; }

        public string Result2
        { get; set; }

        public string Result3
        { get; set; }

        public string Result4
        { get; set; }

        public int? Result1Time
        { get; set; }

        public int? Result2Time
        { get; set; }

        public string Guideline1
        { get; set; }

        public string Guideline2
        { get; set; }

        public string Guideline3
        { get; set; }

        public string Guideline4
        { get; set; }

        public string Guideline5
        { get; set; }

        public Int32 ObsNo
        { get; set; }

        public string NoteCode
        { get; set; }

        public string NoteFreeText
        { get; set; }

        public string PPE1
        { get; set; }

        public string PPE2
        { get; set; }

        public string PPE3
        { get; set; }

        public string PPE4
        { get; set; }

        public string PPE5
        { get; set; }

        public string Precautions1
        { get; set; }

        public string Precautions2
        { get; set; }

        public string Precautions3
        { get; set; }

        public string Precautions4
        { get; set; }
        public string EQP1
        { get; set; }

        public string EQP2
        { get; set; }

        public string EQP3
        { get; set; }

        public string EQP4
        { get; set; }

        public string EQP5
        { get; set; }

        public bool IsHH { get; set; }

        public bool IsPPE { get; set; }
    }
}
