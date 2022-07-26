using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Questionner
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public string Instruction { get; set; }
        public string ShortLink { get; set; }
        public string LongLink { get; set; }
        public string PreviewImage { get; set; }
        public string SubTitle { get; set; }
        public string Logo { get; set; }
        public DateTime Date { get; set; }

        public long? ProfileId { get; set; }
        public Profile Profile { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<QuestionnerPage> QuestionnerPages { get; set; }

        public EmailPhoneStatus Email { get; set; }
        public EmailPhoneStatus PhoneNumber { get; set; }

        public bool SendRespondantEmailAfterAttempt { get; set; }
        public bool SendResponse { get; set; }
        public bool AddTimeFrame { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CloseTime { get; set; }
        public bool Closed { get; set; }

        public bool ShowReSubmitBotton { get; set; }
        public int TotalPage { get; set; }

    }
}
