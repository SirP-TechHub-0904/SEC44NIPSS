﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEC44NIPSS.Data.Model
{
    public class Alumni
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string DateRange { get; set; }
        public string Year { get; set; }
        public string GeneralTopic { get; set; }
        public bool Active { get; set; }
        public string Color { get; set; }
        public int SortOrder { get; set; }

        public ICollection<SecParticipant> Participants { get; set; }
        public ICollection<Executive> Executives { get; set; }
        public ICollection<ManagingStaff> ManagingStaffs { get; set; }
        public ICollection<DirectingStaff> DirectingStaffs { get; set; }
        public virtual SecProject SecProject { get; set; }
        public ICollection<StudyGroup> StudyGroup { get; set; }
        public ICollection<CommitteeCategory> CommitteeCategory { get; set; }
        public ICollection<SecPaper> SecPapers { get; set; }
        
        public ICollection<Tour> Tours { get; set; }
        public ICollection<TourCategory> TourCategories { get; set; }
        public ICollection<Profile> Profiles { get; set; }

    }
}
