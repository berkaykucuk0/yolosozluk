using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YoloSozluk.Api.Application.IRepositories;
using YoloSozluk.Common.Exceptions.User;
using YoloSozluk.Common.Models.Queries;

namespace YoloSozluk.Api.Application.Handlers.QueryHandlers
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, GetUserDetailViewModel>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<GetUserDetailViewModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            Domain.Entities.User user = null;
            if (request.UserId != Guid.Empty)
                 user = await _userRepo.GetByIdAsync(request.UserId);
            else if (string.IsNullOrEmpty(request.UserName))
                user = await _userRepo.GetSingleAsync(x=>x.UserName == request.UserName);
            else
                throw new UserException("UserId and UserName both cannot be null!");

            return _mapper.Map<GetUserDetailViewModel>(user);
        }
    }
}
