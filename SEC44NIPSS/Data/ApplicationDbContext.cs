using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEC44NIPSS.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEC44NIPSS.Data
{
    public class NIPSSDbContext : IdentityDbContext
    {
        public NIPSSDbContext(DbContextOptions<NIPSSDbContext> options)
            : base(options)
        {
        }

      
        public DbSet<TicketStage> TicketStages { get; set; }
        public DbSet<QuestionnerPage> QuestionnerPages { get; set; }
        public DbSet<SliderCategory> SliderCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<LocalGoverment> LocalGoverments { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CurrentAffair> CurrentAffairs { get; set; }
        public DbSet<Executive> Executive { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<StudyGroup> StudyGroups { get; set; }
        public DbSet<StudyGroupMemeber> StudyGroupMemebers { get; set; }
        public DbSet<TourCategory> TourCategories { get; set; }
        public DbSet<TourSubCategory> TourSubCategories { get; set; }
        public DbSet<TourPost> TourPosts { get; set; }
        public DbSet<TourPostType> TourPostTypes { get; set; }
        public DbSet<ReportAbuse> ReportAbuses { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<Questionner> Questionners { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<ActivityPlanner> ActivityPlanners { get; set; }
        public DbSet<LegacyProject> LegacyProjects { get; set; }
        public DbSet<LegacyProjectAnswer> LegacyProjectAnswers { get; set; }
        public DbSet<Alumni> Alumnis { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentCategory> DocumentCategories { get; set; }
        public DbSet<SecProject> secProjects { get; set; }
        public DbSet<SecProjectExecutive> SecProjectExecutives { get; set; }
        public DbSet<NipssStaff> NipssStaff { get; set; }
        public DbSet<Committee> Committees { get; set; }
        public DbSet<CommitteeCategory> CommitteeCategories { get; set; }
        public DbSet<RapidQuestion> RapidQuestions { get; set; }
        public DbSet<RapidOption> RapidOption { get; set; }
        public DbSet<RapidTest> RapidTest { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<AnswerList> AnswerLists { get; set; }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserToNotify> UserToNotifys { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketResponse> TicketResponses { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
        public DbSet<TicketRequirement> TicketRequirements { get; set; }
        public DbSet<TicketStaff> TicketStaff { get; set; }
        public DbSet<TicketSupervisor> TicketSupervisor { get; set; }
        public DbSet<MaintainaceSetting> MaintainaceSettings { get; set; }



    }
}
