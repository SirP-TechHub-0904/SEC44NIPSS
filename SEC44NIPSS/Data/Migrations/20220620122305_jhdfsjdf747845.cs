using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SEC44NIPSS.Data.Migrations
{
    public partial class jhdfsjdf747845 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "Alumnis",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    DateRange = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    GeneralTopic = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alumnis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Replied = table.Column<bool>(nullable: false),
                    Replie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAffairs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    FullContent = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAffairs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderEmail = table.Column<string>(nullable: true),
                    PX = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    DateStart = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    EventDate = table.Column<string>(nullable: true),
                    EventTime = table.Column<string>(nullable: true),
                    SortOrder = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Moderator = table.Column<string>(nullable: true),
                    Lecturer = table.Column<string>(nullable: true),
                    EventType = table.Column<int>(nullable: false),
                    ContentStatus = table.Column<int>(nullable: false),
                    ContentType = table.Column<int>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    IsLecture = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegacyProjectAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    VotingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegacyProjectAnswers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LegacyProjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    P1 = table.Column<string>(nullable: true),
                    P2 = table.Column<string>(nullable: true),
                    P3 = table.Column<string>(nullable: true),
                    P4 = table.Column<string>(nullable: true),
                    P5 = table.Column<string>(nullable: true),
                    P6 = table.Column<string>(nullable: true),
                    P7 = table.Column<string>(nullable: true),
                    P8 = table.Column<string>(nullable: true),
                    P9 = table.Column<string>(nullable: true),
                    P10 = table.Column<string>(nullable: true),
                    VotingType = table.Column<int>(nullable: false),
                    C1 = table.Column<string>(nullable: true),
                    C2 = table.Column<string>(nullable: true),
                    C3 = table.Column<string>(nullable: true),
                    C4 = table.Column<string>(nullable: true),
                    D1 = table.Column<string>(nullable: true),
                    D2 = table.Column<string>(nullable: true),
                    D3 = table.Column<string>(nullable: true),
                    D4 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegacyProjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recipient = table.Column<string>(nullable: true),
                    Mail = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Retries = table.Column<int>(nullable: false),
                    NotificationStatus = table.Column<int>(nullable: false),
                    NotificationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RapidQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapidQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RapidTest",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Instruction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapidTest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportAbuses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnerId = table.Column<long>(nullable: false),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAbuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(nullable: true),
                    SliderType = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourPostTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPostTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommitteeCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommitteeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommitteeCategories_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentCategories_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    DOB = table.Column<string>(nullable: true),
                    AccountRole = table.Column<string>(nullable: true),
                    OfficialRole = table.Column<string>(nullable: true),
                    Sponsor = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AltPhoneNumber = table.Column<string>(nullable: true),
                    PsNumber = table.Column<string>(nullable: true),
                    PsChange = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    ChangeDate = table.Column<string>(nullable: true),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    PXI = table.Column<string>(nullable: true),
                    ParticipanPicture = table.Column<string>(nullable: true),
                    ProfilePhoto = table.Column<string>(nullable: true),
                    ProfileUpdateLevel = table.Column<int>(nullable: false),
                    ParticipantPhoto = table.Column<string>(nullable: true),
                    StudyGroupRole = table.Column<string>(nullable: true),
                    ResidenceAddress = table.Column<string>(nullable: true),
                    StateOfOrigin = table.Column<string>(nullable: true),
                    LGA = table.Column<string>(nullable: true),
                    OfficeAddress = table.Column<string>(nullable: true),
                    ShortProfile = table.Column<string>(nullable: true),
                    ProfileUpdateFirstTime = table.Column<bool>(nullable: false),
                    ProfileUpdatePictureFirstTime = table.Column<bool>(nullable: false),
                    DontShow = table.Column<bool>(nullable: false),
                    AboutProfile = table.Column<string>(nullable: true),
                    Sent = table.Column<bool>(nullable: false),
                    IsParticipant = table.Column<bool>(nullable: false),
                    Roles = table.Column<string>(nullable: true),
                    ProfileHandler = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Profiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "secProjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_secProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_secProjects_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudyGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AKA = table.Column<string>(nullable: true),
                    SortNumber = table.Column<int>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    DirectingStaffOne = table.Column<string>(nullable: true),
                    DirectingStaffTwo = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroups_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TourCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    SubTitle = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourCategories_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    CommentBy = table.Column<string>(nullable: true),
                    NewsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_News_NewsId",
                        column: x => x.NewsId,
                        principalTable: "News",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RapidOption",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Option1 = table.Column<string>(nullable: true),
                    Option2 = table.Column<string>(nullable: true),
                    Option3 = table.Column<string>(nullable: true),
                    Option4 = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    RapidQuestionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RapidOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RapidOption_RapidQuestions_RapidQuestionId",
                        column: x => x.RapidQuestionId,
                        principalTable: "RapidQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalGoverments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LGAName = table.Column<string>(nullable: true),
                    StatesId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalGoverments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalGoverments_States_StatesId",
                        column: x => x.StatesId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityPlanners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    PlannerType = table.Column<int>(nullable: false),
                    RecurrenceType = table.Column<int>(nullable: false),
                    RecurrentNumber = table.Column<int>(nullable: false),
                    ReminderTime = table.Column<int>(nullable: false),
                    MessageId = table.Column<long>(nullable: true),
                    MessageId1 = table.Column<int>(nullable: true),
                    ProfileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityPlanners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityPlanners_Messages_MessageId1",
                        column: x => x.MessageId1,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityPlanners_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Committees",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    CommitteeCategoryId = table.Column<long>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Committees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Committees_CommitteeCategories_CommitteeCategoryId",
                        column: x => x.CommitteeCategoryId,
                        principalTable: "CommitteeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Committees_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DocumentCategoryId = table.Column<long>(nullable: false),
                    EventId = table.Column<long>(nullable: true),
                    ProfileId = table.Column<long>(nullable: false),
                    CoverImage = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Uploaded = table.Column<bool>(nullable: false),
                    DocType = table.Column<int>(nullable: false),
                    DontShow = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_DocumentCategories_DocumentCategoryId",
                        column: x => x.DocumentCategoryId,
                        principalTable: "DocumentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documents_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Executive",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    Manifesto = table.Column<string>(nullable: true),
                    AlumniId = table.Column<long>(nullable: true),
                    ProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Executive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Executive_Alumnis_AlumniId",
                        column: x => x.AlumniId,
                        principalTable: "Alumnis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Executive_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    DontShow = table.Column<bool>(nullable: false),
                    Private = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    UseAsActivity = table.Column<bool>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true),
                    Tag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Galleries_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NipssStaff",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Position = table.Column<string>(nullable: true),
                    SortOrder = table.Column<string>(nullable: true),
                    Manifesto = table.Column<string>(nullable: true),
                    IsExecutive = table.Column<bool>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NipssStaff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NipssStaff_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questionners",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Response = table.Column<string>(nullable: true),
                    Instruction = table.Column<string>(nullable: true),
                    ShortLink = table.Column<string>(nullable: true),
                    LongLink = table.Column<string>(nullable: true),
                    PreviewImage = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true),
                    Email = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    SendRespondantEmailAfterAttempt = table.Column<bool>(nullable: false),
                    SendResponse = table.Column<bool>(nullable: false),
                    AddTimeFrame = table.Column<bool>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    CloseTime = table.Column<DateTime>(nullable: false),
                    Closed = table.Column<bool>(nullable: false),
                    ShowReSubmitBotton = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionners_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Score = table.Column<string>(nullable: true),
                    QuestionNumber = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    ProfileId = table.Column<long>(nullable: true),
                    QuestionsLoaded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserToNotifys",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(nullable: true),
                    IsAndriod = table.Column<bool>(nullable: false),
                    TokenId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToNotifys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToNotifys_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecProjectExecutives",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    SecProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecProjectExecutives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecProjectExecutives_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecProjectExecutives_secProjects_SecProjectId",
                        column: x => x.SecProjectId,
                        principalTable: "secProjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudyGroupMemebers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProfileId = table.Column<long>(nullable: false),
                    SortNumber = table.Column<int>(nullable: false),
                    Position = table.Column<string>(nullable: true),
                    DS = table.Column<bool>(nullable: false),
                    StudyGroupId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyGroupMemebers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudyGroupMemebers_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudyGroupMemebers_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourSubCategories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    TourCategoryId = table.Column<long>(nullable: false),
                    StudyGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourSubCategories_StudyGroups_StudyGroupId",
                        column: x => x.StudyGroupId,
                        principalTable: "StudyGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourSubCategories_TourCategories_TourCategoryId",
                        column: x => x.TourCategoryId,
                        principalTable: "TourCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    QuestionnerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questionners_QuestionnerId",
                        column: x => x.QuestionnerId,
                        principalTable: "Questionners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    Required = table.Column<string>(nullable: true),
                    QuestionnerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Questionners_QuestionnerId",
                        column: x => x.QuestionnerId,
                        principalTable: "Questionners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerLists",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RapidQuestionId = table.Column<long>(nullable: true),
                    Choose = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    SN = table.Column<int>(nullable: false),
                    UserAnswerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerLists_RapidQuestions_RapidQuestionId",
                        column: x => x.RapidQuestionId,
                        principalTable: "RapidQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnswerLists_UserAnswers_UserAnswerId",
                        column: x => x.UserAnswerId,
                        principalTable: "UserAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserToNotifyId = table.Column<long>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    DatetTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Read = table.Column<bool>(nullable: false),
                    Sent = table.Column<bool>(nullable: false),
                    Fullname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_UserToNotifys_UserToNotifyId",
                        column: x => x.UserToNotifyId,
                        principalTable: "UserToNotifys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    ShortDescription = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TourSubCategoryId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TourPostTypeId = table.Column<long>(nullable: false),
                    Photo = table.Column<string>(nullable: true),
                    PostFileType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPosts_TourPostTypes_TourPostTypeId",
                        column: x => x.TourPostTypeId,
                        principalTable: "TourPostTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TourPosts_TourSubCategories_TourSubCategoryId",
                        column: x => x.TourSubCategoryId,
                        principalTable: "TourSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionnerId = table.Column<long>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    AnswerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionAnswers_Questionners_QuestionnerId",
                        column: x => x.QuestionnerId,
                        principalTable: "Questionners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionType = table.Column<int>(nullable: false),
                    ShortNote = table.Column<string>(nullable: true),
                    ShortNoteMinimumLength = table.Column<int>(nullable: false),
                    ShortNoteMaximumLength = table.Column<int>(nullable: false),
                    LongNote = table.Column<string>(nullable: true),
                    LongNoteMinimumLength = table.Column<int>(nullable: false),
                    LongNoteMaximumLength = table.Column<int>(nullable: false),
                    Yes = table.Column<string>(nullable: true),
                    No = table.Column<string>(nullable: true),
                    OptionList1 = table.Column<string>(nullable: true),
                    OptionList2 = table.Column<string>(nullable: true),
                    OptionList3 = table.Column<string>(nullable: true),
                    OptionList4 = table.Column<string>(nullable: true),
                    OptionList5 = table.Column<string>(nullable: true),
                    MultipleOption1 = table.Column<string>(nullable: true),
                    MultipleOption2 = table.Column<string>(nullable: true),
                    MultipleOption3 = table.Column<string>(nullable: true),
                    MultipleOption4 = table.Column<string>(nullable: true),
                    MultipleOption5 = table.Column<string>(nullable: true),
                    MultipleOption6 = table.Column<string>(nullable: true),
                    MultipleOption7 = table.Column<string>(nullable: true),
                    QuestionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionResponses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionId = table.Column<long>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    QuestionAnswerId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_QuestionAnswers_QuestionAnswerId",
                        column: x => x.QuestionAnswerId,
                        principalTable: "QuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionResponses_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityPlanners_MessageId1",
                table: "ActivityPlanners",
                column: "MessageId1");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityPlanners_ProfileId",
                table: "ActivityPlanners",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLists_RapidQuestionId",
                table: "AnswerLists",
                column: "RapidQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerLists_UserAnswerId",
                table: "AnswerLists",
                column: "UserAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionnerId",
                table: "Answers",
                column: "QuestionnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_NewsId",
                table: "Comments",
                column: "NewsId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommitteeCategories_AlumniId",
                table: "CommitteeCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_CommitteeCategoryId",
                table: "Committees",
                column: "CommitteeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Committees_ProfileId",
                table: "Committees",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentCategories_AlumniId",
                table: "DocumentCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DocumentCategoryId",
                table: "Documents",
                column: "DocumentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_EventId",
                table: "Documents",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProfileId",
                table: "Documents",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Executive_AlumniId",
                table: "Executive",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Executive_ProfileId",
                table: "Executive",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Galleries_ProfileId",
                table: "Galleries",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalGoverments_StatesId",
                table: "LocalGoverments",
                column: "StatesId");

            migrationBuilder.CreateIndex(
                name: "IX_NipssStaff_ProfileId",
                table: "NipssStaff",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserToNotifyId",
                table: "Notifications",
                column: "UserToNotifyId");

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_AlumniId",
                table: "Profiles",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_AnswerId",
                table: "QuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionAnswers_QuestionnerId",
                table: "QuestionAnswers",
                column: "QuestionnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Questionners_ProfileId",
                table: "Questionners",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_QuestionAnswerId",
                table: "QuestionResponses",
                column: "QuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionResponses_QuestionId",
                table: "QuestionResponses",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionnerId",
                table: "Questions",
                column: "QuestionnerId");

            migrationBuilder.CreateIndex(
                name: "IX_RapidOption_RapidQuestionId",
                table: "RapidOption",
                column: "RapidQuestionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecProjectExecutives_ProfileId",
                table: "SecProjectExecutives",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SecProjectExecutives_SecProjectId",
                table: "SecProjectExecutives",
                column: "SecProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_secProjects_AlumniId",
                table: "secProjects",
                column: "AlumniId",
                unique: true,
                filter: "[AlumniId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_ProfileId",
                table: "StudyGroupMemebers",
                column: "ProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroupMemebers_StudyGroupId",
                table: "StudyGroupMemebers",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StudyGroups_AlumniId",
                table: "StudyGroups",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_TourCategories_AlumniId",
                table: "TourCategories",
                column: "AlumniId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPosts_TourPostTypeId",
                table: "TourPosts",
                column: "TourPostTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPosts_TourSubCategoryId",
                table: "TourPosts",
                column: "TourSubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TourSubCategories_StudyGroupId",
                table: "TourSubCategories",
                column: "StudyGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TourSubCategories_TourCategoryId",
                table: "TourSubCategories",
                column: "TourCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_ProfileId",
                table: "UserAnswers",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToNotifys_ProfileId",
                table: "UserToNotifys",
                column: "ProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityPlanners");

            migrationBuilder.DropTable(
                name: "AnswerLists");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Committees");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "CurrentAffairs");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "Executive");

            migrationBuilder.DropTable(
                name: "Galleries");

            migrationBuilder.DropTable(
                name: "LegacyProjectAnswers");

            migrationBuilder.DropTable(
                name: "LegacyProjects");

            migrationBuilder.DropTable(
                name: "LocalGoverments");

            migrationBuilder.DropTable(
                name: "NipssStaff");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "QuestionResponses");

            migrationBuilder.DropTable(
                name: "RapidOption");

            migrationBuilder.DropTable(
                name: "RapidTest");

            migrationBuilder.DropTable(
                name: "ReportAbuses");

            migrationBuilder.DropTable(
                name: "SecProjectExecutives");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "StudyGroupMemebers");

            migrationBuilder.DropTable(
                name: "TourPosts");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "CommitteeCategories");

            migrationBuilder.DropTable(
                name: "DocumentCategories");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "UserToNotifys");

            migrationBuilder.DropTable(
                name: "QuestionAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "RapidQuestions");

            migrationBuilder.DropTable(
                name: "secProjects");

            migrationBuilder.DropTable(
                name: "TourPostTypes");

            migrationBuilder.DropTable(
                name: "TourSubCategories");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "StudyGroups");

            migrationBuilder.DropTable(
                name: "TourCategories");

            migrationBuilder.DropTable(
                name: "Questionners");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Alumnis");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
