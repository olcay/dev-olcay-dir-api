using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;
using WebApi.Enums;

namespace WebApi.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageBox> MessageBoxes { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.EnableAutoHistory<CustomAutoHistory>(o => { });

            var allRaces = new List<Race> {
                new Race { Id = 1, Name = "Abyssinian", PetType = PetType.Cat },
                new Race { Id = 2, Name = "American Bobtail", PetType = PetType.Cat },
                new Race { Id = 3, Name = "American Curl", PetType = PetType.Cat },
                new Race { Id = 4, Name = "American Keuda", PetType = PetType.Cat },
                new Race { Id = 5, Name = "American Shorthair", PetType = PetType.Cat },
                new Race { Id = 6, Name = "American Wirehair", PetType = PetType.Cat },
                new Race { Id = 7, Name = "Ankara Kedisi", PetType = PetType.Cat },
                new Race { Id = 8, Name = "Australian Mist", PetType = PetType.Cat },
                new Race { Id = 9, Name = "Balinese", PetType = PetType.Cat },
                new Race { Id = 10, Name = "Bengal", PetType = PetType.Cat },
                new Race { Id = 11, Name = "Birman", PetType = PetType.Cat },
                new Race { Id = 12, Name = "Bombay", PetType = PetType.Cat },
                new Race { Id = 13, Name = "Brazilian Shorthair", PetType = PetType.Cat },
                new Race { Id = 14, Name = "British Shorthair", PetType = PetType.Cat },
                new Race { Id = 15, Name = "Burmese", PetType = PetType.Cat },
                new Race { Id = 16, Name = "Burmilla (Silver Burmese)", PetType = PetType.Cat },
                new Race { Id = 17, Name = "Californian Spangled", PetType = PetType.Cat },
                new Race { Id = 18, Name = "Chartreux", PetType = PetType.Cat },
                new Race { Id = 19, Name = "Chinchilla", PetType = PetType.Cat },
                new Race { Id = 20, Name = "Colorpoint Shorthair", PetType = PetType.Cat },
                new Race { Id = 21, Name = "Cornish Rex", PetType = PetType.Cat },
                new Race { Id = 22, Name = "Cymric", PetType = PetType.Cat },
                new Race { Id = 23, Name = "Devon Rex", PetType = PetType.Cat },
                new Race { Id = 24, Name = "Egyptian Maular", PetType = PetType.Cat },
                new Race { Id = 25, Name = "European Burmese", PetType = PetType.Cat },
                new Race { Id = 26, Name = "European Shorthair", PetType = PetType.Cat },
                new Race { Id = 27, Name = "Exotic Shorthair", PetType = PetType.Cat },
                new Race { Id = 28, Name = "Havana Brown", PetType = PetType.Cat },
                new Race { Id = 29, Name = "Himalayan", PetType = PetType.Cat },
                new Race { Id = 30, Name = "Honey Bear", PetType = PetType.Cat },
                new Race { Id = 31, Name = "İran Kedisi (Persian)", PetType = PetType.Cat },
                new Race { Id = 32, Name = "Japon Bobtail", PetType = PetType.Cat },
                new Race { Id = 33, Name = "Javanese", PetType = PetType.Cat },
                new Race { Id = 34, Name = "Kashmir ", PetType = PetType.Cat },
                new Race { Id = 35, Name = "Korat", PetType = PetType.Cat },
                new Race { Id = 36, Name = "Laperm", PetType = PetType.Cat },
                new Race { Id = 37, Name = "Maine Coon", PetType = PetType.Cat },
                new Race { Id = 38, Name = "Manx", PetType = PetType.Cat },
                new Race { Id = 39, Name = "Mavi Rus", PetType = PetType.Cat },
                new Race { Id = 40, Name = "Mojave Spotted (Mojave çöl Kedisi)", PetType = PetType.Cat },
                new Race { Id = 41, Name = "Munchkin", PetType = PetType.Cat },
                new Race { Id = 42, Name = "Nebelung", PetType = PetType.Cat },
                new Race { Id = 43, Name = "Norveç Ormanı", PetType = PetType.Cat },
                new Race { Id = 44, Name = "Ocicat", PetType = PetType.Cat },
                new Race { Id = 45, Name = "Oriental Longhair", PetType = PetType.Cat },
                new Race { Id = 46, Name = "Oriental Shorthair", PetType = PetType.Cat },
                new Race { Id = 47, Name = "Pixie Bob", PetType = PetType.Cat },
                new Race { Id = 48, Name = "Ragamuffin", PetType = PetType.Cat },
                new Race { Id = 49, Name = "Ragdoll", PetType = PetType.Cat },
                new Race { Id = 50, Name = "Sarman", PetType = PetType.Cat },
                new Race { Id = 51, Name = "Savannah", PetType = PetType.Cat },
                new Race { Id = 52, Name = "Scottish Fold", PetType = PetType.Cat },
                new Race { Id = 53, Name = "Selkirk Rex", PetType = PetType.Cat },
                new Race { Id = 54, Name = "Sibirya Kedisi", PetType = PetType.Cat },
                new Race { Id = 55, Name = "Singapura", PetType = PetType.Cat },
                new Race { Id = 56, Name = "Siyam Kedisi", PetType = PetType.Cat },
                new Race { Id = 57, Name = "Snowshoe (Karayak)", PetType = PetType.Cat },
                new Race { Id = 58, Name = "Soke", PetType = PetType.Cat },
                new Race { Id = 59, Name = "Somali", PetType = PetType.Cat },
                new Race { Id = 60, Name = "Sphynx", PetType = PetType.Cat },
                new Race { Id = 61, Name = "Tekir Kedi", PetType = PetType.Cat },
                new Race { Id = 62, Name = "Tiffanie", PetType = PetType.Cat },
                new Race { Id = 63, Name = "Tiffany/Chantilly", PetType = PetType.Cat },
                new Race { Id = 64, Name = "Tonkinese", PetType = PetType.Cat },
                new Race { Id = 65, Name = "Tuxedo (Smokin) Kedi", PetType = PetType.Cat },
                new Race { Id = 66, Name = "Van Kedisi", PetType = PetType.Cat },
                new Race { Id = 67, Name = "York Chocolate", PetType = PetType.Cat },
                new Race { Id = 68, Name = "Affenpinscher", PetType = PetType.Dog },
                new Race { Id = 69, Name = "Afgan Tazısı", PetType = PetType.Dog },
                new Race { Id = 70, Name = "Aidi", PetType = PetType.Dog },
                new Race { Id = 71, Name = "Ainu", PetType = PetType.Dog },
                new Race { Id = 72, Name = "Airedale Terrier", PetType = PetType.Dog },
                new Race { Id = 73, Name = "Akbaş", PetType = PetType.Dog },
                new Race { Id = 74, Name = "Akita İnu", PetType = PetType.Dog },
                new Race { Id = 75, Name = "Aksaray Malaklısı", PetType = PetType.Dog },
                new Race { Id = 76, Name = "Alabay (Alabai)", PetType = PetType.Dog },
                new Race { Id = 77, Name = "Alaskan Malamute", PetType = PetType.Dog },
                new Race { Id = 78, Name = "Alman Av Terrieri", PetType = PetType.Dog },
                new Race { Id = 79, Name = "Alman Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 80, Name = "Alman Kalın Tüylü Pointer", PetType = PetType.Dog },
                new Race { Id = 81, Name = "Alman Kısa Tüylü Pointer", PetType = PetType.Dog },
                new Race { Id = 82, Name = "Alman Spanieli", PetType = PetType.Dog },
                new Race { Id = 83, Name = "Alpine Dachsbracke", PetType = PetType.Dog },
                new Race { Id = 84, Name = "Amerikan Bulldog", PetType = PetType.Dog },
                new Race { Id = 85, Name = "Amerikan Cocker Spaniel", PetType = PetType.Dog },
                new Race { Id = 86, Name = "Amerikan Eskimo", PetType = PetType.Dog },
                new Race { Id = 87, Name = "Amerikan Pitbull Terrier", PetType = PetType.Dog },
                new Race { Id = 88, Name = "Amerikan Staffordshire Terrier", PetType = PetType.Dog },
                new Race { Id = 89, Name = "Amerikan Su Spanieli", PetType = PetType.Dog },
                new Race { Id = 90, Name = "Amerikan Tilki Tazısı", PetType = PetType.Dog },
                new Race { Id = 91, Name = "Amerikan Tüysüz Terrieri", PetType = PetType.Dog },
                new Race { Id = 92, Name = "Amerikan Yerli Köpeği", PetType = PetType.Dog },
                new Race { Id = 93, Name = "Appenzell Dağ Köpeği", PetType = PetType.Dog },
                new Race { Id = 94, Name = "Ariegeois", PetType = PetType.Dog },
                new Race { Id = 95, Name = "Avustralya Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 96, Name = "Avustralya Sığır Köpeği", PetType = PetType.Dog },
                new Race { Id = 97, Name = "Avustralya Terrier", PetType = PetType.Dog },
                new Race { Id = 98, Name = "Avustralyalı Kelpie", PetType = PetType.Dog },
                new Race { Id = 99, Name = "Avusturya Tazısı", PetType = PetType.Dog },
                new Race { Id = 100, Name = "Avusturyalı Pinscher", PetType = PetType.Dog },
                new Race { Id = 101, Name = "Bandogge Mastiff", PetType = PetType.Dog },
                new Race { Id = 102, Name = "Basenji", PetType = PetType.Dog },
                new Race { Id = 103, Name = "Basset Hound", PetType = PetType.Dog },
                new Race { Id = 104, Name = "Bavyera Dağ Tazısı", PetType = PetType.Dog },
                new Race { Id = 105, Name = "Beagle", PetType = PetType.Dog },
                new Race { Id = 106, Name = "Beauceron", PetType = PetType.Dog },
                new Race { Id = 107, Name = "Bedlington Terrier", PetType = PetType.Dog },
                new Race { Id = 108, Name = "Belçika Groenendael", PetType = PetType.Dog },
                new Race { Id = 109, Name = "Belçika Laekenois", PetType = PetType.Dog },
                new Race { Id = 110, Name = "Belçika Malinois", PetType = PetType.Dog },
                new Race { Id = 111, Name = "Belçika Tervuren", PetType = PetType.Dog },
                new Race { Id = 112, Name = "Bergamasco", PetType = PetType.Dog },
                new Race { Id = 113, Name = "Bernese Dağ Köpeği", PetType = PetType.Dog },
                new Race { Id = 114, Name = "Bichon Frise", PetType = PetType.Dog },
                new Race { Id = 115, Name = "Bichon Havanese", PetType = PetType.Dog },
                new Race { Id = 116, Name = "Billy", PetType = PetType.Dog },
                new Race { Id = 117, Name = "Bloodhound", PetType = PetType.Dog },
                new Race { Id = 118, Name = "Border Collie", PetType = PetType.Dog },
                new Race { Id = 119, Name = "Border Terrier", PetType = PetType.Dog },
                new Race { Id = 120, Name = "Borzoi", PetType = PetType.Dog },
                new Race { Id = 121, Name = "Boston Terrier", PetType = PetType.Dog },
                new Race { Id = 122, Name = "Bouvier des Ardennes", PetType = PetType.Dog },
                new Race { Id = 123, Name = "Bouvier des Flandres", PetType = PetType.Dog },
                new Race { Id = 124, Name = "Boxer", PetType = PetType.Dog },
                new Race { Id = 125, Name = "Brezilya Mastiff", PetType = PetType.Dog },
                new Race { Id = 126, Name = "Briard", PetType = PetType.Dog },
                new Race { Id = 127, Name = "Brittany", PetType = PetType.Dog },
                new Race { Id = 128, Name = "Brüksel Griffonu", PetType = PetType.Dog },
                new Race { Id = 129, Name = "Bull Terrier", PetType = PetType.Dog },
                new Race { Id = 130, Name = "Bullmastiff", PetType = PetType.Dog },
                new Race { Id = 131, Name = "Büyük İsveç Dağ Köpeği", PetType = PetType.Dog },
                new Race { Id = 132, Name = "Cairn Terrier", PetType = PetType.Dog },
                new Race { Id = 133, Name = "Canaan Köpeği", PetType = PetType.Dog },
                new Race { Id = 134, Name = "Cane Corso Italiano", PetType = PetType.Dog },
                new Race { Id = 135, Name = "Cao da Serra da Estrela", PetType = PetType.Dog },
                new Race { Id = 136, Name = "Cao de Castro Laboreiro", PetType = PetType.Dog },
                new Race { Id = 137, Name = "Cao de Serra de Aires", PetType = PetType.Dog },
                new Race { Id = 138, Name = "Cardigan Welsh Corgi", PetType = PetType.Dog },
                new Race { Id = 139, Name = "Catahoula Leopar Köpeği", PetType = PetType.Dog },
                new Race { Id = 140, Name = "Cavalier King Charles Spanieli", PetType = PetType.Dog },
                new Race { Id = 141, Name = "Cesky Terrier", PetType = PetType.Dog },
                new Race { Id = 142, Name = "Chesapeake Bay Retriever", PetType = PetType.Dog },
                new Race { Id = 143, Name = "Chiens Francaises", PetType = PetType.Dog },
                new Race { Id = 144, Name = "Chihuahua", PetType = PetType.Dog },
                new Race { Id = 145, Name = "Chow Chow (çin Aslanı)", PetType = PetType.Dog },
                new Race { Id = 146, Name = "Clumber Spaniel", PetType = PetType.Dog },
                new Race { Id = 147, Name = "Collie", PetType = PetType.Dog },
                new Race { Id = 148, Name = "Coton De Tulear", PetType = PetType.Dog },
                new Race { Id = 149, Name = "Curly Coated Retriever", PetType = PetType.Dog },
                new Race { Id = 150, Name = "Çatalburun", PetType = PetType.Dog },
                new Race { Id = 151, Name = "Çin Creste Köpeği", PetType = PetType.Dog },
                new Race { Id = 152, Name = "Çin Shar Pei", PetType = PetType.Dog },
                new Race { Id = 153, Name = "Dachshund (Sosis)", PetType = PetType.Dog },
                new Race { Id = 154, Name = "Dalmatian", PetType = PetType.Dog },
                new Race { Id = 155, Name = "Dandie Dinmont Terrier", PetType = PetType.Dog },
                new Race { Id = 156, Name = "Dev Schnauzer", PetType = PetType.Dog },
                new Race { Id = 157, Name = "Doberman Pinscher", PetType = PetType.Dog },
                new Race { Id = 158, Name = "Dogo Arjantin", PetType = PetType.Dog },
                new Race { Id = 159, Name = "Entlebucher", PetType = PetType.Dog },
                new Race { Id = 160, Name = "Eskimo Köpeği", PetType = PetType.Dog },
                new Race { Id = 161, Name = "Field Spaniel", PetType = PetType.Dog },
                new Race { Id = 162, Name = "Fin Tazısı", PetType = PetType.Dog },
                new Race { Id = 163, Name = "Finnish Spitz", PetType = PetType.Dog },
                new Race { Id = 164, Name = "Flat Coated Retriever", PetType = PetType.Dog },
                new Race { Id = 165, Name = "Fox Terrier (Smooth)", PetType = PetType.Dog },
                new Race { Id = 166, Name = "Fox Terrier (Wire)", PetType = PetType.Dog },
                new Race { Id = 167, Name = "Fransız Bulldog", PetType = PetType.Dog },
                new Race { Id = 168, Name = "Fransız Mastiff", PetType = PetType.Dog },
                new Race { Id = 169, Name = "Glen of Imaal Terrier", PetType = PetType.Dog },
                new Race { Id = 170, Name = "Golden Retriever", PetType = PetType.Dog },
                new Race { Id = 171, Name = "Gordon Setter", PetType = PetType.Dog },
                new Race { Id = 172, Name = "Grand Bleu de Gascogne", PetType = PetType.Dog },
                new Race { Id = 173, Name = "Grand Gascon Saintongeois", PetType = PetType.Dog },
                new Race { Id = 174, Name = "Great Dane (Danua)", PetType = PetType.Dog },
                new Race { Id = 175, Name = "Great Phyrenees", PetType = PetType.Dog },
                new Race { Id = 176, Name = "Greyhound", PetType = PetType.Dog },
                new Race { Id = 177, Name = "Grönland Köpeği", PetType = PetType.Dog },
                new Race { Id = 178, Name = "Hanover Tazısı", PetType = PetType.Dog },
                new Race { Id = 179, Name = "Harrier", PetType = PetType.Dog },
                new Race { Id = 180, Name = "Hırvat Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 181, Name = "Hollanda Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 182, Name = "Hovawart", PetType = PetType.Dog },
                new Race { Id = 183, Name = "Ibizan Hound", PetType = PetType.Dog },
                new Race { Id = 184, Name = "İlirya çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 185, Name = "İngiliz Bulldog", PetType = PetType.Dog },
                new Race { Id = 186, Name = "İngiliz Cocker Spaniel", PetType = PetType.Dog },
                new Race { Id = 187, Name = "İngiliz Setter", PetType = PetType.Dog },
                new Race { Id = 188, Name = "İngiliz Springer Spaniel", PetType = PetType.Dog },
                new Race { Id = 189, Name = "İngiliz Tilki Tazısı", PetType = PetType.Dog },
                new Race { Id = 190, Name = "İrlandalı Kurt Tazısı", PetType = PetType.Dog },
                new Race { Id = 191, Name = "İrlandalı Setter", PetType = PetType.Dog },
                new Race { Id = 192, Name = "İrlandalı Su Spanieli", PetType = PetType.Dog },
                new Race { Id = 193, Name = "İrlandalı Terrier", PetType = PetType.Dog },
                new Race { Id = 194, Name = "İskoç Geyik Tazısı", PetType = PetType.Dog },
                new Race { Id = 195, Name = "İskoç Terrier", PetType = PetType.Dog },
                new Race { Id = 196, Name = "İspanyol Mastiff", PetType = PetType.Dog },
                new Race { Id = 197, Name = "İsveç çoban köpeği", PetType = PetType.Dog },
                new Race { Id = 198, Name = "İsveç Geyik Avcısı", PetType = PetType.Dog },
                new Race { Id = 199, Name = "İtalyan Tazısı", PetType = PetType.Dog },
                new Race { Id = 200, Name = "İzlanda Köpeği", PetType = PetType.Dog },
                new Race { Id = 201, Name = "Jack Russell Terrier", PetType = PetType.Dog },
                new Race { Id = 202, Name = "Japon Chin", PetType = PetType.Dog },
                new Race { Id = 203, Name = "Kangal", PetType = PetType.Dog },
                new Race { Id = 204, Name = "Karelya Ayı Köpeği", PetType = PetType.Dog },
                new Race { Id = 205, Name = "Kars Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 206, Name = "Karst Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 207, Name = "Katalan Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 208, Name = "Keeshond", PetType = PetType.Dog },
                new Race { Id = 209, Name = "Kerry Blue Terrier", PetType = PetType.Dog },
                new Race { Id = 210, Name = "King Charles Spaniel", PetType = PetType.Dog },
                new Race { Id = 211, Name = "Komondor", PetType = PetType.Dog },
                new Race { Id = 212, Name = "Kuvasz", PetType = PetType.Dog },
                new Race { Id = 213, Name = "Kyüshü", PetType = PetType.Dog },
                new Race { Id = 214, Name = "Labrador Retriever", PetType = PetType.Dog },
                new Race { Id = 215, Name = "Lakeland Terrier", PetType = PetType.Dog },
                new Race { Id = 216, Name = "Landseer", PetType = PetType.Dog },
                new Race { Id = 217, Name = "Lapphund", PetType = PetType.Dog },
                new Race { Id = 218, Name = "Lapponian Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 219, Name = "Leonberger", PetType = PetType.Dog },
                new Race { Id = 220, Name = "Lhasa Apso", PetType = PetType.Dog },
                new Race { Id = 221, Name = "Lowchen", PetType = PetType.Dog },
                new Race { Id = 222, Name = "Maltese", PetType = PetType.Dog },
                new Race { Id = 223, Name = "Manchester Terrier", PetType = PetType.Dog },
                new Race { Id = 224, Name = "Maremma Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 225, Name = "Mastiff", PetType = PetType.Dog },
                new Race { Id = 226, Name = "Minyatür Bull Terrier", PetType = PetType.Dog },
                new Race { Id = 227, Name = "Minyatür Pinscher", PetType = PetType.Dog },
                new Race { Id = 228, Name = "Minyatür Schnauzer", PetType = PetType.Dog },
                new Race { Id = 229, Name = "Mudi", PetType = PetType.Dog },
                new Race { Id = 230, Name = "Napoliten Mastiff", PetType = PetType.Dog },
                new Race { Id = 231, Name = "Newfoundland", PetType = PetType.Dog },
                new Race { Id = 232, Name = "Norfolk Terrier", PetType = PetType.Dog },
                new Race { Id = 233, Name = "Norrbottenspets", PetType = PetType.Dog },
                new Race { Id = 234, Name = "Norsk Buhund", PetType = PetType.Dog },
                new Race { Id = 235, Name = "Norveç Geyik Avcısı", PetType = PetType.Dog },
                new Race { Id = 236, Name = "Norwich Terrier", PetType = PetType.Dog },
                new Race { Id = 237, Name = "Old English Sheepdog", PetType = PetType.Dog },
                new Race { Id = 238, Name = "Otterhound", PetType = PetType.Dog },
                new Race { Id = 239, Name = "Pappilon", PetType = PetType.Dog },
                new Race { Id = 240, Name = "Pekingese", PetType = PetType.Dog },
                new Race { Id = 241, Name = "Pembroke Welsh Corgi", PetType = PetType.Dog },
                new Race { Id = 242, Name = "Peru Tüysüz Köpeği", PetType = PetType.Dog },
                new Race { Id = 243, Name = "Petit Basset Griffon Vendien", PetType = PetType.Dog },
                new Race { Id = 244, Name = "Petit Bleu de Gascogne", PetType = PetType.Dog },
                new Race { Id = 245, Name = "Pharaoh Hound", PetType = PetType.Dog },
                new Race { Id = 246, Name = "Picardy Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 247, Name = "Plott Tazısı", PetType = PetType.Dog },
                new Race { Id = 248, Name = "Pointer", PetType = PetType.Dog },
                new Race { Id = 249, Name = "Poitevin", PetType = PetType.Dog },
                new Race { Id = 250, Name = "Polonya Tazısı", PetType = PetType.Dog },
                new Race { Id = 251, Name = "Pomeranyalı", PetType = PetType.Dog },
                new Race { Id = 252, Name = "Poodle (Minyatür Kaniş)", PetType = PetType.Dog },
                new Race { Id = 253, Name = "Poodle(Standart Kaniş)", PetType = PetType.Dog },
                new Race { Id = 254, Name = "Portekiz Su Köpeği", PetType = PetType.Dog },
                new Race { Id = 255, Name = "Presa Canario", PetType = PetType.Dog },
                new Race { Id = 256, Name = "Pug", PetType = PetType.Dog },
                new Race { Id = 257, Name = "Puli", PetType = PetType.Dog },
                new Race { Id = 258, Name = "Pumi", PetType = PetType.Dog },
                new Race { Id = 259, Name = "Pyrenees Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 260, Name = "Pyrenees Mastiff", PetType = PetType.Dog },
                new Race { Id = 261, Name = "Rafeiro do Alentejo", PetType = PetType.Dog },
                new Race { Id = 262, Name = "Rhodesian Ridgeback", PetType = PetType.Dog },
                new Race { Id = 263, Name = "Rottweiler", PetType = PetType.Dog },
                new Race { Id = 264, Name = "Russian Spaniel", PetType = PetType.Dog },
                new Race { Id = 265, Name = "Sakallı Collie", PetType = PetType.Dog },
                new Race { Id = 266, Name = "Saluki", PetType = PetType.Dog },
                new Race { Id = 267, Name = "Samoyed", PetType = PetType.Dog },
                new Race { Id = 268, Name = "Sanshu", PetType = PetType.Dog },
                new Race { Id = 269, Name = "Schipperkee", PetType = PetType.Dog },
                new Race { Id = 270, Name = "Sealyham Terrier", PetType = PetType.Dog },
                new Race { Id = 271, Name = "Shetland Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 272, Name = "Shiba Inu", PetType = PetType.Dog },
                new Race { Id = 273, Name = "Shih Tzu", PetType = PetType.Dog },
                new Race { Id = 274, Name = "Sibirya Kurdu (Husky)", PetType = PetType.Dog },
                new Race { Id = 275, Name = "Silky Terrier", PetType = PetType.Dog },
                new Race { Id = 276, Name = "Siyah ve Açık Kahverengi Rakun Tazısı", PetType = PetType.Dog },
                new Race { Id = 277, Name = "Skye Terrier", PetType = PetType.Dog },
                new Race { Id = 278, Name = "Slovak Tchouvatch", PetType = PetType.Dog },
                new Race { Id = 279, Name = "Soft Coated Wheaten Terrier", PetType = PetType.Dog },
                new Race { Id = 280, Name = "Sokö (Sokak Köpeği)", PetType = PetType.Dog },
                new Race { Id = 281, Name = "St. Bernard (Saint Bernard)", PetType = PetType.Dog },
                new Race { Id = 282, Name = "Staffordshire Bull Terrier", PetType = PetType.Dog },
                new Race { Id = 283, Name = "Standart Schnauzer", PetType = PetType.Dog },
                new Race { Id = 284, Name = "Steinbracke", PetType = PetType.Dog },
                new Race { Id = 285, Name = "Styrian Dağ Tazısı", PetType = PetType.Dog },
                new Race { Id = 286, Name = "Sussex Spanieli", PetType = PetType.Dog },
                new Race { Id = 287, Name = "Tatra Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 288, Name = "Tibet Terrieri", PetType = PetType.Dog },
                new Race { Id = 289, Name = "Tibetli Mastiff", PetType = PetType.Dog },
                new Race { Id = 290, Name = "Tibetli Spaniel", PetType = PetType.Dog },
                new Race { Id = 291, Name = "Tosa", PetType = PetType.Dog },
                new Race { Id = 292, Name = "Trigg Tazısı", PetType = PetType.Dog },
                new Race { Id = 293, Name = "Türk Tazısı", PetType = PetType.Dog },
                new Race { Id = 294, Name = "Tüysüz Collie", PetType = PetType.Dog },
                new Race { Id = 295, Name = "Tyrolean Tazısı", PetType = PetType.Dog },
                new Race { Id = 296, Name = "Valee Çoban Köpeği", PetType = PetType.Dog },
                new Race { Id = 297, Name = "Vizsla", PetType = PetType.Dog },
                new Race { Id = 298, Name = "Weimaraner", PetType = PetType.Dog },
                new Race { Id = 299, Name = "Welsh Springer Spaniel", PetType = PetType.Dog },
                new Race { Id = 300, Name = "Welsh Terrier", PetType = PetType.Dog },
                new Race { Id = 301, Name = "West Highland White Terrier", PetType = PetType.Dog },
                new Race { Id = 302, Name = "Westphalia Basseti", PetType = PetType.Dog },
                new Race { Id = 303, Name = "Whippet", PetType = PetType.Dog },
                new Race { Id = 304, Name = "Wirehaired Pointing Griffon", PetType = PetType.Dog },
                new Race { Id = 305, Name = "Yorkshire Terrier", PetType = PetType.Dog },
            };

            modelBuilder.Entity<Race>().HasData(allRaces);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to database
            options.UseNpgsql(GetConnectionString("DATABASE_URL"));
        }

        /// <summary>
        /// Formats postgres URI to ADO.NET connection string
        /// </summary>
        private string GetConnectionString(string name)
        {
            var environmentVariable = Environment.GetEnvironmentVariable(name);

            if (!Uri.TryCreate(environmentVariable, UriKind.Absolute, out Uri databaseUri))
            {
                throw new ArgumentException(name);
            }

            return $"User ID={databaseUri.UserInfo.Split(':')[0]};Password={databaseUri.UserInfo.Split(':')[1]};Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.LocalPath.Substring(1)};SSL Mode=Require;Trust Server Certificate=true";
        }

    }
}