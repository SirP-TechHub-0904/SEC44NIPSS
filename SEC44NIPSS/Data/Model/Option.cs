using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Option
    {
        public long Id { get; set; }
        public OptionType OptionType { get; set; }
        public string ShortNote { get; set; }
        public int ShortNoteMinimumLength { get; set; }
        public int ShortNoteMaximumLength { get; set; }
        public string LongNote { get; set; }
        public int LongNoteMinimumLength { get; set; }
        public int LongNoteMaximumLength { get; set; }
        public string Yes { get; set; }
        public string No { get; set; }
        public string OptionList1 { get; set; }
        public string OptionList2 { get; set; }
        public string OptionList3 { get; set; }
        public string OptionList4 { get; set; }
        public string OptionList5 { get; set; }
        public string OptionList6 { get; set; }
        public string OptionList7 { get; set; }
        public string OptionList8 { get; set; }
        public string OptionList9 { get; set; }
        public string OptionList10 { get; set; }

        public string MultipleOption1 { get; set; }
        public string MultipleOption2 { get; set; }
        public string MultipleOption3 { get; set; }
        public string MultipleOption4 { get; set; }
        public string MultipleOption5 { get; set; }
        public string MultipleOption6 { get; set; }
        public string MultipleOption7 { get; set; }

        public string Table1Title { get; set; }
        public string Table1Option1 { get; set; }
        public string Table1Option2 { get; set; }
        public string Table1Option3 { get; set; }
        public string Table1Option4 { get; set; }
        public string Table1Option5 { get; set; }
        public string Table1Option6 { get; set; }
        public string Table1Option7 { get; set; }
        public string Table1Option8 { get; set; }
        public string Table1Option9 { get; set; }
        public string Table1Option10 { get; set; }

        public string Table2Title { get; set; }
        public string Table2Option1 { get; set; }
        public string Table2Option2 { get; set; }
        public string Table2Option3 { get; set; }
        public string Table2Option4 { get; set; }
        public string Table2Option5 { get; set; }
        public string Table2Option6 { get; set; }
        public string Table2Option7 { get; set; }
        public string Table2Option8 { get; set; }
        public string Table2Option9 { get; set; }
        public string Table2Option10 { get; set; }

        public string Table3Title { get; set; }
        public string Table3Option1 { get; set; }
        public string Table3Option2 { get; set; }
        public string Table3Option3 { get; set; }
        public string Table3Option4 { get; set; }
        public string Table3Option5 { get; set; }
        public string Table3Option6 { get; set; }
        public string Table3Option7 { get; set; }
        public string Table3Option8 { get; set; }
        public string Table3Option9 { get; set; }
        public string Table3Option10 { get; set; }

        public string Table4Title { get; set; }
        public string Table4Option1 { get; set; }
        public string Table4Option2 { get; set; }
        public string Table4Option3 { get; set; }
        public string Table4Option4 { get; set; }
        public string Table4Option5 { get; set; }
        public string Table4Option6 { get; set; }
        public string Table4Option7 { get; set; }
        public string Table4Option8 { get; set; }
        public string Table4Option9 { get; set; }
        public string Table4Option10 { get; set; }

        public string Table5Title { get; set; }
        public string Table5Option1 { get; set; }
        public string Table5Option2 { get; set; }
        public string Table5Option3 { get; set; }
        public string Table5Option4 { get; set; }
        public string Table5Option5 { get; set; }
        public string Table5Option6 { get; set; }
        public string Table5Option7 { get; set; }
        public string Table5Option8 { get; set; }
        public string Table5Option9 { get; set; }
        public string Table5Option10 { get; set; }

        public string Table6Title { get; set; }
        public string Table6Option1 { get; set; }
        public string Table6Option2 { get; set; }
        public string Table6Option3 { get; set; }
        public string Table6Option4 { get; set; }
        public string Table6Option5 { get; set; }
        public string Table6Option6 { get; set; }
        public string Table6Option7 { get; set; }
        public string Table6Option8 { get; set; }
        public string Table6Option9 { get; set; }
        public string Table6Option10 { get; set; }

        public string Table7Title { get; set; }
        public string Table7Option1 { get; set; }
        public string Table7Option2 { get; set; }
        public string Table7Option3 { get; set; }
        public string Table7Option4 { get; set; }
        public string Table7Option5 { get; set; }
        public string Table7Option6 { get; set; }
        public string Table7Option7 { get; set; }
        public string Table7Option8 { get; set; }
        public string Table7Option9 { get; set; }
        public string Table7Option10 { get; set; }

        public string Table8Title { get; set; }
        public string Table8Option1 { get; set; }
        public string Table8Option2 { get; set; }
        public string Table8Option3 { get; set; }
        public string Table8Option4 { get; set; }
        public string Table8Option5 { get; set; }
        public string Table8Option6 { get; set; }
        public string Table8Option7 { get; set; }
        public string Table8Option8 { get; set; }
        public string Table8Option9 { get; set; }
        public string Table8Option10 { get; set; }

        public string Table9Title { get; set; }
        public string Table9Option1 { get; set; }
        public string Table9Option2 { get; set; }
        public string Table9Option3 { get; set; }
        public string Table9Option4 { get; set; }
        public string Table9Option5 { get; set; }
        public string Table9Option6 { get; set; }
        public string Table9Option7 { get; set; }
        public string Table9Option8 { get; set; }
        public string Table9Option9 { get; set; }
        public string Table9Option10 { get; set; }

        public string Table10Title { get; set; }
        public string Table10Option1 { get; set; }
        public string Table10Option2 { get; set; }
        public string Table10Option3 { get; set; }
        public string Table10Option4 { get; set; }
        public string Table10Option5 { get; set; }
        public string Table10Option6 { get; set; }
        public string Table10Option7 { get; set; }
        public string Table10Option8 { get; set; }
        public string Table10Option9 { get; set; }
        public string Table10Option10 { get; set; }

        public long QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
