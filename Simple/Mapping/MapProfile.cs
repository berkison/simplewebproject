using AutoMapper;
using Simple.DTOs;
using Simple.Models;

namespace Simple.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile() 
        {
            CreateMap<Kitaplar, KitapListelemeDTO>();
            CreateMap<KitapEklemeDTO, Kitaplar>();
        }
    }
}
