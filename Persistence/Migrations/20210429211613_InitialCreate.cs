﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    AcceptTerms = table.Column<bool>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    VerificationToken = table.Column<string>(nullable: true),
                    Verified = table.Column<DateTimeOffset>(nullable: true),
                    ResetToken = table.Column<string>(nullable: true),
                    ResetTokenExpires = table.Column<DateTimeOffset>(nullable: true),
                    PasswordReset = table.Column<DateTimeOffset>(nullable: true),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    Updated = table.Column<DateTimeOffset>(nullable: true),
                    Banned = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomAutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RowId = table.Column<string>(maxLength: 50, nullable: false),
                    TableName = table.Column<string>(maxLength: 128, nullable: false),
                    Changed = table.Column<string>(maxLength: 2048, nullable: true),
                    Kind = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    AccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAutoHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    PetType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Expires = table.Column<DateTimeOffset>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByIp = table.Column<string>(nullable: true),
                    Revoked = table.Column<DateTimeOffset>(nullable: true),
                    RevokedByIp = table.Column<string>(nullable: true),
                    ReplacedByToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PetType = table.Column<int>(nullable: false),
                    PetStatus = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    FromWhere = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1500, nullable: true),
                    RaceId = table.Column<int>(nullable: true),
                    CityId = table.Column<int>(nullable: false),
                    CreatedById = table.Column<int>(nullable: false),
                    Created = table.Column<DateTimeOffset>(nullable: false),
                    AdoptedById = table.Column<int>(nullable: true),
                    Published = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Accounts_AdoptedById",
                        column: x => x.AdoptedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pets_Accounts_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pets_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "Id", "Name", "PetType" },
                values: new object[,]
                {
                    { 1, "Abyssinian", 1 },
                    { 207, "Katalan Çoban Köpeği", 2 },
                    { 206, "Karst Çoban Köpeği", 2 },
                    { 205, "Kars Çoban Köpeği", 2 },
                    { 204, "Karelya Ayı Köpeği", 2 },
                    { 203, "Kangal", 2 },
                    { 202, "Japon Chin", 2 },
                    { 201, "Jack Russell Terrier", 2 },
                    { 200, "İzlanda Köpeği", 2 },
                    { 199, "İtalyan Tazısı", 2 },
                    { 198, "İsveç Geyik Avcısı", 2 },
                    { 197, "İsveç çoban köpeği", 2 },
                    { 196, "İspanyol Mastiff", 2 },
                    { 195, "İskoç Terrier", 2 },
                    { 194, "İskoç Geyik Tazısı", 2 },
                    { 193, "İrlandalı Terrier", 2 },
                    { 208, "Keeshond", 2 },
                    { 192, "İrlandalı Su Spanieli", 2 },
                    { 209, "Kerry Blue Terrier", 2 },
                    { 211, "Komondor", 2 },
                    { 226, "Minyatür Bull Terrier", 2 },
                    { 225, "Mastiff", 2 },
                    { 224, "Maremma Çoban Köpeği", 2 },
                    { 223, "Manchester Terrier", 2 },
                    { 222, "Maltese", 2 },
                    { 221, "Lowchen", 2 },
                    { 220, "Lhasa Apso", 2 },
                    { 219, "Leonberger", 2 },
                    { 218, "Lapponian Çoban Köpeği", 2 },
                    { 217, "Lapphund", 2 },
                    { 216, "Landseer", 2 },
                    { 215, "Lakeland Terrier", 2 },
                    { 214, "Labrador Retriever", 2 },
                    { 213, "Kyüshü", 2 },
                    { 212, "Kuvasz", 2 },
                    { 210, "King Charles Spaniel", 2 },
                    { 227, "Minyatür Pinscher", 2 },
                    { 191, "İrlandalı Setter", 2 },
                    { 189, "İngiliz Tilki Tazısı", 2 },
                    { 169, "Glen of Imaal Terrier", 2 },
                    { 168, "Fransız Mastiff", 2 },
                    { 167, "Fransız Bulldog", 2 },
                    { 166, "Fox Terrier (Wire)", 2 },
                    { 165, "Fox Terrier (Smooth)", 2 },
                    { 164, "Flat Coated Retriever", 2 },
                    { 163, "Finnish Spitz", 2 },
                    { 162, "Fin Tazısı", 2 },
                    { 161, "Field Spaniel", 2 },
                    { 160, "Eskimo Köpeği", 2 },
                    { 159, "Entlebucher", 2 },
                    { 158, "Dogo Arjantin", 2 },
                    { 157, "Doberman Pinscher", 2 },
                    { 156, "Dev Schnauzer", 2 },
                    { 155, "Dandie Dinmont Terrier", 2 },
                    { 170, "Golden Retriever", 2 },
                    { 190, "İrlandalı Kurt Tazısı", 2 },
                    { 171, "Gordon Setter", 2 },
                    { 173, "Grand Gascon Saintongeois", 2 },
                    { 188, "İngiliz Springer Spaniel", 2 },
                    { 187, "İngiliz Setter", 2 },
                    { 186, "İngiliz Cocker Spaniel", 2 },
                    { 185, "İngiliz Bulldog", 2 },
                    { 184, "İlirya çoban Köpeği", 2 },
                    { 183, "Ibizan Hound", 2 },
                    { 182, "Hovawart", 2 },
                    { 181, "Hollanda Çoban Köpeği", 2 },
                    { 180, "Hırvat Çoban Köpeği", 2 },
                    { 179, "Harrier", 2 },
                    { 178, "Hanover Tazısı", 2 },
                    { 177, "Grönland Köpeği", 2 },
                    { 176, "Greyhound", 2 },
                    { 175, "Great Phyrenees", 2 },
                    { 174, "Great Dane (Danua)", 2 },
                    { 172, "Grand Bleu de Gascogne", 2 },
                    { 228, "Minyatür Schnauzer", 2 },
                    { 229, "Mudi", 2 },
                    { 230, "Napoliten Mastiff", 2 },
                    { 284, "Steinbracke", 2 },
                    { 283, "Standart Schnauzer", 2 },
                    { 282, "Staffordshire Bull Terrier", 2 },
                    { 281, "St. Bernard (Saint Bernard)", 2 },
                    { 280, "Sokö (Sokak Köpeği)", 2 },
                    { 279, "Soft Coated Wheaten Terrier", 2 },
                    { 278, "Slovak Tchouvatch", 2 },
                    { 277, "Skye Terrier", 2 },
                    { 276, "Siyah ve Açık Kahverengi Rakun Tazısı", 2 },
                    { 275, "Silky Terrier", 2 },
                    { 274, "Sibirya Kurdu (Husky)", 2 },
                    { 273, "Shih Tzu", 2 },
                    { 272, "Shiba Inu", 2 },
                    { 271, "Shetland Çoban Köpeği", 2 },
                    { 270, "Sealyham Terrier", 2 },
                    { 285, "Styrian Dağ Tazısı", 2 },
                    { 269, "Schipperkee", 2 },
                    { 286, "Sussex Spanieli", 2 },
                    { 288, "Tibet Terrieri", 2 },
                    { 303, "Whippet", 2 },
                    { 302, "Westphalia Basseti", 2 },
                    { 301, "West Highland White Terrier", 2 },
                    { 300, "Welsh Terrier", 2 },
                    { 299, "Welsh Springer Spaniel", 2 },
                    { 298, "Weimaraner", 2 },
                    { 297, "Vizsla", 2 },
                    { 296, "Valee Çoban Köpeği", 2 },
                    { 295, "Tyrolean Tazısı", 2 },
                    { 294, "Tüysüz Collie", 2 },
                    { 293, "Türk Tazısı", 2 },
                    { 292, "Trigg Tazısı", 2 },
                    { 291, "Tosa", 2 },
                    { 290, "Tibetli Spaniel", 2 },
                    { 289, "Tibetli Mastiff", 2 },
                    { 287, "Tatra Çoban Köpeği", 2 },
                    { 268, "Sanshu", 2 },
                    { 267, "Samoyed", 2 },
                    { 266, "Saluki", 2 },
                    { 245, "Pharaoh Hound", 2 },
                    { 244, "Petit Bleu de Gascogne", 2 },
                    { 243, "Petit Basset Griffon Vendien", 2 },
                    { 242, "Peru Tüysüz Köpeği", 2 },
                    { 241, "Pembroke Welsh Corgi", 2 },
                    { 240, "Pekingese", 2 },
                    { 239, "Pappilon", 2 },
                    { 238, "Otterhound", 2 },
                    { 237, "Old English Sheepdog", 2 },
                    { 236, "Norwich Terrier", 2 },
                    { 235, "Norveç Geyik Avcısı", 2 },
                    { 234, "Norsk Buhund", 2 },
                    { 233, "Norrbottenspets", 2 },
                    { 232, "Norfolk Terrier", 2 },
                    { 231, "Newfoundland", 2 },
                    { 246, "Picardy Çoban Köpeği", 2 },
                    { 247, "Plott Tazısı", 2 },
                    { 248, "Pointer", 2 },
                    { 249, "Poitevin", 2 },
                    { 265, "Sakallı Collie", 2 },
                    { 264, "Russian Spaniel", 2 },
                    { 263, "Rottweiler", 2 },
                    { 262, "Rhodesian Ridgeback", 2 },
                    { 261, "Rafeiro do Alentejo", 2 },
                    { 260, "Pyrenees Mastiff", 2 },
                    { 259, "Pyrenees Çoban Köpeği", 2 },
                    { 154, "Dalmatian", 2 },
                    { 258, "Pumi", 2 },
                    { 256, "Pug", 2 },
                    { 255, "Presa Canario", 2 },
                    { 254, "Portekiz Su Köpeği", 2 },
                    { 253, "Poodle(Standart Kaniş)", 2 },
                    { 252, "Poodle (Minyatür Kaniş)", 2 },
                    { 251, "Pomeranyalı", 2 },
                    { 250, "Polonya Tazısı", 2 },
                    { 257, "Puli", 2 },
                    { 304, "Wirehaired Pointing Griffon", 2 },
                    { 153, "Dachshund (Sosis)", 2 },
                    { 151, "Çin Creste Köpeği", 2 },
                    { 54, "Sibirya Kedisi", 1 },
                    { 53, "Selkirk Rex", 1 },
                    { 52, "Scottish Fold", 1 },
                    { 51, "Savannah", 1 },
                    { 50, "Sarman", 1 },
                    { 49, "Ragdoll", 1 },
                    { 48, "Ragamuffin", 1 },
                    { 47, "Pixie Bob", 1 },
                    { 46, "Oriental Shorthair", 1 },
                    { 45, "Oriental Longhair", 1 },
                    { 44, "Ocicat", 1 },
                    { 43, "Norveç Ormanı", 1 },
                    { 42, "Nebelung", 1 },
                    { 41, "Munchkin", 1 },
                    { 40, "Mojave Spotted (Mojave çöl Kedisi)", 1 },
                    { 55, "Singapura", 1 },
                    { 39, "Mavi Rus", 1 },
                    { 56, "Siyam Kedisi", 1 },
                    { 58, "Soke", 1 },
                    { 73, "Akbaş", 2 },
                    { 72, "Airedale Terrier", 2 },
                    { 71, "Ainu", 2 },
                    { 70, "Aidi", 2 },
                    { 69, "Afgan Tazısı", 2 },
                    { 68, "Affenpinscher", 2 },
                    { 67, "York Chocolate", 1 },
                    { 66, "Van Kedisi", 1 },
                    { 65, "Tuxedo (Smokin) Kedi", 1 },
                    { 64, "Tonkinese", 1 },
                    { 63, "Tiffany/Chantilly", 1 },
                    { 62, "Tiffanie", 1 },
                    { 61, "Tekir Kedi", 1 },
                    { 60, "Sphynx", 1 },
                    { 59, "Somali", 1 },
                    { 57, "Snowshoe (Karayak)", 1 },
                    { 74, "Akita İnu", 2 },
                    { 38, "Manx", 1 },
                    { 36, "Laperm", 1 },
                    { 16, "Burmilla (Silver Burmese)", 1 },
                    { 15, "Burmese", 1 },
                    { 14, "British Shorthair", 1 },
                    { 13, "Brazilian Shorthair", 1 },
                    { 12, "Bombay", 1 },
                    { 11, "Birman", 1 },
                    { 10, "Bengal", 1 },
                    { 9, "Balinese", 1 },
                    { 8, "Australian Mist", 1 },
                    { 7, "Ankara Kedisi", 1 },
                    { 6, "American Wirehair", 1 },
                    { 5, "American Shorthair", 1 },
                    { 4, "American Keuda", 1 },
                    { 3, "American Curl", 1 },
                    { 2, "American Bobtail", 1 },
                    { 17, "Californian Spangled", 1 },
                    { 37, "Maine Coon", 1 },
                    { 18, "Chartreux", 1 },
                    { 20, "Colorpoint Shorthair", 1 },
                    { 35, "Korat", 1 },
                    { 34, "Kashmir ", 1 },
                    { 33, "Javanese", 1 },
                    { 32, "Japon Bobtail", 1 },
                    { 31, "İran Kedisi (Persian)", 1 },
                    { 30, "Honey Bear", 1 },
                    { 29, "Himalayan", 1 },
                    { 28, "Havana Brown", 1 },
                    { 27, "Exotic Shorthair", 1 },
                    { 26, "European Shorthair", 1 },
                    { 25, "European Burmese", 1 },
                    { 24, "Egyptian Maular", 1 },
                    { 23, "Devon Rex", 1 },
                    { 22, "Cymric", 1 },
                    { 21, "Cornish Rex", 1 },
                    { 19, "Chinchilla", 1 },
                    { 75, "Aksaray Malaklısı", 2 },
                    { 76, "Alabay (Alabai)", 2 },
                    { 77, "Alaskan Malamute", 2 },
                    { 131, "Büyük İsveç Dağ Köpeği", 2 },
                    { 130, "Bullmastiff", 2 },
                    { 129, "Bull Terrier", 2 },
                    { 128, "Brüksel Griffonu", 2 },
                    { 127, "Brittany", 2 },
                    { 126, "Briard", 2 },
                    { 125, "Brezilya Mastiff", 2 },
                    { 124, "Boxer", 2 },
                    { 123, "Bouvier des Flandres", 2 },
                    { 122, "Bouvier des Ardennes", 2 },
                    { 121, "Boston Terrier", 2 },
                    { 120, "Borzoi", 2 },
                    { 119, "Border Terrier", 2 },
                    { 118, "Border Collie", 2 },
                    { 117, "Bloodhound", 2 },
                    { 132, "Cairn Terrier", 2 },
                    { 116, "Billy", 2 },
                    { 133, "Canaan Köpeği", 2 },
                    { 135, "Cao da Serra da Estrela", 2 },
                    { 150, "Çatalburun", 2 },
                    { 149, "Curly Coated Retriever", 2 },
                    { 148, "Coton De Tulear", 2 },
                    { 147, "Collie", 2 },
                    { 146, "Clumber Spaniel", 2 },
                    { 145, "Chow Chow (çin Aslanı)", 2 },
                    { 144, "Chihuahua", 2 },
                    { 143, "Chiens Francaises", 2 },
                    { 142, "Chesapeake Bay Retriever", 2 },
                    { 141, "Cesky Terrier", 2 },
                    { 140, "Cavalier King Charles Spanieli", 2 },
                    { 139, "Catahoula Leopar Köpeği", 2 },
                    { 138, "Cardigan Welsh Corgi", 2 },
                    { 137, "Cao de Serra de Aires", 2 },
                    { 136, "Cao de Castro Laboreiro", 2 },
                    { 134, "Cane Corso Italiano", 2 },
                    { 115, "Bichon Havanese", 2 },
                    { 114, "Bichon Frise", 2 },
                    { 113, "Bernese Dağ Köpeği", 2 },
                    { 92, "Amerikan Yerli Köpeği", 2 },
                    { 91, "Amerikan Tüysüz Terrieri", 2 },
                    { 90, "Amerikan Tilki Tazısı", 2 },
                    { 89, "Amerikan Su Spanieli", 2 },
                    { 88, "Amerikan Staffordshire Terrier", 2 },
                    { 87, "Amerikan Pitbull Terrier", 2 },
                    { 86, "Amerikan Eskimo", 2 },
                    { 85, "Amerikan Cocker Spaniel", 2 },
                    { 84, "Amerikan Bulldog", 2 },
                    { 83, "Alpine Dachsbracke", 2 },
                    { 82, "Alman Spanieli", 2 },
                    { 81, "Alman Kısa Tüylü Pointer", 2 },
                    { 80, "Alman Kalın Tüylü Pointer", 2 },
                    { 79, "Alman Çoban Köpeği", 2 },
                    { 78, "Alman Av Terrieri", 2 },
                    { 93, "Appenzell Dağ Köpeği", 2 },
                    { 94, "Ariegeois", 2 },
                    { 95, "Avustralya Çoban Köpeği", 2 },
                    { 96, "Avustralya Sığır Köpeği", 2 },
                    { 112, "Bergamasco", 2 },
                    { 111, "Belçika Tervuren", 2 },
                    { 110, "Belçika Malinois", 2 },
                    { 109, "Belçika Laekenois", 2 },
                    { 108, "Belçika Groenendael", 2 },
                    { 107, "Bedlington Terrier", 2 },
                    { 106, "Beauceron", 2 },
                    { 152, "Çin Shar Pei", 2 },
                    { 105, "Beagle", 2 },
                    { 103, "Basset Hound", 2 },
                    { 102, "Basenji", 2 },
                    { 101, "Bandogge Mastiff", 2 },
                    { 100, "Avusturyalı Pinscher", 2 },
                    { 99, "Avusturya Tazısı", 2 },
                    { 98, "Avustralyalı Kelpie", 2 },
                    { 97, "Avustralya Terrier", 2 },
                    { 104, "Bavyera Dağ Tazısı", 2 },
                    { 305, "Yorkshire Terrier", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AdoptedById",
                table: "Pets",
                column: "AdoptedById");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CreatedById",
                table: "Pets",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_RaceId",
                table: "Pets",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomAutoHistory");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
