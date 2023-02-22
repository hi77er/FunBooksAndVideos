using AutoMapper;

namespace FunBooksAndVideos.WebApi.Handlers.Base
{
    public class BaseHandler
    {
        protected readonly IMapper _mapper;
        public BaseHandler(IMapper _mapper) => this._mapper = _mapper;
    }
}
