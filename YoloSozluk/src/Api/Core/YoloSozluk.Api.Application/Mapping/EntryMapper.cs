using AutoMapper;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Common.Models.Commands;

namespace YoloSozluk.Api.Application.Mapping
{
    public class EntryMapper : Profile
    {
        public EntryMapper()
        {
            CreateMap<EntryCreateCommand, Entry>().ReverseMap();
            CreateMap<EntryCommentCreateCommand, EntryComment>().ReverseMap();
        }
    }
}
