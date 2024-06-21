using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderingKioskSystem.Application.Common.Mappings;
using OrderingKioskSystem.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace OrderingKioskSystem.Application.Kiosk
{
    public class KioskDTO : IMapFrom<KioskEntity>
    {
        [BindProperty(Name = "id")]
        public string ID { get; set; }

        [BindProperty(Name = "location")]
        [Required]
        public string Location { get; set; }

        public string Code { get; set; }

        public int PIN { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<KioskEntity, KioskDTO>();
        }
    }
}
