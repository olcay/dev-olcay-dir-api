using System.Collections.Generic;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public static class EnumService
    {
        public static IEnumerable<EnumDto> GetPetTypes()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)PetType.Cat, Name = "Kedi" },
                new EnumDto{ Id = (int)PetType.Dog, Name = "Köpek" }
            };
        }

        public static IEnumerable<EnumDto> GetGenders()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)Gender.Female, Name = "Dişi" },
                new EnumDto{ Id = (int)Gender.Male, Name = "Erkek" }
            };
        }

        public static IEnumerable<EnumDto> GetAges()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)PetAge.Baby, Name = "Yavru (0 - 6 Aylık)" },
                new EnumDto{ Id = (int)PetAge.Young, Name = "Genç (6 Aylık - 2 Yaş)" },
                new EnumDto{ Id = (int)PetAge.Adult, Name = "Yetişkin (2 - 7 Yaş)" },
                new EnumDto{ Id = (int)PetAge.Old, Name = "Yaşlı (7 Yaş ve üzeri)" }
            };
        }

        public static IEnumerable<EnumDto> GetSizes()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)Size.Small, Name = "Küçük Boy" },
                new EnumDto{ Id = (int)Size.Medium, Name = "Orta Boy" },
                new EnumDto{ Id = (int)Size.Large, Name = "Büyük Boy" }
            };
        }

        public static IEnumerable<EnumDto> GetFromWhere()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = (int)FromWhere.Shelter, Name = "Barınaktan" },
                new EnumDto{ Id = (int)FromWhere.Foster, Name = "Geçiçi Evinden" },
                new EnumDto{ Id = (int)FromWhere.Owner, Name = "Sahibinden" },
                new EnumDto{ Id = (int)FromWhere.Street, Name = "Sokaktan" },
                new EnumDto{ Id = (int)FromWhere.Vet, Name = "Veteriner Hekimden" }
            };
        }

        public static IEnumerable<EnumDto> GetCities()
        {
            return new List<EnumDto>{
                new EnumDto{ Id = 1, Name = "Adana" },
                new EnumDto{ Id = 2, Name = "Adıyaman" },
                new EnumDto{ Id = 3, Name = "Afyonkarahisar" },
                new EnumDto{ Id = 4, Name = "Ağrı" },
                new EnumDto{ Id = 5, Name = "Amasya" },
                new EnumDto{ Id = 6, Name = "Ankara" },
                new EnumDto{ Id = 7, Name = "Antalya" },
                new EnumDto{ Id = 8, Name = "Artvin" },
                new EnumDto{ Id = 9, Name = "Aydın" },
                new EnumDto{ Id = 10, Name = "Balıkesir" },
                new EnumDto{ Id = 11, Name = "Bilecik" },
                new EnumDto{ Id = 12, Name = "Bingöl" },
                new EnumDto{ Id = 13, Name = "Bitlis" },
                new EnumDto{ Id = 14, Name = "Bolu" },
                new EnumDto{ Id = 15, Name = "Burdur" },
                new EnumDto{ Id = 16, Name = "Bursa" },
                new EnumDto{ Id = 17, Name = "Çanakkale" },
                new EnumDto{ Id = 18, Name = "Çankırı" },
                new EnumDto{ Id = 19, Name = "Çorum" },
                new EnumDto{ Id = 20, Name = "Denizli" },
                new EnumDto{ Id = 21, Name = "Diyarbakır" },
                new EnumDto{ Id = 22, Name = "Edirne" },
                new EnumDto{ Id = 23, Name = "Elazığ" },
                new EnumDto{ Id = 24, Name = "Erzincan" },
                new EnumDto{ Id = 25, Name = "Erzurum" },
                new EnumDto{ Id = 26, Name = "Eskişehir" },
                new EnumDto{ Id = 27, Name = "Gaziantep" },
                new EnumDto{ Id = 28, Name = "Giresun" },
                new EnumDto{ Id = 29, Name = "Gümüşhane" },
                new EnumDto{ Id = 30, Name = "Hakkari" },
                new EnumDto{ Id = 31, Name = "Hatay" },
                new EnumDto{ Id = 32, Name = "Isparta" },
                new EnumDto{ Id = 33, Name = "Mersin" },
                new EnumDto{ Id = 34, Name = "İstanbul" },
                new EnumDto{ Id = 35, Name = "İzmir" },
                new EnumDto{ Id = 36, Name = "Kars" },
                new EnumDto{ Id = 37, Name = "Kastamonu" },
                new EnumDto{ Id = 38, Name = "Kayseri" },
                new EnumDto{ Id = 39, Name = "Kırklareli" },
                new EnumDto{ Id = 40, Name = "Kırşehir" },
                new EnumDto{ Id = 41, Name = "Kocaeli" },
                new EnumDto{ Id = 42, Name = "Konya" },
                new EnumDto{ Id = 43, Name = "Kütahya" },
                new EnumDto{ Id = 44, Name = "Malatya" },
                new EnumDto{ Id = 45, Name = "Manisa" },
                new EnumDto{ Id = 46, Name = "Kahramanmaraş" },
                new EnumDto{ Id = 47, Name = "Mardin" },
                new EnumDto{ Id = 48, Name = "Muğla" },
                new EnumDto{ Id = 49, Name = "Muş" },
                new EnumDto{ Id = 50, Name = "Nevşehir" },
                new EnumDto{ Id = 51, Name = "Niğde" },
                new EnumDto{ Id = 52, Name = "Ordu" },
                new EnumDto{ Id = 53, Name = "Rize" },
                new EnumDto{ Id = 54, Name = "Sakarya" },
                new EnumDto{ Id = 55, Name = "Samsun" },
                new EnumDto{ Id = 56, Name = "Siirt" },
                new EnumDto{ Id = 57, Name = "Sinop" },
                new EnumDto{ Id = 58, Name = "Sivas" },
                new EnumDto{ Id = 59, Name = "Tekirdağ" },
                new EnumDto{ Id = 60, Name = "Tokat" },
                new EnumDto{ Id = 61, Name = "Trabzon" },
                new EnumDto{ Id = 62, Name = "Tunceli" },
                new EnumDto{ Id = 63, Name = "Şanlıurfa" },
                new EnumDto{ Id = 64, Name = "Uşak" },
                new EnumDto{ Id = 65, Name = "Van" },
                new EnumDto{ Id = 66, Name = "Yozgat" },
                new EnumDto{ Id = 67, Name = "Zonguldak" },
                new EnumDto{ Id = 68, Name = "Aksaray" },
                new EnumDto{ Id = 69, Name = "Bayburt" },
                new EnumDto{ Id = 70, Name = "Karaman" },
                new EnumDto{ Id = 71, Name = "Kırıkkale" },
                new EnumDto{ Id = 72, Name = "Batman" },
                new EnumDto{ Id = 73, Name = "Şırnak" },
                new EnumDto{ Id = 74, Name = "Bartın" },
                new EnumDto{ Id = 75, Name = "Ardahan" },
                new EnumDto{ Id = 76, Name = "Iğdır" },
                new EnumDto{ Id = 77, Name = "Yalova" },
                new EnumDto{ Id = 78, Name = "Karabük" },
                new EnumDto{ Id = 79, Name = "Kilis" },
                new EnumDto{ Id = 80, Name = "Osmaniye" },
                new EnumDto{ Id = 81, Name = "Düzce" }
            };
        }
    }
}