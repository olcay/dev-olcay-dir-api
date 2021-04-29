using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public static class EnumService
    {
        public static IEnumerable<EnumDto> GetPetTypes()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = PetType.Cat.ToString(), Text = "Kedi" },
                new EnumDto{ Value = PetType.Dog.ToString(), Text = "Köpek" }
            };
        }

        public static IEnumerable<EnumDto> GetPetStatuses()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = PetStatus.Created.ToString(), Text = "Yayında Değil" },
                new EnumDto{ Value = PetStatus.Published.ToString(), Text = "Yayınlandı" },
                new EnumDto{ Value = PetStatus.Adopted.ToString(), Text = "Evlat Edinildi" },
                new EnumDto{ Value = PetStatus.Deleted.ToString(), Text = "Silindi" }
            };
        }

        public static IEnumerable<EnumDto> GetGenders()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = Gender.None.ToString(), Text = "-" },
                new EnumDto{ Value = Gender.Female.ToString(), Text = "Dişi" },
                new EnumDto{ Value = Gender.Male.ToString(), Text = "Erkek" }
            };
        }

        public static IEnumerable<EnumDto> GetAges()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = PetAge.None.ToString(), Text = "-" },
                new EnumDto{ Value = PetAge.Baby.ToString(), Text = "Yavru (0 - 6 Aylık)" },
                new EnumDto{ Value = PetAge.Young.ToString(), Text = "Genç (6 Aylık - 2 Yaş)" },
                new EnumDto{ Value = PetAge.Adult.ToString(), Text = "Yetişkin (2 - 7 Yaş)" },
                new EnumDto{ Value = PetAge.Old.ToString(), Text = "Yaşlı (7 Yaş ve üzeri)" }
            };
        }

        public static IEnumerable<EnumDto> GetSizes()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = Size.None.ToString(), Text = "-" },
                new EnumDto{ Value = Size.Small.ToString(), Text = "Küçük Boy" },
                new EnumDto{ Value = Size.Medium.ToString(), Text = "Orta Boy" },
                new EnumDto{ Value = Size.Large.ToString(), Text = "Büyük Boy" }
            };
        }

        public static IEnumerable<EnumDto> GetFromWhere()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = FromWhere.None.ToString(), Text = "-" },
                new EnumDto{ Value = FromWhere.Shelter.ToString(), Text = "Barınaktan" },
                new EnumDto{ Value = FromWhere.Foster.ToString(), Text = "Geçiçi Evinden" },
                new EnumDto{ Value = FromWhere.Owner.ToString(), Text = "Sahibinden" },
                new EnumDto{ Value = FromWhere.Street.ToString(), Text = "Sokaktan" },
                new EnumDto{ Value = FromWhere.Vet.ToString(), Text = "Veteriner Hekimden" }
            };
        }

        public static bool CityExists(int cityId)
        {
            string stringCityId = cityId.ToString();
            return  GetCities().Any(a => a.Value == stringCityId);
        }

        public static IEnumerable<EnumDto> GetCities()
        {
            return new List<EnumDto>{
                new EnumDto{ Value = "1", Text = "Adana" },
                new EnumDto{ Value = "2", Text = "Adıyaman" },
                new EnumDto{ Value = "3", Text = "Afyonkarahisar" },
                new EnumDto{ Value = "4", Text = "Ağrı" },
                new EnumDto{ Value = "5", Text = "Amasya" },
                new EnumDto{ Value = "6", Text = "Ankara" },
                new EnumDto{ Value = "7", Text = "Antalya" },
                new EnumDto{ Value = "8", Text = "Artvin" },
                new EnumDto{ Value = "9", Text = "Aydın" },
                new EnumDto{ Value = "10", Text = "Balıkesir" },
                new EnumDto{ Value = "11", Text = "Bilecik" },
                new EnumDto{ Value = "12", Text = "Bingöl" },
                new EnumDto{ Value = "13", Text = "Bitlis" },
                new EnumDto{ Value = "14", Text = "Bolu" },
                new EnumDto{ Value = "15", Text = "Burdur" },
                new EnumDto{ Value = "16", Text = "Bursa" },
                new EnumDto{ Value = "17", Text = "Çanakkale" },
                new EnumDto{ Value = "18", Text = "Çankırı" },
                new EnumDto{ Value = "19", Text = "Çorum" },
                new EnumDto{ Value = "20", Text = "Denizli" },
                new EnumDto{ Value = "21", Text = "Diyarbakır" },
                new EnumDto{ Value = "22", Text = "Edirne" },
                new EnumDto{ Value = "23", Text = "Elazığ" },
                new EnumDto{ Value = "24", Text = "Erzincan" },
                new EnumDto{ Value = "25", Text = "Erzurum" },
                new EnumDto{ Value = "26", Text = "Eskişehir" },
                new EnumDto{ Value = "27", Text = "Gaziantep" },
                new EnumDto{ Value = "28", Text = "Giresun" },
                new EnumDto{ Value = "29", Text = "Gümüşhane" },
                new EnumDto{ Value = "30", Text = "Hakkari" },
                new EnumDto{ Value = "31", Text = "Hatay" },
                new EnumDto{ Value = "32", Text = "Isparta" },
                new EnumDto{ Value = "33", Text = "Mersin" },
                new EnumDto{ Value = "34", Text = "İstanbul" },
                new EnumDto{ Value = "35", Text = "İzmir" },
                new EnumDto{ Value = "36", Text = "Kars" },
                new EnumDto{ Value = "37", Text = "Kastamonu" },
                new EnumDto{ Value = "38", Text = "Kayseri" },
                new EnumDto{ Value = "39", Text = "Kırklareli" },
                new EnumDto{ Value = "40", Text = "Kırşehir" },
                new EnumDto{ Value = "41", Text = "Kocaeli" },
                new EnumDto{ Value = "42", Text = "Konya" },
                new EnumDto{ Value = "43", Text = "Kütahya" },
                new EnumDto{ Value = "44", Text = "Malatya" },
                new EnumDto{ Value = "45", Text = "Manisa" },
                new EnumDto{ Value = "46", Text = "Kahramanmaraş" },
                new EnumDto{ Value = "47", Text = "Mardin" },
                new EnumDto{ Value = "48", Text = "Muğla" },
                new EnumDto{ Value = "49", Text = "Muş" },
                new EnumDto{ Value = "50", Text = "Nevşehir" },
                new EnumDto{ Value = "51", Text = "Niğde" },
                new EnumDto{ Value = "52", Text = "Ordu" },
                new EnumDto{ Value = "53", Text = "Rize" },
                new EnumDto{ Value = "54", Text = "Sakarya" },
                new EnumDto{ Value = "55", Text = "Samsun" },
                new EnumDto{ Value = "56", Text = "Siirt" },
                new EnumDto{ Value = "57", Text = "Sinop" },
                new EnumDto{ Value = "58", Text = "Sivas" },
                new EnumDto{ Value = "59", Text = "Tekirdağ" },
                new EnumDto{ Value = "60", Text = "Tokat" },
                new EnumDto{ Value = "61", Text = "Trabzon" },
                new EnumDto{ Value = "62", Text = "Tunceli" },
                new EnumDto{ Value = "63", Text = "Şanlıurfa" },
                new EnumDto{ Value = "64", Text = "Uşak" },
                new EnumDto{ Value = "65", Text = "Van" },
                new EnumDto{ Value = "66", Text = "Yozgat" },
                new EnumDto{ Value = "67", Text = "Zonguldak" },
                new EnumDto{ Value = "68", Text = "Aksaray" },
                new EnumDto{ Value = "69", Text = "Bayburt" },
                new EnumDto{ Value = "70", Text = "Karaman" },
                new EnumDto{ Value = "71", Text = "Kırıkkale" },
                new EnumDto{ Value = "72", Text = "Batman" },
                new EnumDto{ Value = "73", Text = "Şırnak" },
                new EnumDto{ Value = "74", Text = "Bartın" },
                new EnumDto{ Value = "75", Text = "Ardahan" },
                new EnumDto{ Value = "76", Text = "Iğdır" },
                new EnumDto{ Value = "77", Text = "Yalova" },
                new EnumDto{ Value = "78", Text = "Karabük" },
                new EnumDto{ Value = "79", Text = "Kilis" },
                new EnumDto{ Value = "80", Text = "Osmaniye" },
                new EnumDto{ Value = "81", Text = "Düzce" }
            };
        }
    }
}