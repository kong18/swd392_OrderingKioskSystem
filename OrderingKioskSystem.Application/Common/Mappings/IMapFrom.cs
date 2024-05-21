using AutoMapper;

namespace OrderingKioskSystem.Application.Common.Mappings
{
    interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
