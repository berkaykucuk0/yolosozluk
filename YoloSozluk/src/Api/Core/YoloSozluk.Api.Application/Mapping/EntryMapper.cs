using AutoMapper;
using YoloSozluk.Api.Domain.Entities;
using YoloSozluk.Common.Models.Commands;
using YoloSozluk.Common.Models.ViewModels;

namespace YoloSozluk.Api.Application.Mapping
{
    public class EntryMapper : Profile
    {
        public EntryMapper()
        {
            CreateMap<EntryCreateCommand, Entry>().ReverseMap();
            CreateMap<EntryCommentCreateCommand, EntryComment>().ReverseMap();

            CreateMap<Entry, GetEntriesViewModel>().ForMember(x=>x.CommentCount , y=>y.MapFrom(z=>z.EntryComments.Count)).ReverseMap();
        }
    }
}
