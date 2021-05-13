using System.Collections.Generic;
using System.Linq;
using WebApi.Enums;
using WebApi.Models;

namespace WebApi.Services
{
    public static class EnumService
    {
        public static IEnumerable<EnumDto<PetType>> GetPetTypes()
        {
            return new List<EnumDto<PetType>>{
                new EnumDto<PetType>{ Value = PetType.Cat, Text = "Kedi" },
                new EnumDto<PetType>{ Value = PetType.Dog, Text = "Köpek" }
            };
        }

        public static IEnumerable<EnumDto<PetStatus>> GetPetStatuses()
        {
            return new List<EnumDto<PetStatus>>{
                new EnumDto<PetStatus>{ Value = PetStatus.Created, Text = "Yayında Değil" },
                new EnumDto<PetStatus>{ Value = PetStatus.Published, Text = "Yayınlandı" },
                new EnumDto<PetStatus>{ Value = PetStatus.Adopted, Text = "Evlat Edinildi" },
                new EnumDto<PetStatus>{ Value = PetStatus.Deleted, Text = "Silindi" }
            };
        }

        public static IEnumerable<EnumDto<Gender>> GetGenders()
        {
            return new List<EnumDto<Gender>>{
                new EnumDto<Gender>{ Value = Gender.None, Text = "-" },
                new EnumDto<Gender>{ Value = Gender.Female, Text = "Dişi" },
                new EnumDto<Gender>{ Value = Gender.Male, Text = "Erkek" }
            };
        }

        public static IEnumerable<EnumDto<PetAge>> GetAges()
        {
            return new List<EnumDto<PetAge>>{
                new EnumDto<PetAge>{ Value = PetAge.None, Text = "-" },
                new EnumDto<PetAge>{ Value = PetAge.Baby, Text = "Yavru (0 - 6 Aylık)" },
                new EnumDto<PetAge>{ Value = PetAge.Young, Text = "Genç (6 Aylık - 2 Yaş)" },
                new EnumDto<PetAge>{ Value = PetAge.Adult, Text = "Yetişkin (2 - 7 Yaş)" },
                new EnumDto<PetAge>{ Value = PetAge.Old, Text = "Yaşlı (7 Yaş ve üzeri)" }
            };
        }

        public static IEnumerable<EnumDto<Size>> GetSizes()
        {
            return new List<EnumDto<Size>>{
                new EnumDto<Size>{ Value = Size.None, Text = "-" },
                new EnumDto<Size>{ Value = Size.Small, Text = "Küçük Boy" },
                new EnumDto<Size>{ Value = Size.Medium, Text = "Orta Boy" },
                new EnumDto<Size>{ Value = Size.Large, Text = "Büyük Boy" }
            };
        }

        public static IEnumerable<EnumDto<FromWhere>> GetFromWhere()
        {
            return new List<EnumDto<FromWhere>>{
                new EnumDto<FromWhere>{ Value = FromWhere.None, Text = "-" },
                new EnumDto<FromWhere>{ Value = FromWhere.Shelter, Text = "Barınaktan" },
                new EnumDto<FromWhere>{ Value = FromWhere.Foster, Text = "Geçiçi Evinden" },
                new EnumDto<FromWhere>{ Value = FromWhere.Owner, Text = "Sahibinden" },
                new EnumDto<FromWhere>{ Value = FromWhere.Street, Text = "Sokaktan" },
                new EnumDto<FromWhere>{ Value = FromWhere.Vet, Text = "Veteriner Hekimden" }
            };
        }

        public static bool CityExists(int cityId)
        {
            string stringCityId = cityId.ToString();
            return  GetCities().Any(a => a.Value == stringCityId);
        }

        public static IEnumerable<EnumDto> GetCities()
        {
            var cities = new List<EnumDto>{
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

            return cities.OrderBy(c => c.Text);
        }
    }
}