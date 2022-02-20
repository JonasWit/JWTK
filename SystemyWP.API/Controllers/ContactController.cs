using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SystemyWP.API.Data;

namespace SystemyWP.API.Controllers
{
    [Route("[controller]")]
    public class ContactController : ApiControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ContactController(
            AppDbContext context, 
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}